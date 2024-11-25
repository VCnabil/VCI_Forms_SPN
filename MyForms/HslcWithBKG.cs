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

namespace VCI_Forms_SPN.MyForms
{
    public partial class HslcWithBKG : Form
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

        List<VCinc_uc> OnFormItems_CUA = new List<VCinc_uc>();
        List<VCinc_uc> OnFormItems_CUB = new List<VCinc_uc>();
        public HslcWithBKG()
        {
            InitializeComponent();
            #region TemplateInitialize
            cb_uniqueOn.Checked = true;
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
            KvsrManager.Instance.OnMessageReceived += KvsrManager_OnMessageReceived;
            #endregion

            //add all VCinc_uc to list 

        }
        #region TemplateFunctions

        void modified_Dynaminamically() {


            if (vCinc_HELM_A_uc35 != null && vCinc_HELM_B_uc37 != null && vCinc_PNOZ_A_uc45 != null && vCinc_PNOZ_B_uc49 != null && vCinc_SNOZ_A_uc46 != null && vCinc_SNOZ_B_uc50 != null) {

                int _zoroTo100_HELMa = vCinc_HELM_A_uc35.Value;
                float value_NOZZSA_0_255 = (_zoroTo100_HELMa * 255) / 100.0f;

                int _zoroTo100_HELMb = vCinc_HELM_B_uc37.Value;
                float value_NOZZSB_0_255 = (_zoroTo100_HELMb * 255) / 100.0f;

                vCinc_PNOZ_A_uc45.Value = (int)value_NOZZSA_0_255;
                vCinc_SNOZ_A_uc46.Value = (int)value_NOZZSA_0_255;


                vCinc_SNOZ_B_uc50.Value = (int)value_NOZZSB_0_255;
                vCinc_PNOZ_B_uc49.Value = (int)value_NOZZSB_0_255;


            }


            if (vCinc_A_uc12 != null && vCinc_B_uc22 != null && rb_joy !=null && rb_dk != null) {

           


                vCinc_A_uc12.SetSingleBit(1, rb_cua.Checked);
                vCinc_B_uc22.SetSingleBit(1, rb_cub.Checked);

                vCinc_A_uc12.SetSingleBit(4, rb_joy.Checked);
                vCinc_B_uc22.SetSingleBit(4, rb_joy.Checked);

                vCinc_A_uc12.SetSingleBit(5, rb_dk.Checked);
                vCinc_B_uc22.SetSingleBit(5, rb_dk.Checked);


                if (rb_cua.Checked)
                {
                    foreach (VCinc_uc itemA in OnFormItems_CUA)
                    {
                        itemA.SetBorder(Color.Blue, 4);
                        itemA.Visible = true;
                    }
                    foreach (VCinc_uc itemB in OnFormItems_CUB)
                    {
                        itemB.SetBorder(Color.Red, 1);

                        itemB.Visible = false;
                    }
                }
                else
                if (rb_cub.Checked) {
                    foreach (VCinc_uc itemA in OnFormItems_CUA)
                    {
                        itemA.SetBorder(Color.Blue, 1);
                        itemA.Visible = false;
                    }
                    foreach (VCinc_uc itemB in OnFormItems_CUB)
                    {
                        itemB.SetBorder(Color.Red, 4);
                        itemB.Visible = true;
                    }
                }

            }
        }
        private void TempTimer_Tick(object sender, EventArgs e)
        {
            modified_Dynaminamically();
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
            Debug.WriteLine($"[DEBUG] received: {message}");
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


                OnFormItems_CUA = _myPGNManager.GetList_ByNameContaining("_A_");
                OnFormItems_CUB = _myPGNManager.GetList_ByNameContaining("_B_");
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
