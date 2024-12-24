using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OCV_LIB;
using OCV_LIB.UCFiles;
using OpenCvSharp;
using VCI_Forms_SPN._BackEndDataOBJs.DocReaders;
using VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using VCI_Forms_SPN._GLobalz;
namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class Form_ParamsExtractor : Form
    {
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
        public Form_ParamsExtractor()
        {
            InitializeComponent();
            this.Load += Form_ParamsExtractor_Load;
            buttonSearch.Click += buttonSearch_Click;
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
            EventsManagerLib.Call_LogConsole("opened pdf ");
        }
        private void ShowPDF(string _MainPdfPath)
        {
            if (_MainPdfPath == "")
            {
                EventsManagerLib.Call_LogConsole("Main pdf path is empty");
                return;
            }
            LargFullPDF = _mypdf_toInnerFramer.Convert_PDF_TO_MAT_zoomdi(_MainPdfPath, 4.0f, 400);
            pb_MAIN.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(LargFullPDF);
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
                        lbl_02.Text = _line;
                        
                    }
                    else
                    {
                        string cleanedString_title = Regex.Replace(SysDiagInfo_str, @"\s+", " ");
                        string _line = cleanedString_title;
                        lbl_02.Text = _line;
                        EventsManagerLib.Call_LogConsole("regx ed line title = " + _line);
                    }

                    EventsManagerLib.Call_LogConsole(SysDiagInfo_str);
                }

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
                int MAXCAP = 24;
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
                //append to lbl_02
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < smallest; i++)
                {
                    sb.AppendLine(List_Itemnames[i] + " | " + ListNumbers[i]);

                }
                lbl_02.Text += "\n";
                lbl_02.Text += sb.ToString();

            }
        }
        private void Form_ParamsExtractor_Load(object sender, EventArgs e)
        {
            checkedListBoxPaths.Items.Clear();
            var allPaths = FilePathIdentifyer.GetPaths();
            foreach (var path in allPaths)
            {
                checkedListBoxPaths.Items.Add(path, false);
            }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            flowLayoutPanelResults.Controls.Clear();
            var matchedFiles = PerformSearch(SearchBox.Text);
            int linkCount = 0;
            foreach (var filePath in matchedFiles)
            {
                if (linkCount >= 128)
                {
                    //MessageBox.Show("Reached the maximum limit of 128 results.");
                    break;
                }
                var extension = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();
                var fileName = Path.GetFileName(filePath);
                var fileLinkControl = new vcucFileLink
                {
                    FilePath = filePath,
                    FileName = fileName,
                    FileFormat = extension,
                    Certainty = 0.0
                };
                fileLinkControl.FileClicked += FileLinkControl_FileClicked;
                flowLayoutPanelResults.Controls.Add(fileLinkControl);
                linkCount++;
            }
        }

        private List<string> PerformSearch(string searchText)
        {
            var selectedPaths = new List<string>();
            bool selectAll = false;
            foreach (var item in checkedListBoxPaths.CheckedItems)
            {
                if (item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    selectAll = true;
                    break;
                }
                selectedPaths.Add(item.ToString());
            }
            if (selectAll)
            {
                selectedPaths.Clear();
                foreach (var item in checkedListBoxPaths.Items)
                {
                    var path = item.ToString();
                    if (!path.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        selectedPaths.Add(path);
                }
            }
            var matchedFiles = new List<string>();
            foreach (var dir in selectedPaths)
            {
                if (Directory.Exists(dir))
                {
                    var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                    foreach (var f in files)
                    {
                        if (f.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            if (ShouldInclude(f))
                            {
                                matchedFiles.Add(f);
                            }
                        }
                    }
                }
            }
            return matchedFiles;
        }
        private bool ShouldInclude(string filePath)
        {
            string fileName = Path.GetFileName(filePath).ToLowerInvariant();
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (fileName == "main.c")
            {
                string dir = Path.GetDirectoryName(filePath);
                string possibleHPath = Path.Combine(dir, "vci2.h");
                if (File.Exists(possibleHPath))
                {
                    return true;
                }
                return false;
            }
            if (fileName == "parameters.cpp")
            {
                return true;
            }
            if (extension == ".pdf")
            {
                return true;
            }
            return false;
        }
        private void FileLinkControl_FileClicked(object sender, string filePath)
        {
            lbl_path.Text = filePath;
            if (Path.GetExtension(filePath).ToLowerInvariant() == ".pdf")
            {
                ShowPDF(filePath);
            }
            var parsedParams = FirmwareParamExtractor.ExtractFirmwareParameters(filePath);
            if (parsedParams != null)
            {
                lbl_01.Text = BuildParamsDisplayString(parsedParams);
            }
            else
            {
                lbl_01.Text = "No parameters extracted from: " + filePath;
            }
        }
        private string BuildParamsDisplayString(FirmwareParameters fp)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"FileName: {fp.SourceFileName}");  // <-- Add this line
            sb.AppendLine($"Project: {fp.ProjectName}");
            sb.AppendLine($"DirName: {fp.FirmwareDirName}");
            sb.AppendLine($"Version: {fp.Version}");
            sb.AppendLine($"EepromStatus: {fp.EepromStatus}");
            sb.AppendLine($"SvnRevision: {fp.SvnRevision}");
            sb.AppendLine();
            sb.AppendLine($"NumberOfWaterJets: {fp.NumberOfWaterJets}");
            sb.AppendLine($"NumberOfBowThrusters: {fp.NumberOfBowThrusters}");
            sb.AppendLine($"NumberOfNozzles: {fp.NumberOfNozzles}");
            sb.AppendLine($"NumberOfBuckets: {fp.NumberOfBuckets}");
            sb.AppendLine($"NumberOfIntTabs: {fp.NumberOfIntTabs}");
            sb.AppendLine($"NumberOfStations: {fp.NumberOfStations}");
            sb.AppendLine($"NumberOfJoysticks: {fp.NumberOfJoysticks}");
            sb.AppendLine($"NumberOfLevers: {fp.NumberOfLevers}");
            sb.AppendLine($"NumberOfHelms: {fp.NumberOfHelms}");
            sb.AppendLine($"NumberOfTillers: {fp.NumberOfTillers}");
            sb.AppendLine($"NumberOfLCDs: {fp.NumberOfLCDs}");
            sb.AppendLine();
            sb.AppendLine($"HasIdleKnob: {fp.HasIdleKnob}");
            sb.AppendLine($"HasCAN: {fp.HasCAN}");
            sb.AppendLine($"HasRS232: {fp.HasRS232}");
            sb.AppendLine($"CalMode: {fp.CalMode}");
            return sb.ToString();
        }
    }
}