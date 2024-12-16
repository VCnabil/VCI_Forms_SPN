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
using static Kvaser.CanLib.Canlib;

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
            gather_physicalControllers();
            // gather_ucsManually();
            gather_ucsSemiManually();

            button1.Click += SendCall1Status;
        }

        private void SendCall1Status(object sender, EventArgs e)
        {
            string tbString =   textBox1.Text;
            int tbString_int = Convert.ToInt32(tbString, 16);

            if(tbString_int > 255) { tbString_int =255; }
            if (tbString_int < 0) { tbString_int = 0; }


            SendCallCan(tbString_int);
          // int Hexvaluetosend = textBox1.Text.HexToInt();



            //KvsrManager.Instance.SendPGN_withStatus(1, Hexvaluetosend, new byte[8]);
        }

        vCinc_ClutchPanel CP1, CP2;
        vCinc_BackupPanelWJ WJP1;
        vCinc_BackupPanelEng EB1;
        vCinc_Tiller TILLER1;
        vCinc_dualLevers DUALLEVERS;
        vCinc_3AxisJoy JOY3AXIS;
        vCinc_StaCtrlButton STA1, STA2;
        List<UserControl> PhysicalCOntollers;
        List<VCinc_uc> OnScreen;
        void gather_physicalControllers() {
            PhysicalCOntollers = new List<UserControl>();

            CP1 = this.vCinc_ClutchPanel1;
            CP2 = this.vCinc_ClutchPanel2;
            WJP1 = this.vCinc_BackupPanelWJ1;
            EB1 = this.vCinc_BackupPanelEng1;
            TILLER1 = this.vCinc_Tiller1;
            DUALLEVERS = this.vCinc_dualLevers1;
            JOY3AXIS = this.vCinc_3AxisJoy1;
            STA1 = this.vCinc_Sta1ctrl;
            STA2 = this.vCinc_Sta2ctrl;

            PhysicalCOntollers.Add(CP1);
            PhysicalCOntollers.Add(CP2);
            PhysicalCOntollers.Add(WJP1);
            PhysicalCOntollers.Add(EB1);
            PhysicalCOntollers.Add(TILLER1);
            PhysicalCOntollers.Add(DUALLEVERS);
            PhysicalCOntollers.Add(JOY3AXIS);
            PhysicalCOntollers.Add(STA1);
            PhysicalCOntollers.Add(STA2);
        }

        void SendCallCan(int argVAl) {

            int pgn = 0x18FF6429;

            byte[] data = new byte[8];
            data[0] = 0x00;
            data[1] = (byte)argVAl;
            data[2] = 0x01;
            data[3] = 0x01;
            data[4] = 0x00;
            data[5] = 0x00;
            data[6] = 0x00;
            data[7] = 0x00;

            KvsrManager.Instance.SendPGN_withStatus(1,pgn, data);
        
        }

        void gather_ucsManually()
        {
            OnScreen = new List<VCinc_uc>();

            // Add all the vCinc_uc controls as shown in the form’s private fields:
            OnScreen.Add(this.vCinc_sta1IdleKnob_uc20);
            OnScreen.Add(this.vCinc_sta2IdleKnob_uc21);
            OnScreen.Add(this.vCinc_Seng_uc24);
            OnScreen.Add(this.vCinc_Peng_uc23);
            OnScreen.Add(this.vCinc_pbuk_uc22);
            OnScreen.Add(this.vCinc_pnoz_uc21);
            OnScreen.Add(this.vCinc_snoz_uc20);
            OnScreen.Add(this.vCinc_sbuk_uc19);
            OnScreen.Add(this.vCinc_uc17);
            OnScreen.Add(this.vCinc_helm_uc16);
            OnScreen.Add(this.vCinc_uc15);
            OnScreen.Add(this.vCinc_uc14);
            OnScreen.Add(this.vCinc_uc13);
            OnScreen.Add(this.vCinc_uc12);
            OnScreen.Add(this.vCinc_uc11);
            OnScreen.Add(this.vCinc_stationInCtrl_uc10);
            OnScreen.Add(this.vCinc_uc9);
            OnScreen.Add(this.vCinc_uc8);
            OnScreen.Add(this.vCinc_uc7);
            OnScreen.Add(this.vCinc_uc6);
            OnScreen.Add(this.vCinc_uc5);
            OnScreen.Add(this.vCinc_uc4);
            OnScreen.Add(this.vCinc_uc3);
            OnScreen.Add(this.vCinc_uc2);
            OnScreen.Add(this.vCinc_uc1);
            OnScreen.Add(this.vCinc_uc18);
            OnScreen.Add(this.vCinc_FF41St1Xdir_uc10);
            OnScreen.Add(this.vCinc_FF41St1Xpos_uc16);
            OnScreen.Add(this.vCinc_FF41Sta2Xdir_uc19);
            OnScreen.Add(this.vCinc_FF41Sta2Xpos_uc20);
            OnScreen.Add(this.vCinc_FF42Helm2_uc21);
            OnScreen.Add(this.vCinc_FF42helm1_uc22);
            OnScreen.Add(this.vCinc__FF43_st1Slever_uc23);
            OnScreen.Add(this.vCinc_FF43_st1Plever_uc24);
            OnScreen.Add(this.vCinc_FF43_st2Plever_uc25);
            OnScreen.Add(this.vCinc_FF43_st2Slever_uc26);
            OnScreen.Add(this.vCinc_FF33_B0_uc10);
            OnScreen.Add(this.vCinc_FF33_B1_uc16);
            OnScreen.Add(this.vCinc_uc20);
            OnScreen.Add(this.vCinc_uc21);
        }

        void gather_ucsSemiManually()
        {
            OnScreen = new List<VCinc_uc>();

            // Recursively search through all controls on the form for VCinc_uc
            void AddVCincUcs(Control.ControlCollection controls)
            {
                foreach (Control ctrl in controls)
                {
                    if (ctrl is VCinc_uc uc)
                    {
                        OnScreen.Add(uc);
                    }

                    if (ctrl.HasChildren)
                    {
                        AddVCincUcs(ctrl.Controls);
                    }
                }
            }

            AddVCincUcs(this.Controls);

            lbl_OnScreenCount.Text = $"On-Screen UC Count: {OnScreen.Count}";
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

        int STAMASTER = 0;

        void UpdateUcsVisibility()
        {
            bool showTRLS = cbShowControls.Checked;
            bool showUcs = cbShowUcs.Checked;
            foreach (var uc in OnScreen)
            {
                uc.Visible = showUcs;
            }

            vCinc_SFversion1.Visible = showUcs;
            vCinc_SFversion2.Visible = showUcs;
            vCinc_SFversion3.Visible = showUcs;

            foreach (var uc in PhysicalCOntollers)
            {
                uc.Visible = showTRLS;
            }
        }

        void PGN_Controlled()
        {
            UpdateUcsVisibility();

            if (vCinc_Sta1ctrl.Get_MainButtonState() && !vCinc_Sta2ctrl.Get_MainButtonState())
            {
                STAMASTER = 1;
            }
            else if (vCinc_Sta2ctrl.Get_MainButtonState() && !vCinc_Sta1ctrl.Get_MainButtonState())
            {
                STAMASTER = 2;
            }
            else
            {
                STAMASTER = 0;
            }


            if(STAMASTER == 1)
            {
                Station_1_Controle();
            }
            else if (STAMASTER == 2)
            {
                Station_2_Controle();

                //Do Clutch pannel buttons coordination
                if (CP2.Get_Sw0_Engage()) { CP1.Press_Sw0_Engage(); } else { CP1.Press_Sw0_DisEngage(); }
                if (CP2.Get_Sw3_Engage()) { CP1.Press_Sw3_Engage(); } else { CP1.Press_Sw3_DisEngage(); }
                //Do Clutch Pannel knobs coordination
                CP1.KnovValue = CP2.KnovValue;
            }

            LinkControls_HardValues_ToSpns();
            Link_EnginControls();

          //  Read_Controls();
          //if (vCinc_stationInCtrl_uc10.Value == 1)
          //{
          //    Controll_tiller_levers();
          //}
          //else
          //{
          //    Controll_3AxisJoystick();
          //}

        }

        void Station_1_Controle() { }
        void Station_2_Controle() {

        
        }

        void LinkControls_HardValues_ToSpns() {
            if (!cb_LinkControls.Checked) return;

            //station in control 
            vCinc_stationInCtrl_uc10.Value = STAMASTER;

            //clutches always follow clutch panne1 as it is the master pannel for clutches
            vCinc_FF33_B0_uc10.Value = CP1.Get_Sw0_Engage() ? 1 : 0;
            vCinc_FF33_B1_uc16.Value = CP1.Get_Sw3_Engage() ? 1 : 0;


            //Station 1 has tiller and duallevers 
            // tiller is used as helm and dual levers as port engine thrust and stbd engine thrust

            //get tiller hard value 
            vCinc_FF41St1Xpos_uc16.Value = TILLER1.Get_TillerValue_0_1000();
            vCinc_FF42helm1_uc22.Value = TILLER1.Get_TillerValue_0_1000();
            vCinc_FF41St1Xdir_uc10.Value = TILLER1.Get_TillerDirection();

            //get levers hard values
            vCinc_FF43_st1Plever_uc24.Value = DUALLEVERS.Get_Lever_Port_0_1000();
            vCinc__FF43_st1Slever_uc23.Value = DUALLEVERS.Get_Lever_Stbd_0_1000();

            //get idle knob har values
            vCinc_sta1IdleKnob_uc20.Value = CP1.KnovValue;

            //Station2 has joystick3axis only
            // z axis is used as helm 
            // y axis to control port engine thrust and stbd engine thrust
            // x is  TBD

            //get Joystic3axis hard values
            vCinc_FF41Sta2Xpos_uc20.Value = JOY3AXIS.Get_Z_0_1000();
            vCinc_FF41Sta2Xdir_uc19.Value = JOY3AXIS.Get_X_direction();
            vCinc_FF42Helm2_uc21.Value = JOY3AXIS.Get_X_0_1000();

   
            //get joystick3axis hard values
            vCinc_FF43_st2Plever_uc25.Value = JOY3AXIS.Get_Y_0_1000();
            vCinc_FF43_st2Slever_uc26.Value = JOY3AXIS.Get_Y_0_1000();

            //get idle knob har values
            vCinc_sta2IdleKnob_uc21.Value = CP2.KnovValue;
        }

        void Link_EnginControls() {
            if (!cb_LinkControls.Checked) return;

            //check Engin backup or not
            if (EB1.Get_SW1_Backupbutton2State())
            {
                vCinc_Peng_uc23.Value =(int)EB1.get_portKnobValue();

            }
            else {
                if (STAMASTER == 1) { vCinc_Peng_uc23.Value = (int)vCinc_dualLevers1.Get_Lever_Port_0_255(); }
                else if (STAMASTER == 2)
                {
                    vCinc_Peng_uc23.Value = JOY3AXIS.Get_Y_0_255();
                }

            }

            if (EB1.Get_SW2_Backupbutton2State())
            {
                vCinc_Seng_uc24.Value = (int)EB1.get_stbdKnobValue();
            }
            else
            {
                if (STAMASTER == 1) { vCinc_Seng_uc24.Value = (int)vCinc_dualLevers1.Get_Lever_Stbd_0_255(); }
                else if (STAMASTER == 2)
                {
                    vCinc_Seng_uc24.Value = JOY3AXIS.Get_Y_0_255();
                }
            }

            //check waterjeet backup or not
            if (WJP1.Get_switchLefBAckupState())
            {
                vCinc_pnoz_uc21.Value = WJP1.Get_Port_minirailed_X_0_250();
                vCinc_pbuk_uc22.Value = 255 - WJP1.Get_Stbd_minirailed_Y_0_255();

            }
            else
            {
                if (STAMASTER == 1) 
                {
                    float tillerValue = vCinc_Tiller1.TillerValue;

                    if (tillerValue < 0)
                    {
                       
                        vCinc_helm_uc16.Value = (int)(180 + (tillerValue * 180 / 100));
                        vCinc_pnoz_uc21.Value = (int)(127 + (tillerValue * 127 / 100));
                        vCinc_pbuk_uc22.Value = 127;
                    }
                    else
                    {
                         vCinc_helm_uc16.Value = (int)(180 + (tillerValue * 180 / 100));
                        vCinc_pnoz_uc21.Value = (int)(127 + (tillerValue * 127 / 100));
                        vCinc_pbuk_uc22.Value = (int)(tillerValue * 255 / 100);
                    }
                }
                else if (STAMASTER == 2)
                {
                    Controll_3AxisJoystick162122();
                }

            }

            if (WJP1.Get_switchRightBAckupState())
            {
                vCinc_snoz_uc20.Value = WJP1.Get_Stbd_minirailed_X_0_250();
                vCinc_sbuk_uc19.Value = 255 - WJP1.Get_Port_minirailed_Y_0_255();
               
            }
            else
            {
                if (STAMASTER == 1)
                {
                    float tillerValue = vCinc_Tiller1.TillerValue;

                    if (tillerValue < 0)
                    {

                        vCinc_helm_uc16.Value = (int)(180 + (tillerValue * 180 / 100));
                        vCinc_snoz_uc20.Value = (int)(127 + (tillerValue * 127 / 100));
                        vCinc_sbuk_uc19.Value = (int)(-tillerValue * 255 / 100);
                    }
                    else
                    {
                        vCinc_helm_uc16.Value = (int)(180 + (tillerValue * 180 / 100));
                        vCinc_snoz_uc20.Value = (int)(127 + (tillerValue * 127 / 100));
                        vCinc_sbuk_uc19.Value = 127;

                    }
                }
                else if (STAMASTER == 2)
                {
                    Controll_3AxisJoystick161920();
                }

            }

            


        }

        void Controll_3AxisJoystick162122()
        {
            float jx = vCinc_3AxisJoy1.Joystick_XaxisValue;
            float jy = vCinc_3AxisJoy1.Joystick_YaxisValue;
            float jz = vCinc_3AxisJoy1.AngleValue;
            vCinc_helm_uc16.Value = (int)(180 + (jz * 180 / 100));

            if (jx < 0)
            {
                vCinc_pnoz_uc21.Value = (int)(127 + (jx * 127 / 100));
            }
            else
            {
                vCinc_pnoz_uc21.Value = (int)(127 + (jx * 127 / 100));
            }
            if (jy < 0)
            {
                vCinc_pbuk_uc22.Value = (int)(125 + (jy * 125 / 100));
            }
            else
            {
                vCinc_pbuk_uc22.Value = (int)(125 + (jy * 125 / 100));
            }
        }


        void Controll_3AxisJoystick161920()
        {
            float jx = vCinc_3AxisJoy1.Joystick_XaxisValue;
            float jy = vCinc_3AxisJoy1.Joystick_YaxisValue;
            float jz = vCinc_3AxisJoy1.AngleValue;
            vCinc_helm_uc16.Value = (int)(180 + (jz * 180 / 100));
            if (jx < 0)
            {
           
                vCinc_snoz_uc20.Value = (int)(127 + (jx * 127 / 100));
            }
            else
            {
                vCinc_snoz_uc20.Value = (int)(127 + (jx * 127 / 100));
            }

            if (jy < 0)
            {
                vCinc_sbuk_uc19.Value = (int)(125 + (jy * 125 / 100));
            }
            else
            {
                vCinc_sbuk_uc19.Value = (int)(125 + (jy * 125 / 100));
            }
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
