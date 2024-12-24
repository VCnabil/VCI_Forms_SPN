﻿using System;
using System.Collections.Concurrent;
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

namespace VCI_Forms_SPN.MyForms.BKGFroms
{
    public partial class CANnalyzerSSRS : Form
    {
        #region TemplateVariavles
        PGN_MANAGER _myPGNManager;
        StringBuilder messageBuffer;
        const int MaxMessages = 24;
        int _OScreenCount = 0;
        Timer tempTimer;     // Existing timer at 200ms
        Timer receiveTimer;  // New timer at 50ms for processing messages and updating UI
        bool _isOnCanBus;

        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();

        // Thread-safe queue to store incoming CAN messages:
        private ConcurrentQueue<string> receivedMessages = new ConcurrentQueue<string>();

        // Latest data by PGN:
        private Dictionary<string, byte[]> latestDataByPgn = new Dictionary<string, byte[]>();
        #endregion

        List<string> testMessages;

        public CANnalyzerSSRS()
        {
            InitializeComponent();

            #region TemplateInitialize
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;

            // Original timer at 200ms
            tempTimer = new Timer();
            tempTimer.Interval = 200;
            tempTimer.Tick += TempTimer_Tick;
            tempTimer.Start();

            // New timer at 50ms for receiving messages and updating UI
            receiveTimer = new Timer();
            receiveTimer.Interval = 50;
            receiveTimer.Tick += ReceiveTimer_Tick;
            receiveTimer.Start();

            messageBuffer = new StringBuilder();
            #endregion

            this.Load += Form1_Load;
            btn_InjectMessage.Click += (s, e) => InjectNextTestMessage();

            testMessages = new List<string>
            {
                "ID=0CFF3134, DLC=8, Data=02-00-0C-A9-00-00-01-0a, Timestamp=1866",
                "ID=0CFF3134, DLC=8, Data=02-00-0C-A9-00-00-01-0a, Timestamp=1866",
                "ID=0CFF322A, DLC=8, Data=00-00-05-9D-00-00-00-00, Timestamp=1867",
                "ID=18FF3600, DLC=8, Data=00-0B-02-00-00-00-00-00, Timestamp=1863",
                "ID=18FF3600, DLC=8, Data=01-0C-01-00-00-00-00-00, Timestamp=1864",
                "ID=18FF3600, DLC=8, Data=00-0B-01-00-00-00-00-00, Timestamp=1865",
                "ID=0CFF3134, DLC=8, Data=00-00-0C-A9-00-00-00-00, Timestamp=1866",
                "ID=0CFF322A, DLC=8, Data=00-00-05-9D-00-00-00-00, Timestamp=1867",
                "ID=0CFF3134, DLC=8, Data=02-00-0C-A9-00-00-01-0a, Timestamp=1866",
                "ID=0CFF3334, DLC=8, Data=00-02-00-00-02-00-00-00, Timestamp=1868",
                "ID=18FEFC29, DLC=8, Data=00-17-FA-FA-00-69-33-33, Timestamp=1869",
                "ID=18FF3600, DLC=8, Data=00-0B-01-00-00-00-00-00, Timestamp=1870",
                "ID=18FEFC29, DLC=8, Data=02-02-0C-A9-20-a0-01-0a, Timestamp=1866",
                "ID=18FEFC29, DLC=8, Data=02-03-1C-A9-20-a0-01-0a, Timestamp=1866",
                "ID=18FEFC29, DLC=8, Data=02-02-0C-A9-20-a0-01-0a, Timestamp=1866",
                "ID=0CFF3134, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=0CFF322A, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=186",
                "ID=0CFF3334, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FEFC29, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF2100, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF3029, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF4129, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF4229, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF4329, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF4929, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF5329, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF5629, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF5729, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF5829, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF5929, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF6029, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF6129, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF6229, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF6329, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF6429, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF7300, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866",
                "ID=18FF7400, DLC=8, Data=" + GenerateRandomData() + ", Timestamp=1866"
            };
        }
        #region TemplateFunctions
        private List<VCinc_SPNVAL_uc> spnValControls = new List<VCinc_SPNVAL_uc>();
        private void Form1_Load(object sender, EventArgs e)
        {
            spnValControls = Controls.OfType<VCinc_SPNVAL_uc>().ToList();
        }

        // This existing timer remains at 200ms for on-bus checks and PGN sending
        private void TempTimer_Tick(object sender, EventArgs e)
        {
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

        // New timer at 50ms for processing incoming messages and updating UI
        private void ReceiveTimer_Tick(object sender, EventArgs e)
        {
            // Dequeue and process all messages
            while (receivedMessages.TryDequeue(out var msg))
            {
                ProcessMessage(msg);
            }

            // After processing all messages, update the UI controls in bulk
            BatchUpdateUserControls();
        }

        private void ProcessMessage(string message)
        {
            // Extract ID
            string id = message.Substring(3, 8);

            // Extract Data
            int dataStartIndex = message.IndexOf("Data=") + 5;
            int commaIndex = message.IndexOf(',', dataStartIndex);
            string dataHex = (commaIndex > dataStartIndex)
                ? message.Substring(dataStartIndex, commaIndex - dataStartIndex)
                : message.Substring(dataStartIndex);

            dataHex = dataHex.Trim();
            byte[] dataBytes = dataHex.Split('-').Select(hex => Convert.ToByte(hex.Trim(), 16)).ToArray();

            // Store the latest data for this PGN
            lock (latestDataByPgn)
            {
                latestDataByPgn[id.ToUpper()] = dataBytes;
            }

            // Update textual displays for unique and queued messages
            if (uniqueMessages.ContainsKey(id))
            {
                uniqueMessages[id] = message;
            }
            else
            {
                uniqueMessages.Add(id, message);
            }

            Queue<string> localQueue = new Queue<string>(MaxMessages);
            if (localQueue.Count >= MaxMessages)
            {
                localQueue.Dequeue();
            }
            localQueue.Enqueue(message);

            string _uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);
            string _quedPgns = string.Join(Environment.NewLine, localQueue);

            // These UI updates are safe since we are on the UI thread (ReceiveTimer_Tick)
            if (cb_uniqueOn.Checked)
            {
                tb_CAN_Bus_View.Text = _uniquePgns;
            }
            else
            {
                tb_CAN_Bus_View.Text = _quedPgns;
            }
        }

        private void BatchUpdateUserControls()
        {
            // Update all user controls with the latest data
            lock (latestDataByPgn)
            {
                foreach (var control in spnValControls)
                {
                    string pgn = control.PGN.ToUpper();
                    if (latestDataByPgn.TryGetValue(pgn, out var dataBytes))
                    {
                        int startIndex = control.A_FirstByteIndex;
                        int length = control.NumberOfBytes;
                        if (startIndex + length <= dataBytes.Length)
                        {
                            int value = 0;

                            // If certain PGNs need reversing:
                            if (length == 2 && (pgn.Contains("FF3134") || pgn.Contains("FF32")))
                            {
                                value = (dataBytes[startIndex] << 8) | dataBytes[startIndex + 1];
                            }
                            else
                            {
                                for (int i = 0; i < length; i++)
                                {
                                    value |= dataBytes[startIndex + i] << (i * 8);
                                }
                            }

                            // No Invoke needed, ReceiveTimer_Tick is on UI thread already
                            control.Value = value;
                        }
                    }
                }
            }
        }

        private string GenerateRandomData()
        {
            Random rnd = new Random();
            byte[] dataBytes = new byte[8];
            for (int i = 0; i < dataBytes.Length; i++)
            {
                dataBytes[i] = (byte)rnd.Next(0, 256);
            }
            return string.Join("-", dataBytes.Select(b => b.ToString("X2")));
        }

        private int testMessageIndex = 0;
        private void InjectNextTestMessage()
        {
            if (testMessageIndex < testMessages.Count)
            {
                // Enqueue the message to simulate receiving it from CAN
                KvsrManager_OnMessageReceived(testMessages[testMessageIndex]);
                testMessageIndex++;
                if (testMessageIndex >= testMessages.Count)
                    testMessageIndex = 0;
            }
        }

        private void KvsrManager_OnMessageReceived(string message)
        {
            // Called from CAN manager (non-UI thread):
            // Just enqueue the message, processing is done by ReceiveTimer_Tick
            receivedMessages.Enqueue(message);
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

/*   
 *   
 *           private void KvsrManager_OnMessageReceivederrer(string message)
        {
            string id = message.Substring(3, 8); // Extract the part between 'ID=' and ','
            string dataHex = message.Substring(message.IndexOf("Data=") + 5, 23); // Extract the data bytes part
            byte[] dataBytes = dataHex.Split('-').Select(hex => Convert.ToByte(hex, 16)).ToArray();//System.FormatException: 'Additional non-parsable characters are at the end of the string.'

            // Update user controls based on PGN (ID)
            UpdateUserControls(id, dataBytes);

     
            string _uniquePgns = "";
            string _quedPgns = "";

            if (InvokeRequired)
            {
                Invoke(new Action(() => KvsrManager_OnMessageReceived(message)));
                return;
            }

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

 *   
      private void UpdateUserControlsold(string id, byte[] dataBytes)
        {
            // Convert ID to PGN
            string pgn = id; //id.Substring(2, 6); // Extract PGN from ID (bits 8–23)

            foreach (var control in spnValControls)
            {
                if (control.PGN.ToUpper() == pgn.ToUpper()) // Match PGN
                {
                    int startIndex = control.A_FirstByteIndex;
                    int length = control.NumberOfBytes;

                    if (startIndex + length <= dataBytes.Length)
                    {
                        int value = 0;

                        // Combine bytes based on control configuration
                        for (int i = 0; i < length; i++)
                        {
                            value |= dataBytes[startIndex + i] << (i * 8);
                        }

                        // Update the control's value (thread-safe)
                        if (control.InvokeRequired)
                        {
                            control.Invoke(new Action(() => control.Value = value));
                        }
                        else
                        {
                            control.Value = value;
                        }
                    }
                }
            }
        }
   
 *   private string GetValueFromMessage(string message, string key)
        {
            int startIndex = message.IndexOf(key) + key.Length;
            int endIndex = message.IndexOf(',', startIndex);
            if (endIndex == -1) endIndex = message.Length;
            return message.Substring(startIndex, endIndex - startIndex);
        }
*/

//int TestValue_8bit = 0;
//int TestValue_16bit_1 = 0;

//switch (id)
//{
//    case "18FF5629":
//        _Raw_Sta1_Throttle1 = (dataBytes[1] << 8) | dataBytes[0];
//        _Raw_Sta1_Throttle2 = (dataBytes[3] << 8) | dataBytes[2];
//        _Raw_Sta1_Helm = (dataBytes[5] << 8) | dataBytes[4];
//        _Raw_Sta1_IdleSetting = (dataBytes[7] << 8) | dataBytes[6];
//        break;
//    case "18FF5729":
//        _Raw_Sta2_Throttle1 = (dataBytes[1] << 8) | dataBytes[0];
//        _Raw_Sta2_Throttle2 = (dataBytes[3] << 8) | dataBytes[2];
//        _Raw_Sta2_Helm = (dataBytes[5] << 8) | dataBytes[4];
//        _Raw_Sta2_IdleSetting = (dataBytes[7] << 8) | dataBytes[6];
//        break;
//    case "18FF5829":
//        _Raw_Noz1 = (dataBytes[1] << 8) | dataBytes[0];
//        _Raw_Noz2 = (dataBytes[3] << 8) | dataBytes[2];
//        _Raw_Buk1 = (dataBytes[5] << 8) | dataBytes[4];
//        _Raw_Buk2 = (dataBytes[7] << 8) | dataBytes[6];
//        break;

//    case "18FF3600":
//        // Take Byte1 value
//        TestValue_8bit = dataBytes[1];
//        Debug.WriteLine($"[DEBUG] TestValue_8bit updated to {TestValue_8bit}");
//        break;

//    case "18FF3601":
//        // Take Byte7 as high byte and Byte6 as low byte
//        TestValue_16bit_1 = (dataBytes[6] << 8) | dataBytes[5];
//        Debug.WriteLine($"[DEBUG] TestValue_16bit_1 updated to {TestValue_16bit_1}");
//        break;
//    case "18FF3604":
//        // Take Byte2 and bit 0 into _isEngOn 
//        bool _isEngOn = (dataBytes[2] & 0x01) != 0;
//        Debug.WriteLine($"[DEBUG] _isEngOn updated to {_isEngOn}");
//        // Take Byte2 and bit 1 into _isAlarmOn
//        bool _isAlarmOn = (dataBytes[2] & 0x02) != 0;
//        break; 

//    default:
//        Debug.WriteLine($"[DEBUG] Unhandled ID: {id}");
//        break;
//}

// Continue with existing unique message handling




//private void KvsrManager_OnMessageReceivedold(string message)
//{
//    Debug.WriteLine($"[DEBUG] received: {message}");
//    string id = message.Substring(3, 8);  

//    string _uniquePgns = "";
//    string _quedPgns = "";
//    if (InvokeRequired)
//    {
//        Invoke(new Action(() => KvsrManager_OnMessageReceivedold(message)));
//        return;
//    }
//    // Store or replace the message for the specific ID
//    if (uniqueMessages.ContainsKey(id))
//    {
//        uniqueMessages[id] = message;
//    }
//    else
//    {
//        uniqueMessages.Add(id, message);
//    }
//    _uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);

//    if (messageQueue.Count >= MaxMessages)
//    {
//        messageQueue.Dequeue();
//    }
//    messageQueue.Enqueue(message);
//    _quedPgns = string.Join(Environment.NewLine, messageQueue);

//    if (cb_uniqueOn.Checked)
//    {
//        tb_CAN_Bus_View.Text = _uniquePgns;
//    }
//    else
//    {
//        tb_CAN_Bus_View.Text = _quedPgns;
//    }
//}



//private void ProcessMessage(string message)
//{
//    // Parse the message
//    string id = GetValueFromMessage(message, "ID=");
//    string data = GetValueFromMessage(message, "Data=");
//    byte[] dataBytes = data.Split('-').Select(hex => Convert.ToByte(hex, 16)).ToArray();

//    // Convert ID to PGN
//    string pgn = id.Substring(2, 6); // Extract the PGN part (last 6 characters)

//    // Find matching user control
//    foreach (var control in spnValControls)
//    {
//        if (control.PGN == pgn)
//        {
//            int startIndex = control.A_FirstByteIndex;
//            int length = control.NumberOfBytes;

//            if (startIndex + length <= dataBytes.Length)
//            {
//                // Extract value based on byte index and length
//                int value = 0;
//                for (int i = 0; i < length; i++)
//                {
//                    value |= dataBytes[startIndex + i] << (i * 8); // Combine bytes into an integer
//                }

//                // Update the user control
//                control.Value = value;
//            }
//        }
//    }



//}