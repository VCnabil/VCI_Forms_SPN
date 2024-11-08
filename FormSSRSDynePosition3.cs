using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

namespace VCI_Forms_SPN
{
    public partial class FormSSRSDynePosition3 : Form
    {
        /*
        #region TemplateVariavles
        Timer looptimer = new Timer();
        PGN_MANAGER _myPGNManager;
        Queue<string> messageQueue;
        StringBuilder messageBuffer;
        const int MaxMessages = 22;
        int _OScreenCount = 0;
        bool _isOnCanBus;
        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();
        #endregion
        VC_LOCATION VESSEL_LOC;
        double VESSEL_HEADING;
        VC_LOCATION WAYPOINT_LOC;
        */
        public FormSSRSDynePosition3()
        {
           // InitializeComponent();
            /*
            #region TemplateInitialize
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;
            messageBuffer = new StringBuilder();
            messageQueue = new Queue<string>(MaxMessages);
            looptimer.Interval = 200;
            looptimer.Tick += Looptimer_Tick;
            looptimer.Start();
            #endregion
            VESSEL_LOC = new VC_LOCATION();
            WAYPOINT_LOC = new VC_LOCATION();
            vCinc_LatLon_mapCnter.SetLatitude(42.36487);
            vCinc_LatLon_mapCnter.SetLongitude(-71.0545);
            VESSEL_LOC.Latitude = vCinc_LatLon_mapCnter.LatitudeDecimal;
            VESSEL_LOC.Longitude = vCinc_LatLon_mapCnter.LongitudeDecimal;
            VESSEL_HEADING = (trackBar1.Value / 100.00) % 360.00;
            btn_restBit0.Click += bet_restBit0_Click;
            btn_restBit1.Click += bet_restBit1_Click;
            */
        }
        /*
        private byte myByte = 0x00; // The byte to modify
        private void bet_restBit0_Click(object sender, EventArgs e)
        {
            SetBitForOneSecond(0);
        }
        private void bet_restBit1_Click(object sender, EventArgs e)
        {
            SetBitForOneSecond(1);
        }
        private async void SetBitForOneSecond(int bitPosition)
        {
            myByte |= (byte)(1 << bitPosition);
            vCinc_uc18.Value = myByte;
            await Task.Delay(500);
            myByte &= (byte)~(1 << bitPosition);
            vCinc_uc18.Value = myByte;
        }
        private void Looptimer_Tick(object sender, EventArgs e)
        {
            VESSEL_LOC.Latitude = vCinc_LatLon_mapCnter.LatitudeDecimal;
            VESSEL_LOC.Longitude = vCinc_LatLon_mapCnter.LongitudeDecimal;
            VESSEL_HEADING = (trackBar1.Value / 100.00) % 360.00;
            vCinc_DynPos1.Update_CenterMap_Heading(VESSEL_LOC, VESSEL_HEADING);
            vCinc_LatLon_waypoint.SetLatitude(VESSEL_LOC.Latitude);
            SendAllPgnMessages();
        }
        #region can and stuff
        private void KvsrManager_OnMessageReceived(string message)
        {
            if (IsDisposed || Disposing)
                return;

            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action(() => KvsrManager_OnMessageReceived(message)));
                }
                catch (ObjectDisposedException)
                {
                    // Log the exception or handle it appropriately
                    Debug.WriteLine("Attempted to invoke on a disposed object.");
                }
                return;
            }

            // Existing logic for processing the message
            string id = message.Substring(3, 8); // Adjust as needed
            if (uniqueMessages.ContainsKey(id))
            {
                uniqueMessages[id] = message;
            }
            else
            {
                uniqueMessages.Add(id, message);
            }

            string _uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);
            string _quedPgns = string.Join(Environment.NewLine, messageQueue);

            if (cb_uniqueOn.Checked)
            {
                tb_CAN_Bus_View.Text = _uniquePgns;
            }
            else
            {
                tb_CAN_Bus_View.Text = _quedPgns;
            }
        }
        private void Btn_Validate_Click(object sender, EventArgs e)
        {
            _OScreenCount = 0;
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            Call_PgnManager_GatherOnscreen();
            if (_OScreenCount > 0)
            {
                _myPGNManager.First_Call();
            }
            else
            {
                MessageBox.Show("No SPN_Control found to serialize.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void Call_PgnManager_GatherOnscreen()
        {
            if (_myPGNManager == null)
            {
                _myPGNManager = new PGN_MANAGER(this);
            }
            _OScreenCount = _myPGNManager.Get__numberOfunique_PGNADRSS();
            lbl_OnScreenCount.Text = " On Screen UCS: " + _OScreenCount.ToString();
            if (_OScreenCount == 0)
            {
                lbl_OnScreenCount.BackColor = Color.Red;
                lbl_OnScreenCount.ForeColor = Color.White;
                lbl_OnScreenCount.Text += " ZERO ? ";
                return;
            }
            else
            {
                lbl_OnScreenCount.BackColor = Color.Green;
                lbl_OnScreenCount.ForeColor = Color.White;
            }
        }
        private void Btn_RunStop_Click(object sender, EventArgs e)
        {
            if (!_isOnCanBus)
            {
                KvsrManager.Instance.Init();
                KvsrManager.Instance.OnMessageReceived += KvsrManager_OnMessageReceived;
            }
            else
            {
                KvsrManager.Instance.Close();
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
        }
        private void SendAllPgnMessages()
        {
            int busNumber = 1; // Assuming bus number is 1
            _isOnCanBus = KvsrManager.Instance.GetIsOnBus();
            if (_isOnCanBus)
            {
                lbl_onBus.BackColor = Color.Green;
                lbl_onBus.ForeColor = Color.White;
                lbl_onBus.Text = "ON BUS";
            }
            else
            {
                lbl_onBus.BackColor = Color.Red;
                lbl_onBus.ForeColor = Color.White;
                lbl_onBus.Text = "OFF BUS";
            }
            if (!_isOnCanBus) { return; }
            if (_myPGNManager != null)
            {
                _myPGNManager.LoadByteArraysForGroups();
                var pgnByteArrays = _myPGNManager.GetPgnByteArrays();
                foreach (var entry in pgnByteArrays.Values)
                {
                    KvsrManager.Instance.SendPGN_withStatus(1, entry.pgn, entry.data);

                }
            }
            else
            {
                Debug.WriteLine("[DEBUG] PGN Manager is not initialized");
            }
            foreach (var entry in vCinc_DynPos1.Get_PGNData())
            {
                int pgn = entry.Key;
                byte[] data = entry.Value;

                // Send the message using your method
                KvsrManager.Instance.SendPGN_withStatus(busNumber, pgn, data);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            looptimer.Stop();
            if (_isOnCanBus)
            {
                _isOnCanBus = false;
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
                KvsrManager.Instance.Close();
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }
        #endregion
        */
    }
}
