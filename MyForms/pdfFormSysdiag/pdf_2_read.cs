using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;
using VCI_Forms_SPN._GLobalz;
using OpenCvSharp;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using VCI_Forms_SPN._BackEndDataOBJs.DocReaders;
using System.IO;
using System.Text.RegularExpressions;

namespace VCI_Forms_SPN.MyForms.pdfFormSysdiag
{
    public partial class pdf_2_read : Form
    {
        string _pathToPDF = "";
        string _sysdiagTitle = "";
        string _sysdiagNumber = "";
        OCV_Filter_V2 _form_oCV_Filter_V2;
        //PDF Region
        List<string> _list_projectnames;
        string _selectedProjectName = "NAB";
        string _pathToSelectedProject_SysDiag = "";
        MyPDF_toInnerFrameMat _mypdf_toInnerFramer;
        MyCV_SR_Locator _myCV_SR_Locator;
        PictureBox InnerFrameImage;
        Mat InnerFrameMat;
        Mat roi_SR_Mat;
        MyOCvTesseract myOCvTesseract;
        //new pdf paths found
        string safepath = @"C:\_____Ufake\Design__Source_Files\System Diagrams\";
        string UNSAFEPATH = @"U:\Design\Source Files\System Diagrams\";
        string _rootPath_forSysdiagFindng = @"C:\_____Ufake\Design__Source_Files\System Diagrams\";


        StringBuilder StringBuilder1;
        List<string> matchingFilesPDFsysdiag;
        Dictionary<string, string> matchedPdfFilenames_for_comboBoxFileNames = new Dictionary<string, string>();

        public pdf_2_read()
        {
            InitializeComponent();
            setup_ExistingUIelements();
            comboBox_pdfFile.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // DEBUGER_StandaloneForm theDebugger = new DEBUGER_StandaloneForm();
            // theDebugger.Show();
        }


        private void SomeUIelementValueChanged(object sender, EventArgs e)
        {
            EventsManagerLib.Call_Broacdast_OCV_OBJ(_form_oCV_Filter_V2);
            Get_innerframeMat_FromSelectedPDF();
        }

        void setup_ExistingUIelements()
        {
            label1_pdfToRead.Text = "PDF to Read: " + _pathToPDF;
            label2_sysdiagtitle.Text = "Title: " + _sysdiagTitle;
            label3_susdiagNumber.Text = " Number: " + _sysdiagNumber;
            _form_oCV_Filter_V2 = new OCV_Filter_V2();
            _myCV_SR_Locator = new MyCV_SR_Locator();
            myOCvTesseract = new MyOCvTesseract();
            //comboBox_pdfFile
            _list_projectnames = ProjectName_ids.Keys.Skip(0).Take(66).ToList();
            comboBox_pdfFile.DataSource = _list_projectnames;
            comboBox_pdfFile.SelectedIndex = 1;
            _selectedProjectName = _list_projectnames[0];
            _pathToSelectedProject_SysDiag = Get_SysDiagStaticPath(_selectedProjectName);//grab the default path to the first project
            //PDF to Read
            _mypdf_toInnerFramer = new MyPDF_toInnerFrameMat();
            InnerFrameMat = _mypdf_toInnerFramer.GET_0_formated_MatOf_pdf(_pathToSelectedProject_SysDiag, true, _form_oCV_Filter_V2);
            //flowlayout
            InnerFrameImage = new PictureBox();
            InnerFrameImage.SizeMode = PictureBoxSizeMode.StretchImage;
            InnerFrameImage.Width = AdjustReolution(_form_oCV_Filter_V2.PdfwidthNeeded);
            InnerFrameImage.Height = AdjustReolution(_form_oCV_Filter_V2.PdfheightNeeded);
            InnerFrameImage.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(InnerFrameMat);
            flowLayoutPanel1.Controls.Add(InnerFrameImage);
            roi_SR_Mat = _myCV_SR_Locator.Get_ROI_SR_Mat_fromInnerFrame(InnerFrameMat);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(roi_SR_Mat);

            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            _rootPath_forSysdiagFindng = safepath;// UNSAFEPATH;
            lbl_root.Text = "root :" + _rootPath_forSysdiagFindng;

            matchingFilesPDFsysdiag = PdfSysDiagnosticFinder.FindPdfFiles_Sys_Diag(_rootPath_forSysdiagFindng);
            StringBuilder1 = new StringBuilder();
            foreach (string file in matchingFilesPDFsysdiag)
            {
                StringBuilder1.AppendLine(file);
            }
            textBox_Display.Text = "";
            textBox_Display.Text = StringBuilder1.ToString();
            btn_runSearchPDFS.Click += Btn_runSearchPDFS_Click;
            populate_combobox1();
            comboBox1DYNAMIC.SelectedItem = comboBox1DYNAMIC.Items[0];
            comboBox1DYNAMIC.SelectedIndexChanged += ON_ComboBox1DYN_Chnged;
        }

        private void ON_ComboBox1DYN_Chnged(object sender, EventArgs e)
        {
            string selectedFileName = comboBox1DYNAMIC.SelectedItem.ToString();

            // Use the file name to retrieve the full path from the dictionary
            if (matchedPdfFilenames_for_comboBoxFileNames.TryGetValue(selectedFileName, out string fullPath))
            {
                // fullPath now contains the full path of the selected file
                // You can use it here as needed, for example:
                // MessageBox.Show("Selected file path: " + fullPath);
                _pathToSelectedProject_SysDiag = fullPath;
                _pathToPDF = _pathToSelectedProject_SysDiag;
                InnerFrameMat = _mypdf_toInnerFramer.GET_0_formated_MatOf_pdf(_pathToSelectedProject_SysDiag, true, new OCV_Filter_V2());
                InnerFrameImage.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(InnerFrameMat);
                // must run this to get the cloned innerframe mat and the cloned sysdiag title and number mats
                roi_SR_Mat = _myCV_SR_Locator.Get_ROI_SR_Mat_fromInnerFrame(InnerFrameMat);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(roi_SR_Mat);
                //once Get_ROI_SR_Mat_fromInnerFrame was run, we can get the cloned Sysdiag title an number mats
                Mat CroppedSysdiagTitle = _myCV_SR_Locator.Get_clonedSysDiagTitle();
                pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(CroppedSysdiagTitle);
                Mat CroppedSysdiagNumber = _myCV_SR_Locator.Get_clonedSysDiagNumber();
                pictureBox3.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(CroppedSysdiagNumber);
                string sysdiagTitle = myOCvTesseract.ReadTextFromMat(CroppedSysdiagTitle);
                string sysdiagNumber = myOCvTesseract.ReadTextFromMat(CroppedSysdiagNumber);
                label2_sysdiagtitle.Text = "Sysdiag Title: " + CleanInputREGX(sysdiagTitle);
                label3_susdiagNumber.Text = "Sysdiag Number: " + CleanInputREGX(sysdiagNumber);
                lbl_extractionINT.Text = "int: " + ExtractLastNumber_00_int(sysdiagNumber);
                lbl_myextractionSTR.Text = "str:" + ExtractLastNumber_00(sysdiagNumber);
            }
        }

        void populate_combobox1()
        {

            matchingFilesPDFsysdiag = PdfSysDiagnosticFinder.FindPdfFiles_Sys_Diag(_rootPath_forSysdiagFindng);
            StringBuilder1 = new StringBuilder();
            foreach (string file in matchingFilesPDFsysdiag)
            {
                StringBuilder1.AppendLine(file);
                string fileName = Path.GetFileName(file);
                if (!matchedPdfFilenames_for_comboBoxFileNames.ContainsKey(fileName))
                {
                    // Add the filename as the key and the full path as the value
                    matchedPdfFilenames_for_comboBoxFileNames.Add(fileName, file);
                    comboBox1DYNAMIC.Items.Add(fileName); // Add only the file name to the ComboBox
                }
                else
                {
                    // Handle duplicate file names if necessary
                    // For example, by adding a number or directory name to make the key unique
                }
            }
            textBox_Display.Text = "";
            textBox_Display.Text = StringBuilder1.ToString();
        }


        private void Btn_runSearchPDFS_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_innerframeMat_FromSelectedPDF();
        }

        void Get_innerframeMat_FromSelectedPDF()
        {
            _selectedProjectName = comboBox_pdfFile.SelectedItem.ToString();
            _pathToSelectedProject_SysDiag = Get_SysDiagStaticPath(_selectedProjectName);
            _pathToPDF = _pathToSelectedProject_SysDiag;
            InnerFrameMat = _mypdf_toInnerFramer.GET_0_formated_MatOf_pdf(_pathToSelectedProject_SysDiag, true, new OCV_Filter_V2());
            InnerFrameImage.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(InnerFrameMat);
            // must run this to get the cloned innerframe mat and the cloned sysdiag title and number mats
            roi_SR_Mat = _myCV_SR_Locator.Get_ROI_SR_Mat_fromInnerFrame(InnerFrameMat);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(roi_SR_Mat);
            //once Get_ROI_SR_Mat_fromInnerFrame was run, we can get the cloned Sysdiag title an number mats
            Mat CroppedSysdiagTitle = _myCV_SR_Locator.Get_clonedSysDiagTitle();
            pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(CroppedSysdiagTitle);
            Mat CroppedSysdiagNumber = _myCV_SR_Locator.Get_clonedSysDiagNumber();
            pictureBox3.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(CroppedSysdiagNumber);
            string sysdiagTitle = myOCvTesseract.ReadTextFromMat(CroppedSysdiagTitle);
            string sysdiagNumber = myOCvTesseract.ReadTextFromMat(CroppedSysdiagNumber);
            label2_sysdiagtitle.Text = "Sysdiag Title: " + CleanInputREGX(sysdiagTitle);
            label3_susdiagNumber.Text = "Sysdiag Number: " + CleanInputREGX(sysdiagNumber);
            lbl_extractionINT.Text = "int: " + ExtractLastNumber_00_int(sysdiagNumber);
            lbl_myextractionSTR.Text = "str:" + ExtractLastNumber_00(sysdiagNumber);

        }
        static string CleanInputREGX(string input)
        {
            // Remove leading and trailing whitespace and newlines
            string trimmed = input.Trim();

            // Replace multiple whitespace (including newlines and tabs) with a single space
            string cleaned = Regex.Replace(trimmed, @"\s+", " ");

            return cleaned;
        }
        string ExtractLastNumber_00(string input)
        {
            string match = "";

            if (input.Length > 0 && input.Contains("-"))
            {
                int index_dash = input.LastIndexOf('-');
                int index_numeric = index_dash + 1; // Start searching right after the dash
                int start_index = index_numeric; // Remember the starting index for the numeric part

                // Find the start of the next non-numeric character after the dash
                while (index_numeric < input.Length && char.IsDigit(input[index_numeric]))
                {
                    index_numeric++;
                }

                // If we found some digits after the dash
                if (index_numeric > start_index)
                {
                    match = input.Substring(start_index, index_numeric - start_index);
                }
            }

            return match.Trim();
        }
        int ExtractLastNumber_00_int(string input)
        {
            if (input.Length > 0 && input.Contains("-"))
            {
                int index_dash = input.LastIndexOf('-');
                int index_numeric = index_dash + 1;
                int start_index = index_numeric; // Remember the starting index for the numeric part
                // Find the start of the next non-numeric character after the dash
                while (index_numeric < input.Length && char.IsDigit(input[index_numeric]))
                {
                    index_numeric++;
                }
                // If we found some digits after the dash
                if (index_numeric > start_index)
                {
                    string numericPart = input.Substring(start_index, index_numeric - start_index);
                    if (int.TryParse(numericPart, out int result))
                    {
                        return result; // Successfully parsed the number
                    }
                }
            }

            return 9999;
        }
    }
}
