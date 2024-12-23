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

        VCinc_uc uc_ActiveStation;      //ucname:vCinc_FF53_B1_StationInCtrl  SPNName:activeSTATION
        VCinc_uc uc_SwitchAUTOPILOTControlBits;   //ucname:vCinc_FF59_B6_ctrlbits  SPNName:SwitchControlBits
        VCinc_uc uc_ClutchState;         //ucname:vCinc_FF3134_B0_clutch  SPNName:ClutSig

        int STATION_IN_CONTROL = 0;
        int PORT_CLUTCH_State = 0; // 0= DisEnaged 1= Engaged, 2= backflush , 3 = unknown
        int STBD_CLUTCH_State = 0;

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
            if (vCinc_StaCtrlButton == null) missingControls.Add("vCinc_StaCtrlButton (vCinc_Sta1ctrl)");
            if (vCinc_StaCtrlButton2 == null) missingControls.Add("vCinc_StaCtrlButton2 (vCinc_Sta2ctrl)");
            if (uc_ActiveStation == null) missingControls.Add("uc_ActiveStation (vCinc_FF53_B1_StationInCtrl)");
            if (uc_SwitchAUTOPILOTControlBits == null) missingControls.Add("uc_SwitchControlBits (vCinc_FF59_B6_ctrlbits)");
            if (uc_ClutchState == null) missingControls.Add("uc_ClutchState (vCinc_FF3134_B0_clutch)");
      

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
            uc_ActiveStation = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF53_B1_StationInCtrl");
            uc_SwitchAUTOPILOTControlBits = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF59_B6_ctrlbits");
            uc_ClutchState = argListOfCCinc_Ucs.FirstOrDefault(x => x.Name == "vCinc_FF3134_B0_clutch");


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
            }
            else
            {
                var missingControls = GetMissingControls();
                MessageBox.Show($"Not All Controls Found. Missing: {string.Join(", ", missingControls)}");
            }


        }


        public void RunController(bool argLinkOnOff)
        {
            bool Station1State = vCinc_StaCtrlButton.Get_MainButtonState();
            bool Station2State = vCinc_StaCtrlButton2.Get_MainButtonState();

            if (Station1State)
            {
                STATION_IN_CONTROL = 1;
                STA2_CLUTCHpanel.SynchronizeWith(STA1_CLUTCHpanel);
            }
            else if (Station2State)
            {
                STATION_IN_CONTROL = 2;
                STA1_CLUTCHpanel.SynchronizeWith(STA2_CLUTCHpanel);
            }
            else
            {
                STATION_IN_CONTROL = 0;
            }


            if (argLinkOnOff)
            {
                if (uc_ActiveStation != null)
                {
                    uc_ActiveStation.Value = STATION_IN_CONTROL;
                }
            }
        }
    }
}
