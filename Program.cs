using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //  Application.Run(new SerialForm());
            //  Application.Run(new ssrsK12());
            //   Application.Run(new C3iForm());
            //  Application.Run(new LabJ_v1()); 
             Application.Run(new Serial_C3());
           // Application.Run(new XIetaSendForm());
        }
    }
}
