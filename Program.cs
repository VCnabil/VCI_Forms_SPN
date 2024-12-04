using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_SPN.MyForms;
using VCI_Forms_SPN.MyForms.pdfFormSysdiag;
using VCI_Forms_SPN.MyForms.TessForms;

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
            // Application.Run(new SerialForm());
            //Application.Run(new ssrsK12());
            // Application.Run(new C3iForm());
            // Application.Run(new LabJ_v1()); 
            // Application.Run(new Serial_C3());
            // Application.Run(new XIetaSendForm()); 
            // Application.Run(new FormDynPosition2());
            // Application.Run(new FormSSRSDynePosition3()); 
            // Application.Run(new GG_SANFRANForm());
            // Application.Run(new Form2());
            // Application.Run(new HSLC());// <-----------------------last worked verywell
            //Application.Run(new HslcWithBKG()); 
            // Application.Run(new PongForm());
            // Application.Run(new SSRSk12WithBKG()); 
            //  Application.Run(new BirdonFirboatWithBkg());


            // Application.Run(new TesseractWinForms());

            Application.Run(new img_2_Analyze());
           // Application.Run(new img_2_Analyze());
            //Application.Run(new pdf_2_read());
            
          //  Application.Run(new BirdonRawPgn());
          //  Application.Run(new Form1());






        }
    }
}
