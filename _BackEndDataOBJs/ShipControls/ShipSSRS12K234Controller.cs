using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;

namespace VCI_Forms_SPN._BackEndDataOBJs.ShipControls
{
    public class ShipSSRS12K234Controller : IShipUiController
    {
        vCinc_ClutchPanel STA1_CLUTCHpanel; //vCinc_ClutchPanel1
        vCinc_ClutchPanel STA2_CLUTCHpanel; //vCinc_ClutchPanel2
        vCinc_BackupPanelWJ BKP_PanelWJ; //vCinc_BackupPanelWJ1
        vCinc_BackupPanelEng BKP_PanelEng; //vCinc_BackupPanelEng1
        vCinc_3AxisJoy vCinc_3AxisJoy; //vCinc_3AxisJoy1
        vCinc_dualLevers STA1_DualLevers;//vCinc_dualLevers1
        vCinc_Tiller STA1_Tiller1; //vCinc_Tiller1
        vCinc_StaCtrlButton vCinc_StaCtrlButton; //vCinc_Sta1ctrl
        vCinc_StaCtrlButton vCinc_StaCtrlButton2; //vCinc_Sta2ctrl
        VCINC_AutoPilot AutoPilot_Panel; // vcinC_AutoPilot1

        VCinc_uc uc_ActiveStation;      //ucname:vCinc_FF53_B1_StationInCtrl  SPNName:activeSTATION
        VCinc_uc uc_SwitchAUTOPILOTControlBits;   //ucname:vCinc_FF59_B6_ctrlbits  SPNName:SwitchControlBits
        VCinc_uc uc_ClutchState;         //ucname:vCinc_FF3134_B0_clutch  SPNName:ClutSig

        VCinc_uc uc_Stat1_Helm_FF42; //ucname:vCinc_FF42helm1_uc SPNName:Sta1HelmTill
        VCinc_uc uc_Stat2_Helm_FF42; //ucname:vCinc_FF42Helm2_uc SPNName:Sta2HelmTill
        VCinc_uc uc_Sta1IdleKnob_uc; //ucname:vCinc_sta1IdleKnob_uc   SPNName:IDLKnobsta1
        VCinc_uc uc_Sta2IdleKnob_uc; //ucname:vCinc_sta2IdleKnob_uc   SPNName:IDLKnobsta2

        VCinc_uc uc_Sta1JoyXdir;  //ucname:vCinc_FF41St1Xdir_uc   SPNName:Sta1joyXdir0c1L2R
        VCinc_uc uc_Sta1JoyY;  //ucname:vCinc_FF41St1Ypos_uc  SPNName:Station1JoystickY
        VCinc_uc uc_Sta2JoyY;  //ucname:vCinc_FF41Sta2Ypos_uc  SPNName:Station2JoystickY
        VCinc_uc uc_Sta2JoyXdir;  //ucname:vCinc_FF41Sta2Xdir_uc  SPNName:Sta2joyXdir0c1L2R

        VCinc_uc uc_sta1_Plvr;   //ucname:vCinc_FF43_st1Plever_uc  SPNName:Sta1Plvr
        VCinc_uc uc_sta1_Slvr;   //ucname:vCinc__FF43_st1Slever_uc  SPNName:Sta1Slvr
        VCinc_uc uc_sta2_Plvr;   //ucname:vCinc_FF43_st2Plever_uc  SPNName:Sta2Plvr
        VCinc_uc uc_sta2_Slvr;   //ucname:vCinc_FF43_st2Slever_uc  SPNName:Sta2Slvr
        VCinc_uc uc_mainHelm; //ucname:vCinc_helm_uc  SPNName:Helm
        VCinc_uc uc_IdleRpm; //ucname:vCinc_IDLRPM_uc  SPNName:IDLRPM

        VCinc_uc uc_Pnoz; //ucname:vCinc_pnoz_uc  SPNName:Pnoz
        VCinc_uc uc_Pbuk; //ucname:vCinc_pbuk_uc  SPNName:Pbuk
        VCinc_uc uc_Peng; //ucname:vCinc_peng_uc  SPNName:Peng
        VCinc_uc uc_Snoz; //ucname:vCinc_snoz_uc  SPNName:Snoz
        VCinc_uc uc_Sbuk; //ucname:vCinc_sbuk_uc  SPNName:Sbuk
        VCinc_uc uc_Seng; //ucname:vCinc_seng_uc  SPNName:Seng
        VCinc_uc uc_test; //ucname:vCinc_TEST_uc  SPNName:test

        int STATION_IN_CONTROL = 0;
        int STA1_PORT_CLUTCH_State = 0; // 0= DisEnaged 1= Engaged, 2= backflush , 3 = unknown
        int STA1_STBD_CLUTCH_State = 0;
        int STA2_PORT_CLUTCH_State = 0;
        int STA2_STBD_CLUTCH_State = 0;
        int PORT_CLUTCH_Final = 3;
        int STBD_CLUTCH_Final = 3;
        int BackupEngine_Port_InControl=0;
        int BackupEngine_Stbd_InControl=0;
        int BackupWaterJet_Port_inControl = 0;
        int BackupWaterJet_Stbd_inControl = 0;

        public bool AreAllControlsFound()
        {
            //  return (STA1_CLUTCHpanel != null && STA2_CLUTCHpanel != null && BKP_PanelWJ != null && BKP_PanelEng != null && vCinc_3AxisJoy != null && STA1_DualLevers != null && STA1_Tiller1 != null && vCinc_StaCtrlButton != null && vCinc_StaCtrlButton2 != null && uc_ActiveStation != null && uc_SwitchControlBits != null && uc_ClutchState != null);
            return GetMissingControls().Count == 0;
        }
        public List<string> GetMissingControls()
        {
            var missingControls = new List<string>();

            if (STA1_CLUTCHpanel == null) missingControls.Add("STA1_CLUTCHpanel (spnname vCinc_ClutchPanel1)");
            if (STA2_CLUTCHpanel == null) missingControls.Add("STA2_CLUTCHpanel (vCinc_ClutchPanel2)");
            if (BKP_PanelWJ == null) missingControls.Add("BKP_PanelWJ (vCinc_BackupPanelWJ1)");
            if (BKP_PanelEng == null) missingControls.Add("BKP_PanelEng (vCinc_BackupPanelEng1)");
            if (vCinc_3AxisJoy == null) missingControls.Add("vCinc_3AxisJoy (vCinc_3AxisJoy1)");
            if (STA1_DualLevers == null) missingControls.Add("STA1_DualLevers (vCinc_dualLevers1)");
            if (STA1_Tiller1 == null) missingControls.Add("STA1_Tiller1 (vCinc_Tiller1)");
            if (AutoPilot_Panel == null) missingControls.Add("AutoPilotPanel (vcinC_AutoPilot1)");
            if (vCinc_StaCtrlButton == null) missingControls.Add("vCinc_StaCtrlButton (vCinc_Sta1ctrl)");
            if (vCinc_StaCtrlButton2 == null) missingControls.Add("vCinc_StaCtrlButton2 (vCinc_Sta2ctrl)");
            if (uc_ActiveStation == null) missingControls.Add("uc_ActiveStation (vCinc_FF53_B1_StationInCtrl)");
            if (uc_SwitchAUTOPILOTControlBits == null) missingControls.Add("uc_SwitchControlBits (vCinc_FF59_B6_ctrlbits)");
            if (uc_ClutchState == null) missingControls.Add("uc_ClutchState (vCinc_FF3134_B0_clutch)");
            if (uc_Stat1_Helm_FF42 == null) missingControls.Add("uc_Stat1_Helm_FF42 (vCinc_FF42helm1_uc)");
            if (uc_Stat2_Helm_FF42 == null) missingControls.Add("uc_Stat2_Helm_FF42 (vCinc_FF42Helm2_uc)");
            if (uc_Sta1IdleKnob_uc == null) missingControls.Add("uc_Sta1IdleKnob_uc (vCinc_sta1IdleKnob_uc)");
            if (uc_Sta2IdleKnob_uc == null) missingControls.Add("uc_Sta2IdleKnob_uc (vCinc_sta2IdleKnob_uc)");
            if (uc_Sta1JoyXdir == null) missingControls.Add("uc_Sta1JoyXdir (vCinc_FF41St1Xdir_uc)");
            if (uc_Sta1JoyY == null) missingControls.Add("uc_Sta1JoyY (vCinc_FF41St1Ypos_uc)");
            if (uc_Sta2JoyY == null) missingControls.Add("uc_Sta2JoyY (vCinc_FF41Sta2Ypos_uc)");
            if (uc_Sta2JoyXdir == null) missingControls.Add("uc_Sta2JoyXdir (vCinc_FF41Sta2Xdir_uc)");
            if (uc_sta1_Plvr == null) missingControls.Add("uc_sta1_Plvr (vCinc_FF43_st1Plever_uc)");
            if (uc_sta1_Slvr == null) missingControls.Add("uc_sta1_Slvr (vCinc__FF43_st1Slever_uc)");
            if (uc_sta2_Plvr == null) missingControls.Add("uc_sta2_Plvr (vCinc_FF43_st2Plever_uc)");
            if (uc_sta2_Slvr == null) missingControls.Add("uc_sta2_Slvr (vCinc_FF43_st2Slever_uc)");
            if (uc_mainHelm == null) missingControls.Add("uc_mainHelm (vCinc_helm_uc)");
            if (uc_IdleRpm == null) missingControls.Add("uc_IdleRpm (vCinc_IDLRPM_uc)");
            if (uc_Pnoz == null) missingControls.Add("uc_Pnoz (vCinc_pnoz_uc)");
            if (uc_Pbuk == null) missingControls.Add("uc_Pbuk (vCinc_pbuk_uc)");
            if (uc_Peng == null) missingControls.Add("uc_Peng (vCinc_peng_uc)");
            if (uc_Snoz == null) missingControls.Add("uc_Snoz (vCinc_snoz_uc)");
            if (uc_Sbuk == null) missingControls.Add("uc_Sbuk (vCinc_sbuk_uc)");
            if (uc_Seng == null) missingControls.Add("uc_Seng (vCinc_seng_uc)");
            if (uc_test == null) missingControls.Add("uc_test (vCinc_TEST_uc)");


      

            return missingControls;
        }
        public void InitWithLists(List<UserControl> argListOfHardControls, List<VCinc_uc> argListOfCCinc_Ucs)
        {
            STA1_CLUTCHpanel = argListOfHardControls.OfType<vCinc_ClutchPanel>().FirstOrDefault(x => x.Name == "vCinc_ClutchPanel1");
            STA2_CLUTCHpanel = argListOfHardControls.OfType<vCinc_ClutchPanel>().FirstOrDefault(x => x.Name == "vCinc_ClutchPanel2");
            BKP_PanelWJ = argListOfHardControls.OfType<vCinc_BackupPanelWJ>().FirstOrDefault(x => x.Name == "vCinc_BackupPanelWJ1");
            BKP_PanelEng = argListOfHardControls.OfType<vCinc_BackupPanelEng>().FirstOrDefault(x => x.Name == "vCinc_BackupPanelEng1");
            vCinc_3AxisJoy = argListOfHardControls.OfType<vCinc_3AxisJoy>().FirstOrDefault(x => x.Name == "vCinc_3AxisJoy1");
            STA1_DualLevers = argListOfHardControls.OfType<vCinc_dualLevers>().FirstOrDefault(x => x.Name == "vCinc_dualLevers1");
            STA1_Tiller1 = argListOfHardControls.OfType<vCinc_Tiller>().FirstOrDefault(x => x.Name == "vCinc_Tiller1");
            vCinc_StaCtrlButton = argListOfHardControls.OfType<vCinc_StaCtrlButton>().FirstOrDefault(x => x.Name == "vCinc_Sta1ctrl");
            vCinc_StaCtrlButton2 = argListOfHardControls.OfType<vCinc_StaCtrlButton>().FirstOrDefault(x => x.Name == "vCinc_Sta2ctrl");
            AutoPilot_Panel = argListOfHardControls.OfType<VCINC_AutoPilot>().FirstOrDefault(x => x.Name == "vcinC_AutoPilot1");
            uc_ActiveStation = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF53_B1_StationInCtrl");
            uc_SwitchAUTOPILOTControlBits = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF59_B6_ctrlbits");
            uc_ClutchState = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF3134_B0_clutch");
            uc_Stat1_Helm_FF42 = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF42helm1_uc");
            uc_Stat2_Helm_FF42 = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF42Helm2_uc");
            uc_Sta1IdleKnob_uc = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_sta1IdleKnob_uc");
            uc_Sta2IdleKnob_uc = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_sta2IdleKnob_uc");
            uc_Sta1JoyXdir = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF41St1Xdir_uc");
            uc_Sta1JoyY = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF41St1Ypos_uc");
            uc_Sta2JoyY = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF41Sta2Ypos_uc");
            uc_Sta2JoyXdir = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF41Sta2Xdir_uc");
            uc_sta1_Plvr = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF43_st1Plever_uc");
            uc_sta1_Slvr = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc__FF43_st1Slever_uc");
            uc_sta2_Plvr = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF43_st2Plever_uc");
            uc_sta2_Slvr = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF43_st2Slever_uc");
            uc_mainHelm = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_helm_uc");
            uc_IdleRpm = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_IDLRPM_uc");
            uc_Pnoz = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_pnoz_uc");
            uc_Pbuk = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_pbuk_uc");
            uc_Peng = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_peng_uc");
            uc_Snoz = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_snoz_uc");
            uc_Sbuk = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_sbuk_uc");
            uc_Seng = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_seng_uc");
            uc_test = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_TEST_uc");



            vCinc_StaCtrlButton.Set_MainButtonState(true);
            vCinc_StaCtrlButton2.Set_MainButtonState(false);

            vCinc_StaCtrlButton.MainButtonStateChanged += (sender, e) =>
            {
                if (vCinc_StaCtrlButton.Get_MainButtonState())
                {
                    vCinc_StaCtrlButton2.Set_MainButtonState(false);
                }
            };

            vCinc_StaCtrlButton2.MainButtonStateChanged += (sender, e) =>
            {
                if (vCinc_StaCtrlButton2.Get_MainButtonState())
                {
                    vCinc_StaCtrlButton.Set_MainButtonState(false);
                }
            };


            if (AreAllControlsFound())
            {
                //  MessageBox.Show("All Controls Found");
                _SomeNullRefs= false;
            }
            else
            {
                _SomeNullRefs= true;
                var missingControls = GetMissingControls();
                MessageBox.Show($"Not All Controls Found. Missing: {string.Join(", ", missingControls)}");
            }


        }
        bool _SomeNullRefs = true;
         
        public void RunController(bool argLinkOnOff)
        {
             
             if(_SomeNullRefs) return;
    
            

            STA1_PORT_CLUTCH_State = STA1_CLUTCHpanel.ClutchCurrentState_Port();
            STA1_STBD_CLUTCH_State = STA1_CLUTCHpanel.ClutchCurrentState_Stbd();
            STA2_PORT_CLUTCH_State = STA2_CLUTCHpanel.ClutchCurrentState_Port();
            STA2_STBD_CLUTCH_State = STA2_CLUTCHpanel.ClutchCurrentState_Stbd();

            bool Station1State = vCinc_StaCtrlButton.Get_MainButtonState();
            bool Station2State = vCinc_StaCtrlButton2.Get_MainButtonState();

            if (Station1State)
            {
                STATION_IN_CONTROL = 1;
                STA2_CLUTCHpanel.SynchronizeWith(STA1_CLUTCHpanel);
                PORT_CLUTCH_Final= STA1_PORT_CLUTCH_State;
                STBD_CLUTCH_Final = STA1_STBD_CLUTCH_State;

               
            }
            else if (Station2State)
            {
                STATION_IN_CONTROL = 2;
                STA1_CLUTCHpanel.SynchronizeWith(STA2_CLUTCHpanel);
                PORT_CLUTCH_Final = STA2_PORT_CLUTCH_State;
                STBD_CLUTCH_Final = STA2_STBD_CLUTCH_State;
            }
            else
            {
                STATION_IN_CONTROL = 0;
                PORT_CLUTCH_Final = 3;
                STBD_CLUTCH_Final = 3;
            }

            if (BKP_PanelEng.Get_SW1_Backupbutton2State())
            {
                BackupEngine_Port_InControl = 1;
            }
            else {
                BackupEngine_Port_InControl = 0;
            }
            
            if (BKP_PanelEng.Get_SW2_Backupbutton2State())
            {
                BackupEngine_Stbd_InControl = 1;
            }
            else
            {
                BackupEngine_Stbd_InControl = 0;
            }

            if (BKP_PanelWJ.Get_switchLefBAckupState())
            {
                BackupWaterJet_Port_inControl = 1;
            }
            else
            {
                BackupWaterJet_Port_inControl = 0;
            }

            if (BKP_PanelWJ.Get_switchRightBAckupState())
            {
                BackupWaterJet_Stbd_inControl = 1;
            }
            else
            {
                BackupWaterJet_Stbd_inControl = 0;
            }

           // uc_test.Value = BackupWaterJet_Port_inControl;

            if (argLinkOnOff)
            {
                if (AutoPilot_Panel.Get_Btn1Bit_AP())
                {
                    uc_SwitchAUTOPILOTControlBits.SetSingleBit(0, true);
                }
                else
                {
                    uc_SwitchAUTOPILOTControlBits.SetSingleBit(0, false);
                }

                if (AutoPilot_Panel.Get_Btn1Bit_APOVR())
                {
                    uc_SwitchAUTOPILOTControlBits.SetSingleBit(6, true);
                }
                else
                {
                    uc_SwitchAUTOPILOTControlBits.SetSingleBit(6, false);
                }


                //Waterjetbackups
                if (BackupWaterJet_Port_inControl == 1)
                {
                    uc_Pnoz.Value = (int)BKP_PanelWJ.Get_Port_minirailed_X_0_255();
                    uc_Pbuk.Value = (int)BKP_PanelWJ.Get_Port_minirailed_Y_255_0();
                }
                else
                {
                    //if station 1 in control , read Plever . if station 2 is in ctrl read joy Y value 0_255 
                    if (STATION_IN_CONTROL == 1)
                    {
                        uc_Pnoz.Value = STA1_Tiller1.Get_TillerValue_0_255();
                        uc_Pbuk.Value = STA1_DualLevers.Get_Lever_Port_SimpleBucket();
                    }
                    else if (STATION_IN_CONTROL == 2)
                    {
                        uc_Pnoz.Value = vCinc_3AxisJoy.Get_X_0_255();
                        uc_Pbuk.Value = vCinc_3AxisJoy.Get_X_0_255_Positive();
                    }
                    else
                    {
                        uc_Pnoz.Value = 0;
                        uc_Pbuk.Value = 0;
                    }
                }


                
                if (BackupWaterJet_Stbd_inControl == 1)
                {
                    uc_Snoz.Value = (int)BKP_PanelWJ.Get_Stbd_minirailed_X_0_255();
                    uc_Sbuk.Value = (int)BKP_PanelWJ.Get_Stbd_minirailed_Y_255_0();
                }
                else
                {
                    //if station 1 in control , read Plever . if station 2 is in ctrl read joy Y value 0_255 
                    if (STATION_IN_CONTROL == 1)
                    {
                        uc_Snoz.Value = STA1_Tiller1.Get_TillerValue_0_255();
                        uc_Sbuk.Value = STA1_DualLevers.Get_Lever_Stbd_SimpleBucket();
                    }
                    else if (STATION_IN_CONTROL == 2)
                    {
                        uc_Snoz.Value = vCinc_3AxisJoy.Get_X_0_255();
                        uc_Sbuk.Value = vCinc_3AxisJoy.Get_X_0_255_Negative();
                    }
                    else
                    {
                        uc_Snoz.Value = 0;
                        uc_Sbuk.Value = 0;
                    }
                }
                
                //EnginOutput : Backup superceeds stations 
                if (BackupEngine_Port_InControl==1)
                {
                    uc_Peng.Value = (int)BKP_PanelEng.get_portKnobValue();                  
                }
                else
                {
                    //if station 1 in control , read Plever . if station 2 is in ctrl read joy Y value 0_255 
                    if (STATION_IN_CONTROL == 1)
                    {
                        uc_Peng.Value = STA1_DualLevers.Get_Lever_Port_255_0_255_ForEngin();
                    }
                    else if (STATION_IN_CONTROL == 2)
                    {
                        uc_Peng.Value = vCinc_3AxisJoy.Get_Y_0_255();
                    }
                    else
                    {
                        uc_Peng.Value = 0;
                    }
                }

                if (BackupEngine_Stbd_InControl == 1)
                {
                    uc_Seng.Value = (int)BKP_PanelEng.get_stbdKnobValue();
                }
                else
                {
                    //if station 1 in control , read Plever . if station 2 is in ctrl read joy Y value 0_255 
                    if (STATION_IN_CONTROL == 1)
                    {
                        uc_Seng.Value = STA1_DualLevers.Get_Lever_Stbd_255_0_255_ForEngin();
                    }
                    else if (STATION_IN_CONTROL == 2)
                    {
                        uc_Seng.Value = vCinc_3AxisJoy.Get_Y_0_255();
                    }
                    else
                    {
                        uc_Seng.Value = 0;
                    }
                }


                //******************************************************simple connection no station or backup involved
                uc_ActiveStation.Value = STATION_IN_CONTROL;
                //STA1_Tiller1 into  sta1helm 

                uc_Stat1_Helm_FF42.Value = STA1_Tiller1.Get_TillerValue_0_1000();
                uc_Sta1JoyXdir.Value = STA1_Tiller1.Get_TillerDirection();
                // vCinc_3AxisJoy.Zposition 0_1000 into  sta2helm

                uc_Stat2_Helm_FF42.Value = vCinc_3AxisJoy.Get_Z_0_1000();
                uc_Sta1JoyY.Value = STA1_Tiller1.Get_TillerValue_0_1000();
                uc_Sta2JoyXdir.Value = vCinc_3AxisJoy.Get_X_direction();
                uc_Sta2JoyY.Value = vCinc_3AxisJoy.Get_Y_0_1000();
                //idleknobs

                uc_Sta1IdleKnob_uc.Value = (int)STA1_CLUTCHpanel.KnobValueINIT * 18;
                uc_Sta2IdleKnob_uc.Value = (int)STA2_CLUTCHpanel.KnobValueINIT * 18;
                //clutch 
                switch (STBD_CLUTCH_Final) // 0 =DIS 1=ENG 2=BKF 3=UNK (on the byte bit0=SEngage, bit1=PEngage, bit2=SBKF, bit3=PBKF)
                {
                    case 0://Port DisEngaged
                        uc_ClutchState.SetSingleBit(0, false);//S eng-dis
                        uc_ClutchState.SetSingleBit(2, false);//S BKFOn-BKFoff
                        break;
                    case 1://Port Engaged
                        uc_ClutchState.SetSingleBit(0, true);//S eng-dis
                                                             // uc_ClutchState.SetSingleBit(1, true);//P eng-dis
                        uc_ClutchState.SetSingleBit(2, false);//S BKFOn-BKFoff
                        break;
                    case 2://Port BKF   
                        uc_ClutchState.SetSingleBit(0, false);//S eng-dis
                                                              // uc_ClutchState.SetSingleBit(1, false);//P eng-dis
                        uc_ClutchState.SetSingleBit(2, true);//S BKFOn-BKFoff
                        break;
                    case 3://Port Unknown
                        uc_ClutchState.SetSingleBit(0, false);//S eng-dis
                        uc_ClutchState.SetSingleBit(2, false);//S BKFOn-BKFoff
                        break;


                }

                switch (PORT_CLUTCH_Final)
                {
                    case 0://Port DisEngaged

                        uc_ClutchState.SetSingleBit(1, false);//P eng-dis
                        uc_ClutchState.SetSingleBit(3, false);//P BKFOn-BKFoff
                        break;
                    case 1://Port Engaged
                        uc_ClutchState.SetSingleBit(1, true);
                        uc_ClutchState.SetSingleBit(3, false);
                        break;
                    case 2://Port BKF   
                        uc_ClutchState.SetSingleBit(1, false);
                        uc_ClutchState.SetSingleBit(3, true);
                        break;
                    case 3://Port Unknown
                        uc_ClutchState.SetSingleBit(1, false);
                        uc_ClutchState.SetSingleBit(3, false);

                        break;
                }

            }
        }
    }
}
