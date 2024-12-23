using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;

namespace VCI_Forms_SPN._BackEndDataOBJs.ShipControls
{
    public class ShipBirdonController : IShipUiController
    {
        vCinc_StaCtrlMaster HARD_STA1_buttons;//vCinc_StaCtrlMaster1
        vCinc_joy STA1_Joy; //vCinc_joy1
        vCinc_dualLevers STA1_DualLevers;//vCinc_dualLevers1
        vCinc_Tiller STA1_Tiller1; //vCinc_Tiller1
        vCinc_Tiller STA1_Tiller2; //vCinc_Tiller2
        vCinc_ClutchPanel STA1_CLUTCHpanel; //vCinc_ClutchPanel1

        vCinc_StaCtrlAft HARD_STA2_buttons;//vCinc_StaCtrlAft1
        vCinc_joy STA2_Joy; //vCinc_joy2
        vCinc_dualLevers STA2_DualLevers;//vCinc_dualLevers2
        vCinc_Tiller STA2_Tiller1; //vCinc_Tiller3
        vCinc_ClutchPanel STA2_CLUTCHpanel; //vCinc_ClutchPanel2

        vCinc_BackupPanelWJ BKP_PanelWJ; //vCinc_BackupPanelWJ1
        vCinc_BackupPanelEng BKP_PanelEng; //vCinc_BackupPanelEng1

        VCinc_uc uc_ActiveStation;      //ucname:vCinc_FF53_B1_StationInCtrl  SPNName:activeSTATION   
        VCinc_uc uc_OpMode;              //ucname:vCinc_FF53_B4_OpMode  SPNName:OpMode   
        VCinc_uc uc_SwitchControlBits;   //ucname:vCinc_FF59_B7_ctrlbits  SPNName:SwitchControlBits
        VCinc_uc uc_ClutchState;         //ucname:vCinc_FF3134_B0_clutch  SPNName:ClutSig
        VCinc_uc uc_DPcmdStats;    //ucname:vCinc_DPcmd_FF65_B0  SPNName:DPcmdstat

        VCinc_uc VCinttest; //ucname:vCinc_testuc   SPNName:testSpn

        int STATION_IN_CONTROL = 0;
        int PORT_CLUTCH_ENGAGED = 0;
        int STBD_CLUTCH_ENGAGED = 0;

        public void InitWithLists(List<UserControl> argListOfHardControls, List<VCinc_uc> argListOfCCinc_Ucs)
        {
            VCinttest = argListOfCCinc_Ucs.OfType<VCinc_uc>().FirstOrDefault(x => x.Name == "vCinc_testuc");

            HARD_STA1_buttons = argListOfHardControls.OfType<vCinc_StaCtrlMaster>()
                                .FirstOrDefault(x => x.Name == "vCinc_StaCtrlMaster1");
            STA1_Joy = argListOfHardControls.OfType<vCinc_joy>()
                       .FirstOrDefault(x => x.Name == "vCinc_joy1");
            STA1_DualLevers = argListOfHardControls.OfType<vCinc_dualLevers>()
                             .FirstOrDefault(x => x.Name == "vCinc_dualLevers1");
            STA1_Tiller1 = argListOfHardControls.OfType<vCinc_Tiller>()
                           .FirstOrDefault(x => x.Name == "vCinc_Tiller1");
            STA1_Tiller2 = argListOfHardControls.OfType<vCinc_Tiller>()
                           .FirstOrDefault(x => x.Name == "vCinc_Tiller2");
            STA1_CLUTCHpanel = argListOfHardControls.OfType<vCinc_ClutchPanel>()
                               .FirstOrDefault(x => x.Name == "vCinc_ClutchPanel1");

            HARD_STA2_buttons = argListOfHardControls.OfType<vCinc_StaCtrlAft>()
                                .FirstOrDefault(x => x.Name == "vCinc_StaCtrlAft1");
            STA2_Joy = argListOfHardControls.OfType<vCinc_joy>()
                       .FirstOrDefault(x => x.Name == "vCinc_joy2");
            STA2_DualLevers = argListOfHardControls.OfType<vCinc_dualLevers>()
                             .FirstOrDefault(x => x.Name == "vCinc_dualLevers2");
            STA2_Tiller1 = argListOfHardControls.OfType<vCinc_Tiller>()
                           .FirstOrDefault(x => x.Name == "vCinc_Tiller3");
            STA2_CLUTCHpanel = argListOfHardControls.OfType<vCinc_ClutchPanel>()
                               .FirstOrDefault(x => x.Name == "vCinc_ClutchPanel2");

            BKP_PanelWJ = argListOfHardControls.OfType<vCinc_BackupPanelWJ>()
                         .FirstOrDefault(x => x.Name == "vCinc_BackupPanelWJ1");
            BKP_PanelEng = argListOfHardControls.OfType<vCinc_BackupPanelEng>()
                          .FirstOrDefault(x => x.Name == "vCinc_BackupPanelEng1");

            uc_ActiveStation = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF53_B1_StationInCtrl");
            uc_OpMode = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF53_B4_OpMode");
            uc_SwitchControlBits = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF59_B7_ctrlbits");
            uc_ClutchState = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF3134_B0_clutch");
            uc_DPcmdStats = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_DPcmd_FF65_B0");

            if (HARD_STA1_buttons != null && HARD_STA2_buttons != null)
            {
                HARD_STA1_buttons.Set_MainButtonState(true);
                HARD_STA1_buttons.Set_DP_buttonState(false);
                HARD_STA1_buttons.Set_CHAIR_ButtonState(false);
                HARD_STA1_buttons.Set_JoyDK_buttonState(false);
                HARD_STA2_buttons.Set_JoyDK_buttonState(false);

                HARD_STA1_buttons.MainButtonStateChanged += (sender, e) =>
                {
                    if (HARD_STA1_buttons.Get_MainButtonState())
                    {
                        HARD_STA2_buttons.Set_MainButtonState(false);
                    }
                };

                HARD_STA2_buttons.MainButtonStateChanged += (sender, e) =>
                {
                    if (HARD_STA2_buttons.Get_MainButtonState())
                    {
                        HARD_STA1_buttons.Set_MainButtonState(false);
                    }
                };

         
            }
            if (STA1_CLUTCHpanel != null && STA2_CLUTCHpanel!=null) { 
                //set clutch button initially on off
                STA1_CLUTCHpanel.VerticalSwitch_Port_EngageDisengageState = true; //if not hen we are in ENGAGED mode 
                STA1_CLUTCHpanel.VerticalSwitch_Stbd_EngageDisengageState = true;
                STA1_CLUTCHpanel.VerticalSwitch_Port_BkflshBright = false;
                STA1_CLUTCHpanel.VerticalSwitch_Stbd_BkflshDimm = false;
                STA2_CLUTCHpanel.VerticalSwitch_Port_EngageDisengageState = true;
                STA2_CLUTCHpanel.VerticalSwitch_Stbd_EngageDisengageState = true;
                STA2_CLUTCHpanel.VerticalSwitch_Port_BkflshBright = false;
                STA2_CLUTCHpanel.VerticalSwitch_Stbd_BkflshDimm = false;

            }

            if(BKP_PanelWJ != null && BKP_PanelEng != null)
            {
                //BKP_PanelWJ.Set_DP_buttonState(false);
                //BKP_PanelWJ.Set_CHAIR_ButtonState(false);
                //BKP_PanelWJ.Set_JoyDK_buttonState(false);
                //BKP_PanelEng.Set_DP_buttonState(false);
                //BKP_PanelEng.Set_CHAIR_ButtonState(false);
                //BKP_PanelEng.Set_JoyDK_buttonState(false);

                BKP_PanelWJ.VerticalSwitch1Button1InitialState = true;
                BKP_PanelWJ.VerticalSwitch2Button1InitialState = true;

                BKP_PanelEng.VerticalSwitch1Button1InitialState = true;
                BKP_PanelEng.VerticalSwitch2Button1InitialState = true;
            }


        }
        void InitializeControls_StationControls()
        { 
        
        
        }
            public bool AreAllControlsFound()
        {
            return HARD_STA1_buttons != null &&
                   STA1_Joy != null &&
                   STA1_DualLevers != null &&
                   STA1_Tiller1 != null &&
                   STA1_Tiller2 != null &&
                   STA1_CLUTCHpanel != null &&
                   HARD_STA2_buttons != null &&
                   STA2_Joy != null &&
                   STA2_DualLevers != null &&
                   STA2_Tiller1 != null &&
                   STA2_CLUTCHpanel != null &&
                   BKP_PanelWJ != null &&
                   BKP_PanelEng != null &&
                   uc_ActiveStation != null &&
                   uc_OpMode != null &&
                   uc_SwitchControlBits != null;
        }


        public void Initialize()
        {
            HARD_STA1_buttons.Set_MainButtonState(true);

        }
        public ShipBirdonController()
        {

        }

        public void RunController(bool argLinkControlsToUcs) {

            //***********************************************************************************************************************************
            //identify variables for the controller
            bool Station1State = HARD_STA1_buttons.Get_MainButtonState();
            bool Station2State = HARD_STA2_buttons.Get_MainButtonState();
            if (Station1State)
            {
                STATION_IN_CONTROL = 1;
            }
            else if (Station2State)
            {
                STATION_IN_CONTROL = 2;
            }
            else
            {
                STATION_IN_CONTROL = 0;
            }

            //Do Clutch pannel buttons and knob coordination depending on station in control

            /*
            if (STATION_IN_CONTROL == 1)
            {
                //if (STA1_CLUTCHpanel.Get_Sw0_Engage()) { STA2_CLUTCHpanel.Press_Sw0_Engage(); } else { STA2_CLUTCHpanel.Press_Sw0_DisEngage(); }
                //if (STA1_CLUTCHpanel.Get_Sw3_Engage()) { STA2_CLUTCHpanel.Press_Sw3_Engage(); } else { STA2_CLUTCHpanel.Press_Sw3_DisEngage(); }
                // if (STA1_CLUTCHpanel.Get_Sw1_BKFLSH() ) { STA2_CLUTCHpanel.Press_Sw1_Engage(); } else { STA2_CLUTCHpanel.Press_Sw1_DisEngage(); }
                // if (STA1_CLUTCHpanel.Get_Sw2_BKFLSH()) { STA2_CLUTCHpanel.Press_Sw2_Engage(); } else { STA2_CLUTCHpanel.Press_Sw2_DisEngage(); }

                if (STA1_CLUTCHpanel.Get_Sw1_BKFLSH()) { 
                    STA1_CLUTCHpanel.Press_Sw1_Engage();
                    STA2_CLUTCHpanel.Press_Sw1_Engage();
                } else {
                    STA1_CLUTCHpanel.Press_Sw0_Engage(); 
                }
                STA2_CLUTCHpanel.KnovValue = STA1_CLUTCHpanel.KnovValue;
            }
            else if(STATION_IN_CONTROL == 2)
            {
                //if (STA2_CLUTCHpanel.Get_Sw0_Engage()) { STA1_CLUTCHpanel.Press_Sw0_Engage(); } else { STA1_CLUTCHpanel.Press_Sw0_DisEngage(); }
                //if (STA2_CLUTCHpanel.Get_Sw3_Engage()) { STA1_CLUTCHpanel.Press_Sw3_Engage(); } else { STA1_CLUTCHpanel.Press_Sw3_DisEngage(); }
                //if (STA2_CLUTCHpanel.Get_Sw1_BKFLSH()) { STA1_CLUTCHpanel.Press_Sw1_Engage(); } else { STA1_CLUTCHpanel.Press_Sw1_DisEngage(); }
               // if (STA2_CLUTCHpanel.Get_Sw2_BKFLSH()) { STA1_CLUTCHpanel.Press_Sw2_Engage(); } else { STA1_CLUTCHpanel.Press_Sw2_DisEngage(); }

                if (STA2_CLUTCHpanel.Get_Sw1_BKFLSH()) { 
                    STA2_CLUTCHpanel.Press_Sw1_Engage();
                    STA1_CLUTCHpanel.Press_Sw1_Engage();
                } else {
                    STA2_CLUTCHpanel.Press_Sw0_Engage(); 
                }
                STA1_CLUTCHpanel.KnovValue = STA2_CLUTCHpanel.KnovValue;
            }
            */

            //Do Clutch  button read REVERSE LOGIC <IF BUTTON IS PRESSED THEN CLUTCH IS DISENGAGED> depending on station in control
            if (STATION_IN_CONTROL == 1)
            {
                PORT_CLUTCH_ENGAGED = STA1_CLUTCHpanel.Get_Sw0_Engage() ? 1 : 0;
                STBD_CLUTCH_ENGAGED = STA1_CLUTCHpanel.Get_Sw3_Engage() ? 1 : 0;
            }
            else if (STATION_IN_CONTROL == 2)
            {
                PORT_CLUTCH_ENGAGED = STA2_CLUTCHpanel.Get_Sw0_Engage() ? 1 : 0;
                STBD_CLUTCH_ENGAGED = STA2_CLUTCHpanel.Get_Sw3_Engage() ? 1 : 0;
            }
      
            //***********************************************************************************************************************************
            // upate ucs
            if (argLinkControlsToUcs) { 
                if (uc_ActiveStation != null)
                uc_ActiveStation.Value=STATION_IN_CONTROL;
                uc_ClutchState.SetSingleBit(0, STBD_CLUTCH_ENGAGED == 1);
                uc_ClutchState.SetSingleBit(1, PORT_CLUTCH_ENGAGED == 1);
            }

        }

        void Station_1_Controle() { }
        void Station_2_Controle()
        {


        }


    }
}


 