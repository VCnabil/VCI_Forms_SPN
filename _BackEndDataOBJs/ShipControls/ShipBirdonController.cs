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


        int STATION_IN_CONTROL = 0;
   

        public void InitWithLists(List<UserControl> argListOfHardControls, List<VCinc_uc> argListOfCCinc_Ucs)
        {
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

            if (HARD_STA1_buttons != null && HARD_STA2_buttons != null)
            {
                HARD_STA1_buttons.Set_MainButtonState(true);

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
            //identify 

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

            // upate ucs
            if (argLinkControlsToUcs) { 
                if (uc_ActiveStation != null)
                uc_ActiveStation.Value=STATION_IN_CONTROL;          
            }

        }


    }
}


 