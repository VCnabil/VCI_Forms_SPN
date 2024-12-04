using System;
using OpenCvSharp;
using System.Drawing; // For converting OpenCvSharp Mat to System.Drawing Imageeeeeeeeeeeeee
using System.Drawing.Imaging;
using System.IO;
using OpenCvSharp.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rect = OpenCvSharp.Rect;
using Point = OpenCvSharp.Point;
using VCI_Forms_SPN._GLobalz;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{

    public class MyOpenCV3
    {
        System.Drawing.Image _latest_imageOfPDF = null;
        Mat _latest_MatOfPDF = null;
        Mat _Cloned_latestMat = null;
        Mat _Cloned_innerFrame = null;
        Mat ptr_matToShow = null;
        const int _max_intermediat_Ptrs_index = 5;
        Mat[] ptr_mat_arra;

        Rect SysDiagTitle;

        Rect sysdiag_partchart;
        Rect _farRighrStripRect;
        Rect _farLeftStripRect;
        OCV_Filter_V2 _myOCVObj;
        OCV_FilterActions actionFilter;
        bool _usesOldfalse;
        Rect SysDiagTitleRoi;
        Rect SysDiagNumberRoi;
        void EventsManagerLib_On_UseOldMethodChanged(OCV_FilterActions argActionFilter)
        {
            if (argActionFilter == null)
            {
                // MessageBox.Show("One of the OCV_filterObj instances is null.");
                EventsManagerLib.Call_LogConsole("!!!!!!!!!!!!!!!!!111One of argActionFilter   instances is null.");
                return;
            }
            actionFilter.CopyDataFromOtherObject(argActionFilter);
        }
        public MyOpenCV3()
        {
            ptr_mat_arra = new Mat[_max_intermediat_Ptrs_index + 1];

            _myOCVObj = new OCV_Filter_V2();
            actionFilter = new OCV_FilterActions();
            EventsManagerLib.On_BROADCAST_OCVOBJevent += EventsManagerLib_On_BROADCAST_OCVOBJevent;
            EventsManagerLib.On_PdfPageSizeRead += EventsManagerLib_On_PdfPageSizeRead;
            EventsManagerLib.On_UpdateActionFilter += EventsManagerLib_On_UseOldMethodChanged;
        }
        //destructor
        ~MyOpenCV3()
        {
            EventsManagerLib.On_BROADCAST_OCVOBJevent -= EventsManagerLib_On_BROADCAST_OCVOBJevent;
            EventsManagerLib.On_PdfPageSizeRead -= EventsManagerLib_On_PdfPageSizeRead;
            EventsManagerLib.On_UpdateActionFilter -= EventsManagerLib_On_UseOldMethodChanged;
        }

        private void EventsManagerLib_On_PdfPageSizeRead(float arg_width, float arg_height, bool argIstypeA)
        {
            if (_myOCVObj != null)
            {
                _myOCVObj.actualPdfPage_width = (int)arg_width;
                _myOCVObj.actualPdfPage_height = (int)arg_height;
            }
        }

        private void EventsManagerLib_On_BROADCAST_OCVOBJevent(OCV_Filter_V2 arg_OCVfilterObj)
        {
            if (arg_OCVfilterObj == null || _myOCVObj == null)
            {
                // Handle the null case, e.g., log an error, throw an exception, etc.
                // MessageBox.Show("One of the OCV_filterObj instances is null.");
                EventsManagerLib.Call_LogConsole("!!!!!!!!!!!!!!!!!111One of the OCV_filterObj instances is null.");
                return;
            }
            _myOCVObj.CopyDataFromOtherObject(arg_OCVfilterObj);

        }

        public System.Drawing.Image GET_ImageFromAMat()
        {


            return ptr_mat_arra[1].ToBitmap();
        }

        public System.Drawing.Image GET_ImageFromAMat(Mat argNMat)
        {
            if (argNMat == null)
            {
                string pathtogrid = @"C:\___Root_VCI_Projects\Generic_VC_PGN_SIMULATOR\genericSim\VC_PGN_ManagerGUI\_Proj_Image_Dir\tmpImages\grid_bluetiles_1700x1100.jpg";

                argNMat = Cv2.ImRead(pathtogrid);

            }
            ptr_mat_arra[0] = argNMat;

            //  FindAndDrawLinesInROI_RightSrip_DYNA(argNMat);

            // Draw_Frame_R_Rect(argNMat);
            FindAndDrawLinesInROI_RightStrip(argNMat);

            return ptr_mat_arra[0].ToBitmap();
        }



        void FindAndDrawLinesInROI_RightStrip(Mat argMat)
        {
            _myOCVObj.PrintValues();

            if (_usesOldfalse)
                newStrightFowaardMethod(argMat);// oldStrightFowaardMethod(argMat);
            else
                HArdCodedVals(argMat);// new_StaticValuesMethod(argMat);// newStrightFowaardMethod(argMat);

        }

        void Use_BlurrDefault(Mat argmatToBlur, Mat blur_SR)
        {
            int req_blur_ksize = _myOCVObj.Blur_ksize;

            Cv2.GaussianBlur(argmatToBlur, blur_SR, new OpenCvSharp.Size(req_blur_ksize, req_blur_ksize), 1);
        }
        void Use_BlurrGausian(Mat argmatToBlur, Mat blur_SR)
        {
            int req_blur_ksize = _myOCVObj.Blur_ksize;
            double req_Sigmax = _myOCVObj.BlurSigmax;
            double req_Sigmay = _myOCVObj.BlurSigmay;

            Cv2.GaussianBlur(argmatToBlur, blur_SR, new OpenCvSharp.Size(req_blur_ksize, req_blur_ksize), req_Sigmax, req_Sigmay);
        }
        void Use_BlurrMedian(Mat argmatToBlur, Mat blur_SR)
        {
            int req_blur_ksize = _myOCVObj.Blur_ksize;

            Cv2.MedianBlur(argmatToBlur, blur_SR, req_blur_ksize);
        }
        void Use_BlurrPYRdown(Mat argmatToBlur, Mat blur_SR)
        {


            Cv2.PyrDown(argmatToBlur, blur_SR);
        }
        void Use_BlurrPYRup(Mat argmatToBlur, Mat blur_SR)
        {

            Cv2.PyrUp(argmatToBlur, blur_SR);

        }

        void Use_DTD_threshhold(Mat argMat)
        {
            double req_threshhold = _myOCVObj.StdThreshThr;
            double req_maxval = _myOCVObj.StdThreshMaxv;
            ThresholdTypes req_ThresholdType = _myOCVObj.StdThres_tt;
            Cv2.Threshold(argMat, argMat, req_threshhold, req_maxval, req_ThresholdType);
        }
        void Use_Adaptive_threshhold(Mat argMat)
        {
            double req_threshhold = _myOCVObj.StdThreshThr;
            double req_maxval = _myOCVObj.StdThreshMaxv;
            AdaptiveThresholdTypes req_ThresholdType = _myOCVObj.AdaptiveThresh_at;
            ThresholdTypes req_ThresholdType2 = _myOCVObj.AdaptiveThresh_tt;
            int req_blocksize = _myOCVObj.AdaptiveThreshBlockSize;
            double req_c = _myOCVObj.AdaptiveThreshC;
            Cv2.AdaptiveThreshold(argMat, argMat, req_maxval, req_ThresholdType, req_ThresholdType2, req_blocksize, req_c);
        }

        void new_StaticValuesMethod(Mat argMat)
        {
            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;
            int req_strip_SR_margin = G_Strip_R_Margin; // 4
            int req_strip_SR_Width = G_Strip_R_width;   // 340
            if ((req_strip_SR_margin + req_strip_SR_Width) > (widthOftheMat - 1))
            {
                req_strip_SR_margin = 0;
                req_strip_SR_Width = widthOftheMat - 1;
            }
            int stripRect_X = widthOftheMat - (req_strip_SR_Width + req_strip_SR_margin);
            int req_strip_SR_height = G_Strip_R_height; // 100
            int req_strip_SR_BotMargin = G_Strip_R_botMargin; // 4
            if (req_strip_SR_BotMargin + req_strip_SR_height > (heightOfMat - 1))
            {
                req_strip_SR_BotMargin = 0;
                req_strip_SR_height = heightOfMat - 1;
            }
            int stripRect_Y = heightOfMat - (req_strip_SR_height + req_strip_SR_BotMargin);
            int stripRect_Width = req_strip_SR_Width;
            int stripRect_Height = req_strip_SR_height;
            _farRighrStripRect = new Rect(stripRect_X, stripRect_Y, stripRect_Width, stripRect_Height);



            Cv2.Rectangle(argMat, _farRighrStripRect, H_GetRandom_CyanScalar(), 3);
            Mat roiMat_SR = new Mat(argMat, _farRighrStripRect);
            ptr_mat_arra[1] = roiMat_SR;
        }

        Point ConvertPointFromRoiStripRs_to_originalMat(Point argPoint)
        {
            Point newPoint = new Point(argPoint.X + _farRighrStripRect.X, argPoint.Y + _farRighrStripRect.Y);
            return newPoint;
        }
        Rect ConvertRectFromRoiStripRs_to_originalMat(Rect argRect)
        {
            Rect newRect = new Rect(argRect.X + _farRighrStripRect.X, argRect.Y + _farRighrStripRect.Y, argRect.Width, argRect.Height);
            return newRect;
        }

        void newStrightFowaardMethod(Mat argMat)
        {
            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;
            int req_strip_SR_margin = _myOCVObj.Strip_R_Margin;
            int req_strip_SR_Width = _myOCVObj.Strip_R_width;
            if ((req_strip_SR_margin + req_strip_SR_Width) > (widthOftheMat - 1))
            {
                req_strip_SR_margin = 0;
                req_strip_SR_Width = widthOftheMat - 1;
            }
            int stripRect_X = widthOftheMat - (req_strip_SR_Width + req_strip_SR_margin);
            int req_strip_SR_height = _myOCVObj.Strip_R_height;
            int req_strip_SR_BotMargin = _myOCVObj.Strip_R_botMargin;
            if (req_strip_SR_BotMargin + req_strip_SR_height > (heightOfMat - 1))
            {
                req_strip_SR_BotMargin = 0;
                req_strip_SR_height = heightOfMat - 1;
            }
            int stripRect_Y = heightOfMat - (req_strip_SR_height + req_strip_SR_BotMargin);
            int stripRect_Width = req_strip_SR_Width;
            int stripRect_Height = req_strip_SR_height;
            _farRighrStripRect = new Rect(stripRect_X, stripRect_Y, stripRect_Width, stripRect_Height);
            Cv2.Rectangle(argMat, _farRighrStripRect, H_GetRandom_BlueScalar(), 3);
            int req_blur_ksize = _myOCVObj.Blur_ksize;
            int req_multiplier = _myOCVObj.LinesP_thetaMultiplyer;
            //  double minlen_SR = _myOCVObj.Strip_R_minLen; //160
            // double Calcedminlen_SR = G_LinesP_R_minLen;
            // if (minlen_SR > (req_strip_SR_Width)) req_strip_SR_Width = req_strip_SR_Width - 10;

            double MaxPercentWidth = _myOCVObj.LinesP_PercentWidth;
            double linesP_maxWidthCuttoff = (req_strip_SR_Width * MaxPercentWidth) / 100;

            double MaxPercentHeight = _myOCVObj.LinesP_PercentHeight;
            double linesP_maxHeightCuttoff = (req_strip_SR_height * MaxPercentHeight) / 100;


            double maxGap_SR = 0;
            double req_MaxGap = _myOCVObj.Strip_R_maxGap;//req_strip_SR_Width / 6;
            if (req_MaxGap > (req_strip_SR_Width / 2)) maxGap_SR = req_MaxGap;
            else
                maxGap_SR = _myOCVObj.Strip_R_maxGap;

            double req_RHO = _myOCVObj.LinesP_rho;
            double req_minimumYGap_RS = _myOCVObj.LinesP_YGap;//  10;// _myOCVObj.Pox_total_index;
            int req_thershholdPlne = _myOCVObj.LinesP_Thr;
            double canny_thresh1 = _myOCVObj.CannyThr1; //50
            double canny_thresh2 = _myOCVObj.CannyThr2; //250

            double req_Sigmax = _myOCVObj.BlurSigmax;
            double req_Sigmay = _myOCVObj.BlurSigmay;

            double minimumYGap_RS = _myOCVObj.LinesP_YGap;
            if (minimumYGap_RS < 1) minimumYGap_RS = 1;
            if (minimumYGap_RS > 100) minimumYGap_RS = 100;

            double _myLineP_maxDx = _myOCVObj.LinesP_MaxDx;
            double _myLineP_maxDy = _myOCVObj.LinesP_MaxDy;

            Mat roiMat_SR = new Mat(argMat, _farRighrStripRect);
            Mat grayRoi_SR = new Mat();
            if (roiMat_SR.Channels() > 1)
            {
                Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayRoi_SR = roiMat_SR.Clone();
            }

            ptr_mat_arra[1] = roiMat_SR;
            enum_OCVBlurAction enum_OCVBlurTypes = _myOCVObj.Blur_ActionToUse;
            Mat blur_SR = new Mat();
            switch (enum_OCVBlurTypes)
            {
                case enum_OCVBlurAction.BLUR:
                    Use_BlurrDefault(grayRoi_SR, blur_SR);
                    break;
                case enum_OCVBlurAction.GAUSSIAN:
                    Use_BlurrGausian(grayRoi_SR, blur_SR);
                    break;
                case enum_OCVBlurAction.MEDIAN:
                    Use_BlurrMedian(grayRoi_SR, blur_SR);
                    break;
                case enum_OCVBlurAction.PYRDOWN:
                    Use_BlurrPYRdown(grayRoi_SR, blur_SR);
                    break;
                case enum_OCVBlurAction.PYRUP:
                    Use_BlurrPYRup(grayRoi_SR, blur_SR);
                    break;
                default:
                    Use_BlurrDefault(grayRoi_SR, blur_SR);
                    break;
            }
            Cv2.ImShow("blur_SR", blur_SR);
            enum_OCVThresholdAction enum_OCVThresholdTypes = _myOCVObj.ThresholdActionToUse;
            switch (enum_OCVThresholdTypes)
            {
                case enum_OCVThresholdAction.STD:
                    Use_DTD_threshhold(blur_SR);
                    break;
                case enum_OCVThresholdAction.ADAPTIVE:
                    Use_Adaptive_threshhold(blur_SR);
                    break;
                default:
                    Use_DTD_threshhold(blur_SR);
                    break;
            }


            Mat tempclone = blur_SR;//.Clone();
            Cv2.GaussianBlur(tempclone, tempclone, new OpenCvSharp.Size(5, 5), 1);

            Cv2.ImShow("thresh", tempclone);
            Mat canny = new Mat();
            Cv2.Canny(tempclone, canny, canny_thresh1, canny_thresh2, 3);
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(canny, req_RHO, req_multiplier * Math.PI / 180, req_thershholdPlne, minLineLength: linesP_maxWidthCuttoff, maxLineGap: req_MaxGap);



            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0;
            int LatsLineP2Y_RS = 0;

            Cv2.ImShow("canny", canny);
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < _myLineP_maxDy) // Adjust the threshold as needed
                {
                    //if (Math.Abs(dx_sr) > linesP_maxWidthCuttoff)
                    //{
                    if (line_sr.P2.Y > LatsLineP2Y_RS + minimumYGap_RS)
                    {
                        LatsLineP2Y_RS = line_sr.P2.Y;
                        Point pt1_sr = new Point(line_sr.P1.X + _farRighrStripRect.X, line_sr.P1.Y + _farRighrStripRect.Y);
                        Point pt2_Sr = new Point(line_sr.P2.X + _farRighrStripRect.X, line_sr.P2.Y + _farRighrStripRect.Y);
                        EventsManagerLib.Call_LogConsole("line " + linenum_SR + "  dx: " + dx_sr + " P2Y =" + pt2_Sr.Y);
                        if (linenum_SR == 0)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_RedScalar(), 3);
                        }
                        else if (linenum_SR == 1)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_GreenScalar(), 3);
                        }
                        else if (linenum_SR == 2)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_BlueScalar(), 3);
                        }
                        else
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_OrangeScalar(), 3);

                        }
                        linenum_SR++;
                    }
                    else
                    {
                        //too close to each other on y axis
                    }

                    // }
                    //  else
                    // {
                    //smaller than required length
                    // }
                    //}
                }
            }

        }


        void HArdCodedVals(Mat argMat)
        {
            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;
            int req_strip_SR_margin = 4;
            int req_strip_SR_Width = 340;
            if ((req_strip_SR_margin + req_strip_SR_Width) > (widthOftheMat - 1))
            {
                req_strip_SR_margin = 0;
                req_strip_SR_Width = widthOftheMat - 1;
            }
            int stripRect_X = widthOftheMat - (req_strip_SR_Width + req_strip_SR_margin);
            int req_strip_SR_height = 178;
            int req_strip_SR_BotMargin = 21;
            if (req_strip_SR_BotMargin + req_strip_SR_height > (heightOfMat - 1))
            {
                req_strip_SR_BotMargin = 0;
                req_strip_SR_height = heightOfMat - 1;
            }
            int stripRect_Y = heightOfMat - (req_strip_SR_height + req_strip_SR_BotMargin);
            int stripRect_Width = req_strip_SR_Width;
            int stripRect_Height = req_strip_SR_height;
            Rect _tempfarRighrtS = new Rect(stripRect_X, stripRect_Y, stripRect_Width, stripRect_Height);
            Cv2.Rectangle(argMat, _tempfarRighrtS, H_GetRandom_BlueScalar(), 3);
            int req_blur_ksize = 19;
            int req_multiplier = 1;


            double MaxPercentWidth = 80.00;
            double linesP_maxWidthCuttoff = (req_strip_SR_Width * MaxPercentWidth) / 100;

            double MaxPercentHeight = 10;
            double linesP_maxHeightCuttoff = (req_strip_SR_height * MaxPercentHeight) / 100;


            double maxGap_SR = 0;
            double req_MaxGap = 6;
            if (req_MaxGap > (req_strip_SR_Width / 2)) maxGap_SR = req_MaxGap;
            else
                maxGap_SR = 6;

            double req_RHO = 1.18;
            double req_minimumYGap_RS = 20;// 
            int req_thershholdPlne = 100;
            double canny_thresh1 = 100; //50
            double canny_thresh2 = 250; //250

            double req_Sigmax = 1.119;
            double req_Sigmay = 1.538;

            double minimumYGap_RS = 20;
            if (minimumYGap_RS < 1) minimumYGap_RS = 1;
            if (minimumYGap_RS > 100) minimumYGap_RS = 100;

            double _myLineP_maxDx = 4;
            double _myLineP_maxDy = 4;

            Mat roiMat_SR = new Mat(argMat, _tempfarRighrtS);
            Mat grayRoi_SR = new Mat();
            if (roiMat_SR.Channels() > 1)
            {
                Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayRoi_SR = roiMat_SR.Clone();
            }

            ptr_mat_arra[1] = roiMat_SR;
            enum_OCVBlurAction enum_OCVBlurTypes = enum_OCVBlurAction.GAUSSIAN;
            Mat blur_SR = new Mat();
            Cv2.GaussianBlur(grayRoi_SR, blur_SR, new OpenCvSharp.Size(req_blur_ksize, req_blur_ksize), req_Sigmax, req_Sigmay);

            Cv2.ImShow("blur_SR", blur_SR);
            double req_threshhold = 200;
            double req_maxval = 255;

            AdaptiveThresholdTypes req_ThresholdType = AdaptiveThresholdTypes.GaussianC;
            ThresholdTypes req_ThresholdType2 = ThresholdTypes.Binary;
            int req_adaptive_blocksize = 3;
            double req_c = 0.084;
            Cv2.AdaptiveThreshold(blur_SR, blur_SR, req_maxval, req_ThresholdType, req_ThresholdType2, req_adaptive_blocksize, req_c);


            Mat tempclone = blur_SR;//.Clone();
            Cv2.GaussianBlur(tempclone, tempclone, new OpenCvSharp.Size(5, 5), 1);

            Cv2.ImShow("thresh", tempclone);
            Mat canny = new Mat();
            Cv2.Canny(tempclone, canny, canny_thresh1, canny_thresh2, 3);
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(canny, req_RHO, req_multiplier * Math.PI / 180, req_thershholdPlne, minLineLength: linesP_maxWidthCuttoff, maxLineGap: req_MaxGap);



            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0;
            int LatsLineP2Y_RS = 0;

            Cv2.ImShow("canny", canny);
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < _myLineP_maxDy) // Adjust the threshold as needed
                {
                    //if (Math.Abs(dx_sr) > linesP_maxWidthCuttoff)
                    //{
                    if (line_sr.P2.Y > LatsLineP2Y_RS + minimumYGap_RS)
                    {
                        LatsLineP2Y_RS = line_sr.P2.Y;
                        Point pt1_sr = new Point(line_sr.P1.X + _tempfarRighrtS.X, line_sr.P1.Y + _tempfarRighrtS.Y);
                        Point pt2_Sr = new Point(line_sr.P2.X + _tempfarRighrtS.X, line_sr.P2.Y + _tempfarRighrtS.Y);
                        EventsManagerLib.Call_LogConsole("line " + linenum_SR + "  dx: " + dx_sr + " P2Y =" + pt2_Sr.Y);
                        if (linenum_SR == 0)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_OrangeScalar(), 3);
                        }
                        else if (linenum_SR == 1)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_GreenScalar(), 3);
                        }
                        else if (linenum_SR == 2)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_RedScalar(), 3);
                        }
                        else
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_OrangeScalar(), 3);

                        }
                        linenum_SR++;
                    }
                    else
                    {
                        //too close to each other on y axis
                    }

                    // }
                    //  else
                    // {
                    //smaller than required length
                    // }
                    //}
                }
            }

        }

        void oldStrightFowaardMethod(Mat argMat)
        {

            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;

            int req_strip_SR_margin = _myOCVObj.Strip_R_Margin;
            int req_strip_SR_Width = _myOCVObj.Strip_R_width;

            if ((req_strip_SR_margin + req_strip_SR_Width) > (widthOftheMat - 1))
            {

                req_strip_SR_margin = 0;
                req_strip_SR_Width = widthOftheMat - 1;

            }

            int strip_SL_height = _myOCVObj.Strip_R_height;


            _farRighrStripRect = new Rect(widthOftheMat - (req_strip_SR_Width + req_strip_SR_margin), 0, req_strip_SR_Width, heightOfMat);

            //draw a circle at each corner of the rect
            Cv2.Circle(argMat, new Point(_farRighrStripRect.X, _farRighrStripRect.Y), 5, H_GetRandomColorScalar(), 3);
            Cv2.Circle(argMat, new Point(_farRighrStripRect.X + _farRighrStripRect.Width, _farRighrStripRect.Y), 5, H_GetRandomColorScalar(), 3);
            Cv2.Circle(argMat, new Point(_farRighrStripRect.X, _farRighrStripRect.Y + _farRighrStripRect.Height), 5, H_GetRandomColorScalar(), 3);
            Cv2.Circle(argMat, new Point(_farRighrStripRect.X + _farRighrStripRect.Width, _farRighrStripRect.Y + _farRighrStripRect.Height), 5, H_GetRandomColorScalar(), 3);


            Point tempXY = new Point(_myOCVObj.TempIntX_0_900, _myOCVObj.TempIntY_0_900);
            Cv2.Circle(argMat, tempXY, 5, H_GetRandomColorScalar(), 3);

            Cv2.Rectangle(argMat, _farRighrStripRect, H_GetRandom_BlueScalar(), 3);
            EventsManagerLib.Call_LogConsole("widthOftheMat  =  " + widthOftheMat);
            Mat roiMat_SR = new Mat(argMat, _farRighrStripRect);
            Mat grayRoi_SR = new Mat();
            if (roiMat_SR.Channels() > 1)
            {
                Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayRoi_SR = roiMat_SR.Clone();
            }

            Mat blur_SR = new Mat();
            int blur_ksize = _myOCVObj.Blur_ksize; //5
            Cv2.GaussianBlur(grayRoi_SR, blur_SR, new OpenCvSharp.Size(blur_ksize, blur_ksize), 1);
            Mat Dialate_SR = new Mat();
            Dialate_SR = blur_SR.Clone();
            Cv2.ImShow("Dialate", Dialate_SR);
            Mat edges_SR = new Mat();
            double canny_thresh1 = _myOCVObj.CannyThr1; //50
            double canny_thresh2 = _myOCVObj.CannyThr2; //250
            Cv2.Canny(Dialate_SR, edges_SR, canny_thresh1, canny_thresh2, 3);
            double minlen_SR = _myOCVObj.Strip_R_minLen; //160

            double Calcedminlen_SR = 160;

            if (minlen_SR > (req_strip_SR_Width)) req_strip_SR_Width = req_strip_SR_Width - 10;
            double maxGap_SR = 0;
            double req_MaxGap = _myOCVObj.Strip_R_maxGap;//  req_strip_SR_Width / 6;

            if (req_MaxGap > (req_strip_SR_Width / 2)) maxGap_SR = req_MaxGap;
            else

                maxGap_SR = _myOCVObj.Strip_R_maxGap; //req_strip_SR_Width / 6;

            double req_RHO = _myOCVObj.LinesP_rho;



            int req_multiplier = _myOCVObj.LinesP_thetaMultiplyer;

            int req_thershholdPlne = _myOCVObj.LinesP_Thr;
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(edges_SR, req_RHO, req_multiplier * Math.PI / 180, req_thershholdPlne, minLineLength: Calcedminlen_SR, maxLineGap: req_MaxGap);
            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0;
            int LatsLineP2Y_RS = 0;
            double minimumYGap_RS = _myOCVObj.LinesP_YGap;//  10;// _myOCVObj.Pox_total_index;
            if (minimumYGap_RS < 1) minimumYGap_RS = 1;
            if (minimumYGap_RS > 100) minimumYGap_RS = 100;
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < 4) // Adjust the threshold as needed
                {
                    double percentHeight_temp_sr = (line_sr.P2.Y / (double)heightOfMat) * 100;
                    int precent_int_floor_temp_sr = (int)Math.Floor(percentHeight_temp_sr);
                    if (precent_int_floor_temp_sr > 80)
                    {
                        int Eightypercent_ofWidth_sr = (req_strip_SR_Width * 80) / 100;
                        int ninetypercent_ofWidth_sr = (req_strip_SR_Width * 90) / 100;
                        int SeventyFivepercent_ofWidth = (req_strip_SR_Width * 75) / 100;

                        if (Math.Abs(dx_sr) > Eightypercent_ofWidth_sr)
                        {
                            if (line_sr.P2.Y > LatsLineP2Y_RS + minimumYGap_RS)
                            {
                                LatsLineP2Y_RS = line_sr.P2.Y;
                                Point pt1_sr = new Point(line_sr.P1.X + _farRighrStripRect.X, line_sr.P1.Y + _farRighrStripRect.Y);
                                Point pt2_Sr = new Point(line_sr.P2.X + _farRighrStripRect.X, line_sr.P2.Y + _farRighrStripRect.Y);
                                EventsManagerLib.Call_LogConsole("line " + linenum_SR + "  dx: " + dx_sr + " P2Y =" + pt2_Sr.Y);
                                if (linenum_SR == 0)
                                {
                                    Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_RedScalar(), 1);
                                }
                                else
                                {
                                    Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_GreenScalar(), 1);
                                }


                                linenum_SR++;
                            }
                            else
                            {
                                //too close to each other on y axis
                            }

                        }
                        else
                        {
                            //smaller than required length
                        }
                    }
                }
            }
        }



        void FindAndDrawLinesInROI_RightStrip_working(Mat argMat)
        {

            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;
            int strip_SR_margin = 14;
            int strip_SR_Width = 180;
            _farRighrStripRect = new Rect(widthOftheMat - (strip_SR_Width + strip_SR_margin), 0, strip_SR_Width, heightOfMat);
            EventsManagerLib.Call_LogConsole("widthOftheMat  =  " + widthOftheMat);
            Mat roiMat_SR = new Mat(argMat, _farRighrStripRect);
            Mat grayRoi_SR = new Mat();
            if (roiMat_SR.Channels() > 1)
            {
                Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayRoi_SR = roiMat_SR.Clone();
            }

            Mat blur_SR = new Mat();
            Cv2.GaussianBlur(grayRoi_SR, blur_SR, new OpenCvSharp.Size(5, 5), 1);
            Mat Dialate_SR = new Mat();
            Dialate_SR = blur_SR.Clone();
            Cv2.ImShow("Dialate", Dialate_SR);
            Mat edges_SR = new Mat();
            Cv2.Canny(Dialate_SR, edges_SR, 50, 250, 3);
            int minlen_SR = 160;
            int maxGap_SR = strip_SR_Width / 6;
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(edges_SR, 4, 5 * Math.PI / 180, 100, minLineLength: minlen_SR, maxLineGap: maxGap_SR);
            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0;
            int LatsLineP2Y_RS = 0;
            int minimumYGap_RS = 10;// _myOCVObj.Pox_total_index;
            if (minimumYGap_RS < 1) minimumYGap_RS = 1;
            if (minimumYGap_RS > 100) minimumYGap_RS = 100;
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < 4) // Adjust the threshold as needed
                {
                    double percentHeight_temp_sr = (line_sr.P2.Y / (double)heightOfMat) * 100;
                    int precent_int_floor_temp_sr = (int)Math.Floor(percentHeight_temp_sr);
                    if (precent_int_floor_temp_sr > 80)
                    {
                        int Eightypercent_ofWidth_sr = (strip_SR_Width * 80) / 100;
                        int ninetypercent_ofWidth_sr = (strip_SR_Width * 90) / 100;
                        int SeventyFivepercent_ofWidth = (strip_SR_Width * 75) / 100;

                        if (Math.Abs(dx_sr) > Eightypercent_ofWidth_sr)
                        {
                            if (line_sr.P2.Y > LatsLineP2Y_RS + minimumYGap_RS)
                            {
                                LatsLineP2Y_RS = line_sr.P2.Y;
                                Point pt1_sr = new Point(line_sr.P1.X + _farRighrStripRect.X, line_sr.P1.Y + _farRighrStripRect.Y);
                                Point pt2_Sr = new Point(line_sr.P2.X + _farRighrStripRect.X, line_sr.P2.Y + _farRighrStripRect.Y);
                                EventsManagerLib.Call_LogConsole("line " + linenum_SR + "  dx: " + dx_sr + " P2Y =" + pt2_Sr.Y);
                                if (linenum_SR == 0)
                                {
                                    Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_RedScalar(), 1);
                                }
                                else
                                {
                                    Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_GreenScalar(), 1);
                                }


                                linenum_SR++;
                            }
                            else
                            {
                                //too close to each other on y axis
                            }

                        }
                        else
                        {
                            //smaller than required length
                        }
                    }
                }
            }
        }

        void FindAndDrawLinesInROI_RightSrip_DYNA(Mat argMat)
        {

            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;
            int strip_SR_margin = _myOCVObj.Strip_R_Margin;
            int strip_SR_Width = _myOCVObj.Strip_R_width;
            _farRighrStripRect = new Rect(widthOftheMat - (strip_SR_Width + strip_SR_margin), 0, strip_SR_Width, heightOfMat);
            EventsManagerLib.Call_LogConsole("widthOftheMat  =  " + widthOftheMat);
            Mat roiMat_SR = new Mat(argMat, _farRighrStripRect);
            Mat grayRoi_SR = new Mat();
            if (roiMat_SR.Channels() > 1)
            {
                Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayRoi_SR = roiMat_SR.Clone();
            }

            Mat blur_SR = new Mat();
            Cv2.GaussianBlur(grayRoi_SR, blur_SR, new OpenCvSharp.Size(_myOCVObj.Blur_ksize, _myOCVObj.Blur_ksize), 1);
            Mat Dialate_SR = new Mat();
            Dialate_SR = blur_SR.Clone();
            Cv2.ImShow("Dialate", Dialate_SR);
            Mat edges_SR = new Mat();
            Cv2.Canny(Dialate_SR, edges_SR, 50, 250, 3);
            double minlen_SR = _myOCVObj.Strip_R_minLen;
            double maxGap_SR = _myOCVObj.Strip_R_maxGap;// strip_SR_Width / 6;
            double _calculatedtheta = _myOCVObj.LinesP_thetaMultiplyer * Math.PI / 180;
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(edges_SR, _myOCVObj.LinesP_rho, _calculatedtheta, _myOCVObj.LinesP_Thr, minLineLength: minlen_SR, maxLineGap: maxGap_SR);
            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0;
            int LatsLineP2Y_RS = 0;
            double minimumYGap_RS = _myOCVObj.LinesP_YGap;
            if (minimumYGap_RS < 1) minimumYGap_RS = 1;
            if (minimumYGap_RS > 100) minimumYGap_RS = 100;

            EventsManagerLib.Call_LogConsole(" min ygap " + minimumYGap_RS);
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < 4) // Adjust the threshold as needed
                {
                    double percentHeight_temp_sr = (line_sr.P2.Y / (double)heightOfMat) * 100;
                    int precent_int_floor_temp_sr = (int)Math.Floor(percentHeight_temp_sr);
                    if (precent_int_floor_temp_sr > 80)
                    {
                        int Eightypercent_ofWidth_sr = (strip_SR_Width * 80) / 100;
                        int ninetypercent_ofWidth_sr = (strip_SR_Width * 90) / 100;
                        int SeventyFivepercent_ofWidth = (strip_SR_Width * 75) / 100;

                        if (Math.Abs(dx_sr) > Eightypercent_ofWidth_sr)
                        {
                            if (line_sr.P2.Y > LatsLineP2Y_RS + minimumYGap_RS)
                            {
                                LatsLineP2Y_RS = line_sr.P2.Y;
                                Point pt1_sr = new Point(line_sr.P1.X + _farRighrStripRect.X, line_sr.P1.Y + _farRighrStripRect.Y);
                                Point pt2_Sr = new Point(line_sr.P2.X + _farRighrStripRect.X, line_sr.P2.Y + _farRighrStripRect.Y);
                                EventsManagerLib.Call_LogConsole("line " + linenum_SR + "  dx: " + dx_sr + " P2Y =" + pt2_Sr.Y);
                                if (linenum_SR == 0)
                                {
                                    Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_RedScalar(), 1);
                                }
                                else
                                {
                                    Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_GreenScalar(), 1);
                                }


                                linenum_SR++;
                            }
                            else
                            {
                                //too close to each other on y axis
                            }

                        }
                        else
                        {
                            //smaller than required length
                        }
                    }
                }
            }
        }

        void HardCodedVals(Mat argMat)
        {
            int widthOftheMat = argMat.Width;
            int heightOfMat = argMat.Height;
            int req_strip_SR_margin = 4;
            int req_strip_SR_Width = 340;
            if ((req_strip_SR_margin + req_strip_SR_Width) > (widthOftheMat - 1))
            {
                req_strip_SR_margin = 0;
                req_strip_SR_Width = widthOftheMat - 1;
            }
            int stripRect_X = widthOftheMat - (req_strip_SR_Width + req_strip_SR_margin);
            int req_strip_SR_height = 178;
            int req_strip_SR_BotMargin = 21;
            if (req_strip_SR_BotMargin + req_strip_SR_height > (heightOfMat - 1))
            {
                req_strip_SR_BotMargin = 0;
                req_strip_SR_height = heightOfMat - 1;
            }
            int stripRect_Y = heightOfMat - (req_strip_SR_height + req_strip_SR_BotMargin);
            int stripRect_Width = req_strip_SR_Width;
            int stripRect_Height = req_strip_SR_height;
            Rect _tempfarRighrtS = new Rect(stripRect_X, stripRect_Y, stripRect_Width, stripRect_Height);
            Cv2.Rectangle(argMat, _tempfarRighrtS, H_GetRandom_BlueScalar(), 3);
            int req_blur_ksize = 19;
            int req_multiplier = 1;


            double MaxPercentWidth = 80.00;
            double linesP_maxWidthCuttoff = (req_strip_SR_Width * MaxPercentWidth) / 100;

            double MaxPercentHeight = 10;
            double linesP_maxHeightCuttoff = (req_strip_SR_height * MaxPercentHeight) / 100;


            double maxGap_SR = 0;
            double req_MaxGap = 6;
            if (req_MaxGap > (req_strip_SR_Width / 2)) maxGap_SR = req_MaxGap;
            else
                maxGap_SR = 6;

            double req_RHO = 1.18;
            double req_minimumYGap_RS = 20;// 
            int req_thershholdPlne = 100;
            double canny_thresh1 = 100; //50
            double canny_thresh2 = 250; //250

            double req_Sigmax = 1.119;
            double req_Sigmay = 1.538;

            double minimumYGap_RS = 20;
            if (minimumYGap_RS < 1) minimumYGap_RS = 1;
            if (minimumYGap_RS > 100) minimumYGap_RS = 100;

            double _myLineP_maxDx = 4;
            double _myLineP_maxDy = 4;

            Mat roiMat_SR = new Mat(argMat, _tempfarRighrtS);
            Mat grayRoi_SR = new Mat();
            if (roiMat_SR.Channels() > 1)
            {
                Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayRoi_SR = roiMat_SR.Clone();
            }


            enum_OCVBlurAction enum_OCVBlurTypes = enum_OCVBlurAction.GAUSSIAN;
            Mat blur_SR = new Mat();
            Cv2.GaussianBlur(grayRoi_SR, blur_SR, new OpenCvSharp.Size(req_blur_ksize, req_blur_ksize), req_Sigmax, req_Sigmay);

            Cv2.ImShow("blur_SR", blur_SR);
            double req_threshhold = 200;
            double req_maxval = 255;

            AdaptiveThresholdTypes req_ThresholdType = AdaptiveThresholdTypes.GaussianC;
            ThresholdTypes req_ThresholdType2 = ThresholdTypes.Binary;
            int req_adaptive_blocksize = 3;
            double req_c = 0.084;
            Cv2.AdaptiveThreshold(blur_SR, blur_SR, req_maxval, req_ThresholdType, req_ThresholdType2, req_adaptive_blocksize, req_c);


            Mat tempclone = blur_SR;//.Clone();
            Cv2.GaussianBlur(tempclone, tempclone, new OpenCvSharp.Size(5, 5), 1);

            Cv2.ImShow("thresh", tempclone);
            Mat canny = new Mat();
            Cv2.Canny(tempclone, canny, canny_thresh1, canny_thresh2, 3);
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(canny, req_RHO, req_multiplier * Math.PI / 180, req_thershholdPlne, minLineLength: linesP_maxWidthCuttoff, maxLineGap: req_MaxGap);



            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0;
            int LatsLineP2Y_RS = 0;

            Cv2.ImShow("canny", canny);
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < _myLineP_maxDy) // Adjust the threshold as needed
                {
                    //if (Math.Abs(dx_sr) > linesP_maxWidthCuttoff)
                    //{
                    if (line_sr.P2.Y > LatsLineP2Y_RS + minimumYGap_RS)
                    {
                        LatsLineP2Y_RS = line_sr.P2.Y;
                        Point pt1_sr = new Point(line_sr.P1.X + _tempfarRighrtS.X, line_sr.P1.Y + _tempfarRighrtS.Y);
                        Point pt2_Sr = new Point(line_sr.P2.X + _tempfarRighrtS.X, line_sr.P2.Y + _tempfarRighrtS.Y);
                        EventsManagerLib.Call_LogConsole("line " + linenum_SR + "  dx: " + dx_sr + " P2Y =" + pt2_Sr.Y);
                        if (linenum_SR == 0)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_OrangeScalar(), 3);
                        }
                        else if (linenum_SR == 1)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_GreenScalar(), 3);
                        }
                        else if (linenum_SR == 2)
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_RedScalar(), 3);
                        }
                        else
                        {
                            Cv2.Line(argMat, pt1_sr, pt2_Sr, H_GetRandom_OrangeScalar(), 3);

                        }
                        linenum_SR++;
                    }
                    else
                    {
                        //too close to each other on y axis
                    }

                    // }
                    //  else
                    // {
                    //smaller than required length
                    // }
                    //}
                }
            }
            //use the array element 1 here _extracted_SR_mat = roiMat_SR.Clone();


        }

    }
}

