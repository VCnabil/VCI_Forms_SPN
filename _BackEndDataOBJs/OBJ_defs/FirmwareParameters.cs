using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs
{
    public class FirmwareParameters
    {
        public string ProjectName { get; set; }
        public string ParametersFilePath { get; set; } // New property
        public string FirmwareDirName { get; set; }
        public string Version { get; set; }
        public string EepromStatus { get; set; }
        public string SvnRevision { get; set; }
        public Dictionary<string, string> EParams { get; set; } // Key: Parameter Name, Value: Parameter Value (a)

        public FirmwareParameters()
        {
            EParams = new Dictionary<string, string>();
        }
    }

    public class vcEparam
    { 

    
    }
}
