using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{

    public class OCV_LowerROIManager
    {
        OCV_TempDataObj _TempDataObj;
        Mat OriginalMatCloned = null;
        public OCV_LowerROIManager()
        {
            _TempDataObj = new OCV_TempDataObj();
        }
        public OCV_LowerROIManager(OCV_TempDataObj argDatz)
        {
            _TempDataObj = argDatz;
        }
        ~OCV_LowerROIManager()
        {

        }
     


        void Find_biggestCOntourOnMatAndDrawRedRect(Mat argMat)
        {

            if (argMat == null)
            {
                return;

            }
            Mat gray_0big = new Mat();
            Cv2.CvtColor(argMat, gray_0big, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray_0big, gray_0big, 200, 255, ThresholdTypes.BinaryInv);
            OpenCvSharp.Point[][] contours_big;
            HierarchyIndex[] hierarchy_big;
            List<Rect> _allRects = new List<Rect>();
            Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            if (contours_big.Length == 0)
            {
                return;
            }

            List<Rect> _allRects_big = new List<Rect>();

            //get largest contour from _allRects_big
            Rect _largestRect = new Rect();
            double _largestArea = 0;

            for (int i = 0; i < contours_big.Length; i++)
            {
                Rect _rect = Cv2.BoundingRect(contours_big[i]);
                _allRects_big.Add(_rect);
                //draw the bounding rect in magenta 
                // Cv2.Rectangle(arg_Mat, _rect, H_GetRandom_MagentaScalar(), 1);
                if ((_rect.Width * _rect.Height) > _largestArea)
                {
                    _largestArea = _rect.Width * _rect.Height;
                    _largestRect = _rect;
                }
            }

            Cv2.Rectangle(argMat, _largestRect, H_GetRandom_WhiteColorScalar(), 2);

            //print the nmber of rects found at top Left dorner of mat in red 
            // old way Cv2.PutText(arg_Mat, "Rects Found: " + _allRects_big.Count, new Point(_widthOfMat - 100, 20), HersheyFonts.HersheySimplex, 0.5, H_GetRandom_RedScalar(), 1);

            // Cv2.PutText(argMat, "Rct under : " + _rects_whosY_Under_HorizontalLimit_Y.Count, new Point(10, 10), HersheyFonts.HersheySimplex, 0.5, H_GetRandom_RedScalar(), 1);


        }
    }
}
