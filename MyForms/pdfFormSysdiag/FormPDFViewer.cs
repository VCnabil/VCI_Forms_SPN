using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;
using System.IO;

namespace VCI_Forms_SPN.MyForms.pdfFormSysdiag
{
    public partial class FormPDFViewer : Form
    {
        public FormPDFViewer()
        {
            InitializeComponent();
        }
        public void LoadPDF(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                pdfViewer1.Document = PdfDocument.Load(filePath);
            }
            else
            {
                MessageBox.Show("The specified file path is either invalid or the file does not exist.", "Error Loading PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
