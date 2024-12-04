using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCI_Forms_SPN._GLobalz;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
namespace VCI_Forms_SPN._BackEndDataOBJs.OCVObjs
{
    //internal class OCV_filterObj
    //{
    //}
    public class OCV_Filter_V2
    {
        private int _pdfwidthNeeded;
        private int _pdfheightNeeded;
        private int _pdfDPI_needed;
        private int _pdf_0_GrayThr;
        private int _pdf_1_GrayMaxVal;
        private ThresholdTypes _pdf_tt;
        private enum_OCVBlurAction _blur_ActionToUse;
        private BorderTypes _bbLurBorderType;
        private int _blur_ksize;
        private double _blurSigmax;
        private double _blurSigmay;
        private enum_OCVThresholdAction _thresholdActionToUse;
        private double _stdThreshThr;
        private double _stdThreshMaxv;
        private ThresholdTypes _stdThres_tt;
        private AdaptiveThresholdTypes _adaptiveThresh_at;
        private ThresholdTypes _adaptiveThresh_tt;
        private ThresholdTypes _adaptiveThresh_tt2;
        private int _curAdaptiveThreshBlockSize;
        private double _curAdaptiveThreshC;
        private double _adp0;
        private double _adp1;
        private int _adp2;
        private int _adp3;
        private double _CannyThr1;
        private double _CannyThr2;
        private double _LinesP_rho;
        private int _LinesP_thetaMultiplyer;
        private int _LinesP_Thr;
        private double _LinesP_YGap;
        private double _LinesP_XGap;
        private double _LinesP_MaxDy;
        private double _LinesP_MaxDx;
        private double _LinesP_R_minLen;
        private double _LinesP_R_maxGap;
        private int _strip_R_width;
        private int _strip_R_Margin;
        private int _strip_R_hright;
        private int _strip_R_botMargin;
        private double _LinesP_L_minLen;
        private double _LinesP_L_maxGap;
        private int _strip_L_width;
        private int _strip_L_Margin;
        private int _strip_L_hright;
        private int _strip_L_Top_limit;
        private double _LinesP_V_minLen;
        private double _LinesP_V_maxGap;
        private int _strip_V_width;
        private int _strip_V_Margin;
        private int _strip_V_hright;
        private int _strip_V_Top_limit;
        private int _tempIntX_0_900;
        private int _tempIntY_0_900;
        private int _pdfPage_width;
        private int _pdfPage_height;
        private double _LinesP_PercentWidth;
        private double _LinesP_PercentHeight;
        public enum_OCVBlurAction Blur_ActionToUse
        {
            get => _blur_ActionToUse;
            set
            {
                if ((int)value < 0)
                {
                    value = enum_OCVBlurAction.GAUSSIAN;
                }
                if ((int)value > 4)
                {
                    value = enum_OCVBlurAction.PYRUP;
                }
                _blur_ActionToUse = value;
            }
        }
        public enum_OCVThresholdAction ThresholdActionToUse
        {
            get => _thresholdActionToUse;
            set
            {
                if ((int)value < 0)
                {
                    value = enum_OCVThresholdAction.STD;
                }
                if ((int)value > 1)
                {
                    value = enum_OCVThresholdAction.ADAPTIVE;
                }
                _thresholdActionToUse = value;
            }
        }
        public int actualPdfPage_width
        {
            get => _pdfPage_width;
            set
            {
                if (value < 0)
                {
                    _pdfPage_width = 0;
                }
                else
                {
                    _pdfPage_width = value;
                }
            }
        }
        public int actualPdfPage_height
        {
            get => _pdfPage_height;
            set
            {
                if (value < 0)
                {
                    _pdfPage_height = 0;
                }
                else
                {
                    _pdfPage_height = value;
                }
            }
        }
        public int Pdf_0_GrayThr
        {
            get => _pdf_0_GrayThr;
            set
            {
                if (value > 255)
                {
                    _pdf_0_GrayThr = 255;
                }
                else if (value < 0)
                {
                    _pdf_0_GrayThr = 0;
                }
                else
                {
                    _pdf_0_GrayThr = value;
                }
            }
        }
        public int Pdf_1_GrayMaxVal
        {
            get => _pdf_1_GrayMaxVal;
            set
            {
                if (value > 255)
                {
                    _pdf_1_GrayMaxVal = 255;
                }
                else if (value < 0)
                {
                    _pdf_1_GrayMaxVal = 0;
                }
                else
                {
                    _pdf_1_GrayMaxVal = value;
                }
            }
        }
        public ThresholdTypes Pdf_tt
        {
            get => _pdf_tt;
            set
            {
                _pdf_tt = value;
            }
        }
        public BorderTypes BbLurBorderType
        {
            get => _bbLurBorderType;
            set
            {
                _bbLurBorderType = value;
            }
        }
        public int Blur_ksize
        {
            get => _blur_ksize;
            set
            {
                if (value % 2 == 0)
                {
                    value += 1;
                }
                if (value < 3)
                {
                    _blur_ksize = 3;
                }
                else if (value > 21)
                {
                    _blur_ksize = 21;
                }
                else
                {
                    _blur_ksize = value;
                }
            }
        }
        public double BlurSigmax
        {
            get => _blurSigmax;
            set
            {
                if (value > 15)
                {
                    _blurSigmax = 15;
                }
                else if (value < 1)
                {
                    _blurSigmax = 1;
                }
                else
                {
                    _blurSigmax = value;
                }
            }
        }
        public double BlurSigmay
        {
            get => _blurSigmay;
            set
            {
                if (value > 15)
                {
                    _blurSigmay = 15;
                }
                else if (value < 1)
                {
                    _blurSigmay = 1;
                }
                else
                {
                    _blurSigmay = value;
                }
            }
        }
        public double StdThreshThr
        {
            get => _stdThreshThr;
            set
            {
                if (value > 255)
                {
                    _stdThreshThr = 255;
                }
                else if (value < 0)
                {
                    _stdThreshThr = 0;
                }
                else
                {
                    _stdThreshThr = value;
                }
            }
        }
        public double StdThreshMaxv
        {
            get => _stdThreshMaxv;
            set
            {
                if (value > 255)
                {
                    _stdThreshMaxv = 255;
                }
                else if (value < 0)
                {
                    _stdThreshMaxv = 0;
                }
                else
                {
                    _stdThreshMaxv = value;
                }
            }
        }
        public ThresholdTypes StdThres_tt
        {
            get => _stdThres_tt;
            set
            {
                _stdThres_tt = value;
            }
        }
        public AdaptiveThresholdTypes AdaptiveThresh_at
        {
            get => _adaptiveThresh_at;
            set
            {
                if ((int)value < 0)
                {
                    value = AdaptiveThresholdTypes.MeanC;
                }
                if ((int)value > 1)
                {
                    value = AdaptiveThresholdTypes.GaussianC;
                }
                _adaptiveThresh_at = value;
            }
        }
        public ThresholdTypes AdaptiveThresh_tt
        {
            get => _adaptiveThresh_tt;
            set
            {
                _adaptiveThresh_tt = value;
            }
        }
        public ThresholdTypes AdaptiveThresh_tt2
        {
            get => _adaptiveThresh_tt2;
            set
            {
                _adaptiveThresh_tt2 = value;
            }
        }
        public int AdaptiveThreshBlockSize
        {
            get => _curAdaptiveThreshBlockSize;
            set
            {
                if (value % 2 == 0)
                {
                    value += 1;
                }
                if (value < 3)
                {
                    _curAdaptiveThreshBlockSize = 3;
                }
                else if (value > 65)
                {
                    _curAdaptiveThreshBlockSize = 65;
                }
                else
                {
                    _curAdaptiveThreshBlockSize = value;
                }
            }
        }
        public double AdaptiveThreshC
        {
            get => _curAdaptiveThreshC;
            set
            {
                if (value > 255.00)
                {
                    _curAdaptiveThreshC = 255.00;
                }
                else if (value < -255)
                {
                    _curAdaptiveThreshC = -255.00;
                }
                else
                {
                    _curAdaptiveThreshC = value;
                }
            }
        }
        public double Adp0
        {
            get => _adp0;
            set
            {
                if (value > 255)
                {
                    _adp0 = 255;
                }
                else if (value < 0)
                {
                    _adp0 = 0;
                }
                else
                {
                    _adp0 = value;
                }
            }
        }
        public double Adp1
        {
            get => _adp1;
            set
            {
                if (value > 255)
                {
                    _adp1 = 255;
                }
                else if (value < 0)
                {
                    _adp1 = 0;
                }
                else
                {
                    _adp1 = value;
                }
            }
        }
        public int Adp2
        {
            get => _adp2;
            set
            {
                if (value > 255)
                {
                    _adp2 = 255;
                }
                else if (value < 0)
                {
                    _adp2 = 0;
                }
                else
                {
                    _adp2 = value;
                }
            }
        }
        public int Adp3
        {
            get => _adp3;
            set
            {
                if (value > 255)
                {
                    _adp3 = 255;
                }
                else if (value < 0)
                {
                    _adp3 = 0;
                }
                else
                {
                    _adp3 = value;
                }
            }
        }
        public double CannyThr1
        {
            get => _CannyThr1;
            set
            {
                if (value > 255)
                {
                    _CannyThr1 = 255;
                }
                else if (value < 0)
                {
                    _CannyThr1 = 0;
                }
                else
                {
                    _CannyThr1 = value;
                }
            }
        }
        public double CannyThr2
        {
            get => _CannyThr2;
            set
            {
                if (value > 255)
                {
                    _CannyThr2 = 255;
                }
                else if (value < 0)
                {
                    _CannyThr2 = 0;
                }
                else
                {
                    _CannyThr2 = value;
                }
            }
        }
        public double LinesP_rho
        {
            get => _LinesP_rho;
            set
            {
                if (value < 0.01)
                {
                    _LinesP_rho = 0.01;
                }
                else if (value > 50.00)
                {
                    _LinesP_rho = 50.00;
                }
                else
                {
                    _LinesP_rho = value;
                }
            }
        }
        public int LinesP_thetaMultiplyer
        {
            get => _LinesP_thetaMultiplyer;
            set
            {
                if (value < 1)
                {
                    _LinesP_thetaMultiplyer = 1;
                }
                else if (value > 180)
                {
                    _LinesP_thetaMultiplyer = 180;
                }
                else
                {
                    _LinesP_thetaMultiplyer = value;
                }
            }
        }
        public int LinesP_Thr
        {
            get => _LinesP_Thr;
            set
            {
                if (value < 1)
                {
                    _LinesP_Thr = 1;
                }
                else if (value > 1000)
                {
                    _LinesP_Thr = 1000;
                }
                else
                {
                    _LinesP_Thr = value;
                }
            }
        }
        public double LinesP_XGap
        {
            get => _LinesP_XGap;
            set
            {
                if (value < 0)
                {
                    _LinesP_XGap = 0;
                }
                else if (value > 64)
                {
                    _LinesP_XGap = 64;
                }
                else
                {
                    _LinesP_XGap = value;
                }
            }
        }
        public double LinesP_YGap
        {
            get => _LinesP_YGap;
            set
            {
                if (value < 0)
                {
                    _LinesP_YGap = 0;
                }
                else if (value > 64)
                {
                    _LinesP_YGap = 64;
                }
                else
                {
                    _LinesP_YGap = value;
                }
            }
        }
        public double Strip_R_minLen
        {
            get => _LinesP_R_minLen;
            set
            {
                if (value < 4)
                {
                    _LinesP_R_minLen = 4;
                }
                else if (value > 1000)
                {
                    _LinesP_R_minLen = 1000;
                }
                else
                {
                    _LinesP_R_minLen = value;
                }
            }
        }
        public double Strip_R_maxGap
        {
            get => _LinesP_R_maxGap;
            set
            {
                if (value < 1)
                {
                    _LinesP_R_maxGap = 1;
                }
                else if (value > 1000)
                {
                    _LinesP_R_maxGap = 1000;
                }
                else
                {
                    _LinesP_R_maxGap = value;
                }
            }
        }
        public int Strip_R_width
        {
            get => _strip_R_width;
            set
            {
                if (value < 4)
                {
                    _strip_R_width = 4;
                }
                else if (value > 1000)
                {
                    _strip_R_width = 1000;
                }
                else
                {
                    _strip_R_width = value;
                }
            }
        }
        public int Strip_R_Margin
        {
            get => _strip_R_Margin;
            set
            {
                if (value < 0)
                {
                    _strip_R_Margin = 0;
                }
                else if (value > 200)
                {
                    _strip_R_Margin = 200;
                }
                else
                {
                    _strip_R_Margin = value;
                }
            }
        }
        public int Strip_R_height
        {
            get => _strip_R_hright;
            set
            {
                if (value < 4)
                {
                    _strip_R_hright = 4;
                }
                else if (value > 800)
                {
                    _strip_R_hright = 800;
                }
                else
                {
                    _strip_R_hright = value;
                }
            }
        }
        public int Strip_R_botMargin
        {
            get => _strip_R_botMargin;
            set
            {
                if (value < 0)
                {
                    _strip_R_botMargin = 0;
                }
                else if (value > 800)
                {
                    _strip_R_botMargin = 800;
                }
                else
                {
                    _strip_R_botMargin = value;
                }
            }
        }
        public double LinesP_L_minLen
        {
            get => _LinesP_L_minLen;
            set
            {
                if (value < 4)
                {
                    _LinesP_L_minLen = 4;
                }
                else if (value > 1000)
                {
                    _LinesP_L_minLen = 1000;
                }
                else
                {
                    _LinesP_L_minLen = value;
                }
            }
        }
        public double LinesP_L_maxGap
        {
            get => _LinesP_L_maxGap;
            set
            {
                if (value < 1)
                {
                    _LinesP_L_maxGap = 1;
                }
                else if (value > 1000)
                {
                    _LinesP_L_maxGap = 1000;
                }
                else
                {
                    _LinesP_L_maxGap = value;
                }
            }
        }
        public int Strip_L_width
        {
            get => _strip_L_width;
            set
            {
                if (value < 4)
                {
                    _strip_L_width = 4;
                }
                else if (value > 1000)
                {
                    _strip_L_width = 1000;
                }
                else
                {
                    _strip_L_width = value;
                }
            }
        }
        public int Strip_L_Margin
        {
            get => _strip_L_Margin;
            set
            {
                if (value < 0)
                {
                    _strip_L_Margin = 0;
                }
                else if (value > 200)
                {
                    _strip_L_Margin = 200;
                }
                else
                {
                    _strip_L_Margin = value;
                }
            }
        }
        public int Strip_L_height
        {
            get => _strip_L_hright;
            set
            {
                if (value < 4)
                {
                    _strip_L_hright = 4;
                }
                else if (value > 800)
                {
                    _strip_L_hright = 800;
                }
                else
                {
                    _strip_L_hright = value;
                }
            }
        }
        public int Strip_L_TopLimit
        {
            get => _strip_L_Top_limit;
            set
            {
                if (value < 0)
                {
                    _strip_L_Top_limit = 0;
                }
                else if (value > 800)
                {
                    _strip_L_Top_limit = 800;
                }
                else
                {
                    _strip_L_Top_limit = value;
                }
            }
        }
        public double LinesP_V_minLen
        {
            get => _LinesP_V_minLen;
            set
            {
                if (value < 4)
                {
                    _LinesP_V_minLen = 4;
                }
                else if (value > 1000)
                {
                    _LinesP_V_minLen = 1000;
                }
                else
                {
                    _LinesP_V_minLen = value;
                }
            }
        }
        public double LinesP_V_maxGap
        {
            get => _LinesP_V_maxGap;
            set
            {
                if (value < 1)
                {
                    _LinesP_V_maxGap = 1;
                }
                else if (value > 1000)
                {
                    _LinesP_V_maxGap = 1000;
                }
                else
                {
                    _LinesP_V_maxGap = value;
                }
            }
        }
        public int Strip_V_width
        {
            get => _strip_V_width;
            set
            {
                if (value < 4)
                {
                    _strip_V_width = 4;
                }
                else if (value > 800)
                {
                    _strip_V_width = 800;
                }
                else
                {
                    _strip_V_width = value;
                }
            }
        }
        public int Strip_V_Margin
        {
            get => _strip_V_Margin;
            set
            {
                if (value < 0)
                {
                    _strip_V_Margin = 0;
                }
                else if (value > 200)
                {
                    _strip_V_Margin = 200;
                }
                else
                {
                    _strip_V_Margin = value;
                }
            }
        }
        public int Strip_V_height
        {
            get => _strip_V_hright;
            set
            {
                if (value < 4)
                {
                    _strip_V_hright = 4;
                }
                else if (value > 800)
                {
                    _strip_V_hright = 800;
                }
                else
                {
                    _strip_V_hright = value;
                }
            }
        }
        public int Strip_V_TopLimit
        {
            get => _strip_V_Top_limit;
            set
            {
                if (value < 0)
                {
                    _strip_V_Top_limit = 0;
                }
                else if (value > 800)
                {
                    _strip_V_Top_limit = 800;
                }
                else
                {
                    _strip_V_Top_limit = value;
                }
            }
        }
        public int TempIntX_0_900
        {
            get => _tempIntX_0_900;
            set
            {
                if (value < 0)
                {
                    _tempIntX_0_900 = 0;
                }
                else if (value > 900)
                {
                    _tempIntX_0_900 = 900;
                }
                else
                {
                    _tempIntX_0_900 = value;
                }
            }
        }
        public int TempIntY_0_900
        {
            get => _tempIntY_0_900;
            set
            {
                if (value < 0)
                {
                    _tempIntY_0_900 = 0;
                }
                else if (value > 900)
                {
                    _tempIntY_0_900 = 900;
                }
                else
                {
                    _tempIntY_0_900 = value;
                }
            }
        }
        public double LinesP_MaxDy
        {
            get => _LinesP_MaxDy;
            set
            {
                if (value < 0.01)
                {
                    _LinesP_MaxDy = 0.01;
                }
                else if (value > 255.00)
                {
                    _LinesP_MaxDy = 255.00;
                }
                else
                {
                    _LinesP_MaxDy = value;
                }
            }
        }
        public double LinesP_MaxDx
        {
            get => _LinesP_MaxDx;
            set
            {
                if (value < 0.01)
                {
                    _LinesP_MaxDx = 0.01;
                }
                else if (value > 255.00)
                {
                    _LinesP_MaxDx = 255.00;
                }
                else
                {
                    _LinesP_MaxDx = value;
                }
            }
        }
        public double LinesP_PercentWidth
        {
            get => _LinesP_PercentWidth;
            set
            {
                if (value < 0.01)
                {
                    _LinesP_PercentWidth = 0.01;
                }
                else if (value > 100.00)
                {
                    _LinesP_PercentWidth = 100.00;
                }
                else
                {
                    _LinesP_PercentWidth = value;
                }
            }
        }
        public double LinesP_PercentHeight
        {
            get => _LinesP_PercentHeight;
            set
            {
                if (value < 0.01)
                {
                    _LinesP_PercentHeight = 0.01;
                }
                else if (value > 100.00)
                {
                    _LinesP_PercentHeight = 100.00;
                }
                else
                {
                    _LinesP_PercentHeight = value;
                }
            }
        }
        public int PdfwidthNeeded
        {
            get => _pdfwidthNeeded;
            set
            {
                if (value < 0)
                {
                    _pdfwidthNeeded = 0;
                }
                else
                {
                    _pdfwidthNeeded = value;
                }
            }
        }
        public int PdfheightNeeded
        {
            get => _pdfheightNeeded;
            set
            {
                if (value < 0)
                {
                    _pdfheightNeeded = 0;
                }
                else
                {
                    _pdfheightNeeded = value;
                }
            }
        }
        public int PdfDPI_needed
        {
            get => _pdfDPI_needed;
            set
            {
                if (value < 0)
                {
                    _pdfDPI_needed = 0;
                }
                else
                {
                    _pdfDPI_needed = value;
                }
            }
        }
        public OCV_Filter_V2()
        {
            _pdfwidthNeeded = G_PdfwidthNeeded;
            _pdfheightNeeded = G_PdfheightNeeded;
            _pdfDPI_needed = G_PdfDPI_needed;
            //initialze values like in the OCV_filterObj
            _pdf_0_GrayThr = G_Pdf_0_GrayThr;
            _pdf_1_GrayMaxVal = G_Pdf_1_GrayMaxVal;
            _pdf_tt = G_Pdf_tt;
            _blur_ActionToUse = G_Blur_TypeToUse;
            _bbLurBorderType = G_bLurBorderType;
            _blur_ksize = G_Blur_ksize;
            _blurSigmax = G_BlurSigmax;
            _blurSigmay = G_BlurSigmay;
            _thresholdActionToUse = G_ThresholdTypeToUse;
            _stdThreshThr = G_StdThreshThr;
            _stdThreshMaxv = G_StdThreshMaxV;
            _stdThres_tt = G_StdThres_tt;
            _adaptiveThresh_at = G_AdaptiveThresh_at;
            _adaptiveThresh_tt = G_AdaptiveThresh_tt;
            _adaptiveThresh_tt2 = G_AdaptiveThresh_tt2;
            _curAdaptiveThreshBlockSize = G_AdaptiveThreshBlockSize;
            _curAdaptiveThreshC = G_AdaptiveThreshC;
            _adp0 = G_AdaptiveThreshMaxV;
            _adp1 = G_AdaptiveThreshThresh;
            _adp2 = G_Adp2;
            _adp3 = G_Adp3;
            _CannyThr1 = G_CannyThr1;
            _CannyThr2 = G_CannyThr2;
            _LinesP_rho = G_LinesP_rho;
            _LinesP_thetaMultiplyer = G_LinesP_thetaMultiplyer;
            _LinesP_Thr = G_LinesP_Thr;
            _LinesP_R_minLen = G_LinesP_R_minLen;
            _LinesP_R_maxGap = G_LinesP_R_maxGap;
            _LinesP_YGap = G_LinesP_YGap;
            _LinesP_XGap = G_LinesP_XGap;
            _strip_R_width = G_Strip_R_width;
            _strip_R_Margin = G_Strip_R_Margin;
            _strip_R_hright = G_Strip_R_height;
            _strip_R_botMargin = G_Strip_R_botMargin;
            _LinesP_L_minLen = G_LinesP_L_minLen;
            _LinesP_L_maxGap = G_LinesP_L_maxGap;
            _strip_L_width = G_Strip_L_width;
            _strip_L_Margin = G_Strip_L_Margin;
            _strip_L_hright = G_Strip_L_height;
            _strip_L_Top_limit = G_Strip_L_TopLimit;
            _LinesP_V_minLen = G_LinesP_V_minLen;
            _LinesP_V_maxGap = G_LinesP_V_maxGap;
            _strip_V_width = G_Strip_V_width;
            _strip_V_Margin = G_Strip_V_Margin;
            _strip_V_hright = G_Strip_V_height;
            _strip_V_Top_limit = G_Strip_V_TopLimit;
            _tempIntX_0_900 = G_TempIntX_0_900;
            _tempIntY_0_900 = G_TempIntY_0_900;
            _pdfPage_height = 1;
            _pdfPage_width = 1;
            _LinesP_MaxDy = G_LinesP_MaxDy;
            _LinesP_MaxDx = G_LinesP_MaxDx;
            _LinesP_PercentHeight = G_LinesP_PercentHeight;
            _LinesP_PercentWidth = G_LinesP_PercentWidth;
        }
        public void CopyDataFromOtherObject(OCV_Filter_V2 argOtherObject)
        {
            if (argOtherObject != null)
            {
                this._pdfwidthNeeded = argOtherObject._pdfwidthNeeded;
                this._pdfheightNeeded = argOtherObject._pdfheightNeeded;
                this._pdfDPI_needed = argOtherObject._pdfDPI_needed;
                this._pdf_0_GrayThr = argOtherObject._pdf_0_GrayThr;
                this._pdf_1_GrayMaxVal = argOtherObject._pdf_1_GrayMaxVal;
                this._pdf_tt = argOtherObject._pdf_tt;
                this._blur_ActionToUse = argOtherObject._blur_ActionToUse;
                this._bbLurBorderType = argOtherObject._bbLurBorderType;
                this._blur_ksize = argOtherObject._blur_ksize;
                this._blurSigmax = argOtherObject._blurSigmax;
                this._blurSigmay = argOtherObject._blurSigmay;
                this._thresholdActionToUse = argOtherObject._thresholdActionToUse;
                this._stdThreshThr = argOtherObject._stdThreshThr;
                this._stdThreshMaxv = argOtherObject._stdThreshMaxv;
                this._stdThres_tt = argOtherObject._stdThres_tt;
                this._adaptiveThresh_at = argOtherObject._adaptiveThresh_at;
                this._adaptiveThresh_tt = argOtherObject._adaptiveThresh_tt;
                this._adaptiveThresh_tt2 = argOtherObject._adaptiveThresh_tt2;
                this._curAdaptiveThreshBlockSize = argOtherObject._curAdaptiveThreshBlockSize;
                this._curAdaptiveThreshC = argOtherObject._curAdaptiveThreshC;
                this._CannyThr1 = argOtherObject._CannyThr1;
                this._CannyThr2 = argOtherObject._CannyThr2;
                this._LinesP_rho = argOtherObject._LinesP_rho;
                this._LinesP_thetaMultiplyer = argOtherObject._LinesP_thetaMultiplyer;
                this._LinesP_Thr = argOtherObject._LinesP_Thr;
                this._LinesP_YGap = argOtherObject._LinesP_YGap;
                this._LinesP_R_minLen = argOtherObject._LinesP_R_minLen;
                this._LinesP_R_maxGap = argOtherObject._LinesP_R_maxGap;
                this._strip_R_width = argOtherObject._strip_R_width;
                this._strip_R_hright = argOtherObject._strip_R_hright;
                this._strip_R_Margin = argOtherObject._strip_R_Margin;
                this._strip_R_botMargin = argOtherObject._strip_R_botMargin;
                this._LinesP_L_minLen = argOtherObject._LinesP_L_minLen;
                this._LinesP_L_maxGap = argOtherObject._LinesP_L_maxGap;
                this._strip_L_width = argOtherObject._strip_L_width;
                this._strip_L_hright = argOtherObject._strip_L_hright;
                this._strip_L_Margin = argOtherObject._strip_L_Margin;
                this._strip_L_Top_limit = argOtherObject._strip_L_Top_limit;
                this._LinesP_V_minLen = argOtherObject._LinesP_V_minLen;
                this._LinesP_V_maxGap = argOtherObject._LinesP_V_maxGap;
                this._strip_V_width = argOtherObject._strip_V_width;
                this._strip_V_hright = argOtherObject._strip_V_hright;
                this._strip_V_Margin = argOtherObject._strip_V_Margin;
                this._strip_V_Top_limit = argOtherObject._strip_V_Top_limit;
                this._tempIntX_0_900 = argOtherObject._tempIntX_0_900;
                this._tempIntY_0_900 = argOtherObject._tempIntY_0_900;
                this._pdfPage_height = argOtherObject._pdfPage_height;
                this._pdfPage_width = argOtherObject._pdfPage_width;
                this._LinesP_MaxDy = argOtherObject._LinesP_MaxDy;
                this._LinesP_MaxDx = argOtherObject._LinesP_MaxDx;
                this._LinesP_PercentHeight = argOtherObject._LinesP_PercentHeight;
                this._LinesP_PercentWidth = argOtherObject._LinesP_PercentWidth;
            }
        }
        public void PrintValues()
        {
            //string _pdfpageDim = "pdfPage_width : " + _pdfPage_width + " pdfPage_height   : " + _pdfPage_height;
            //string _pdf_0_Gray = "pdf_0_GrayThr : " + _pdf_0_GrayThr + " pdf_1_GrayMaxVal : " + _pdf_1_GrayMaxVal + " pdf_tt : " + _pdf_tt;
            //string _blur = "blur_TypeToUse : " + _blur_ActionToUse + " bbLurBorderType : " + _bbLurBorderType + " blur_ksize : " + _blur_ksize + " blurSigmax : " + _blurSigmax + " blurSigmay : " + _blurSigmay;
            //string _threshold = "thresholdTypeToUse : " + _thresholdActionToUse + " stdThreshThr : " + _stdThreshThr + " stdThreshMaxv : " + _stdThreshMaxv + " stdThres_tt : " + _stdThres_tt;
            string _bk_size = "adplockSize : " + _curAdaptiveThreshBlockSize + "   blur_ksize : " + _blur_ksize;
            //  string _adaptive = "adaptiveThresh_at : " + _adaptiveThresh_at + " adaptiveThresh_tt : " + _adaptiveThresh_tt + " curAdaptiveThreshBlockSize : " + _curAdaptiveThreshBlockSize + " curAdaptiveThreshC : " + _curAdaptiveThreshC;
            //string _adp = "adp0 : " + _adp0 + " adp1 : " + _adp1 + " adp2 : " + _adp2 + " adp3 : " + _adp3;
            //string _canny = "CannyThr1 : " + _CannyThr1 + " CannyThr2 : " + _CannyThr2;
            //string _lines = "LinesP_rho : " + _LinesP_rho + " LinesP_thetaMultiplyer : " + _LinesP_thetaMultiplyer + " LinesP_Thr : " + _LinesP_Thr + " LinesP_R_minLen : " + _LinesP_R_minLen + " LinesP_R_maxGap : " + _LinesP_R_maxGap + " LinesP_YGap : " + _LinesP_YGap;
            //string _stripR = "strip_R_width : " + _strip_R_width + " strip_R_Margin : " + _strip_R_Margin + " strip_R_hright : " + _strip_R_hright + " strip_R_Top_limit : " + _strip_R_Top_limit;
            //string _stripL = "LinesP_L_minLen : " + _LinesP_L_minLen + " LinesP_L_maxGap : " + _LinesP_L_maxGap + " strip_L_width : " + _strip_L_width + " strip_L_Margin : " + _strip_L_Margin + " strip_L_hright : " + _strip_L_hright + " strip_L_Top_limit : " + _strip_L_Top_limit;
            //string _stripV = "LinesP_V_minLen : " + _LinesP_V_minLen + " LinesP_V_maxGap : " + _LinesP_V_maxGap + " strip_V_width : " + _strip_V_width + " strip_V_Margin : " + _strip_V_Margin + " strip_V_hright : " + _strip_V_hright + " strip_V_Top_limit : " + _strip_V_Top_limit;
            //string _tempInt = "tempIntX_0_900 : " + _tempIntX_0_900 + " tempIntY_0_900 : " + _tempIntY_0_900;
            EventsManagerLib.Call_LogConsole("*");
            EventsManagerLib.Call_LogConsole("*" + _bk_size);
            //EventsManagerLib.Call_LogConsole("*"+_lines);
            //EventsManagerLib.Call_LogConsole("*" + _stripR);
            //EventsManagerLib.Call_LogConsole("*" + _tempInt);
            EventsManagerLib.Call_LogConsole("*");
            //EventsManagerLib.Call_LogConsole("ksize : " + _blur_ksize + " _blurSigmax : " + _blurSigmax + " _blurSigmay : " + _blurSigmay);
            //EventsManagerLib.Call_LogConsole(" P thr   : " + _LinesP_Thr + "Ygap : " + _LinesP_YGap + " Xgap : " + _LinesP_XGap);
            //EventsManagerLib.Call_LogConsole("L min len : " + _LinesP_L_minLen + " L max gap : " + _LinesP_L_maxGap + " strip L width : " + _strip_L_width + " strip L Margin : " + _strip_L_Margin + " strip L hright : " + _strip_L_hright + " strip L Top limit : " + _strip_L_Top_limit);
            //EventsManagerLib.Call_LogConsole("V min len : " + _LinesP_V_minLen + " V max gap : " + _LinesP_V_maxGap + " strip V width : " + _strip_V_width + " strip V Margin : " + _strip_V_Margin + " strip V hright : " + _strip_V_hright + " strip V Top limit : " + _strip_V_Top_limit);
            //EventsManagerLib.Call_LogConsole("R min len : " + _LinesP_R_minLen + " R max gap : " + _LinesP_R_maxGap + " strip R width : " + _strip_R_width + " strip R Margin : " + _strip_R_Margin + " strip R hright : " + _strip_R_hright + " strip R Top limit : " + _strip_R_Top_limit);
            //EventsManagerLib.Call_LogConsole("_pdf_0_GrayThr : " + _pdf_0_GrayThr + " _pdf_1_GrayMaxVal : " + _pdf_1_GrayMaxVal + " _pdf_tt : " + _pdf_tt);
            //EventsManagerLib.Call_LogConsole("_blur_TypeToUse : " + _blur_TypeToUse + " _bbLurBorderType : " + _bbLurBorderType + " _blur_ksize : " + _blur_ksize + " _blurSigmax : " + _blurSigmax + " _blurSigmay : " + _blurSigmay);
            //EventsManagerLib.Call_LogConsole("_thresholdTypeToUse : " + _thresholdTypeToUse + " _stdThreshThr : " + _stdThreshThr + " _stdThreshMaxv : " + _stdThreshMaxv + " _stdThres_tt : " + _stdThres_tt);
            //EventsManagerLib.Call_LogConsole("_adaptiveThresh_at : " + _adaptiveThresh_at + " _adaptiveThresh_tt : " + _adaptiveThresh_tt + " _curAdaptiveThreshBlockSize : " + _curAdaptiveThreshBlockSize + " _curAdaptiveThreshC : " + _curAdaptiveThreshC);
            //EventsManagerLib.Call_LogConsole("adp0 : " + _adp0 + " adp1 : " + _adp1 + " adp2 : " + _adp2 + " adp3 : " + _adp3);
            //EventsManagerLib.Call_LogConsole("_CannyThr1 : " + _CannyThr1 + " _CannyThr2 : " + _CannyThr2);
            //EventsManagerLib.Call_LogConsole("_LinesP_rho : " + _LinesP_rho + " _LinesP_thetaMultiplyer : " + _LinesP + " _LinesP_Thr : " + _LinesP_Thr + " _LinesP_R_minLen : " + _LinesP_R_minLen + " _LinesP_R_maxGap : " + _LinesP_R_maxGap + " _LinesP_YGap : " + _LinesP_YGap);
            //EventsManagerLib.Call_LogConsole("_strip_R_width : " + _strip_R_width + " _strip_R_Margin : " + _strip_R_Margin + " _strip_R_hright : " + _strip_R_hright + " _strip_R_Top_limit : " + _strip_R_Top_limit);
            //EventsManagerLib.Call_LogConsole("_LinesP_L_minLen : " + _LinesP_L_minLen + " _LinesP_L_maxGap : " + _LinesP_L_maxGap + " _strip_L_width : " + _strip_L_width + " _strip_L_Margin : " + _strip_L_Margin + " _strip_L_hright : " + _strip_L_hright + " _strip_L_Top_limit : " + _strip_L_Top_limit);
            //EventsManagerLib.Call_LogConsole("_LinesP_V_minLen : " + _LinesP_V_minLen + " _LinesP_V_maxGap : " + _LinesP_V_maxGap + " _strip_V_width : " + _strip_V_width + " _strip_V_Margin : " + _strip_V_Margin + " _strip_V_hright : " + _strip_V_hright + " _strip_V_Top_limit : " + _strip_V_Top_limit);
        }
    }
    public class OCV_FilterActions
    {
        private bool _run_OLDMeTHOD = false;
        private bool _run_GaussianBlur = false;
        private bool _run_MedianBlur = false;
        public bool Run_OLDMeTHOD
        {
            get => _run_OLDMeTHOD;
            set
            {
                _run_OLDMeTHOD = value;
            }
        }
        public bool Run_GaussianBlur
        {
            get => _run_GaussianBlur;
            set
            {
                _run_GaussianBlur = value;
            }
        }
        public bool Run_MedianBlur
        {
            get => _run_MedianBlur;
            set
            {
                _run_MedianBlur = value;
            }
        }
        public OCV_FilterActions()
        {
            _run_OLDMeTHOD = false;
            _run_GaussianBlur = false;
            _run_MedianBlur = false;
        }
        public void CopyDataFromOtherObject(OCV_FilterActions argOtherObject)
        {
            if (argOtherObject != null)
            {
                this._run_OLDMeTHOD = argOtherObject._run_OLDMeTHOD;
                this._run_GaussianBlur = argOtherObject._run_GaussianBlur;
                this._run_MedianBlur = argOtherObject._run_MedianBlur;
            }
        }
    }
}