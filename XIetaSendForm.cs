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
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

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
            //{ 0x09F1127F, new byte[] { 0x00, 0x3A, 0x19, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            //{ 0x09F8017F, new byte[] { 0x9A, 0x87, 0x01, 0x00, 0xDB, 0x00, 0x00, 0x00 } },
            //{ 0x09FF6729, new byte[] { 0x80, 0xF9, 0x64, 0x3F, 0xE4, 0x7B, 0x41, 0x41 } },
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
            looptimer.Interval = 200;
            looptimer.Tick += Looptimer_Tick;
            looptimer.Start();


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
            //// Store or replace the message for the specific ID
            //if (uniqueMessages.ContainsKey(id))
            //{
            //    uniqueMessages[id] = message;
            //}
            //else
            //{
            //    uniqueMessages.Add(id, message);
            //}
            //_uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);

            //if (messageQueue.Count >= MaxMessages)
            //{
            //    messageQueue.Dequeue();
            //}
            //messageQueue.Enqueue(message);
            //_quedPgns = string.Join(Environment.NewLine, messageQueue);

            //if (cb_uniqueOn.Checked)
            //{
            //    tb_CAN_Bus_View.Text = _uniquePgns;
            //}
            //else
            //{
            //    tb_CAN_Bus_View.Text = _quedPgns;
            //}
        }

        private void Looptimer_Tick(object sender, EventArgs e)
        {
            _isOnCanBus = KvsrManager.Instance.GetIsOnBus();
            if (!_isOnCanBus) { return; }
            SendAllPgnMessages();
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
