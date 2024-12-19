using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;

namespace VCI_Forms_SPN._BackEndDataOBJs.ShipControls
{
    public  interface IShipUiController
    {
        void InitWithLists(List<UserControl> argListOfHardControls, List<VCinc_uc> argListOfCCinc_Ucs);
        bool AreAllControlsFound();

        void RunController(bool argLinkOnOff);
    }
}
