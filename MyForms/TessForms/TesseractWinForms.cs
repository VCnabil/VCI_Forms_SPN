using MetroFramework.Controls;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using VCI_Forms_SPN._BackEndDataOBJs.DocReaders;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN.MyForms.pdfFormSysdiag;

namespace VCI_Forms_SPN.MyForms.TessForms
{
    public partial class TesseractWinForms : Form
    {
        Dictionary<string, MetroTrackBar> AllTrackBArs;
        List<MetroTrackBar> All_TrackBars_forUI;
        List<MetroLabel> All_ValueLabels_forUI;
        List<MetroLabel> All_TackerNames_forUI;
        OCV_Filter_V2 oCV_Filter_V2;
        OCV_FilterActions actionFilter;

        int _CURSOR_row_ = 0;
        int _CURSOR_Col_ = 0;

        int _TrackBarSize_Width = 220;
        int _TrackBarSize_Height = 20;
        int _LabelSize_Width = 120;
        int _LabelSize_Height = 20;

        //PDF Region
        List<string> _list_projectnames;
        string _selectedProjectName = "NAB";
        string _pathToSelectedProject_SysDiag = "";
        PictureBox[] _picboxesArra;
        MyPDF_toInnerFrameMat _mypdf_toInnerFramer;
        MyOpenCV3 _myOCVmatTwiddler;

        //forpdf initia lload 
        int __factor = 10;
        int __widthneded = 170;
        int __heightneeded = 110;

        PictureBox SR_imag;
        public TesseractWinForms()
        {
            __factor = 10;
            __widthneded = 170 * __factor;
            __heightneeded = 110 * __factor;
            InitializeComponent();
            AllTrackBArs = new Dictionary<string, MetroTrackBar>();
            All_TrackBars_forUI = new List<MetroTrackBar>();
            All_ValueLabels_forUI = new List<MetroLabel>();
            All_TackerNames_forUI = new List<MetroLabel>();

            oCV_Filter_V2 = new OCV_Filter_V2();
            actionFilter = new OCV_FilterActions();
            _mypdf_toInnerFramer = new MyPDF_toInnerFrameMat();
            _myOCVmatTwiddler = new MyOpenCV3();

            DEBUGER_StandaloneForm theDebugger = new DEBUGER_StandaloneForm();
            theDebugger.Show();

            //comboBox_pdfFile
            _list_projectnames = ProjectName_ids.Keys.Skip(0).Take(75).ToList();
            comboBox_pdfFile.DataSource = _list_projectnames;
            comboBox_pdfFile.SelectedIndex = 1;
            _selectedProjectName = _list_projectnames[0];
            _pathToSelectedProject_SysDiag = Get_SysDiagStaticPath(_selectedProjectName);//grab the default path to the first project

            int imagecount = 2;
            _picboxesArra = new PictureBox[imagecount];
            int mage_Height = AdjustReolution(WP_imagebox_p7_p7.Height);
            int mage_Width = AdjustReolution(WP_imagebox_p7_p7.Width);
            flowLayoutPanel1.Width = mage_Width + 2;
            for (int x = 0; x < imagecount; x++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Width = mage_Width;
                pictureBox.Height = mage_Height;
                _picboxesArra[x] = pictureBox;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
            int flowlayoutHeigh = AdjustReolution(flowLayoutPanel1.Height);

            this.Width = AdjustReolution(1500) + flowLayoutPanel1.Width;
            int currentHeightofForm = AdjustReolution(this.Height);

            SR_imag = new PictureBox();
            SR_imag.SizeMode = PictureBoxSizeMode.Normal;
            SR_imag.Width = AdjustReolution(G_Strip_R_width);
            SR_imag.Height = AdjustReolution(G_Strip_R_height);
            SR_imag.Location = new System.Drawing.Point(10, currentHeightofForm - SR_imag.Height - 10);
            this.Controls.Add(SR_imag);
            SR_imag.BringToFront();
            if (currentHeightofForm < (SR_imag.Height + flowlayoutHeigh + 10))
            {
                this.Height = SR_imag.Height + flowlayoutHeigh + 10;
            }
            else
            {
                this.Height = currentHeightofForm;
            }

            Run_selecetPdf();
            comboBox_pdfFile.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;


            Init_UI_DataValues();
        }
        void Init_UI_DataValues()
        {

            trkbar_pdf_0_GrayThr.Minimum = 0;
            trkbar_pdf_0_GrayThr.Maximum = 255;
            trkbar_pdf_0_GrayThr.Value = G_Pdf_0_GrayThr;
            trkbar_pdf_0_GrayThr.SmallChange = 1;
            trkbar_pdf_0_GrayThr.LargeChange = 10;
            trkbar_pdf_0_GrayThr.ValueChanged += SomeUIelementValueChanged;
            mlbv_pdf_0_GrayThr.Text = G_Pdf_0_GrayThr.ToString();

            trkbar_pdf_1_GrayMaxVal.Minimum = 0;
            trkbar_pdf_1_GrayMaxVal.Maximum = 255;
            trkbar_pdf_1_GrayMaxVal.Value = G_Pdf_1_GrayMaxVal;
            trkbar_pdf_1_GrayMaxVal.SmallChange = 1;
            trkbar_pdf_1_GrayMaxVal.LargeChange = 10;
            trkbar_pdf_1_GrayMaxVal.ValueChanged += SomeUIelementValueChanged;
            mlbv_pdf_0_GrayThr.Text = G_Pdf_1_GrayMaxVal.ToString();

            cb_pdf_threshbordertype.DataSource = Enum.GetValues(typeof(OpenCvSharp.ThresholdTypes));
            cb_pdf_threshbordertype.SelectedIndex = 1;
            cb_pdf_threshbordertype.SelectedIndexChanged += SomeUIelementValueChanged;

            comboBox_blurToUse.DataSource = Enum.GetValues(typeof(enum_OCVBlurAction));
            comboBox_blurToUse.SelectedIndex = 1;
            comboBox_blurToUse.SelectedIndexChanged += SomeUIelementValueChanged;

            comboBox_blurBoarderType.DataSource = Enum.GetValues(typeof(OpenCvSharp.BorderTypes));
            comboBox_blurBoarderType.SelectedIndex = 0;
            comboBox_blurBoarderType.SelectedIndexChanged += SomeUIelementValueChanged;

            trkbar_blurSigmax.Minimum = 100;
            trkbar_blurSigmax.Maximum = 5000;
            trkbar_blurSigmax.Value = (int)(G_BlurSigmax * 1000);
            trkbar_blurSigmax.SmallChange = 100;
            trkbar_blurSigmax.LargeChange = 500;
            trkbar_blurSigmax.ValueChanged += SomeUIelementValueChanged;
            mlbv_blurSigmax.Text = ((double)trkbar_blurSigmax.Value / 1000.00).ToString();

            trkbar_blurSigmay.Minimum = 100;
            trkbar_blurSigmay.Maximum = 5000;
            trkbar_blurSigmay.Value = (int)(G_BlurSigmay * 1000);
            trkbar_blurSigmay.SmallChange = 100;
            trkbar_blurSigmay.LargeChange = 500;
            trkbar_blurSigmay.ValueChanged += SomeUIelementValueChanged;
            mlbv_blurSigmay.Text = ((double)trkbar_blurSigmay.Value / 1000.00).ToString();

            comboBox_ThresholdTypeToUse.DataSource = Enum.GetValues(typeof(enum_OCVThresholdAction));
            comboBox_ThresholdTypeToUse.SelectedIndex = 1;
            comboBox_ThresholdTypeToUse.SelectedIndexChanged += SomeUIelementValueChanged;

            //comboBox_StdThres_tt.DataSource = Enum.GetValues(typeof(OpenCvSharp.ThresholdTypes));
            //comboBox_StdThres_tt.SelectedIndex = 0;
            //comboBox_StdThres_tt.SelectedIndexChanged += SomeUIelementValueChanged;

            //only take the first 2 elements of the enum
            comboBox_StdThres_tt.DataSource = Enum.GetValues(typeof(OpenCvSharp.ThresholdTypes));
            comboBox_StdThres_tt.SelectedIndex = 0;
            comboBox_StdThres_tt.SelectedIndexChanged += SomeUIelementValueChanged;



            comboBox_AdaptiveThresh_tt.DataSource = Enum.GetValues(typeof(OpenCvSharp.ThresholdTypes)).Cast<OpenCvSharp.ThresholdTypes>().Take(2).ToList();
            comboBox_AdaptiveThresh_tt.SelectedIndex = 0;
            comboBox_AdaptiveThresh_tt.SelectedIndexChanged += SomeUIelementValueChanged;

            comboBox_AdaptiveThresh_tt2.DataSource = Enum.GetValues(typeof(OpenCvSharp.ThresholdTypes));
            comboBox_AdaptiveThresh_tt2.SelectedIndex = 0;
            comboBox_AdaptiveThresh_tt2.SelectedIndexChanged += SomeUIelementValueChanged;


            comboBox_AdaptiveThresh_at.DataSource = Enum.GetValues(typeof(OpenCvSharp.AdaptiveThresholdTypes));
            comboBox_AdaptiveThresh_at.SelectedIndex = 1;
            comboBox_AdaptiveThresh_at.SelectedIndexChanged += SomeUIelementValueChanged;

            trkbar_StdThreshThr.Minimum = 0;
            trkbar_StdThreshThr.Maximum = 255;
            trkbar_StdThreshThr.Value = (int)G_StdThreshThr;
            trkbar_StdThreshThr.SmallChange = 1;
            trkbar_StdThreshThr.LargeChange = 10;
            trkbar_StdThreshThr.ValueChanged += SomeUIelementValueChanged;
            mlbv_StdThreshThr.Text = G_StdThreshThr.ToString();


            trkbar_StdThreshMaxV.Minimum = 0;
            trkbar_StdThreshMaxV.Maximum = 255;
            trkbar_StdThreshMaxV.Value = (int)G_StdThreshMaxV;
            trkbar_StdThreshMaxV.SmallChange = 1;
            trkbar_StdThreshMaxV.LargeChange = 5;
            trkbar_StdThreshMaxV.ValueChanged += SomeUIelementValueChanged;
            mlbv_StdThreshMaxV.Text = G_StdThreshMaxV.ToString();

            trkbar_AdaptiveThreshC.Minimum = -5000;
            trkbar_AdaptiveThreshC.Maximum = 5000;
            trkbar_AdaptiveThreshC.Value = (int)(G_AdaptiveThreshC * 1000);
            trkbar_AdaptiveThreshC.SmallChange = 100;
            trkbar_AdaptiveThreshC.LargeChange = 500;
            trkbar_AdaptiveThreshC.ValueChanged += SomeUIelementValueChanged;
            mlbv_AdaptiveThreshC.Text = ((double)trkbar_AdaptiveThreshC.Value / 1000.00).ToString();

            trkbar_CannyThr1.Minimum = 0;
            trkbar_CannyThr1.Maximum = 255;
            trkbar_CannyThr1.Value = (int)G_CannyThr1;
            trkbar_CannyThr1.SmallChange = 1;
            trkbar_CannyThr1.LargeChange = 5;
            trkbar_CannyThr1.ValueChanged += SomeUIelementValueChanged;
            mlbv_CannyThr1.Text = G_CannyThr1.ToString();

            trkbar_CannyThr2.Minimum = 0;
            trkbar_CannyThr2.Maximum = 255;
            trkbar_CannyThr2.Value = (int)G_CannyThr2;
            trkbar_CannyThr2.SmallChange = 1;
            trkbar_CannyThr2.LargeChange = 5;
            trkbar_CannyThr2.ValueChanged += SomeUIelementValueChanged;
            mlbv_CannyThr2.Text = G_CannyThr2.ToString();

            trkbar_LinesP_thetaMultiplyer.Minimum = 1;
            trkbar_LinesP_thetaMultiplyer.Maximum = 180;
            trkbar_LinesP_thetaMultiplyer.Value = G_LinesP_thetaMultiplyer;
            trkbar_LinesP_thetaMultiplyer.SmallChange = 1;
            trkbar_LinesP_thetaMultiplyer.LargeChange = 1;
            trkbar_LinesP_thetaMultiplyer.ValueChanged += SomeUIelementValueChanged;
            mlbv_LinesP_thetaMultiplyer.Text = G_LinesP_thetaMultiplyer.ToString();

            tb_Xvalue.Minimum = 0;
            tb_Xvalue.Maximum = 900;
            tb_Xvalue.Value = 300;
            tb_Xvalue.SmallChange = 1;
            tb_Xvalue.LargeChange = 10;
            tb_Xvalue.ValueChanged += SomeUIelementValueChanged;
            tb_Yvalue.Minimum = 0;
            tb_Yvalue.Maximum = 900;
            tb_Yvalue.Value = 900; //****************************************************************** the value needs to be calculated! it should be the max - value 
            tb_Yvalue.SmallChange = 1;
            tb_Yvalue.LargeChange = 10;
            tb_Yvalue.ValueChanged += SomeUIelementValueChanged;


            trkbar_LinesP_rho.Minimum = 0;
            trkbar_LinesP_rho.Maximum = 5000;
            trkbar_LinesP_rho.Value = (int)(G_LinesP_rho * 1000);
            trkbar_LinesP_rho.SmallChange = 100;
            trkbar_LinesP_rho.LargeChange = 500;
            trkbar_LinesP_rho.ValueChanged += SomeUIelementValueChanged;
            mlbv_LinesP_rho.Text = ((double)trkbar_LinesP_rho.Value / 1000.00).ToString();
            trkbar_LinesP_Thr.Minimum = 0;
            trkbar_LinesP_Thr.Maximum = 255;
            trkbar_LinesP_Thr.Value = G_LinesP_Thr;
            trkbar_LinesP_Thr.SmallChange = 1;
            trkbar_LinesP_Thr.LargeChange = 1;
            mblv_LinesP_Thr.Text = trkbar_LinesP_Thr.Value.ToString();
            trkbar_LinesP_Thr.ValueChanged += SomeUIelementValueChanged;

            tb_LinesP_MaxDx.Text = G_LinesP_MaxDx.ToString();
            tb_LinesP_MaxDx.TextChanged += SomeUIelementValueChanged;
            tb_LinesP_MaxDy.Text = G_LinesP_MaxDy.ToString();
            tb_LinesP_MaxDy.TextChanged += SomeUIelementValueChanged;

            tb_Ksize_3_5_7_15.Text = G_Blur_ksize.ToString();
            tb_Ksize_3_5_7_15.TextChanged += SomeUIelementValueChanged;
            tb_daptiveThreshBlockSize.Text = G_AdaptiveThreshBlockSize.ToString();
            tb_daptiveThreshBlockSize.TextChanged += SomeUIelementValueChanged;
            //tb_LinesP_Thr.Text = G_LinesP_Thr.ToString();
            //tb_LinesP_Thr.TextChanged += SomeUIelementValueChanged;
            tb__LinesP_YGap.Text = G_LinesP_YGap.ToString();
            tb__LinesP_YGap.TextChanged += SomeUIelementValueChanged;
            tb__LinesP_XGap.Text = G_LinesP_XGap.ToString();
            tb__LinesP_XGap.TextChanged += SomeUIelementValueChanged;


            mtkb_sL_minLen.Value = (int)G_LinesP_L_minLen;
            mtkb_sL_mxgap.Value = (int)G_LinesP_L_maxGap;
            mtkb_sL_height.Value = (int)G_Strip_L_height;
            mtkb_sL_width.Value = (int)G_Strip_L_width;
            mtkb_sL_top.Value = (int)G_Strip_L_TopLimit;
            mtkb_sL_margin.Value = (int)G_Strip_L_Margin;

            mtkb_sR_minLen.Value = (int)G_LinesP_R_minLen;
            mtkb_sR_mxgap.Value = (int)G_LinesP_R_maxGap;
            mtkb_sR_height.Value = (int)G_Strip_R_height;
            mtkb_sR_width.Value = (int)G_Strip_R_width;
            mtkb_sR_top.Value = (int)G_Strip_R_botMargin;
            mtkb_sR_margin.Value = (int)G_Strip_R_Margin;

            mtkb_sV_minLen.Value = (int)G_LinesP_V_minLen;
            mtkb_sV_mxgap.Value = (int)G_LinesP_V_maxGap;
            mtkb_sV_height.Value = (int)G_Strip_V_height;
            mtkb_sV_width.Value = (int)G_Strip_V_width;
            mtkb_sV_top.Value = (int)G_Strip_V_TopLimit;
            mtkb_sV_margin.Value = (int)G_Strip_V_Margin;

            trkbar_LinesP_PercentWidth.Minimum = 1;
            trkbar_LinesP_PercentWidth.Maximum = 100;
            trkbar_LinesP_PercentWidth.Value = (int)G_LinesP_PercentWidth;
            mlbv_LinesP_PercentWidth.Text = trkbar_LinesP_PercentWidth.Value.ToString();
            trkbar_LinesP_PercentWidth.ValueChanged += SomeUIelementValueChanged;

            trkbar_LinesP_PercentHeight.Minimum = 1;
            trkbar_LinesP_PercentHeight.Maximum = 100;
            trkbar_LinesP_PercentHeight.Value = (int)G_LinesP_PercentHeight;
            mlbv_LinesP_PercentHeight.Text = trkbar_LinesP_PercentHeight.Value.ToString();
            trkbar_LinesP_PercentHeight.ValueChanged += SomeUIelementValueChanged;

            mtkb_sL_minLen.ValueChanged += SomeUIelementValueChanged;
            mtkb_sL_mxgap.ValueChanged += SomeUIelementValueChanged;
            mtkb_sL_height.ValueChanged += SomeUIelementValueChanged;
            mtkb_sL_width.ValueChanged += SomeUIelementValueChanged;
            mtkb_sL_top.ValueChanged += SomeUIelementValueChanged;
            mtkb_sL_margin.ValueChanged += SomeUIelementValueChanged;

            mtkb_sR_minLen.ValueChanged += SomeUIelementValueChanged;
            mtkb_sR_mxgap.ValueChanged += SomeUIelementValueChanged;
            mtkb_sR_height.ValueChanged += SomeUIelementValueChanged;
            mtkb_sR_width.ValueChanged += SomeUIelementValueChanged;
            mtkb_sR_top.ValueChanged += SomeUIelementValueChanged;
            mtkb_sR_margin.ValueChanged += SomeUIelementValueChanged;

            mtkb_sV_minLen.ValueChanged += SomeUIelementValueChanged;
            mtkb_sV_mxgap.ValueChanged += SomeUIelementValueChanged;
            mtkb_sV_height.ValueChanged += SomeUIelementValueChanged;
            mtkb_sV_width.ValueChanged += SomeUIelementValueChanged;
            mtkb_sV_top.ValueChanged += SomeUIelementValueChanged;
            mtkb_sV_margin.ValueChanged += SomeUIelementValueChanged;

            cb_useOld.CheckedChanged += ON_checkbox_useOld_checkChanged;
        }

        void Update__All_Data_From_UI()
        {
            oCV_Filter_V2.Pdf_0_GrayThr = trkbar_pdf_0_GrayThr.Value;
            oCV_Filter_V2.Pdf_1_GrayMaxVal = trkbar_pdf_1_GrayMaxVal.Value;

            string _pdf_tt = cb_pdf_threshbordertype.SelectedItem.ToString();
            oCV_Filter_V2.Pdf_tt = StringToEnum_ThresholdTypes(_pdf_tt);

            oCV_Filter_V2.Blur_ActionToUse = (enum_OCVBlurAction)comboBox_blurToUse.SelectedIndex;

            string _blurBoarderType = comboBox_blurBoarderType.SelectedItem.ToString();
            oCV_Filter_V2.BbLurBorderType = StringToEnum_BorderTypes(_blurBoarderType);
            // oCV_Filter_V2.BbLurBorderType = (OpenCvSharp.BorderTypes)comboBox_blurBoarderType.SelectedIndex;
            int _ksize = 5;
            //if the text is noto a number then set it to 5
            if (!int.TryParse(tb_Ksize_3_5_7_15.Text, out _ksize))
            {
                _ksize = 5;
            }

            //if the value is not odd make it odd

            if (_ksize % 2 == 0)
            {
                _ksize++;
            }
            if (_ksize < 3)
            {
                _ksize = 3;
            }
            else if (_ksize > 21)
            {
                _ksize = 21;
            }



            oCV_Filter_V2.Blur_ksize = _ksize;
            lbl6_pdfksize.Text = "Ks " + _ksize.ToString();

            double _sigmax = (double)trkbar_blurSigmax.Value / 1000.00;
            double _sigmay = (double)trkbar_blurSigmay.Value / 1000.00;
            oCV_Filter_V2.BlurSigmax = _sigmax;
            oCV_Filter_V2.BlurSigmay = _sigmay;
            oCV_Filter_V2.ThresholdActionToUse = (enum_OCVThresholdAction)comboBox_ThresholdTypeToUse.SelectedIndex;

            string _stdThres_tt = comboBox_StdThres_tt.SelectedItem.ToString();
            ThresholdTypes selected_tt = StringToEnum_ThresholdTypes(_stdThres_tt);
            oCV_Filter_V2.StdThres_tt = selected_tt;

            string _adaptiveThresh_tt = comboBox_AdaptiveThresh_tt.SelectedItem.ToString();
            ThresholdTypes selected_adaptive_tt = StringToEnum_ThresholdTypes(_adaptiveThresh_tt);
            oCV_Filter_V2.AdaptiveThresh_tt = selected_adaptive_tt;


            string _adaptiveThresh_tt2 = comboBox_AdaptiveThresh_tt2.SelectedItem.ToString();
            ThresholdTypes selected_adaptive_tt2 = StringToEnum_ThresholdTypes(_adaptiveThresh_tt2);
            oCV_Filter_V2.AdaptiveThresh_tt2 = selected_adaptive_tt2;


            oCV_Filter_V2.AdaptiveThresh_at = (OpenCvSharp.AdaptiveThresholdTypes)comboBox_AdaptiveThresh_at.SelectedIndex;
            oCV_Filter_V2.StdThreshThr = trkbar_StdThreshThr.Value;
            oCV_Filter_V2.StdThreshMaxv = trkbar_StdThreshMaxV.Value;
            int _bsize = 5;
            //if the text is noto a number then set it to 5
            if (!int.TryParse(tb_daptiveThreshBlockSize.Text, out _bsize))
            {
                _bsize = 5;
            }
            if (_bsize % 2 == 0)
            {
                _bsize++;
            }
            if (_bsize < 3)
            {
                _bsize = 3;
            }
            else if (_bsize > 63)
            {
                _bsize = 63;
            }
            oCV_Filter_V2.AdaptiveThreshBlockSize = _bsize;
            lbl6_bsize.Text = "Bs " + _bsize.ToString();
            double _c = (double)trkbar_AdaptiveThreshC.Value / 1000.00;
            oCV_Filter_V2.AdaptiveThreshC = _c;
            oCV_Filter_V2.CannyThr1 = trkbar_CannyThr1.Value;
            oCV_Filter_V2.CannyThr2 = trkbar_CannyThr2.Value;
            oCV_Filter_V2.LinesP_thetaMultiplyer = trkbar_LinesP_thetaMultiplyer.Value;
            double _rho = (double)trkbar_LinesP_rho.Value / 1000.00;
            oCV_Filter_V2.LinesP_rho = _rho;
            mlbv_LinesP_rho.Text = _rho.ToString();
            //******************************************************************** THRESH Ygaps for list clean and Xgap for plines
            //int _lineP_thr = G_LinesP_Thr;
            //if (!int.TryParse(tb_LinesP_Thr.Text, out _lineP_thr))
            //{
            //    _lineP_thr = G_LinesP_Thr;
            //}
            oCV_Filter_V2.LinesP_Thr = trkbar_LinesP_Thr.Value;
            mblv_LinesP_Thr.Text = trkbar_LinesP_Thr.Value.ToString();

            double _lineP_MaxDY = G_LinesP_MaxDy;
            if (!double.TryParse(tb_LinesP_MaxDy.Text, out _lineP_MaxDY))
            {
                _lineP_MaxDY = G_LinesP_MaxDy;
            }
            oCV_Filter_V2.LinesP_MaxDy = _lineP_MaxDY;
            lbl_LinesP_MaxDy.Text = "Dy " + _lineP_MaxDY.ToString();

            double _lineP_MaxDX = G_LinesP_MaxDx;
            if (!double.TryParse(tb_LinesP_MaxDx.Text, out _lineP_MaxDX))
            {
                _lineP_MaxDX = G_LinesP_MaxDx;
            }
            oCV_Filter_V2.LinesP_MaxDx = _lineP_MaxDX;
            lbl_LinesP_MaxDx.Text = "Dx " + _lineP_MaxDX.ToString();

            double _lineP_YGap = G_LinesP_YGap;
            if (!double.TryParse(tb__LinesP_YGap.Text, out _lineP_YGap))
            {
                _lineP_YGap = G_LinesP_YGap;
            }
            oCV_Filter_V2.LinesP_YGap = _lineP_YGap;
            double _lineP_XGap = G_LinesP_XGap;
            if (!double.TryParse(tb__LinesP_XGap.Text, out _lineP_XGap))
            {
                _lineP_XGap = G_LinesP_XGap;
            }
            oCV_Filter_V2.LinesP_XGap = _lineP_XGap;

            oCV_Filter_V2.LinesP_PercentWidth = trkbar_LinesP_PercentWidth.Value;
            mlbv_LinesP_PercentWidth.Text = trkbar_LinesP_PercentWidth.Value.ToString();

            oCV_Filter_V2.LinesP_PercentHeight = trkbar_LinesP_PercentHeight.Value;
            mlbv_LinesP_PercentHeight.Text = trkbar_LinesP_PercentHeight.Value.ToString();
            //******************************************************************** StripDefinitions  L
            oCV_Filter_V2.LinesP_L_minLen = mtkb_sL_minLen.Value;
            oCV_Filter_V2.LinesP_L_maxGap = mtkb_sL_mxgap.Value;
            oCV_Filter_V2.Strip_L_height = mtkb_sL_height.Value;
            oCV_Filter_V2.Strip_L_width = mtkb_sL_width.Value;
            oCV_Filter_V2.Strip_L_TopLimit = mtkb_sL_top.Value;
            oCV_Filter_V2.Strip_L_Margin = mtkb_sL_margin.Value;
            mlbl_sL_minLen.Text = mtkb_sL_minLen.Value.ToString();
            mlbl_sL_mxgap.Text = mtkb_sL_mxgap.Value.ToString();
            mlbl_sL_height.Text = mtkb_sL_height.Value.ToString();
            mlbl_sL_width.Text = mtkb_sL_width.Value.ToString();
            mlbl_sL_top.Text = mtkb_sL_top.Value.ToString();
            mlbl_sL_margin.Text = mtkb_sL_margin.Value.ToString();
            //******************************************************************** StripDefinitions  R
            oCV_Filter_V2.Strip_R_minLen = mtkb_sR_minLen.Value;
            oCV_Filter_V2.Strip_R_maxGap = mtkb_sR_mxgap.Value;
            oCV_Filter_V2.Strip_R_height = mtkb_sR_height.Value;
            oCV_Filter_V2.Strip_R_width = mtkb_sR_width.Value;
            oCV_Filter_V2.Strip_R_botMargin = mtkb_sR_top.Value;
            oCV_Filter_V2.Strip_R_Margin = mtkb_sR_margin.Value;
            mlbl_sR_minLen.Text = mtkb_sR_minLen.Value.ToString();
            mlbl_sR_mxgap.Text = mtkb_sR_mxgap.Value.ToString();
            mlbl_sR_height.Text = mtkb_sR_height.Value.ToString();
            mlbl_sR_width.Text = mtkb_sR_width.Value.ToString();
            mlbl_sR_top.Text = mtkb_sR_top.Value.ToString();
            mlbl_sR_margin.Text = mtkb_sR_margin.Value.ToString();
            //******************************************************************** StripDefinitions  V
            oCV_Filter_V2.LinesP_V_minLen = mtkb_sV_minLen.Value;
            oCV_Filter_V2.LinesP_V_maxGap = mtkb_sV_mxgap.Value;
            oCV_Filter_V2.Strip_V_height = mtkb_sV_height.Value;
            oCV_Filter_V2.Strip_V_width = mtkb_sV_width.Value;
            oCV_Filter_V2.Strip_V_TopLimit = mtkb_sV_top.Value;
            oCV_Filter_V2.Strip_V_Margin = mtkb_sV_margin.Value;
            mlbl_sV_minLen.Text = mtkb_sV_minLen.Value.ToString();
            mlbl_sV_mxgap.Text = mtkb_sV_mxgap.Value.ToString();
            mlbl_sV_height.Text = mtkb_sV_height.Value.ToString();
            mlbl_sV_width.Text = mtkb_sV_width.Value.ToString();
            mlbl_sV_top.Text = mtkb_sV_top.Value.ToString();
            mlbl_sV_margin.Text = mtkb_sV_margin.Value.ToString();
            //******************************************************************** temp x and y 
            mlbv_pdf_0_GrayThr.Text = trkbar_pdf_0_GrayThr.Value.ToString();
            mlbv_pdf_1_GrayMaxVal.Text = trkbar_pdf_1_GrayMaxVal.Value.ToString();
            mlbv_blurSigmax.Text = ((double)trkbar_blurSigmax.Value / 1000.00).ToString();
            mlbv_blurSigmay.Text = ((double)trkbar_blurSigmay.Value / 1000.00).ToString();
            mlbv_StdThreshThr.Text = trkbar_StdThreshThr.Value.ToString();
            mlbv_StdThreshMaxV.Text = trkbar_StdThreshMaxV.Value.ToString();
            mlbv_CannyThr1.Text = trkbar_CannyThr1.Value.ToString();
            mlbv_CannyThr2.Text = trkbar_CannyThr2.Value.ToString();
            mlbv_LinesP_thetaMultiplyer.Text = trkbar_LinesP_thetaMultiplyer.Value.ToString();
            mlbv_AdaptiveThreshC.Text = ((double)trkbar_AdaptiveThreshC.Value / 1000.00).ToString();
            int _temp_x = tb_Xvalue.Value;
            lbl_Xvalue.Text = _temp_x.ToString();
            oCV_Filter_V2.TempIntX_0_900 = _temp_x;
            int _temp_y = tb_Yvalue.Maximum - tb_Yvalue.Value;
            lbl_Yvalue.Text = _temp_y.ToString();
            oCV_Filter_V2.TempIntY_0_900 = _temp_y;
        }

        private void SomeUIelementValueChanged(object sender, EventArgs e)
        {
            Update__All_Data_From_UI();
            EventsManagerLib.Call_LogConsole("Factor= " + __factor + "  widthneded : " + __widthneded + " heightneeded : " + __heightneeded);
            // oCV_Filter_V2.PrintValues();
            EventsManagerLib.Call_Broacdast_OCV_OBJ(oCV_Filter_V2);
            Run_selecetPdf();
        }

        void Run_selecetPdf()
        {
            EventsManagerLib.Call_LogConsoleCLEAR();

            _selectedProjectName = comboBox_pdfFile.SelectedItem.ToString();
            _pathToSelectedProject_SysDiag = Get_SysDiagStaticPath(_selectedProjectName);


            _picboxesArra[0].Image = _myOCVmatTwiddler.GET_ImageFromAMat(
                _mypdf_toInnerFramer.GET_0_formated_MatOf_pdf(
                    _pathToSelectedProject_SysDiag, true, oCV_Filter_V2)
                );

            _picboxesArra[1].Image = _myOCVmatTwiddler.GET_ImageFromAMat();
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Run_selecetPdf();
        }



        private void ON_checkbox_useOld_checkChanged(object sender, EventArgs e)
        {
            EventsManagerLib.Call_UpdatActionFilte(actionFilter);
            Run_selecetPdf();
        }

    }
}
