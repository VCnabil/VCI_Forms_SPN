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
    public partial class ssrsK12 : Form
    {
        #region TemplateVariavles
        PGN_MANAGER _myPGNManager;
        Queue<string> messageQueue;
        StringBuilder messageBuffer;
        const int MaxMessages = 12;
        int _OScreenCount = 0;
        Timer tempTimer;
        bool _isOnCanBus;
        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();
        #endregion
        public ssrsK12()
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
            tempTimer.Tick += TempTimer_Tick;
            tempTimer.Start();
            messageBuffer = new StringBuilder();
            messageQueue = new Queue<string>(MaxMessages);

            #endregion

            textBox1.TextChanged += TextBox1_TextChanged;

        }

        double _unity_shiplat = 0;
        double _unity_shiplon = 0;
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string _strInpiut = textBox1.Text;
            //the inpu looks like " 42.0121,-12.005" stre them in doubles 
            string[] _strArray = _strInpiut.Split(',');
            if (_strArray.Length == 2)
            {
                _unity_shiplat = Convert.ToDouble(_strArray[0]);
                _unity_shiplon = Convert.ToDouble(_strArray[1]);
                vCinc_GPS1.SetShipLocation(_unity_shiplat, _unity_shiplon);
            }
            label5.Text = _unity_shiplat.ToString() + " " + _unity_shiplon.ToString();


        }
        #region TemplateFunctions
        private void TempTimer_Tick(object sender, EventArgs e)
        {
           //vCinc_GPS1.SetShipLocation(42.111, -85.22);
            vCinc_GPS1.UPDATE_CAN_DATA();
            label3.Text = string.Join(",", vCinc_GPS1.Get_PGNdata_CMDCOORDINATES_09F8017F()); //Get_PGNdata_Heading_09F1127F
            label4.Text = string.Join(",", vCinc_GPS1.Get_PGNdata_Heading_09F1127F());
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
            if (_OScreenCount == 0) { return; }
            if (_myPGNManager != null)
            {
                string _shiplatlon = "";
                _shiplatlon = vCinc_GPS1.Get_ShipLocation_LAT() + " " + vCinc_GPS1.Get_ShipLocation_LON();
                label1.Text = _shiplatlon;

                string _purpleLatlon = "";
                _purpleLatlon = vCinc_GPS1.Get_purpleDot_LAT() + " " + vCinc_GPS1.Get_purpleDot_LON();
                label2.Text = _purpleLatlon;
                
            
                _myPGNManager.LoadByteArraysForGroups();
                var pgnByteArrays = _myPGNManager.GetPgnByteArrays();
                foreach (var entry in pgnByteArrays.Values)
                {
                    int pgn = entry.pgn;
                    byte[] data = entry.data;
                    KvsrManager.Instance.SendPGN_withStatus(1, pgn, data);


                }
            }
            else
            {
                Debug.WriteLine("[DEBUG] PGN Manager is not initialized");
            }
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
        #endregion
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isOnCanBus) { 
            
             KvsrManager.Instance.Close();
             KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }

    }
}
