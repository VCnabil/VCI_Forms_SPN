using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;
using VCI_Forms_SPN._GLobalz;

namespace VCI_Forms_SPN._Managers
{
    public class PGN_MANAGER
    {
        bool _SPN_CONFLICT = false;
        int _cnt_Onscreen_SPNucs = 0;
        private List<VCinc_uc> _list_Onscreen_SPNucs;
        private int _cnt_Onscreen_pgnAdrs_Groups = 0;
        private Dictionary<string, List<VCinc_uc>> _pgnAdrs_Groups;
        List<double> distinctHues_pgnAdrs_Groups;
        private List<Color[]> _ArrayOfColorGradients;
        private Dictionary<string, (int pgn, byte[] data)> _pgnByteArrays; // Stores PGN and corresponding byte array
        public Dictionary<string, (int pgn, byte[] data)> GetPgnByteArrays() => _pgnByteArrays; // Method to access PGNs and byte arrays on the fly 
        private readonly List<VCinc_SFversion> _listVCincSFversionUcs;
        private HashSet<string> _uniqueSFversionIdentifiers;

        private int _foundGPSonform = 0;
        public PGN_MANAGER(Form form)
        {
            _list_Onscreen_SPNucs = new List<VCinc_uc>();
            _listVCincSFversionUcs = new List<VCinc_SFversion>();
            _pgnAdrs_Groups = new Dictionary<string, List<VCinc_uc>>();
            distinctHues_pgnAdrs_Groups = ColorHelpers.GetDistinctHues(10);
            _ArrayOfColorGradients = new List<Color[]>();
            _pgnByteArrays = new Dictionary<string, (int pgn, byte[] data)>();
            _uniqueSFversionIdentifiers = new HashSet<string>();

            InitializeOnscreenSpnUcs(form);
            GroupVCincSFversionUcs(form);
            Init_onscreenGPS(form);

            ValidateVCincSFversions(); // New validation method added here

            if (_cnt_Onscreen_SPNucs == 0)
            {
                MessageBox.Show("No SPN_Control found in form");
            }
            GroupControlsByPGNAndAddress();
        }

        private void ValidateVCincSFversions()
        {
            foreach (var sfVersion in _listVCincSFversionUcs)
            {
                bool isValid = true;
                StringBuilder errorMessage = new StringBuilder();

                // Create a unique identifier for the SFversion based on PRIO, PGN, and Address
                string uniqueIdentifier = sfVersion.Priority + sfVersion.PGN + sfVersion.Source;

                // Check if the identifier is unique
                if (_uniqueSFversionIdentifiers.Contains(uniqueIdentifier))
                {
                    errorMessage.AppendLine("Duplicate SFversion found with the same PRIO, PGN, and Address.");
                    isValid = false;
                }
                else
                {
                    _uniqueSFversionIdentifiers.Add(uniqueIdentifier);
                }

                // Validate PRIO (must not be empty)
                if (string.IsNullOrEmpty(sfVersion.Priority))
                {
                    errorMessage.AppendLine("Invalid Priority (PRIO). Cannot be empty.");
                    isValid = false;
                }

                // Validate PGN (must not be empty)
                if (string.IsNullOrEmpty(sfVersion.PGN))
                {
                    errorMessage.AppendLine("Invalid PGN. Cannot be empty.");
                    isValid = false;
                }

                // Validate Address (cannot be null or empty)
                if (string.IsNullOrEmpty(sfVersion.Source))
                {
                    errorMessage.AppendLine("Invalid Address. Address cannot be empty.");
                    isValid = false;
                }

                if (!isValid)
                {
                    sfVersion.Set_Lbl_status("ERROR", Color.Red, Color.Black);
                    MessageBox.Show($"Validation Failed for VCinc_SFversion:\n{errorMessage}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sfVersion.Set_Lbl_status("OK", Color.Green, Color.Black);
                }
            }
        }

        private void GroupControlsByPGNAndAddress()
        {
            foreach (var control in _list_Onscreen_SPNucs)
            {
                string uniqueIdentifier = control.PRIO + control.PGN + control.Address;
                if (!_pgnAdrs_Groups.ContainsKey(uniqueIdentifier))
                {
                    _pgnAdrs_Groups[uniqueIdentifier] = new List<VCinc_uc>();
                }
                _pgnAdrs_Groups[uniqueIdentifier].Add(control);
            }
            _cnt_Onscreen_pgnAdrs_Groups = _pgnAdrs_Groups.Count;

            distinctHues_pgnAdrs_Groups = ColorHelpers.GetDistinctHues(_cnt_Onscreen_pgnAdrs_Groups);
            for (int x = 0; x < _pgnAdrs_Groups.Count; x++)
            {
                _ArrayOfColorGradients.Add(ColorHelpers.PUB_used_Gen_WithListParam(ColorHelpers.ColorFromHSV(distinctHues_pgnAdrs_Groups[x], 0.95, 1.0), 8).ToArray());
            }
            InitializeByteArraysForGroups();
        }

        private void GroupVCincSFversionUcs(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage page in tabControl.TabPages)
                    {
                        foreach (Control control2 in page.Controls)
                        {
                            if (control2 is VCinc_SFversion vcIncSFversion)
                            {
                                _listVCincSFversionUcs.Add(vcIncSFversion);
                            }
                        }
                    }
                }
                else if (control is VCinc_SFversion vcIncSFversionControl)
                {
                    _listVCincSFversionUcs.Add(vcIncSFversionControl);

                    byte[] tempDataBytes = vcIncSFversionControl.GetCANByteArray();
                    int tempPgn = vcIncSFversionControl.GetCANPGN();
                    string tempPgnHex = tempPgn.ToString("X");
                    //add to dictionary 
                    _pgnByteArrays[tempPgnHex] = (tempPgn, tempDataBytes);
                }
            }
        }
        private void InitializeByteArraysForGroups()
        {
            foreach (var group in _pgnAdrs_Groups)
            {
                string groupKey = group.Key;
                int pgn = Convert.ToInt32(groupKey, 16); // Convert PGN from hex string to integer
                _pgnByteArrays[groupKey] = (pgn, new byte[8]); // Initialize a new byte array for each group with the PGN
            }
        }

        private void InitializeOnscreenSpnUcs(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TabControl tabcontrol)
                {
                    TabPage page = tabcontrol.SelectedTab;
                    foreach (Control control2 in page.Controls)
                    {
                        if (control2 is VCinc_uc vCinc_Uc)
                        {
                            _list_Onscreen_SPNucs.Add(vCinc_Uc);
                            _cnt_Onscreen_SPNucs++;
                        }
                    }
                }
                if (control is VCinc_uc myUserControl)
                {
                    _list_Onscreen_SPNucs.Add(myUserControl);
                    _cnt_Onscreen_SPNucs++;
                }
            }
        }
        private void Init_onscreenGPS(Form form)
        {
            // Look for VCinc_GPS, only take the first one if found
            foreach (Control control in form.Controls)
            {
                if (control is VCinc_GPS gpsControl)
                {
                    _foundGPSonform++;
                    // Gather data bytes from the GPS control
                    byte[] headingData = gpsControl.Get_PGNdata_Heading_09F1127F();
                    byte[] combinedXiEtaData = gpsControl.Get_PGNdata_combined_Xi_eta_18FF6729();
                    byte[] cmdCoordinatesData = gpsControl.Get_PGNdata_CMDCOORDINATES_09F8017F();

                    // Add the data to _pgnByteArrays with appropriate PGNs
                    _pgnByteArrays["09F1127F"] = (0x09F1127F, headingData);
                    _pgnByteArrays["18FF6729"] = (0x18FF6729, combinedXiEtaData);
                    _pgnByteArrays["09F8017F"] = (0x09F8017F, cmdCoordinatesData);
                    break;
                }
            }

            if (_foundGPSonform == 0)
            {
              //  MessageBox.Show("No GPS_Control found in form");
            }
        }

        private void Color_EachSPN_ByPGNAndAddress()
        {
            int i = 0;
            foreach (var group in _pgnAdrs_Groups)
            {
                foreach (var control in group.Value)
                {
                    control.Set_Lbl_status("OK", Color.Black, _ArrayOfColorGradients[i][control.A_FirstByteIndex]);
                }
                i++;
            }
        }

        private void ValidateCANPayload()
        {
            _SPN_CONFLICT = false;
            int _conflictIndex = 0;
            int _conflictObverlapIndex = 0;
            foreach (var group in _pgnAdrs_Groups)
            {
                List<VCinc_uc> spnControls = group.Value;
                // Check if the group exceeds the maximum  
                if (spnControls.Count > 8)
                {
                    _SPN_CONFLICT = true;

                    foreach (var spn in spnControls)
                    {
                        spn.Set_Lbl_status("MAX", Color.Red, Color.Gray);
                    }
                    continue; // Skip further validation for this group
                }

                // Dictionary to track the used byte ranges and corresponding SPN controls within the group
                Dictionary<int, VCinc_uc> byteIndexUsage = new Dictionary<int, VCinc_uc>();

                // Validate byte ranges for conflicts within each group
                foreach (var spn in spnControls)
                {
                    int startByte = spn.A_FirstByteIndex;
                    int byteLength = spn.NumberOfBytes;
                    // Check if the last byte index exceeds the maximum allowed index of 7
                    if (startByte + byteLength > 8)
                    {
                        spn.Set_Lbl_status("RANGE", Color.Red, Color.DarkGray);
                        _SPN_CONFLICT = true;
                        continue; // Skip further processing for this SPN since it already has an error
                    }

                    for (int i = startByte; i < startByte + byteLength; i++)
                    {
                        if (byteIndexUsage.ContainsKey(i))
                        {
                            // Conflict found: Same byte index used by multiple SPNs in the same group
                            _conflictIndex++;
                            string _yo_string = "YO" + _conflictIndex;
                            spn.Set_Lbl_status(_yo_string, Color.Red, Color.DarkGray);
                            byteIndexUsage[i].Set_Lbl_status(_yo_string, Color.Red, Color.DarkGray);

                            _SPN_CONFLICT = true;
                        }
                        else
                        {
                            byteIndexUsage[i] = spn;
                        }
                    }
                }

                // Check for overlapping byte ranges between SPNs within the group
                foreach (var spn in spnControls)
                {
                    int startByte = spn.A_FirstByteIndex;
                    int byteLength = spn.NumberOfBytes;

                    for (int i = startByte; i < startByte + byteLength; i++)
                    {
                        foreach (var checkSpn in spnControls.Where(s => s != spn))
                        {
                            int checkStart = checkSpn.A_FirstByteIndex;
                            int checkLength = checkSpn.NumberOfBytes;

                            if (i >= checkStart && i < checkStart + checkLength)
                            {
                                // Overlapping ranges detected within the group
                                _conflictObverlapIndex++;
                                string _yo_string = "YOvlp" + _conflictObverlapIndex;
                                spn.Set_Lbl_status(_yo_string, Color.Red, Color.DarkGray);
                                checkSpn.Set_Lbl_status(_yo_string, Color.Red, Color.DarkGray);
                                _SPN_CONFLICT = true;
                            }
                        }
                    }
                }
            }
        }

        public void LoadByteArraysForGroups()
        {
            foreach (var group in _pgnAdrs_Groups)
            {
                string groupKey = group.Key;
                var (pgn, byteArray) = _pgnByteArrays[groupKey]; // Retrieve PGN and byte array

                foreach (var spn in group.Value)
                {
                    int startByte = spn.A_FirstByteIndex;
                    byte[] spnBytes = spn.GetBytes(); // Use your SPN_Control GetBytes method

                    // Check if the SPN fits within the byte array
                    if (startByte + spnBytes.Length > 8)
                    {
                        spn.Set_Lbl_status("nofit", Color.Red, Color.Black);
                        _SPN_CONFLICT = true;
                        continue; // Skip loading this SPN
                    }

                    // Load SPN bytes into the byte array
                    for (int i = 0; i < spnBytes.Length; i++)
                    {
                        byteArray[startByte + i] = spnBytes[i];
                    }
                }

                // Update the loaded byte array in the dictionary
                _pgnByteArrays[groupKey] = (pgn, byteArray);
            }
        }
        public void First_Call()
        {
            if (_cnt_Onscreen_SPNucs == 0) { MessageBox.Show("No SPN_Control found in form"); return; }

            Color_EachSPN_ByPGNAndAddress();

            ValidateCANPayload();
        }

        public int Get__numberOfunique_PGNADRSS() => _cnt_Onscreen_pgnAdrs_Groups;

        public List<VCinc_uc> GetOnFormItems()
        {
            if (_list_Onscreen_SPNucs.Count == 0)
            {
                MessageBox.Show("No SPN_Control found in form");
            }
            return _list_Onscreen_SPNucs;

        }

        public List<VCinc_uc> GetList_ByNameContaining(string argContained)
        {
            List<VCinc_uc> _list = new List<VCinc_uc>();
            foreach (var item in _list_Onscreen_SPNucs)
            {
                if (item.Name.Contains(argContained))
                {
                    _list.Add(item);
                }
            }
            return _list;
        }
    }
}
