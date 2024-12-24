﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;

namespace VCI_Forms_SPN._BackEndDataOBJs.ShipControls
{
    public class ShipSTEML_HSLCController : IShipUiController
    {

        vCinc_BackupPanel vCinc_BackupPanel; //vCinc_BackupPanel1
        VCinc_uc VCinttest; //ucname:vCinc_testuc   SPNName:testSpn
        VCinc_uc uc_DPcmdStats;    //ucname:vCinc_DPcmd_FF65_B0  SPNName:DPcmdstat
        public ShipSTEML_HSLCController() { 
        
        }

        public void InitWithLists(List<UserControl> argListOfHardControls, List<VCinc_uc> argListOfCCinc_Ucs)
        {
            vCinc_BackupPanel = argListOfHardControls.OfType<vCinc_BackupPanel>().FirstOrDefault(x => x.Name == "vCinc_BackupPanel1");
            VCinttest = argListOfCCinc_Ucs.OfType<VCinc_uc>().FirstOrDefault(x => x.Name == "vCinc_testuc");
        }
        public bool AreAllControlsFound()
        {
          
            return vCinc_BackupPanel != null && 
                VCinttest != null;
        }

        public void RunController(bool argLinkOnOff)
        {

            bool PNOZ_isInAuto = vCinc_BackupPanel.VertSwitch_1_StartState;



            if (!argLinkOnOff){return;}
            

            if(PNOZ_isInAuto)
            {
                VCinttest.Value = 1;
            }
            else
            {
                VCinttest.Value = 0;
            }
        }
    }
}