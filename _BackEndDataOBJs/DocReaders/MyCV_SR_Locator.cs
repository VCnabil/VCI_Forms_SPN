using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using VCI_Forms_SPN._GLobalz;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public class MyCV_SR_Locator
    {
        OCV_Filter_V2 _myOCVObj;
        Mat _extracted_SR_mat;
        Mat _Cloned_SysDiag_Title_Mat_ForReading = null;
        Mat _Cloned_SysDiag_number_Mat_ForReading = null;
        public MyCV_SR_Locator()
        {
            _myOCVObj = new OCV_Filter_V2();
            _extracted_SR_mat = new Mat();
            EventsManagerLib.On_BROADCAST_OCVOBJevent += EventsManagerLib_On_BROADCAST_OCVOBJevent;
            EventsManagerLib.On_PdfPageSizeRead += EventsManagerLib_On_PdfPageSizeRead;
        }
        ~MyCV_SR_Locator()
        {
            EventsManagerLib.On_BROADCAST_OCVOBJevent -= EventsManagerLib_On_BROADCAST_OCVOBJevent;
            EventsManagerLib.On_PdfPageSizeRead -= EventsManagerLib_On_PdfPageSizeRead;
        }
        private void EventsManagerLib_On_PdfPageSizeRead(float arg_width, float arg_height, bool arg_isAtype)
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
                EventsManagerLib.Call_LogConsole("!!!!!!!!!!!!!!!!!111One of the OCV_filterObj instances is null.");
                return;
            }
            _myOCVObj.CopyDataFromOtherObject(arg_OCVfilterObj);
        }
        void HardCodedValsha(Mat argMat, int argx, int argy)
        {
            int widthOfMat = argMat.Width, heightOfMat = argMat.Height;
            int req_strip_SR_margin = (4 + 340) > (widthOfMat - 1) ? 0 : 4;
            int req_strip_SR_Width = (4 + 340) > (widthOfMat - 1) ? widthOfMat - 1 : 340;
            int req_strip_SR_height = 21 + 178 > (heightOfMat - 1) ? heightOfMat - 1 : 178;
            int req_strip_SR_BotMargin = 21 + 178 > (heightOfMat - 1) ? 0 : 21;
            Rect _tempfarRighrtS = new Rect(widthOfMat - (req_strip_SR_Width + req_strip_SR_margin), heightOfMat - (req_strip_SR_height + req_strip_SR_BotMargin), req_strip_SR_Width, req_strip_SR_height);
            Mat roiMat_SR = new Mat(argMat, _tempfarRighrtS), grayRoi_SR = new Mat(), blur_SR = new Mat(), canny = new Mat();
            if (roiMat_SR.Channels() > 1) Cv2.CvtColor(roiMat_SR, grayRoi_SR, ColorConversionCodes.BGR2GRAY);
            else grayRoi_SR = roiMat_SR.Clone();
            Cv2.GaussianBlur(grayRoi_SR, blur_SR, new OpenCvSharp.Size(19, 19), 1.119, 1.538);
            Cv2.AdaptiveThreshold(blur_SR, blur_SR, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 3, 0.084);
            Cv2.GaussianBlur(blur_SR, blur_SR, new OpenCvSharp.Size(5, 5), 1);
            Cv2.Canny(blur_SR, canny, 100, 250, 3);
            LineSegmentPoint[] lines_SR = Cv2.HoughLinesP(canny, 1.18, Math.PI / 180, 100, minLineLength: 340 * 0.8, maxLineGap: 6);
            LineSegmentPoint[] lines_RS_inOrderBy_P2Y = lines_SR.OrderBy(segment => segment.P2.Y).ToArray();
            int linenum_SR = 0, LatsLineP2Y_RS = 0;
            List<LineSegmentPoint> lines_SR_List = new List<LineSegmentPoint>();
            foreach (var line_sr in lines_RS_inOrderBy_P2Y)
            {
                int dx_sr = line_sr.P1.X - line_sr.P2.X;
                int dy_Sr = line_sr.P1.Y - line_sr.P2.Y;
                if (Math.Abs(dy_Sr) < Math.Abs(dx_sr) && Math.Abs(dy_Sr) < 4)
                {
                    if (line_sr.P2.Y > LatsLineP2Y_RS + 20)
                    {
                        LatsLineP2Y_RS = line_sr.P2.Y;
                        Point pt1_sr = new Point(line_sr.P1.X + _tempfarRighrtS.X, line_sr.P1.Y + _tempfarRighrtS.Y);
                        Point pt2_Sr = new Point(line_sr.P2.X + _tempfarRighrtS.X, line_sr.P2.Y + _tempfarRighrtS.Y);
                        if (linenum_SR == 0)
                        {
                            lines_SR_List.Add(line_sr);
                        }
                        else if (linenum_SR == 1)
                        {
                            lines_SR_List.Add(line_sr);
                        }
                        else if (linenum_SR == 2)
                        {
                            lines_SR_List.Add(line_sr);
                        }
                        else
                        {
                        }
                        linenum_SR++;
                    }
                    else
                    {
                    }
                }
            }
            if (lines_SR_List.Count > 2)
            {
                Rect rect_D = new Rect(_tempfarRighrtS.X, lines_SR_List[0].P1.Y + _tempfarRighrtS.Y, _tempfarRighrtS.Width, lines_SR_List[1].P2.Y - lines_SR_List[0].P1.Y);
                Rect rect_E = new Rect(_tempfarRighrtS.X, lines_SR_List[1].P1.Y + _tempfarRighrtS.Y, _tempfarRighrtS.Width, lines_SR_List[2].P2.Y - lines_SR_List[1].P1.Y);
                Mat mat_D_ = new Mat(argMat, rect_D); Mat mat_Dgray = new Mat(), mat_Dblur = new Mat(), mat_Dcanny = new Mat();
                Mat mat_D = mat_D_;
                if (mat_D.Channels() > 1) Cv2.CvtColor(mat_D, mat_Dgray, ColorConversionCodes.BGR2GRAY);
                else mat_Dgray = mat_D.Clone();
                Cv2.GaussianBlur(mat_Dgray, mat_Dblur, new OpenCvSharp.Size(19, 19), 1.119, 1.538);
                Cv2.AdaptiveThreshold(mat_Dblur, mat_Dblur, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 3, 0.084);
                Cv2.GaussianBlur(mat_Dblur, mat_Dblur, new OpenCvSharp.Size(5, 5), 1);
                Cv2.Canny(mat_Dblur, mat_Dcanny, 90, 250, 3);
                int length_isTheHeightofmatd = mat_D.Height;
                LineSegmentPoint[] lines_D = Cv2.HoughLinesP(mat_Dcanny, 1.18, 1 * Math.PI / 180, 35, minLineLength: length_isTheHeightofmatd * 0.8, maxLineGap: 6);
                var verticalLines = lines_D.Where(line => Math.Abs(line.P1.X - line.P2.X) < Math.Abs(line.P1.Y - line.P2.Y)).ToArray();
                verticalLines = verticalLines.OrderBy(segment => segment.P1.X).ToArray();
                List<LineSegmentPoint> verticalLinesList_filtered = new List<LineSegmentPoint>();
                EventsManagerLib.Call_LogConsole("verticalLines found  = " + verticalLines.Length);
                string str_All_P1X = "";
                int lastP1X = 0;
                foreach (var line in verticalLines)
                {
                    str_All_P1X += line.P1.X + " ";
                    if (line.P1.X > lastP1X + 30)
                    {
                        if (line.P1.X < mat_D.Width - 30 && line.P1.X > 30)
                        {
                            verticalLinesList_filtered.Add(line);
                        }
                    }
                    lastP1X = line.P1.X;
                }
                EventsManagerLib.Call_LogConsole(str_All_P1X);
                int _p1X = 0;
                if (verticalLinesList_filtered.Count > 0)
                {
                    _p1X = verticalLinesList_filtered[0].P1.X;
                }
                else
                {
                    _p1X = 0;
                }
                Mat Cropped_matD = new Mat(mat_D, new Rect(_p1X, 0, mat_D.Width - _p1X, mat_D.Height));
                _Cloned_SysDiag_Title_Mat_ForReading = Cropped_matD.Clone();
                Mat mat_E = new Mat(argMat, rect_E);
                Mat clonede = mat_E.Clone();
                Mat matToret = null;
                Mat grayImage = new Mat();
                Cv2.CvtColor(clonede, grayImage, ColorConversionCodes.BGR2GRAY);
                Mat blurredImage = new Mat();
                Cv2.GaussianBlur(grayImage, blurredImage, new OpenCvSharp.Size(0, 0), 3);
                Mat sharpenedImage = new Mat();
                Cv2.AddWeighted(grayImage, 1.5, blurredImage, -0.5, 0, sharpenedImage);
                matToret = sharpenedImage.Clone();
                _Cloned_SysDiag_number_Mat_ForReading = clonede;
                Cv2.ImShow("mat_E", matToret);
            }
            else
            {
            }
            _extracted_SR_mat = roiMat_SR.Clone();
        }
        public Mat Get_ROI_SR_Mat_fromInnerFrame(Mat argMat)
        {
            HardCodedValsha(argMat, 0, 0);
            return _extracted_SR_mat;
        }
        public Mat Get_clonedSysDiagTitle()
        {
            return _Cloned_SysDiag_Title_Mat_ForReading;
        }
        public Mat Get_clonedSysDiagNumber()
        {
            return _Cloned_SysDiag_number_Mat_ForReading;
        }
    }
}
