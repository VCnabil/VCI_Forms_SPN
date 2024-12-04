using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using VCI_Forms_SPN._BackEndDataOBJs.DocReaders;
using System.IO;
using System.Text.RegularExpressions;
 

namespace VCI_Forms_SPN.MyForms.pdfFormSysdiag
{
    public partial class img_2_Analyze : Form
    {
        private DEBUGER_StandaloneForm debugForm = null;
        private string _searchTocken = "";
        private string _searchTocken_2 = "";
        private string _MainPdfPath = "";
        private string _MainPdfPath_2 = "";
        private int PathsMatchedCount = 0;
        private int PathsMatchedCount_2 = 0;


        #region OCV
        MyPDF_toInnerFrameMat _mypdf_toInnerFramer;
        Mat LargFullPDF;
        Mat InnerFrameMat;
        Mat Lower_LFrameMat;
        Mat Lower_RFrameMat;
        MyOpenCV5_3MatsCoordinator myOpenCV5_3MatsCoordinator;
        MyOCvTesseract myOCvTesseract;
        bool _isA = false;
        #endregion

        public img_2_Analyze()
        {
            InitializeComponent();
            _searchTocken = tb_search.Text;
            btn_OpenDebugConsole.Click += Btn_OpenDebugConsole_Click;
            tb_search.TextChanged += Tb_search_TextChanged;
            btn_search.Click += Btn_search_Click;
            btn_runOCV.Click += Btn_runOCV_Click;
            _mypdf_toInnerFramer = new MyPDF_toInnerFrameMat();
            myOpenCV5_3MatsCoordinator = new MyOpenCV5_3MatsCoordinator();
            myOCvTesseract = new MyOCvTesseract();
            EventsManagerLib.On_PdfPageSizeRead += EventsManagerLib_On_PdfPageSizeRead;

        }
        private void EventsManagerLib_On_PdfPageSizeRead(float arg_width, float arg_height, bool arg_isA)
        {
            _isA = arg_isA;
            int the_h = 0;
            the_h = (int)Math.Ceiling(arg_height);

            the_h += 0;

            string outStr = " " + arg_width + " x " + the_h;
            if (_isA)
            {
                outStr += " is A type";
            }
            else
            {
                outStr += " is B type";
            }

            lbl_PdfOriginalDims.Text = ".." + outStr;
            EventsManagerLib.Call_LogConsole("opened pdf " + lbl_PdfOriginalDims.Text);

        }
        private void Btn_runOCV_Click(object sender, EventArgs e)
        {
            //clear cb_Hits_2

            cb_Hits_2.SelectedIndexChanged -= cb_Hits_2_SelectedIndexChanged;
            cb_Hits_2.Items.Clear();
            cb_Hits_2.Text = "";
            cb_Hits_2.SelectedIndex = -1;


            foreach (Control control in flowLayout_UCNAVBTNS.Controls)
            {
                if (control is UC_NavButton navButton)
                {
                    navButton.OnUCNavButtonClicked -= NavButton_OnUCNavButtonClicked;
                }
            }

            flowLayout_UCNAVBTNS.Controls.Clear();
            flowLayout_UCNAVBTNS.FlowDirection = FlowDirection.TopDown;
            flowLayout_UCNAVBTNS.WrapContents = false;

            // Disable AutoSize to manually manage the size
            flowLayout_UCNAVBTNS.AutoSize = false;


            // Enable AutoScroll to allow scrolling when items exceed the panel's bounds
            flowLayout_UCNAVBTNS.AutoScroll = true;


            //flowLayout_UCNAVBTNS.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Since AutoSize is disabled, AutoSizeMode is not required, but if you need it for another reason, adjust accordingly
            flowLayout_UCNAVBTNS.AutoSizeMode = AutoSizeMode.GrowOnly; // Adjusted to GrowOnly for illustration, or you can omit this line



            if (_MainPdfPath == "")
            {
                EventsManagerLib.Call_LogConsole("Main pdf path is empty");
                return;
            }
            if (PathsMatchedCount == 0)
            {
                EventsManagerLib.Call_LogConsole("No paths matched");
                return;
            }

            LargFullPDF = _mypdf_toInnerFramer.Convert_PDF_TO_MAT_zoomdi(_MainPdfPath, 4.0f, 400);
            float _vertPercnt_L = 42.0f;
            float _horizPercnt_L = 55.2f;
            float _vertPercnt_R = 18.0f;
            float _horizPercnt_R = 68.0f;
            myOpenCV5_3MatsCoordinator.FeedmeA_LargeMAT_Icloneit_andMake_LandR_mats(LargFullPDF, _isA, _vertPercnt_L, _horizPercnt_L, _vertPercnt_R, _horizPercnt_R);
            myOpenCV5_3MatsCoordinator.IdentifyTBL_makeColumnMatLists(0);
            myOpenCV5_3MatsCoordinator.Identify_SysDiagInfo();
            InnerFrameMat = myOpenCV5_3MatsCoordinator.GET_Original();
            Lower_LFrameMat = myOpenCV5_3MatsCoordinator.Get_LowerROI_L();
            Lower_RFrameMat = myOpenCV5_3MatsCoordinator.Get_LowerROI_R();
            if (InnerFrameMat == null)
            {
                MessageBox.Show("InnerFrameMat is null");
            }
            else
                pb_MAIN.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(InnerFrameMat);

            if (Lower_LFrameMat == null)
            {
                //  MessageBox.Show("Lower_LFrameMat is null");
            }
            else
                pb_TABLE.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(Lower_LFrameMat);

            if (Lower_RFrameMat == null)
            {
                //  MessageBox.Show("Lower_RFrameMat is null");
            }
            else
                pb_Title.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(Lower_RFrameMat);

            List<Mat> _tempSysinfoMats;
            List<Mat> _tempTBLrowsMats;


            _tempSysinfoMats = myOpenCV5_3MatsCoordinator.GetMatsToRead_ifo_oTitle_1_P_2Number();
            _tempTBLrowsMats = myOpenCV5_3MatsCoordinator.Get_MatsToRead_TableRows();

            if (_tempSysinfoMats != null)
            {
                if (_tempSysinfoMats.Count > 0)
                {
                    string SysDiagInfo_str = myOCvTesseract.ReadTextFromListOfMats(_tempSysinfoMats);

                    //split on '^' item0 is the title , item1 is the rev number item2 is DrawingNumber
                    string[] _splitted = SysDiagInfo_str.Split('^');
                    if (_splitted.Length >= 3)
                    {
                        string cleanedString_title = Regex.Replace(_splitted[0], @"\s+", " ");
                        string cleanedString_rev = Regex.Replace(_splitted[1], @"\s+", " ");
                        string cleanedString_drawingNumber = Regex.Replace(_splitted[2], @"\s+", " ");
                        string _line = cleanedString_title + " | " + cleanedString_rev + " | " + cleanedString_drawingNumber;
                        EventsManagerLib.Call_LogConsole(_line);
                        lbl_pdfTitle.Text = cleanedString_title;
                        lbl_pdfRev.Text = cleanedString_rev;
                        lbl_pdfNumber.Text = cleanedString_drawingNumber;
                    }
                    else
                    {

                        string cleanedString_title = Regex.Replace(SysDiagInfo_str, @"\s+", " ");
                        string _line = cleanedString_title;
                        lbl_pdfTitle.Text = cleanedString_title;
                        lbl_pdfRev.Text = "N/A";
                        lbl_pdfNumber.Text = "N/A";
                        EventsManagerLib.Call_LogConsole("regx ed line title = " + _line);
                    }

                    EventsManagerLib.Call_LogConsole(SysDiagInfo_str);
                }
            }


            // Example data for List_Itemnames and ListNumbers
            List<string> List_Itemnames = new List<string> { /* Populate with your items */ };
            List<string> ListNumbers = new List<string> { /* Populate with your numbers */ };
            List<List<Mat>> DUALLIST;

            DUALLIST = myOpenCV5_3MatsCoordinator.Get_MatsToRead_ColumnItems_andColumnNumners();
            if (DUALLIST != null)
            {
                if (DUALLIST.Count > 0)
                {
                    myOCvTesseract.ReadTextFromListOfMats_IntoLists(DUALLIST);

                    int _numOfItems = myOCvTesseract.GetList_Items().Count;
                    int _numOfItemNumbers = myOCvTesseract.GetList_ItemNumbers().Count;

                    if (_numOfItems == _numOfItemNumbers)
                    {
                        for (int i = 0; i < _numOfItems; i++)
                        {
                            string strItem = myOCvTesseract.GetList_Items()[i];
                            string strItemNumebr = myOCvTesseract.GetList_ItemNumbers()[i];

                            string cleanedString_item = Regex.Replace(strItem, @"\s+", " ");
                            string cleanedString_itemNumber = Regex.Replace(strItemNumebr, @"\s+", " ");

                            string _line = cleanedString_item + " | " + cleanedString_itemNumber;

                            string _thisItemNameInLower = cleanedString_item.ToLower();
                            if (_thisItemNameInLower.Contains("drawing") || _thisItemNameInLower.Contains("descrip"))
                            {
                                continue;
                            }
                            else
                            {
                                List_Itemnames.Add(cleanedString_item);
                                ListNumbers.Add(cleanedString_itemNumber);
                                EventsManagerLib.Call_LogConsole(_line);
                            }
                        }
                    }
                    else
                    {
                        //take the smallest count
                        int _minCount = Math.Min(_numOfItems, _numOfItemNumbers);

                        for (int i = 0; i < _minCount; i++)
                        {
                            string strItem = myOCvTesseract.GetList_Items()[i];
                            string strItemNumebr = myOCvTesseract.GetList_ItemNumbers()[i];

                            string cleanedString_item = Regex.Replace(strItem, @"\s+", " ");
                            string cleanedString_itemNumber = Regex.Replace(strItemNumebr, @"\s+", " ");

                            string _line = cleanedString_item + " | " + cleanedString_itemNumber;
                            string _thisItemNameInLower = cleanedString_item.ToLower();
                            if (_thisItemNameInLower.Contains("drawing") || _thisItemNameInLower.Contains("descrip"))
                            {
                                continue;
                            }
                            else
                            {
                                List_Itemnames.Add(cleanedString_item);
                                ListNumbers.Add(cleanedString_itemNumber);
                                EventsManagerLib.Call_LogConsole(_line);
                            }
                        }
                    }
                }
            }

            //for (int i = 0; i < Math.Min(List_Itemnames.Count, ListNumbers.Count); i++)
            int MAXCAP = 10;
            int smallest = Math.Min(List_Itemnames.Count, ListNumbers.Count);
            if (smallest == 0)
            {
                EventsManagerLib.Call_LogConsole("No items found");
                return;
            }
            if (smallest > MAXCAP)
            {
                EventsManagerLib.Call_LogConsole("Too many items found, only 10 will be displayed");
                smallest = MAXCAP;
            }
            for (int i = 0; i < MAXCAP; i++)
            {

                UC_NavButton navButton = new UC_NavButton
                {
                    ItemName = List_Itemnames[i],
                    ItemNumber = ListNumbers[i]
                };
                navButton.OnUCNavButtonClicked += NavButton_OnUCNavButtonClicked;

                // Add the UC_NavButton instance to the FlowLayoutPanel
                flowLayout_UCNAVBTNS.Controls.Add(navButton);
            }

        }


        public void SplitAndClean(string input, out string strLeft, out string strRight)
        {
            var parts = input.Split('-').Select(p => p.Trim()).ToArray();

            if (parts.Length >= 2)
            {
                strLeft = new string(parts[0].Where(char.IsDigit).ToArray());
                strRight = new string(parts[1].Where(char.IsDigit).ToArray());
            }
            else
            {
                strLeft = string.Empty;
                strRight = string.Empty;
            }
        }

        private void NavButton_OnUCNavButtonClicked(string itemNumber)
        {
            string strLeft = "";
            string strRight = "";
            SplitAndClean(itemNumber, out strLeft, out strRight);

            EventsManagerLib.Call_LogConsole("Item Number: " + itemNumber);
            EventsManagerLib.Call_LogConsole("Cleaned Left: " + strLeft);
            EventsManagerLib.Call_LogConsole("Cleaned Right: " + strRight);


            Search_token_2(strRight);
        }


        private void Btn_search_Click(object sender, EventArgs e)
        {
            PathsMatchedCount = 0;

            if (string.IsNullOrEmpty(_searchTocken))
            {
                EventsManagerLib.Call_LogConsole("Search token is empty");
                return;
            }
            EventsManagerLib.Call_LogConsole("Search token is : " + _searchTocken);

            _searchTocken = tb_search.Text;

            List<string> hits = PdfSysDiagnosticFinder.FindIfAnyLineInHardCodedFileContains(_searchTocken);
            PathsMatchedCount = hits.Count;
            EventsManagerLib.Call_LogConsole("found " + PathsMatchedCount + " hits from looking at U paths file ");

            cb_Hits.Items.Clear();

            if (hits.Count > 0)
            {
                foreach (var hit in hits)
                {
                    cb_Hits.Items.Add(hit);
                }
            }
            else
            {
                cb_Hits.Items.Add("No matches found.");
            }

            if (cb_Hits.Items.Count > 0)
            {
                cb_Hits.SelectedIndex = 0;
            }
            _MainPdfPath = cb_Hits.SelectedItem.ToString();
            EventsManagerLib.Call_LogConsole("main path selected " + _MainPdfPath);

            cb_Hits.SelectedIndexChanged += cb_Hits_SelectedIndexChanged;
        }

        void Search_token_2(string argToken)
        {
            _searchTocken_2 = argToken; // Move this line up here before any checks

            PathsMatchedCount_2 = 0;

            if (string.IsNullOrEmpty(_searchTocken_2))
            {
                EventsManagerLib.Call_LogConsole("Search token 2 is empty");
                return;
            }
            EventsManagerLib.Call_LogConsole("Search token is 2: " + _searchTocken_2);


            List<string> hits_2 = PdfSysDiagnosticFinder.FindIfAnyLineInHardCodedFileContains(_searchTocken_2);
            PathsMatchedCount_2 = hits_2.Count;
            EventsManagerLib.Call_LogConsole("found " + PathsMatchedCount + " hits from looking at U paths file ");

            cb_Hits_2.Items.Clear();

            if (hits_2.Count > 0)
            {
                foreach (var hit in hits_2)
                {
                    cb_Hits_2.Items.Add(hit);
                }
            }
            else
            {
                cb_Hits_2.Items.Add("No matches found.");
            }

            if (cb_Hits_2.Items.Count > 0)
            {
                cb_Hits_2.SelectedIndex = 0;
            }
            _MainPdfPath_2 = cb_Hits_2.SelectedItem.ToString();
            EventsManagerLib.Call_LogConsole("main path 2 selected " + _MainPdfPath_2);
            cb_Hits_2.SelectedIndexChanged += cb_Hits_2_SelectedIndexChanged;



        }

        private void cb_Hits_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Hits_2.SelectedItem != null)
            {
                _MainPdfPath_2 = cb_Hits_2.SelectedItem.ToString();
                EventsManagerLib.Call_LogConsole("main path 2 selected " + _MainPdfPath_2);
                FormPDFViewer pdfViewer = new FormPDFViewer();
                pdfViewer.LoadPDF(_MainPdfPath_2);
                pdfViewer.ShowDialog();
            }

        }
        private void cb_Hits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Hits.SelectedItem != null)
            {
                _MainPdfPath = cb_Hits.SelectedItem.ToString();
                EventsManagerLib.Call_LogConsole("main path selected " + _MainPdfPath);
            }
        }
        private void Tb_search_TextChanged(object sender, EventArgs e)
        {
            _searchTocken = tb_search.Text;
        }

        private void Btn_OpenDebugConsole_Click(object sender, EventArgs e)
        {
            if (debugForm == null || debugForm.IsDisposed)
            {
                if (debugForm != null)
                {
                    debugForm.FormClosed -= DebugForm_FormClosed;
                }
                debugForm = new DEBUGER_StandaloneForm();
                debugForm.FormClosed += DebugForm_FormClosed;
                debugForm.Show();
            }
            else
            {
                debugForm.BringToFront();
            }
        }

        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            debugForm = null; // Reset the reference to allow a new form to be opened later
        }
    }
}