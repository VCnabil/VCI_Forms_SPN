using OpenCvSharp;
using static VCI_Forms_SPN._GLobalz.G_Helpers;

namespace VCI_Forms_SPN._BackEndDataOBJs.OCVObjs
{
    public class OCV_TempDataObj
    {
        private float _pdf_To_image_ratio;
        #region ROI Lower Left-Right
        private float _ROI_LowerLeft_percent_Width_min;
        private float _ROI_LowerLeft_percent_Width_max;

        private float _ROI_LowerLeft_percent_Height_min;
        private float _ROI_LowerLeft_percent_Height_max;

        private float _ROI_LowerRight_percent_Width_min;
        private float _ROI_LowerRight_percent_Width_max;

        private float _ROI_LowerRight_percent_Height_min;
        private float _ROI_LowerRight_percent_Height_max;

        public float Pdf_To_image_ratio { get => _pdf_To_image_ratio; set => _pdf_To_image_ratio = value; }
        public float ROI_LowerLeft_percent_Width_min { get => _ROI_LowerLeft_percent_Width_min; set => _ROI_LowerLeft_percent_Width_min = value; }
        public float ROI_LowerLeft_percent_Width_max { get => _ROI_LowerLeft_percent_Width_max; set => _ROI_LowerLeft_percent_Width_max = value; }
        public float ROI_LowerLeft_percent_Height_min { get => _ROI_LowerLeft_percent_Height_min; set => _ROI_LowerLeft_percent_Height_min = value; }
        public float ROI_LowerLeft_percent_Height_max { get => _ROI_LowerLeft_percent_Height_max; set => _ROI_LowerLeft_percent_Height_max = value; }
        public float ROI_LowerRight_percent_Width_min { get => _ROI_LowerRight_percent_Width_min; set => _ROI_LowerRight_percent_Width_min = value; }
        public float ROI_LowerRight_percent_Width_max { get => _ROI_LowerRight_percent_Width_max; set => _ROI_LowerRight_percent_Width_max = value; }
        public float ROI_LowerRight_percent_Height_min { get => _ROI_LowerRight_percent_Height_min; set => _ROI_LowerRight_percent_Height_min = value; }
        public float ROI_LowerRight_percent_Height_max { get => _ROI_LowerRight_percent_Height_max; set => _ROI_LowerRight_percent_Height_max = value; }
        #endregion

        #region AdaptiveThresh
        private double _adpThreshMaxv;
        private ThresholdTypes _adpThresh_tt;
        private AdaptiveThresholdTypes _adaptiveThresh_at;
        private int _adaptiveThreshBlockSize;
        private double _adaptiveThreshC;
        public double ADPThreshMaxv
        {
            get => _adpThreshMaxv;
            set
            {
                if (value > 255)
                {
                    _adpThreshMaxv = 255;
                }
                else if (value < 0)
                {
                    _adpThreshMaxv = 0;
                }
                else
                {
                    _adpThreshMaxv = value;
                }
            }
        }
        public ThresholdTypes ADPThresh_tt
        {
            get => _adpThresh_tt;
            set
            {
                _adpThresh_tt = value;
            }
        }
        public AdaptiveThresholdTypes ADPThresh_at
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
        public int ADPThreshBlockSize
        {
            get => _adaptiveThreshBlockSize;
            set
            {
                if (value < 3)
                {
                    _adaptiveThreshBlockSize = 3;
                }
                else if (value % 2 == 0)
                {
                    _adaptiveThreshBlockSize = value + 1;
                }
                else
                {
                    _adaptiveThreshBlockSize = value;
                }
            }
        }
        public double ADPThreshC
        {
            get => _adaptiveThreshC;
            set
            {
                if (value > 255.00)
                {
                    _adaptiveThreshC = 255.00;
                }
                else if (value < -255)
                {
                    _adaptiveThreshC = -255.00;
                }
                else
                {
                    _adaptiveThreshC = value;
                }
            }
        }
        #endregion

        #region STDThresh
        double _stdThreshThr;
        public double StdThreshThr { get => _stdThreshThr; set => _stdThreshThr = value; }
        double _stdThreshMaxV;
        public double StdThreshMaxV { get => _stdThreshMaxV; set => _stdThreshMaxV = value; }
        private ThresholdTypes _stdThresh_tt;
        public ThresholdTypes StdThresh_tt { get => _stdThresh_tt; set => _stdThresh_tt = value; }
        #endregion

        #region Gossian Blurr
        private int _blur_ksize;
        private double _blurSigmax;
        private double _blurSigmay;

        public int BlurKsize
        {
            get => _blur_ksize;
            set
            {
                //if not odd make it odd by adding 1

                if (value % 2 == 0)
                {
                    value += 1;
                }


                if (value < 3)
                {
                    _blur_ksize = 3;
                }
                else if (value > 15)
                {
                    _blur_ksize = 15;
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
                if (value > 50)
                {
                    _blurSigmax = 50;
                }
                else if (value < -50)
                {
                    _blurSigmax = -50;
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
                if (value > 50)
                {
                    _blurSigmay = 50;
                }
                else if (value < -50)
                {
                    _blurSigmay = -50;
                }
                else
                {
                    _blurSigmay = value;
                }
            }
        }

        #endregion


        #region Sharmen AddWights

        private double _Alpha;
        private double _Beta;
        private double _Gamma;

        public double Alpha
        {
            get => _Alpha;
            set
            {
                if (value > 50)
                {
                    _Alpha = 50;
                }
                else if (value < -50)
                {
                    _Alpha = -50;
                }
                else
                {
                    _Alpha = value;
                }
            }
        }
        public double Beta
        {
            get => _Beta;
            set
            {
                if (value > 50)
                {
                    _Beta = 50;
                }
                else if (value < -50)
                {
                    _Beta = -50;
                }
                else
                {
                    _Beta = value;
                }
            }
        }

        public double Gamma
        {
            get => _Gamma;
            set
            {
                if (value > 50)
                {
                    _Gamma = 50;
                }
                else if (value < -50)
                {
                    _Gamma = -50;
                }
                else
                {
                    _Gamma = value;
                }
            }
        }
        #endregion

        #region Canny
        private double _CannyThr1;
        private double _CannyThr2;
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
        //   public double CannyThr1 { get => _CannyThr1; set => _CannyThr1 = value; }
        //   public double CannyThr2 { get => _CannyThr2; set => _CannyThr2 = value; }

        #endregion
        public OCV_TempDataObj()
        {
            _pdf_To_image_ratio = 1.0f;

            _ROI_LowerLeft_percent_Width_min = G_ROI_LowerLeft_percent_Width_min;
            _ROI_LowerLeft_percent_Width_max = G_ROI_LowerLeft_percent_Width_max;
            _ROI_LowerLeft_percent_Height_min = G_ROI_LowerLeft_percent_Height_min;
            _ROI_LowerLeft_percent_Height_max = G_ROI_LowerLeft_percent_Height_max;
            _ROI_LowerRight_percent_Width_min = G_ROI_LowerRight_percent_Width_min;
            _ROI_LowerRight_percent_Width_max = G_ROI_LowerRight_percent_Width_max;
            _ROI_LowerRight_percent_Height_min = G_ROI_LowerRight_percent_Height_min;
            _ROI_LowerRight_percent_Height_max = G_ROI_LowerRight_percent_Height_max;

            _adaptiveThreshC = G_AdaptiveThreshC;
            _adpThreshMaxv = G_AdaptiveThreshMaxV;
            _adpThresh_tt = G_AdaptiveThresh_tt;
            _adaptiveThreshBlockSize = G_AdaptiveThreshBlockSize;
            _adaptiveThresh_at = G_AdaptiveThresh_at;

            _stdThreshThr = G_StdThreshThr;
            _stdThreshMaxV = G_StdThreshMaxV;
            _stdThresh_tt = G_StdThres_tt;

            _blur_ksize = G_Blur_ksize;
            _blurSigmax = G_BlurSigmax;
            _blurSigmay = G_BlurSigmay;

            _Alpha = G_Alpha;
            _Beta = G_Beta;
            _Gamma = G_Gamma;

            _CannyThr1 = G_CannyThr1;
            _CannyThr2 = G_CannyThr2;

        }

        public void CopyData(OCV_TempDataObj arg)
        {
            if (arg == null)
                return;

            _pdf_To_image_ratio = arg.Pdf_To_image_ratio;
            _ROI_LowerLeft_percent_Width_min = arg.ROI_LowerLeft_percent_Width_min;
            _ROI_LowerLeft_percent_Width_max = arg.ROI_LowerLeft_percent_Width_max;
            _ROI_LowerLeft_percent_Height_min = arg.ROI_LowerLeft_percent_Height_min;
            _ROI_LowerLeft_percent_Height_max = arg.ROI_LowerLeft_percent_Height_max;
            _ROI_LowerRight_percent_Width_min = arg.ROI_LowerRight_percent_Width_min;
            _ROI_LowerRight_percent_Width_max = arg.ROI_LowerRight_percent_Width_max;
            _ROI_LowerRight_percent_Height_min = arg.ROI_LowerRight_percent_Height_min;
            _ROI_LowerRight_percent_Height_max = arg.ROI_LowerRight_percent_Height_max;

            _adaptiveThreshC = arg.ADPThreshC;
            _adpThreshMaxv = arg.ADPThreshMaxv;
            _adpThresh_tt = arg.ADPThresh_tt;
            _adaptiveThreshBlockSize = arg.ADPThreshBlockSize;
            _adaptiveThresh_at = arg.ADPThresh_at;

            _stdThreshThr = arg.StdThreshThr;
            _stdThreshMaxV = arg.StdThreshMaxV;
            _stdThresh_tt = arg.StdThresh_tt;

            _blur_ksize = arg.BlurKsize;
            _blurSigmax = arg.BlurSigmax;
            _blurSigmay = arg.BlurSigmay;

            _Alpha = arg.Alpha;
            _Beta = arg.Beta;
            _Gamma = arg.Gamma;

            _CannyThr1 = arg.CannyThr1;
            _CannyThr2 = arg.CannyThr2;
        }

    }
}
