using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using PdfiumViewer;
using VCI_Forms_SPN._GLobalz;//for enevets manger lib
using Rect = OpenCvSharp.Rect;
namespace VCI_Forms_SPN._BackEndDataOBJs.OCVObjs
{
    public class MyPDF_toInnerFrameMat
    {
        System.Drawing.Image _latest_imageOfPDF = null;
        Mat _latest_MatOfPDF = null;
        Mat _Cloned_latestMat = null;
        Mat _Cloned_innerFrame = null;
        Mat _Cloned_Mat_PDF = null;
        Mat _Cloned_Mat_PDF_at1X = null;
        bool _pdfIsTypeA = true;
        bool _pdfIsTypeB = false;
        public Mat GET_0_formated_MatOf_pdf(string arg_pdfFilePath, bool arg_DoDebug, OCV_Filter_V2 argOtherObject)
        {
            double argpdfGrayThresh = argOtherObject.Pdf_0_GrayThr;
            double argpdfGrayMaxv = argOtherObject.Pdf_1_GrayMaxVal;
            int argDPI_needed = argOtherObject.PdfDPI_needed;
            int arg_widthNeeded = argOtherObject.PdfwidthNeeded;
            int arg_HeightNeeded = argOtherObject.PdfheightNeeded;
            ThresholdTypes argpdftt = argOtherObject.Pdf_tt;
            using (var document = PdfDocument.Load(arg_pdfFilePath))
            {
                var page = document.PageSizes[0];
                float pagewidthF = page.Width;
                float pageHeightF = page.Height;
                if (_curpdfWidth > 800)
                {
                    _pdfIsTypeA = false;
                    _pdfIsTypeB = true;
                }
                else
                {
                    _pdfIsTypeA = true;
                    _pdfIsTypeB = false;
                }
                bool needsRotation = page.Width < page.Height;
                if (needsRotation)
                {
                    EventsManagerLib.Call_PdfPageSizeRead(pageHeightF, pagewidthF, _pdfIsTypeA);
                    _latest_imageOfPDF = document.Render(0, arg_widthNeeded, arg_HeightNeeded, argDPI_needed, argDPI_needed, PdfRotation.Rotate270, PdfRenderFlags.None);
                }
                else
                {
                    EventsManagerLib.Call_PdfPageSizeRead(pagewidthF, pageHeightF, _pdfIsTypeA);
                    _latest_imageOfPDF = document.Render(0, arg_widthNeeded, arg_HeightNeeded, argDPI_needed, argDPI_needed, PdfRenderFlags.None);
                }
                _latest_MatOfPDF = ConvertImageToMat_v2(_latest_imageOfPDF);
                if (_Cloned_latestMat != null)
                {
                    _Cloned_latestMat.Dispose();
                }
                _Cloned_latestMat = _latest_MatOfPDF.Clone();
            }
            using (Mat gray_0big = new Mat())
            {
                Cv2.CvtColor(_Cloned_latestMat, gray_0big, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray_0big, gray_0big, argpdfGrayThresh, argpdfGrayMaxv, argpdftt);
                OpenCvSharp.Point[][] contours_big;
                HierarchyIndex[] hierarchy_big;
                Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                double maxArea = 0;
                Rect innerFrame = new Rect(20, 20, _Cloned_latestMat.Width - 20, _Cloned_latestMat.Height - 20);
                int maxAreaIdx = -1;
                int contoursFound = 0;
                contoursFound = contours_big.Length;
                for (int i = 0; i < contours_big.Length; i++)
                {
                    double area = Cv2.ContourArea(contours_big[i]);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        maxAreaIdx = i;
                    }
                }
                if (maxAreaIdx != -1)
                {
                    innerFrame = Cv2.BoundingRect(contours_big[maxAreaIdx]);
                }
                if (_Cloned_innerFrame != null)
                {
                    _Cloned_innerFrame.Dispose();
                }
                using (Mat cropped = new Mat(_Cloned_latestMat, innerFrame))
                {
                    _Cloned_innerFrame = cropped.Clone();
                }
            }
            return _Cloned_innerFrame;
        }
        float _curpdfWidth = 0;
        float _curpdfHeight = 0;
        float _curpdfWidthToHight_desiredRatio = 1.545454f;  //    is  17 x 11
        float _applied_width = 0;
        float _applied_height = 0;
        float _applied_width_at1x = 0;
        float _applied_height_at1x = 0;
        public float Get_PDF_applied_Width()
        {
            return _applied_width;
        }
        public float Get_PDF_applied_Height()
        {
            return _applied_height;
        }
        public Mat Get_Original_at1x()
        {
            return _Cloned_Mat_PDF_at1X;
        }
        public Mat Convert_PDF_TO_MAT_zoomdi(string arg_pdfFilePath, float argZoom, int argDPI)
        {
            if (argDPI < 1)
            {
                argDPI = 1;
            }
            if (argDPI > 600)
            {
                argDPI = 600;
            }
            if (argZoom < 0.001f)
            {
                argZoom = 0.001f;
            }
            if (argZoom > 20.0F)
            {
                argZoom = 20.0f;
            }
            using (var document = PdfDocument.Load(arg_pdfFilePath))
            {
                var page = document.PageSizes[0];
                float pagewidthF = page.Width;
                float pageHeightF = page.Height;
                bool needsRotation = page.Width < page.Height;
                PdfRotation _rotneeded = PdfRotation.Rotate0;
                if (needsRotation)
                {
                    _rotneeded = PdfRotation.Rotate270;
                    _curpdfWidth = pageHeightF;
                    _curpdfHeight = pagewidthF;
                }
                else
                {
                    _curpdfWidth = pagewidthF;
                    _curpdfHeight = pageHeightF;
                    _rotneeded = PdfRotation.Rotate0;
                }
                float temp_widthToHeightRatio = _curpdfWidth / _curpdfHeight;
                _curpdfWidthToHight_desiredRatio = temp_widthToHeightRatio;
                _applied_width = _curpdfWidth * argZoom;
                _applied_height = _curpdfHeight * argZoom;
                if (_curpdfWidth > 792)
                {
                    _pdfIsTypeA = false;
                    _pdfIsTypeB = true;
                }
                else
                {
                    _pdfIsTypeA = true;
                    _pdfIsTypeB = false;
                }
                _applied_width_at1x = _curpdfWidth * 1.0f;
                _applied_height_at1x = _curpdfHeight * 1.0f;
                EventsManagerLib.Call_PdfPageSizeRead(_applied_width, _applied_height, _pdfIsTypeA);
                _latest_imageOfPDF = document.Render(0, (int)_applied_width, (int)_applied_height, argDPI, argDPI, _rotneeded, PdfRenderFlags.None);
                _latest_MatOfPDF = ConvertImageToMat_v2(_latest_imageOfPDF);
                if (_Cloned_Mat_PDF != null)
                {
                    _Cloned_Mat_PDF.Dispose();
                }
                _Cloned_Mat_PDF = _latest_MatOfPDF.Clone();
                _latest_imageOfPDF = document.Render(0, (int)_applied_width_at1x, (int)_applied_height_at1x, argDPI, argDPI, _rotneeded, PdfRenderFlags.None);
                _latest_MatOfPDF = ConvertImageToMat_v2(_latest_imageOfPDF);
                if (_Cloned_Mat_PDF_at1X != null)
                {
                    _Cloned_Mat_PDF_at1X.Dispose();
                }
                _Cloned_Mat_PDF_at1X = _latest_MatOfPDF.Clone();
            }
            return _Cloned_Mat_PDF;
        }
        Mat ConvertImageToMat_v2(System.Drawing.Image image)
        {
            using (Bitmap bitmap = new Bitmap(image))
            {
                return BitmapConverter.ToMat(bitmap);
            }
        }
    }
}