using Newtonsoft.Json;
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
using VCI_Forms_SPN._BackEndDataOBJs;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

namespace VCI_Forms_SPN.MyForms.BKGFroms
{
    public partial class SSRSk12_234BKG : Form
    {
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
        private PipeManager _pipeManager;
        private bool _pipeIsOpen = false;
        private Timer pipeTimer;
        private readonly object _syncLock = new object();
        double _myJET_ANG = 50;
        double _myTHRUST = 0;

        public SSRSk12_234BKG()
        {
            InitializeComponent();
          
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
        

            pipeTimer = new Timer();
            pipeTimer.Interval = 320;
            pipeTimer.Tick += PipeTimer_Tick;

            _pipeManager = new PipeManager();
            _pipeManager.OnMessageReceived += PipeManager_OnMessageReceived;

            UpdateButtonState();
            InitializeControls_StationControls();
        }
        public void InitializeControls_StationControls()
        {

            vCinc_Sta1ctrl.MainButtonStateChanged += (s, e) =>
            {
                if (vCinc_Sta1ctrl.Get_MainButtonState())
                {
                    vCinc_Sta2ctrl.Set_MainButtonState(false); // Turn off Station 2
                }
            };

            vCinc_Sta2ctrl.MainButtonStateChanged += (s, e) =>
            {
                if (vCinc_Sta2ctrl.Get_MainButtonState())
                {
                    vCinc_Sta1ctrl.Set_MainButtonState(false); // Turn off Station 1
                }
            };


        }

        void Read_Controls()
        {

            bool Station1State = vCinc_Sta1ctrl.Get_MainButtonState();
            bool Station2State = vCinc_Sta2ctrl.Get_MainButtonState();

            vCinc_stationInCtrl_uc10.Value = Station1State ? 1 : 2;

           int station1LeverP = (int)(vCinc_dualLevers1.Lever_LVal ) * 10;
           int station1LeverS = (int)(vCinc_dualLevers1.Lever_RVal ) *10;

            vCinc_FF43_st1Plever_uc24.Value= station1LeverP;
            vCinc__FF43_st1Slever_uc23.Value= station1LeverS;

            float station1TillerValue = vCinc_Tiller1.TillerValue;
            int station1TillerDirection = 0;
            if (station1TillerValue > -5.0 && station1TillerValue < 5.0)
            {
                station1TillerDirection = 0;
            }
            else if (station1TillerValue < -5.0)
            {
                station1TillerDirection = 1;
            }
            else {
                station1TillerDirection = 2;
            }
             
            int station1_tiller_0_1000 = (int)((station1TillerValue + 100.0f) * (1000.0f / 200.0f)); 

            vCinc_FF41St1Xpos_uc16.Value = station1_tiller_0_1000;
            vCinc_FF42helm1_uc22.Value = station1_tiller_0_1000;

            vCinc_FF41St1Xdir_uc10.Value= station1TillerDirection;

            float  Joy3axis_Yvalue = vCinc_3AxisJoy1.Joystick_YaxisValue;
            float Joy3axis_Xvalue = vCinc_3AxisJoy1.Joystick_XaxisValue;

            int Yvalue_0_1000 = 0;
            int Xvalue_0_1000 = 0;

            //Joy3axis_Yvalue goes from -100.0 to +100 must be converted to 0 to 1000 
           Yvalue_0_1000 = (int)((Joy3axis_Yvalue + 100.0f) * (1000.0f / 200.0f));
            Xvalue_0_1000= (int)((Joy3axis_Xvalue + 100.0f) * (1000.0f / 200.0f));








            float Joy3axis_Zvalue  = vCinc_3AxisJoy1.AngleValue;
            int station2DirectionFromZ = 0;
            int stationZ_0_1000 = 0;

            // Joy3axis_Zvalue 0-360 coverter to 0-1000
            stationZ_0_1000 = (int)(Joy3axis_Zvalue * 1000 / 360);

            //if (Joy3axis_Zvalue >= 0.0f && Joy3axis_Zvalue < 5.0f)
            //{
            //    station2DirectionFromZ = 0;
            //}
            //else if (Joy3axis_Zvalue >= 5.0f && Joy3axis_Zvalue < 180.0f)
            //{
            //    station2DirectionFromZ = 2;
            //}
            //else
            //{
            //    station2DirectionFromZ = 1;
            //}

            if (Joy3axis_Xvalue > -5.0 && Joy3axis_Xvalue < 5.0)
            {
                station2DirectionFromZ = 0;
            }
            else if (Joy3axis_Xvalue < -5.0)
            {
                station2DirectionFromZ = 1;
            }
            else
            {
                station2DirectionFromZ = 2;
            }

            vCinc_FF41Sta2Xdir_uc19.Value= station2DirectionFromZ;

             vCinc_FF41Sta2Xpos_uc20.Value= Yvalue_0_1000;


            vCinc_FF42Helm2_uc21.Value =  Xvalue_0_1000;

            vCinc_FF43_st2Plever_uc25.Value = vCinc_FF43_st2Slever_uc26.Value = stationZ_0_1000;// Yvalue_0_1000;



            vCinc_sta1IdleKnob_uc20.Value = vCinc_ClutchPanel1.KnovValue;
            vCinc_sta2IdleKnob_uc21.Value = vCinc_ClutchPanel2.KnovValue ;
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
 
        private void UpdateButtonState()
        {
            if (_pipeIsOpen)
            {
             //   btn_PipeToggle.Text = "Close Pipe";
            //    btn_PipeToggle.BackColor = Color.Green;
            }
            else
            {
             //   btn_PipeToggle.Text = "Open Pipe";
             //   btn_PipeToggle.BackColor = Color.Red;
            }
        }
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
        private byte myByte = 0x00;
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

        void PGN_Controlled()
        {
            Read_Controls();
            if (vCinc_stationInCtrl_uc10.Value == 1)
            {
                Controll_tiller_levers();
            }
            else
            {
                Controll_3AxisJoystick();
            }

        }

        void Controll_tiller_levers()
        {
            vCinc_Seng_uc24.Value = (int)(vCinc_dualLevers1.Lever_RVal * 255 / 100.00);
            vCinc_Peng_uc23.Value = (int)(vCinc_dualLevers1.Lever_LVal * 255 / 100.00);

            // Tiller value ranges from -100 to +100, convert it to a 0 to 360 value
            float tillerValue = vCinc_Tiller1.TillerValue;

            if (tillerValue < 0)
            {
                // Convert -100 to 0 -> 180 to 0
                vCinc_helm_uc16.Value = (int)(180 + (tillerValue * 180 / 100));
                // Convert -100 to 0 -> 127 to 0
                vCinc_pnoz_uc21.Value = (int)(127 + (tillerValue * 127 / 100));
                vCinc_snoz_uc20.Value = (int)(127 + (tillerValue * 127 / 100));

                // Must make the pbuck lower from 255 down to 0
                // vCinc_pbuk_uc22.Value = (int)(255 + (tillerValue * 255 / 100));
                vCinc_pbuk_uc22.Value = 127;
                // And sbuck increase from 0 to 255
                vCinc_sbuk_uc19.Value = (int)(-tillerValue * 255 / 100);
            }
            else
            {
                // Convert 0 to 100 -> 180 to 360
                vCinc_helm_uc16.Value = (int)(180 + (tillerValue * 180 / 100));
                // Convert 0 to 100 -> 127 to 254
                vCinc_pnoz_uc21.Value = (int)(127 + (tillerValue * 127 / 100));
                vCinc_snoz_uc20.Value = (int)(127 + (tillerValue * 127 / 100));

                // Must make the sbuck lower from 255 down to 0
                //  vCinc_sbuk_uc19.Value = (int)(255 - (tillerValue * 255 / 100));
                vCinc_sbuk_uc19.Value = 127;
                // And pbuck increase from 0 to 255
                vCinc_pbuk_uc22.Value = (int)(tillerValue * 255 / 100);
            }
        }

        void Controll_3AxisJoystick()
        {
            //joyx value from -100 to +100   -> for helm
            //joyy value from -100 to +100  -> for throttle
            //joyAxis 0-360 

            float jx = vCinc_3AxisJoy1.Joystick_XaxisValue;
            float jy = vCinc_3AxisJoy1.Joystick_YaxisValue;
            float jz = vCinc_3AxisJoy1.AngleValue;

            if (jx < 0)
            {
                vCinc_helm_uc16.Value = (int)(180 + (jx * 180 / 100));
                vCinc_pnoz_uc21.Value = (int)(127 + (jx * 127 / 100));
                vCinc_snoz_uc20.Value = (int)(127 + (jx * 127 / 100));


            }
            else
            {
                vCinc_helm_uc16.Value = (int)(180 + (jx * 180 / 100));
                vCinc_pnoz_uc21.Value = (int)(127 + (jx * 127 / 100));
                vCinc_snoz_uc20.Value = (int)(127 + (jx * 127 / 100));

            }



            //vCinc_sbuk_uc19  need a value from 0 to 250 
            //  vCinc_pbuk_uc22 need a value from 0 to 250
            //use jy value from -100 to +100

            if (jy < 0)
            {
                vCinc_sbuk_uc19.Value = (int)(125 + (jy * 125 / 100));
                vCinc_pbuk_uc22.Value = (int)(125 + (jy * 125 / 100));
            }
            else
            {
                vCinc_sbuk_uc19.Value = (int)(125 + (jy * 125 / 100));
                vCinc_pbuk_uc22.Value = (int)(125 + (jy * 125 / 100));
            }

            //use angle 0-360 for throttle
            vCinc_Seng_uc24.Value = (int)(jz * 255 / 360);
            vCinc_Peng_uc23.Value = (int)(jz * 255 / 360);

        }

        private void Looptimer_Tick(object sender, EventArgs e)
        {
            lock (_syncLock)
            {
             
                PGN_Controlled();

                SendAllPgnMessages();
            }
        }
        void UpdateTheGPS_withLocalControlValues()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateTheGPS_withLocalControlValues));
                return;
            }
            lock (_syncLock)
            {
              //  vCinc_DynPos1.Update_CenterMap_Heading(VESSEL_LOC, VESSEL_HEADING);
            }
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
                    Debug.WriteLine("Attempted to invoke on a disposed object.");
                }
                return;
            }
            string id = message.Substring(3, 8);
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
            //foreach (var entry in vCinc_DynPos1.Get_PGNData())
            //{
            //    int pgn = entry.Key;
            //    byte[] data = entry.Value;
            //    KvsrManager.Instance.SendPGN_withStatus(busNumber, pgn, data);
            //}
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_pipeIsOpen)
            {
                _pipeManager.StopPipeServer();
                pipeTimer.Stop();
            }
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

        private void vCinc_uc10_Load(object sender, EventArgs e)
        {

        }

        private void vCinc_uc16_Load(object sender, EventArgs e)
        {

        }
    }
}
