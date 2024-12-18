using Newtonsoft.Json;
using System;
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
using VCI_Forms_SPN._BackEndDataOBJs;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

namespace VCI_Forms_SPN.MyForms.BKGFroms
{
    public partial class BirdonSLick : Form
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
        List<string> testMessages;
        private List<VCinc_SPNVAL_uc> spnValControls = new List<VCinc_SPNVAL_uc>();
        private void Form1_Load(object sender, EventArgs e)
        {
            spnValControls = Controls.OfType<VCinc_SPNVAL_uc>().ToList();
        }
        #endregion
        #region Location andPiper 
        VC_LOCATION VESSEL_LOC;
        double VESSEL_HEADING;
        VC_LOCATION WAYPOINT_LOC;
        private PipeManager _pipeManager;
        private bool _pipeIsOpen = false;
        private Timer pipeTimer;
        private readonly object _syncLock = new object();
        double _myJET_ANG = 50;
        double _myTHRUST = 0;
        private byte myByte = 0x00;
        #endregion
        public BirdonSLick()
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
            // tempTimer.Tick += TempTimer_Tick;

            tempTimer.Tick += Looptimer_Tick;
            tempTimer.Start();
            // New timer at 50ms for receiving messages and updating UI
            receiveTimer = new Timer();
            receiveTimer.Interval = 500;
            receiveTimer.Tick += ReceiveTimer_Tick;
            receiveTimer.Start();
            messageBuffer = new StringBuilder();
            this.Load += Form1_Load;
            #endregion

            VESSEL_LOC = new VC_LOCATION();
            WAYPOINT_LOC = new VC_LOCATION();
            vCinc_LatLon_mapCnter.SetLatLon(VESSEL_LOC);
            VESSEL_LOC = vCinc_LatLon_mapCnter.GetLatLon();
            VESSEL_HEADING = (tb_manualHEading.Value / 100.00) % 360.00;
            btn_restBit0.Click += bet_restBit0_Click;
            btn_restBit1.Click += bet_restBit1_Click;

            pipeTimer = new Timer();
            pipeTimer.Interval = 320;
            pipeTimer.Tick += PipeTimer_Tick;

            _pipeManager = new PipeManager();
            _pipeManager.OnMessageReceived += PipeManager_OnMessageReceived;

            btn_PipeToggle.Click += Btn_PipeToggle_Click;
            UpdateButtonState();
            btn_setLatLonToUnity.Click += Btn_setLatLonToUnity_Click;
            btn_webview2.Click += Btn_webview2_Click;
        }

        #region LocPos

        private async void Btn_PipeToggle_Click(object sender, EventArgs e)
        {
            if (_pipeIsOpen)
            {
                _pipeManager.StopPipeServer();
                _pipeIsOpen = false;
                pipeTimer.Stop();
            }
            else
            {
                await _pipeManager.StartPipeServer();
                _pipeIsOpen = true;
                pipeTimer.Start();
            }
            UpdateButtonState();
        }
        private async void PipeTimer_Tick(object sender, EventArgs e)
        {
            var dataToSend = new
            {
                Thrust = _myTHRUST,
                Jet1Angle = _myJET_ANG,
                Jet2Angle = _myJET_ANG,
                WaypointLat = WAYPOINT_LOC.Latitude,
                WaypointLon = WAYPOINT_LOC.Longitude
            };
            string message = JsonConvert.SerializeObject(dataToSend);
            await _pipeManager.SendMessageAsync(message);
        }
        private async void bet_restBit0_Click(object sender, EventArgs e)
        {
            await SetBitForOneSecond(0);
        }
        private async void bet_restBit1_Click(object sender, EventArgs e)
        {
            await SetBitForOneSecond(1);
        }
        private async Task SetBitForOneSecond(int bitPosition)
        {
            myByte |= (byte)(1 << bitPosition);
            vCinc_uc18.Value = myByte;
            await Task.Delay(500);
            myByte &= (byte)~(1 << bitPosition);
            vCinc_uc18.Value = myByte;
        }
        private void Btn_webview2_Click(object sender, EventArgs e)
        {
            // Create and show Form1 with the coordinates
            Form1 form1 = new Form1(vCinc_LatLon_mapCnter.LatitudeDecimal.ToString(), vCinc_LatLon_mapCnter.LongitudeDecimal.ToString(), vCinc_LatLon_waypoint.LatitudeDecimal.ToString(), vCinc_LatLon_waypoint.LongitudeDecimal.ToString());
            form1.Show();
        }

        private void PipeManager_OnMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => PipeManager_OnMessageReceived(message)));
                return;
            }
            try
            {
                if (!_pipeIsOpen)
                {
                    return;
                }
                ShipStatus shipStatus = JsonConvert.DeserializeObject<ShipStatus>(message);
                if (shipStatus != null)
                {
                    lock (_syncLock)
                    {
                        VESSEL_LOC.Latitude = shipStatus.ActualLat;
                        VESSEL_LOC.Longitude = shipStatus.ActualLon;
                        VESSEL_HEADING = shipStatus.ShipHeading;
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"[DEBUG] Error deserializing JSON: {ex.Message}");
            }
        }
        private async void Btn_setLatLonToUnity_Click(object sender, EventArgs e)
        {
            vCinc_DynPos1.Update_CenterMap_Heading(VESSEL_LOC, 0);
            vCinc_DynPos1.ZeroMe();

            // Ensure these calls execute sequentially
            await SetBitForOneSecond(0);
            await SetBitForOneSecond(1);

            var shipCommands = new ShipCommands
            {
                ResetCoordinatesLat = VESSEL_LOC.Latitude,
                ResetCoordinatesLon = VESSEL_LOC.Longitude
            };
            string jsonMessage = JsonConvert.SerializeObject(shipCommands);
            _ = _pipeManager.SendMessageAsync(jsonMessage);

            vCinc_uc18.Value = 1;
            Timer timer = new Timer
            {
                Interval = 1500 // 1.5 seconds
            };
            timer.Tick += (s, args) =>
            {
                vCinc_uc18.Value = 0;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        private void UpdateButtonState()
        {
            if (_pipeIsOpen)
            {
                btn_PipeToggle.Text = "Close Pipe";
                btn_PipeToggle.BackColor = Color.Green;
            }
            else
            {
                btn_PipeToggle.Text = "Open Pipe";
                btn_PipeToggle.BackColor = Color.Red;
            }
        }
        #endregion
        void PGN_Controlled()
        {
        

        }
        #region TemplateFuncs
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
        private void Looptimer_Tick(object sender, EventArgs e)
        {
            lock (_syncLock)
            {
                if (!_pipeIsOpen)
                {
                    VESSEL_LOC = vCinc_LatLon_mapCnter.GetLatLon();
                    VESSEL_HEADING = (tb_manualHEading.Value / 100.00) % 360.00;
                }
                PGN_Controlled();

                vCinc_DynPos1.Update_CenterMap_Heading(VESSEL_LOC, VESSEL_HEADING);
                WAYPOINT_LOC = vCinc_DynPos1.Get_WayPointLOC();
                vCinc_LatLon_waypoint.SetLatLon(WAYPOINT_LOC);
                SendAllPgnMessages();
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
        private void SendAllPgnMessages()
        {
            int busNumber = 1;
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
                KvsrManager.Instance.SendPGN_withStatus(busNumber, pgn, data);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_pipeIsOpen)
            {
                _pipeManager.StopPipeServer();
                pipeTimer.Stop();
            }
            tempTimer.Stop();
            if (_isOnCanBus)
            {
                _isOnCanBus = false;
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
                KvsrManager.Instance.Close();
            }
            receiveTimer.Stop();
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }
        #endregion
    }
}
