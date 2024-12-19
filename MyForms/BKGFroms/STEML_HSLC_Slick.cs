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
using VCI_Forms_SPN._BackEndDataOBJs.ShipControls;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

namespace VCI_Forms_SPN.MyForms.BKGFroms
{
    public partial class STEML_HSLC_Slick : Form
    {
        #region TemplateVariavles
        PGN_MANAGER _myPGNManager;
        StringBuilder messageBuffer;
        const int MaxMessages = 24;
        int _OScreenCount = 0;
        Timer MainLoopTimer;     // Existing timer at 200ms
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

        #region ShowHIde
        List<UserControl> _MasterCustomUcsList = new List<UserControl>(); // Master list of all UserControls
        List<UserControl> _ALLHardControls = new List<UserControl>(); // Master list of HArd UserControls like duallevers , clutchpanels ...
        List<UserControl> _All_UCS = new List<UserControl>(); // Master list of all UserControls
        List<UserControl> _All_GpsRelated = new List<UserControl>(); // Master list of allGPS stuf , the uc , latlon, uc for send reset heading/pos, and buttons
        List<vCinc_ClutchPanel> _OnScreenClutchPanels = new List<vCinc_ClutchPanel>();
        List<vCinc_BackupPanelWJ> _OnScreenBackupPanelWJs = new List<vCinc_BackupPanelWJ>();
        List<vCinc_BackupPanelEng> _OnScreenBackupPanelEng = new List<vCinc_BackupPanelEng>();
        List<vCinc_BackupPanelClutch> vCinc_BackupPanelClutches = new List<vCinc_BackupPanelClutch>();
        List<vCinc_BackupPanel> vCinc_BackupPanels = new List<vCinc_BackupPanel>();
        List<vCinc_steerWheel> vCinc_SteerWheels = new List<vCinc_steerWheel>();
        List<vCinc_Tiller> _OnscreenTillers = new List<vCinc_Tiller>();
        List<vCinc_dualLevers> _OnscreenDualLevers = new List<vCinc_dualLevers>();
        List<vCinc_joy> _OnscreenJoys = new List<vCinc_joy>();
        List<vCinc_3AxisJoy> _Onscreen3AxisJoys = new List<vCinc_3AxisJoy>();
        List<vCinc_StaCtrlButton> _OnscreenStaCtrlButtons = new List<vCinc_StaCtrlButton>();
        List<vCinc_StaCtrlMaster> _OnscreenStaCtrlMaster = new List<vCinc_StaCtrlMaster>();
        List<vCinc_StaCtrlAft> _OnscreenStaCtrlAft = new List<vCinc_StaCtrlAft>();
        List<VCinc_uc> _OnsceenVcUcs = new List<VCinc_uc>();
        List<VCinc_SFversion> vCinc_SFversions = new List<VCinc_SFversion>();
        List<VCinc_SPNVAL_uc> OnscreenVcSpns = new List<VCinc_SPNVAL_uc>();
        List<VCinc_DynPos> _OnscreenDynPos = new List<VCinc_DynPos>();
        List<VCinc_LatLon> _OnscreenLatLon = new List<VCinc_LatLon>();
        List<Button> _OnscreenGPSButtons = new List<Button>();
        List<TrackBar> OnScreenTrakBarGPS = new List<TrackBar>();
        void InitializeUiLists()
        {
            // Dynamically populate lists with specific types
            foreach (Control control in Controls)
            {
                if (control is vCinc_ClutchPanel clutchPanel)
                {
                    _OnScreenClutchPanels.Add(clutchPanel);
                    _MasterCustomUcsList.Add(clutchPanel);
                    _ALLHardControls.Add(clutchPanel);
                }
                if (control is vCinc_BackupPanelWJ backupPanelWJ)
                {
                    _OnScreenBackupPanelWJs.Add(backupPanelWJ);
                    _MasterCustomUcsList.Add(backupPanelWJ);
                    _ALLHardControls.Add(backupPanelWJ);
                }
                if (control is vCinc_BackupPanelEng backupPanelEng)
                {
                    _OnScreenBackupPanelEng.Add(backupPanelEng);
                    _MasterCustomUcsList.Add(backupPanelEng);
                    _ALLHardControls.Add(backupPanelEng);
                }
                if (control is vCinc_steerWheel steerWheel)
                {
                    vCinc_SteerWheels.Add(steerWheel);
                    _MasterCustomUcsList.Add(steerWheel);
                    _ALLHardControls.Add(steerWheel);
                }
                if (control is vCinc_Tiller tiller)
                {
                    _OnscreenTillers.Add(tiller);
                    _MasterCustomUcsList.Add(tiller);
                    _ALLHardControls.Add(tiller);
                }
                if (control is vCinc_dualLevers dualLevers)
                {
                    _OnscreenDualLevers.Add(dualLevers);
                    _MasterCustomUcsList.Add(dualLevers);
                    _ALLHardControls.Add(dualLevers);
                }
                if (control is vCinc_joy joy)
                {
                    _OnscreenJoys.Add(joy);
                    _MasterCustomUcsList.Add(joy);
                    _ALLHardControls.Add(joy);
                }
                if (control is vCinc_3AxisJoy axisJoy)
                {
                    _Onscreen3AxisJoys.Add(axisJoy);
                    _MasterCustomUcsList.Add(axisJoy);
                    _ALLHardControls.Add(axisJoy);
                }
                if (control is vCinc_StaCtrlButton staCtrlButton)
                {
                    _OnscreenStaCtrlButtons.Add(staCtrlButton);
                    _MasterCustomUcsList.Add(staCtrlButton);
                    _ALLHardControls.Add(staCtrlButton);
                }
                if (control is vCinc_StaCtrlMaster staCtrlMaster)
                {
                    _OnscreenStaCtrlMaster.Add(staCtrlMaster);
                    _MasterCustomUcsList.Add(staCtrlMaster);
                    _ALLHardControls.Add(staCtrlMaster);
                }
                if (control is vCinc_StaCtrlAft staCtrlAft)
                {
                    _OnscreenStaCtrlAft.Add(staCtrlAft);
                    _MasterCustomUcsList.Add(staCtrlAft);
                    _ALLHardControls.Add(staCtrlAft);
                }
                if (control is vCinc_BackupPanelClutch backupPanelClutch)
                {
                    vCinc_BackupPanelClutches.Add(backupPanelClutch);
                    _MasterCustomUcsList.Add(backupPanelClutch);
                    _ALLHardControls.Add(backupPanelClutch);
                }
                if (control is vCinc_BackupPanel backupPanel)
                {
                    vCinc_BackupPanels.Add(backupPanel);
                    _MasterCustomUcsList.Add(backupPanel);
                    _ALLHardControls.Add(backupPanel);
                }
                //ucs and spns
                if (control is VCinc_uc vcUc)
                {
                    _MasterCustomUcsList.Add(vcUc);
                    //do not include FF88 as it is a GPS related uc
                    if (vcUc.PGN != "FF88")
                    {
                        _All_UCS.Add(vcUc);
                        _OnsceenVcUcs.Add(vcUc);
                    }
                    else
                    {
                        _All_GpsRelated.Add(vcUc);
                    }
                }
                if (control is VCinc_SFversion sfVersion)
                {
                    vCinc_SFversions.Add(sfVersion);
                    _MasterCustomUcsList.Add(sfVersion);
                    _All_UCS.Add(sfVersion);
                }
                if (control is VCinc_SPNVAL_uc spnVal)
                {
                    OnscreenVcSpns.Add(spnVal);
                    _MasterCustomUcsList.Add(spnVal);
                    //not part of ucs 
                }
                //gps
                if (control is VCinc_DynPos dynPos)
                {
                    _OnscreenDynPos.Add(dynPos);
                    _MasterCustomUcsList.Add(dynPos);
                    _All_GpsRelated.Add(dynPos);
                }
                if (control is VCinc_LatLon latLon)
                {
                    _OnscreenLatLon.Add(latLon);
                    _MasterCustomUcsList.Add(latLon);
                    _All_GpsRelated.Add(latLon);
                }
                if (control is Button gpsButton)
                {
                    if (gpsButton.Name.Contains("btn_GPS"))
                    {
                        _OnscreenGPSButtons.Add(gpsButton);
                    }

                }
                //if trackbar 
                if (control is TrackBar trackBar)
                {
                    if (trackBar.Name.Contains("tb_GPS"))
                    {
                        OnScreenTrakBarGPS.Add(trackBar);
                    }
                }
            }
        }
        IShipUiController myShipUiController;
        #endregion
        public STEML_HSLC_Slick()
        {
            InitializeComponent();
            #region ShowhideUcs
            InitializeUiLists();
            #endregion
            #region TemplateInitialize
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;
            // New timer at 50ms for receiving messages and updating UI
            receiveTimer = new Timer();
            receiveTimer.Interval = 500;
            receiveTimer.Tick += ReceiveTimer_Tick;
            receiveTimer.Start();
            messageBuffer = new StringBuilder();
            this.Load += Form1_Load;
            #endregion

            cbShowHardCtrls.Checked = true;
            cbShowUcs.Checked = true;
            cb_LinkControls.Checked = true;
            myShipUiController = new ShipSTEML_HSLCController();
            myShipUiController.InitWithLists(_ALLHardControls, _OnsceenVcUcs);
            lbl_allFound.BackColor = myShipUiController.AreAllControlsFound() ? Color.Green : Color.Red;

            // Original timer at 200ms
            MainLoopTimer = new Timer();
            MainLoopTimer.Interval = 200;
            // MainLoopTimer.Tick += TempTimer_Tick; <--this one has no gos or pipe 
            MainLoopTimer.Tick += Looptimer_Tick;
            MainLoopTimer.Start();
            #region LocLatLonPipe

            VESSEL_LOC = new VC_LOCATION();
            WAYPOINT_LOC = new VC_LOCATION();
            vCinc_LatLon_mapCnter.SetLatLon(VESSEL_LOC);
            VESSEL_LOC = vCinc_LatLon_mapCnter.GetLatLon();
            VESSEL_HEADING = (tb_GPSmanualHEading.Value / 100.00) % 360.00;
            btn_GPSrestBit0.Click += bet_restBit0_Click;
            btn_GPSrestBit1.Click += bet_restBit1_Click;

            pipeTimer = new Timer();
            pipeTimer.Interval = 320;
            pipeTimer.Tick += PipeTimer_Tick;

            _pipeManager = new PipeManager();
            _pipeManager.OnMessageReceived += PipeManager_OnMessageReceived;

            btn_PipeToggle.Click += Btn_PipeToggle_Click;
            UpdateButtonState();
            btn_GPSsetLatLonToUnity.Click += Btn_setLatLonToUnity_Click;
            btn_GPSwebview2.Click += Btn_webview2_Click;
            #endregion
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

            myShipUiController.RunController(cb_LinkControls.Checked);

        }
        void ShowHideUiElements()
        {
            //HardControls
            foreach (var userControl in _ALLHardControls)
                userControl.Visible = cbShowHardCtrls.Checked;
            //GPS
            foreach (var userControl in _All_GpsRelated)
                userControl.Visible = cbShowGps.Checked;
            //UCS
            foreach (var userControl in _All_UCS)
                userControl.Visible = cbShowUcs.Checked;
            //SPN
            foreach (var userControl in OnscreenVcSpns)
                userControl.Visible = cbShowSpns.Checked;
            //GPSButtons
            foreach (var userControl in _OnscreenGPSButtons)
                userControl.Visible = cbShowGps.Checked;
            //GPStrackbar
            foreach (var userControl in OnScreenTrakBarGPS)
                userControl.Visible = cbShowGps.Checked;
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
                    if (vCinc_LatLon_mapCnter != null && VESSEL_LOC != null)
                    {
                        VESSEL_LOC = vCinc_LatLon_mapCnter.GetLatLon();
                        VESSEL_HEADING = (tb_GPSmanualHEading.Value / 100.00) % 360.00;
                    }

                }

                ShowHideUiElements();
                PGN_Controlled();

                if (vCinc_LatLon_mapCnter != null && VESSEL_LOC != null && WAYPOINT_LOC != null)
                {
                    vCinc_DynPos1.Update_CenterMap_Heading(VESSEL_LOC, VESSEL_HEADING);
                    WAYPOINT_LOC = vCinc_DynPos1.Get_WayPointLOC();
                    vCinc_LatLon_waypoint.SetLatLon(WAYPOINT_LOC);
                }
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
            MainLoopTimer.Stop();
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
