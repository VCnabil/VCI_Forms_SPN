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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VCI_Forms_SPN
{
    public partial class XIetaSendForm : Form
    {
        Timer looptimer = new Timer();
        #region TemplateVariavles
        PGN_MANAGER _myPGNManager;
        Queue<string> messageQueue;
        StringBuilder messageBuffer;
        const int MaxMessages = 22;
        int _OScreenCount = 0;
        Timer tempTimer;
        bool _isOnCanBus;
        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();
        #endregion

        private Dictionary<int, byte[]> pgnDataDictionary = new Dictionary<int, byte[]>()
        {
            { 0x09F1127F, new byte[] { 0x00, 0x3A, 0x19, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x09F8017F, new byte[] { 0x9A, 0x87, 0x01, 0x00, 0xDB, 0x00, 0x00, 0x00 } },
            { 0x18FF6729, new byte[] { 0x80, 0xF9, 0x64, 0x3F, 0xE4, 0x7B, 0x41, 0x41 } },
            { 0x18FEFC29, new byte[] { 0x80, 0x00, 0x80, 0x80, 0x00, 0x80, 0x00, 0x00 } },
            { 0x18FF4929, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF5329, new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF5929, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF6529, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF6629, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x68, 0x01, 0x64, 0x00 } },
            { 0x18FF7300, new byte[] { 0xE7, 0x03, 0x00, 0x00, 0x19, 0xFC, 0xFF, 0xFF } }
        };
        public XIetaSendForm()
        {
            InitializeComponent();
            #region TemplateInitialize
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;
            tempTimer = new Timer();
            tempTimer.Interval = 200;
            tempTimer.Start();
            messageBuffer = new StringBuilder();
            messageQueue = new Queue<string>(MaxMessages);
            #endregion
            looptimer.Interval = 200;
            looptimer.Tick += Looptimer_Tick;
            looptimer.Start();
        }
        private void Looptimer_Tick(object sender, EventArgs e)
        {
            //max 90000 min -90000
            double trackerLat = trackBarLat.Value / 1000.000;
            double trackerLon = trackBarLon.Value / 1000.000;

            vCinc_LatLon1.SetLatitude(trackerLat);
            vCinc_LatLon1.SetLongitude(trackerLon);

            // trackBar1 max 36000
            double _myheading = trackBar1.Value / 100.00;
            ushort heading = (ushort)Math.Round(_myheading * Math.PI / 180 * 10000);
             
            label1.Text = _myheading.ToString();
            byte lowByte = (byte)heading;
            byte highByte = (byte)(heading >> 8);
            if (pgnDataDictionary.ContainsKey(0x09F1127F))
            {
                pgnDataDictionary[0x09F1127F][1] = lowByte;
                pgnDataDictionary[0x09F1127F][2] = highByte;
            }

            uint _my_COORD_LAT_x1E7 = (uint)(vCinc_LatLon1.LatitudeDecimal * 1E7);
            uint _my_COORD_LON_x1E7 = (uint)(vCinc_LatLon1.LongitudeDecimal * 1E7);

            byte byt3 = (byte)(_my_COORD_LAT_x1E7 >> 24);
            byte byt2 = (byte)(_my_COORD_LAT_x1E7 >> 16);
            byte byt1 = (byte)(_my_COORD_LAT_x1E7 >> 8);
            byte byt0 = (byte)_my_COORD_LAT_x1E7;

            byte byt7 = (byte)(_my_COORD_LON_x1E7 >> 24);
            byte byt6 = (byte)(_my_COORD_LON_x1E7 >> 16);
            byte byt5 = (byte)(_my_COORD_LON_x1E7 >> 8);
            byte byt4 = (byte)_my_COORD_LON_x1E7;

            if (pgnDataDictionary.ContainsKey(0x09F8017F))
            {
                pgnDataDictionary[0x09F8017F][0] = byt0;
                pgnDataDictionary[0x09F8017F][1] = byt1;
                pgnDataDictionary[0x09F8017F][2] = byt2;
                pgnDataDictionary[0x09F8017F][3] = byt3;

                pgnDataDictionary[0x09F8017F][4] = byt4;
                pgnDataDictionary[0x09F8017F][5] = byt5;
                pgnDataDictionary[0x09F8017F][6] = byt6;
                pgnDataDictionary[0x09F8017F][7] = byt7;
            }

            int my32bitvalue_XI = 12254855;
             

            string textXI = tb_XI.Text;
            if (textXI != "")
            {
                my32bitvalue_XI = Convert.ToInt32(textXI);
            }



            byte byte3 = (byte)(my32bitvalue_XI >> 24);
            byte byte2 = (byte)(my32bitvalue_XI >> 16);
            byte byte1 = (byte)(my32bitvalue_XI >> 8);
            byte byte0 = (byte)my32bitvalue_XI;

            int my32bitvalue_ETA = 5520000;

            string textETA = tb_ETA.Text;
            if (textETA != "")
            {
                my32bitvalue_ETA = Convert.ToInt32(textETA);
            }
            byte byte7 = (byte)(my32bitvalue_ETA >> 24);
            byte byte6 = (byte)(my32bitvalue_ETA >> 16);
            byte byte5 = (byte)(my32bitvalue_ETA >> 8);
            byte byte4 = (byte)my32bitvalue_ETA;

            if (pgnDataDictionary.ContainsKey(0x18FF6729))
            {
                pgnDataDictionary[0x18FF6729][0] = byte0;
                pgnDataDictionary[0x18FF6729][1] = byte1;
                pgnDataDictionary[0x18FF6729][2] = byte2;
                pgnDataDictionary[0x18FF6729][3] = byte3;

                pgnDataDictionary[0x18FF6729][4] = byte4;
                pgnDataDictionary[0x18FF6729][5] = byte5;
                pgnDataDictionary[0x18FF6729][6] = byte6;
                pgnDataDictionary[0x18FF6729][7] = byte7;
            }

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
            SendAllPgnMessages();
            if (_OScreenCount == 0) { return; }

  

        }
        private void KvsrManager_OnMessageReceived(string message)
        {
            // Debug.WriteLine($"[DEBUG] received: {message}");
            string id = message.Substring(3, 8); // "ID=18EA0028" extracts the part between 'ID=' and ','

            string _uniquePgns = "";
            string _quedPgns = "";
            if (InvokeRequired)
            {
                Invoke(new Action(() => KvsrManager_OnMessageReceived(message)));
                return;
            }
            // Store or replace the message for the specific ID
            if (uniqueMessages.ContainsKey(id))
            {
                uniqueMessages[id] = message;
            }
            else
            {
                uniqueMessages.Add(id, message);
            }
            _uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);

            if (messageQueue.Count >= MaxMessages)
            {
                messageQueue.Dequeue();
            }
            messageQueue.Enqueue(message);
            _quedPgns = string.Join(Environment.NewLine, messageQueue);

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

            foreach (var entry in pgnDataDictionary)
            {
                int pgn = entry.Key;
                byte[] data = entry.Value;

                // Send the message using your method
                KvsrManager.Instance.SendPGN_withStatus(busNumber, pgn, data);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isOnCanBus)
            {

                KvsrManager.Instance.Close();
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }
    }
}
