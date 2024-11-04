using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCI_Forms_SPN._BackEndDataOBJs
{
    public class JSONSHIPdatamodle
    {
    }

    public class ShipLiveData
    {
        public double Thrust { get; set; }
        public double Jet1Angle { get; set; }
        public double Jet2Angle { get; set; }
        public int StationInControl { get; set; }
        public string HarborMode { get; set; }
        public string DpMode { get; set; }
        public double WaypointLat { get; set; }
        public double WaypointLon { get; set; }
        public string AlarmState { get; set; }
        public byte[] FaultsBytes { get; set; } = new byte[8];
    }

    public class ShipCommands
    {
        public double ResetCoordinatesLat { get; set; }
        public double ResetCoordinatesLon { get; set; }
    }

    public class ShipStatus
    {
        public double ActualLat { get; set; }
        public double ActualLon { get; set; }
        public double ShipHeading { get; set; }
    }
}
