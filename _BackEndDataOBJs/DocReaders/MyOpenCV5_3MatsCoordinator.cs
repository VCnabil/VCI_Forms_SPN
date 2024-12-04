using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCI_Forms_SPN._GLobalz;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public class MyOpenCV5_3MatsCoordinator
    {
        //const and dest 
        public MyOpenCV5_3MatsCoordinator()
        {
            //init
        }
        ~MyOpenCV5_3MatsCoordinator()
        {
            //dispose
        }
        Mat OriginalMatCloned = null;
        Mat ptr_MatLow_L = null;
        Mat ptr_MatLow_R = null;
        bool _isTypeA = true;
        List<Mat> matsToRead_ifo_oTitle_1_P_2Number;
        Dictionary<int, List<Mat>> _dictOfColumns_tables;
        List<Rect> _Rows_RECTs;
        List<Mat> _Rows_Mats;
        List<Mat> _ColumnITEMS_Mats;
        List<Rect> _ColumnITEMS_RECTs;
        List<Mat> _ColumnItemNumbers_Mats;
        List<Rect> _ColumnItemNumbers_RECTs;
        List<List<Mat>> _ColumnItem_And_Numbers_Mats_List;
        public List<Mat> GetMatsToRead_ifo_oTitle_1_P_2Number()
        {
            return matsToRead_ifo_oTitle_1_P_2Number;
        }
        public List<Mat> Get_MatsToRead_TableRows()
        {
            return _Rows_Mats;
        }
        public List<List<Mat>> Get_MatsToRead_ColumnItems_andColumnNumners()
        {
            if (_ColumnItem_And_Numbers_Mats_List == null)
            {
                _ColumnItem_And_Numbers_Mats_List = new List<List<Mat>>();
            }
            else
            {
                _ColumnItem_And_Numbers_Mats_List.Clear();
            }
            _ColumnItem_And_Numbers_Mats_List.Add(_ColumnITEMS_Mats);
            _ColumnItem_And_Numbers_Mats_List.Add(_ColumnItemNumbers_Mats);
            return _ColumnItem_And_Numbers_Mats_List;
        }
        public void FeedmeA_LargeMAT_Icloneit_andMake_LandR_mats(Mat arg_Mat, bool arg_isTypeA, float a_pctVert_L, float a_pct_Hori_L, float a_pctVert_R, float a_pct_Hori_R)
        {
            EventsManagerLib.Call_LogConsole(" I hope we got type A or B by now   is A? " + arg_isTypeA);
            _isTypeA = arg_isTypeA; ;
            Mat tempClone = arg_Mat.Clone();
            Mat gray_0big = new Mat();
            Cv2.CvtColor(tempClone, gray_0big, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray_0big, gray_0big, 200, 250, ThresholdTypes.BinaryInv);
            OpenCvSharp.Point[][] contours_big;
            HierarchyIndex[] hierarchy_big;
            Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            double maxArea = 0;
            Rect innerFrame_RECT = new Rect(20, 20, tempClone.Width - 20, tempClone.Height - 20);
            int maxAreaIdx = -1;
            int contoursFound = 0;
            contoursFound = contours_big.Length;
            for (int i = 0; i < contours_big.Length; i++)
            {
                double area = Cv2.ContourArea(contours_big[i]);
                if (area < 6000)
                {
                    continue;
                }
                if (area > maxArea)
                {
                    maxArea = area;
                    maxAreaIdx = i;
                }
            }
            if (maxAreaIdx != -1)
            {
                innerFrame_RECT = Cv2.BoundingRect(contours_big[maxAreaIdx]);
            }
            if (OriginalMatCloned != null)
            {
                OriginalMatCloned.Dispose();
            }
            OriginalMatCloned = tempClone.Clone(innerFrame_RECT);
            Mat argMat = OriginalMatCloned;
            if (ptr_MatLow_L != null)
            {
                ptr_MatLow_L.Dispose();
            }
            int _Horizontal_CropLine_Ypos_L = (int)(argMat.Height - (argMat.Height * a_pctVert_L / 100));
            int _Vertical_CropLine_Xpos_L = (int)(argMat.Width * a_pct_Hori_L / 100);
            //make roi for left 
            ptr_MatLow_L = argMat.Clone(new Rect(0, _Horizontal_CropLine_Ypos_L, _Vertical_CropLine_Xpos_L, argMat.Height - _Horizontal_CropLine_Ypos_L));
            if (ptr_MatLow_R != null)
            {
                ptr_MatLow_R.Dispose();
            }
            int _Horizontal_CropLine_Ypos_R = (int)(argMat.Height - (argMat.Height * a_pctVert_R / 100));
            int _Vertical_CropLine_Xpos_R = (int)(argMat.Width * a_pct_Hori_R / 100);
            //make roi for right
            ptr_MatLow_R = argMat.Clone(new Rect(_Vertical_CropLine_Xpos_R, _Horizontal_CropLine_Ypos_R, argMat.Width - _Vertical_CropLine_Xpos_R, argMat.Height - _Horizontal_CropLine_Ypos_R));
            //draw rect rois on the original mat
            Scalar Color_roiL_A = H_GetRandom_RedScalar();
            Scalar Color_roiR_A = H_GetRandom_YellowScalar();
            Scalar Color_roiL_B = H_GetRandom_BlueScalar();
            Scalar Color_roiR_B = H_GetRandom_GreenScalar();
            Scalar ColorTouSe_L = Color_roiL_A;
            Scalar ColorTouSe_R = Color_roiR_A;
            if (!_isTypeA)
            {
                ColorTouSe_L = Color_roiL_B;
                ColorTouSe_R = Color_roiR_B;
            }
            // Cv2.Rectangle(OriginalMatCloned, new Rect(0, _Horizontal_CropLine_Ypos_L, _Vertical_CropLine_Xpos_L, argMat.Height - _Horizontal_CropLine_Ypos_L), ColorTouSe_L, 4);
            // Cv2.Rectangle(OriginalMatCloned, new Rect(_Vertical_CropLine_Xpos_R, _Horizontal_CropLine_Ypos_R, argMat.Width - _Vertical_CropLine_Xpos_R, argMat.Height - _Horizontal_CropLine_Ypos_R), ColorTouSe_R, 4);
        }
        public void Identify_Tables_byRows_()
        {
            //*************************************************************************************************************************************************************************************
            //float ptr_MatLow_L_width = (float)ptr_MatLow_L.Width;
            //float ptr_MatLow_L_height = (float)ptr_MatLow_L.Height;
            //float _percentage_of_height_of_arg_Hmin = (float)arg_Hmin / ptr_MatLow_L_height * 100.00f;
            //float _percentage_of_height_of_arg_Hmax = (float)arg_Hmax / ptr_MatLow_L_height * 100.00f;
            //float _percentage_of_width_of_arg_Wmin = (float)arg_Wmin / ptr_MatLow_L_width * 100.00f;
            //float _percentage_of_width_of_arg_Wmax = (float)arg_Wmax / ptr_MatLow_L_width * 100.00f;
            //string _widthCalculations = "Wmin " + arg_Wmin + " = " + _percentage_of_width_of_arg_Wmin + " %  " + "    Wmax " + arg_Wmax + " = " + _percentage_of_width_of_arg_Wmax + " %  ";
            //string _heightCalculations = "Hmin " + arg_Hmin + " = " + _percentage_of_height_of_arg_Hmin + " %  " + "    Hmax " + arg_Hmax + " = " + _percentage_of_height_of_arg_Hmax + " %  ";
            //EventsManagerLib.Call_LogConsole(_widthCalculations);
            //EventsManagerLib.Call_LogConsole(_heightCalculations);
            //this yields the following 
            // Wmin 55 = 7.227332 %      Wmax 592 = 77.79237 %  
            // Hmin 11 = 2.075472 % Hmax 30 = 5.660378 %
            //*************************************************************************************************************************************************************************************
            float arg_Wmin = 1;
            float arg_Wmax = 2;
            float arg_Hmin = 1;
            float arg_Hmax = 2;
            arg_Wmin = 7.227332f * ptr_MatLow_L.Width / 100.000f;
            arg_Wmax = 77.79237f * ptr_MatLow_L.Width / 100.000f;
            arg_Hmin = 2.075472f * ptr_MatLow_L.Height / 100.000f;
            arg_Hmax = 5.660378f * ptr_MatLow_L.Height / 100.000f;
            int _xBoundaryMax_ofTheTBL = 0;
            int _xBoundaryMin_ofTheTBL = 0;
            int _LowestRow_NumberOfElements = 0;
            int _yBoundaryMax_ofTheTBL = 0;
            if (_Rows_Mats != null)
            {
                _Rows_Mats.Clear();
            }
            else
            {
                _Rows_Mats = new List<Mat>();
            }
            using (Mat gray_0big = new Mat())
            {
                Cv2.CvtColor(ptr_MatLow_L, gray_0big, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray_0big, gray_0big, 200, 255, ThresholdTypes.BinaryInv);
                OpenCvSharp.Point[][] contours_big;
                HierarchyIndex[] hierarchy_big;
                List<Rect> _allRects = new List<Rect>();
                Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                if (contours_big.Length == 0)
                {
                    return;
                }
                //sort contours by destance from the bottom of the top of image first
                contours_big = contours_big.OrderByDescending(x => Cv2.BoundingRect(x).Y).ToArray();
                for (int r = 0; r < contours_big.Length; r++)
                {
                    Rect _rect = Cv2.BoundingRect(contours_big[r]);
                    if (_rect.Height > arg_Hmin && _rect.Height < arg_Hmax && _rect.Width > arg_Wmin && _rect.Width < arg_Wmax)
                    {
                        _allRects.Add(_rect);
                    }
                }
                List<Rect> _LowestRects = new List<Rect>();
                for (int i = 0; i < _allRects.Count; i++)
                {
                    if (_allRects[i].Y + _allRects[i].Height >= ptr_MatLow_L.Height - 10)
                    {
                        _LowestRects.Add(_allRects[i]);
                    }
                }
                _LowestRow_NumberOfElements = _LowestRects.Count;
                if (_LowestRects.Count == 0)
                {
                    return;
                }
                //order the rects by X from lowest to highest
                _LowestRects = _LowestRects.OrderBy(x => x.X).ToList();
                _xBoundaryMin_ofTheTBL = _LowestRects[0].X;
                _xBoundaryMax_ofTheTBL = _LowestRects[_LowestRects.Count - 1].X + _LowestRects[_LowestRects.Count - 1].Width;
                //draw vertical lines on the image at the x boundaries
                Cv2.Line(ptr_MatLow_L, new Point(_xBoundaryMin_ofTheTBL, 0), new Point(_xBoundaryMin_ofTheTBL, ptr_MatLow_L.Height), H_GetRandom_BlueScalar(), 4);
                Cv2.Line(ptr_MatLow_L, new Point(_xBoundaryMax_ofTheTBL, 0), new Point(_xBoundaryMax_ofTheTBL, ptr_MatLow_L.Height), H_GetRandom_BlueScalar(), 4);
                // make a dictionary of int List<Rect> where the key is the Y of the rect
                Dictionary<int, List<Rect>> _dictOfRows = new Dictionary<int, List<Rect>>();
                for (int i = 0; i < _allRects.Count; i++)
                {
                    if (_dictOfRows.ContainsKey(_allRects[i].Y))
                    {
                        _dictOfRows[_allRects[i].Y].Add(_allRects[i]);
                    }
                    else
                    {
                        _dictOfRows[_allRects[i].Y] = new List<Rect>();
                        _dictOfRows[_allRects[i].Y].Add(_allRects[i]);
                    }
                }
                //remove the lists that contain only one rect
                List<int> _keysToRemove = new List<int>();
                foreach (KeyValuePair<int, List<Rect>> entry in _dictOfRows)
                {
                    if (entry.Value.Count != _LowestRow_NumberOfElements)
                    {
                        _keysToRemove.Add(entry.Key);
                    }
                }
                for (int i = 0; i < _keysToRemove.Count; i++)
                {
                    _dictOfRows.Remove(_keysToRemove[i]);
                }
                //sort the dictionary by key from lowest to highest
                _dictOfRows = _dictOfRows.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                // _yBoundaryMax_ofTheTBL is the the largest Y 
                _yBoundaryMax_ofTheTBL = _dictOfRows.ElementAt(0).Key;
                //draw horizontal lines on the image at the y boundaries
                Cv2.Line(ptr_MatLow_L, new Point(0, _yBoundaryMax_ofTheTBL), new Point(ptr_MatLow_L.Width, _yBoundaryMax_ofTheTBL), H_GetRandom_BlueScalar(), 4);
                //dreaw each row in a different color
                int _colorIndex = 0;
                int _rowX = 0;
                int _rowY = 0;
                int _rowHeight = 0;
                int _rowWidth = 0;
                int _curLen = 0;
                //foreach (KeyValuePair<int, List<Rect>> entry in _dictOfRows)
                //{
                //    _colorIndex++;
                //    if (_colorIndex > 6)
                //    {
                //        _colorIndex = 0;
                //    }
                //    Scalar _color = H_GetRandom_ColorScalar(_colorIndex);
                //    _curLen = 0;
                //    _rowX = 0;
                //    _rowY = 0;
                //    _rowHeight = 0;
                //    _rowWidth = 0;
                //    _rowX = _xBoundaryMin_ofTheTBL;
                //    _rowY = entry.Key;
                //    _rowHeight = entry.Value[0].Height;
                //    _rowWidth = _xBoundaryMax_ofTheTBL - _xBoundaryMin_ofTheTBL;
                //    Rect _rowRect = new Rect(_rowX, _rowY, _rowWidth, _rowHeight);
                //    Cv2.Rectangle(ptr_MatLow_L, _rowRect, _color, 4);
                //}
                List<int> mykeys = new List<int>(_dictOfRows.Keys); // Get all the keys from the dictionary and put them in a list
                for (int i = 0; i < mykeys.Count; i++)
                {
                    _colorIndex++;
                    if (_colorIndex > 6)
                    {
                        _colorIndex = 0;
                    }
                    Scalar _color = H_GetRandom_ColorScalar(_colorIndex); // Assuming this function returns a Scalar based on the given index
                    _curLen = 0; // Assuming these variables are declared and used elsewhere in your code
                    _rowX = 0;
                    _rowY = 0;
                    _rowHeight = 0;
                    _rowWidth = 0;
                    _rowX = _xBoundaryMin_ofTheTBL; // Assuming _xBoundaryMin_ofTheTBL is declared and initialized
                    _rowY = mykeys[i]; // Use the current key as _rowY
                    _rowHeight = _dictOfRows[mykeys[i]][0].Height; // Access the first Rect's Height in the List<Rect> for the current key
                    _rowWidth = _xBoundaryMax_ofTheTBL - _xBoundaryMin_ofTheTBL; // Assuming _xBoundaryMax_ofTheTBL is declared and initialized
                    Rect _rowRect = new Rect(_rowX, _rowY, _rowWidth, _rowHeight);
                    //ROI
                    Mat _roi = new Mat(ptr_MatLow_L, _rowRect);
                    _Rows_Mats.Add(_roi);
                    Cv2.Rectangle(ptr_MatLow_L, _rowRect, _color, 4); // Draw the rectangle on your image/mat
                }
                return;
            }
        }
        public void Identify_SysDiagInfo()
        {
            Rect SystTITLE = new Rect();
            Rect SYS_P = new Rect();
            Rect SYS_Number = new Rect();
            if (matsToRead_ifo_oTitle_1_P_2Number != null)
            {
                matsToRead_ifo_oTitle_1_P_2Number.Clear();
            }
            else
            {
                matsToRead_ifo_oTitle_1_P_2Number = new List<Mat>();
            }
            using (Mat gray_0big = new Mat())
            {
                Cv2.CvtColor(ptr_MatLow_R, gray_0big, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray_0big, gray_0big, 200, 255, ThresholdTypes.BinaryInv);
                OpenCvSharp.Point[][] contours_big;
                HierarchyIndex[] hierarchy_big;
                Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                //sort contours by destance from the bottom of the top of image first
                contours_big = contours_big.OrderByDescending(x => Cv2.BoundingRect(x).Y).ToArray();
                List<Rect> _recs_whosAre_is_filtered = new List<Rect>();
                //if (arg_Hmin >= 95)
                //{
                //    arg_Hmin = 95;
                //}
                //if (arg_Hmin >= 10) {
                //    arg_Hmin = 10;
                //}
                float _percentageOfHeight = 11.00f; // (float)arg_Hmin;//
                float MinHeight = ptr_MatLow_R.Height * _percentageOfHeight / 100.00f;
                int _rectsCount = 0;
                _rectsCount = contours_big.Length;
                for (int r = 0; r < _rectsCount; r++)
                {
                    Rect _rect = Cv2.BoundingRect(contours_big[r]);
                    if (_rect.Height > MinHeight && _rect.Height < ptr_MatLow_R.Height - 20)
                    {
                        _recs_whosAre_is_filtered.Add(_rect);
                        EventsManagerLib.Call_LogConsole("rect  area " + (_rect.Width * _rect.Height) + "   x." + _rect.X + " y." + _rect.Y + " " + _rect.Width + "x" + _rect.Height);
                    }
                }
                //order by area 
                _recs_whosAre_is_filtered = _recs_whosAre_is_filtered.OrderByDescending(x => x.Width * x.Height).ToList();
                List<Rect> _listOFOrderedRects_justFirstItem = new List<Rect>();
                List<Rect> _listOFOrderedRects_Rest = new List<Rect>();
                int LowLimit = 0;
                if (_recs_whosAre_is_filtered.Count > 0)
                {
                    LowLimit = _recs_whosAre_is_filtered[0].Y + _recs_whosAre_is_filtered[0].Height;
                }
                //draw the horizontal line 
                Cv2.Line(ptr_MatLow_R, new Point(0, LowLimit - 4), new Point(ptr_MatLow_R.Width, LowLimit - 4), H_GetRandom_BlueScalar(), 1);
                Cv2.Line(ptr_MatLow_R, new Point(0, LowLimit + 4), new Point(ptr_MatLow_R.Width, LowLimit + 4), H_GetRandom_BlueScalar(), 1);
                for (int i = 0; i < _recs_whosAre_is_filtered.Count; i++)
                {
                    if (i == 0)
                    {
                        SystTITLE = _recs_whosAre_is_filtered[i];
                        _listOFOrderedRects_justFirstItem.Add(_recs_whosAre_is_filtered[i]);// Cv2.Rectangle(ptr_MatLow_R, _recs_whosAre_is_filtered[0], H_GetRandom_BlueScalar(), 2);
                    }
                    else
                    {
                        if (_recs_whosAre_is_filtered[i].Y > (LowLimit - 4) && _recs_whosAre_is_filtered[i].Y < (LowLimit + 4))
                        {
                            _listOFOrderedRects_Rest.Add(_recs_whosAre_is_filtered[i]);
                        }
                    }
                }
                //order _listOFOrderedRects_Rest by largesr X to smallest X
                _listOFOrderedRects_Rest = _listOFOrderedRects_Rest.OrderByDescending(x => x.X).ToList();
                if (_listOFOrderedRects_Rest.Count >= 2)
                {
                    SYS_P = _listOFOrderedRects_Rest[0];
                    SYS_Number = _listOFOrderedRects_Rest[1];
                }
                else
                 if (_listOFOrderedRects_Rest.Count == 1)
                {
                    SYS_P = _listOFOrderedRects_Rest[0];
                    // SYS_Number = _listOFOrderedRects_Rest[0];
                }
                if (SystTITLE.Width > 0)
                {
                    Cv2.Rectangle(ptr_MatLow_R, SystTITLE, H_GetRandom_RedScalar(), 2);
                    //create ROi and add it to matsToRead   
                    Mat _roi = new Mat(ptr_MatLow_R, SystTITLE);
                    matsToRead_ifo_oTitle_1_P_2Number.Add(_roi);
                }
                if (SYS_P.Width > 0)
                {
                    Cv2.Rectangle(ptr_MatLow_R, SYS_P, H_GetRandom_BlueScalar(), 2);
                    Mat _roi = new Mat(ptr_MatLow_R, SYS_P);
                    matsToRead_ifo_oTitle_1_P_2Number.Add(_roi);
                }
                if (SYS_Number.Width > 0)
                {
                    Cv2.Rectangle(ptr_MatLow_R, SYS_Number, H_GetRandom_GreenScalar(), 2);
                    Mat _roi = new Mat(ptr_MatLow_R, SYS_Number);
                    matsToRead_ifo_oTitle_1_P_2Number.Add(_roi);
                }
            }
        }
        public Mat GET_Original()
        {
            return OriginalMatCloned;
        }
        public Mat Get_LowerROI_L()
        {
            return ptr_MatLow_L;
        }
        public Mat Get_LowerROI_R()
        {
            return ptr_MatLow_R;
        }
        public void IdentifyTBL_makeColumnMatLists(int arg_drawupto)
        {
            float arg_Wmin = 1;
            float arg_Wmax = 2;
            float arg_Hmin = 1;
            float arg_Hmax = 2;
            arg_Wmin = 6.227332f * ptr_MatLow_L.Width / 100.000f;
            arg_Wmax = 77.79237f * ptr_MatLow_L.Width / 100.000f;
            arg_Hmin = 2.075472f * ptr_MatLow_L.Height / 100.000f;
            arg_Hmax = 5.660378f * ptr_MatLow_L.Height / 100.000f;
            int _xBoundaryMax_ofTheTBL = 0;
            int _xBoundaryMin_ofTheTBL = 0;
            int _LowestRow_NumberOfElements = 0;
            int _yBoundaryMax_ofTheTBL = 0;
            using (Mat gray_0big = new Mat())
            {
                Cv2.CvtColor(ptr_MatLow_L, gray_0big, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray_0big, gray_0big, 200, 255, ThresholdTypes.BinaryInv);
                OpenCvSharp.Point[][] contours_big;
                HierarchyIndex[] hierarchy_big;
                List<Rect> _allRects = new List<Rect>();
                Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                if (contours_big.Length == 0)
                {
                    return;
                }
                //sort contours LeftToRigt and bottomtotop 
                contours_big = contours_big.OrderBy(x => Cv2.BoundingRect(x).Y).ToArray();
                contours_big = contours_big.OrderBy(x => Cv2.BoundingRect(x).X).ToArray();
                for (int r = 0; r < contours_big.Length; r++)
                {
                    Rect _rect = Cv2.BoundingRect(contours_big[r]);
                    if (_rect.Height > arg_Hmin && _rect.Height < arg_Hmax && _rect.Width > arg_Wmin && _rect.Width < arg_Wmax)
                    {
                        _allRects.Add(_rect);
                    }
                }
                if (_allRects.Count == 0)
                {
                    return;
                }
                Dictionary<int, int> _dictOf_key_RectY_valueHowManyRectsonKey = new Dictionary<int, int>();
                for (int i = 0; i < _allRects.Count; i++)
                {
                    if (_dictOf_key_RectY_valueHowManyRectsonKey.ContainsKey(_allRects[i].Y))
                    {
                        _dictOf_key_RectY_valueHowManyRectsonKey[_allRects[i].Y]++;
                    }
                    else
                    {
                        _dictOf_key_RectY_valueHowManyRectsonKey[_allRects[i].Y] = 1;
                    }
                }
                //find the largest value in the dictionary
                int _largestNumberOfRects_perY = 0;
                int _numberOfLegitRowsFound = 0;
                foreach (KeyValuePair<int, int> entry in _dictOf_key_RectY_valueHowManyRectsonKey)
                {
                    if (entry.Value > _largestNumberOfRects_perY)
                    {
                        _largestNumberOfRects_perY = entry.Value;
                    }
                }//----------------------------------------------------------------------------------------------------_largestNumberOfRects_perY is defind
                //remove the keys that do not have the largest number of rects
                List<int> _keysToRemove = new List<int>();
                foreach (KeyValuePair<int, int> entry in _dictOf_key_RectY_valueHowManyRectsonKey)
                {
                    if (entry.Value != _largestNumberOfRects_perY)
                    {
                        _keysToRemove.Add(entry.Key);
                    }
                }
                for (int i = 0; i < _keysToRemove.Count; i++)
                {
                    _dictOf_key_RectY_valueHowManyRectsonKey.Remove(_keysToRemove[i]);
                }
                _numberOfLegitRowsFound = _dictOf_key_RectY_valueHowManyRectsonKey.Count;//----------------------------- _numberOfLegitRowsFound is defind
                foreach (KeyValuePair<int, int> entry in _dictOf_key_RectY_valueHowManyRectsonKey)
                {
                    EventsManagerLib.Call_LogConsole("Key = " + entry.Key + ", Value = " + entry.Value);
                }
                //use _dictOf_key_RectY_valueHowManyRectsonKey to create a dictionary of int List<Rect> where the key is the Y of the rect
                Dictionary<int, List<Rect>> _organaizedRects_byRowsx_X = new Dictionary<int, List<Rect>>();
                for (int i = 0; i < _dictOf_key_RectY_valueHowManyRectsonKey.Count; i++)
                {
                    int theYforThisRow = _dictOf_key_RectY_valueHowManyRectsonKey.ElementAt(i).Key;
                    if (!_organaizedRects_byRowsx_X.ContainsKey(theYforThisRow))
                    {
                        _organaizedRects_byRowsx_X[theYforThisRow] = new List<Rect>();
                        for (int j = 0; j < _allRects.Count; j++)
                        {
                            if (_allRects[j].Y == theYforThisRow)
                            {
                                _organaizedRects_byRowsx_X[theYforThisRow].Add(_allRects[j]);
                            }
                        }
                        //sort the rects in the list by X from lowest to highest
                        _organaizedRects_byRowsx_X[theYforThisRow] = _organaizedRects_byRowsx_X[theYforThisRow].OrderBy(x => x.X).ToList();
                    }
                    else
                    {
                    }
                }
                //draw the horizontal lines on the image at the Y boundaries
                List<int> _keys = new List<int>(_organaizedRects_byRowsx_X.Keys);
                Cv2.Line(ptr_MatLow_L, new Point(0, _keys[0]), new Point(ptr_MatLow_L.Width, _keys[0]), H_GetRandom_GreenScalar(), 4);
                if (_largestNumberOfRects_perY == 0)
                {
                    return;
                }
                if (_ColumnITEMS_RECTs != null)
                {
                    _ColumnITEMS_RECTs.Clear();
                }
                else
                {
                    _ColumnITEMS_RECTs = new List<Rect>();
                }
                if (_ColumnITEMS_Mats != null)
                {
                    _ColumnITEMS_Mats.Clear();
                }
                else
                {
                    _ColumnITEMS_Mats = new List<Mat>();
                }
                if (_ColumnItemNumbers_RECTs != null)
                {
                    _ColumnItemNumbers_RECTs.Clear();
                }
                else
                {
                    _ColumnItemNumbers_RECTs = new List<Rect>();
                }
                if (_ColumnItemNumbers_Mats != null)
                {
                    _ColumnItemNumbers_Mats.Clear();
                }
                else
                {
                    _ColumnItemNumbers_Mats = new List<Mat>();
                }
                List<int> _XcoordinatesOfRectsinTopestRow = new List<int>();
                for (int i = 0; i < _organaizedRects_byRowsx_X[_keys[0]].Count; i++)
                {
                    _XcoordinatesOfRectsinTopestRow.Add(_organaizedRects_byRowsx_X[_keys[0]][i].X);
                }
                //create a dictionary of int List<Rect> where the key is the X of the rect
                Dictionary<int, List<Rect>> _dictOfColumns = new Dictionary<int, List<Rect>>();
                for (int i = 0; i < _XcoordinatesOfRectsinTopestRow.Count; i++)
                {
                    _dictOfColumns[_XcoordinatesOfRectsinTopestRow[i]] = new List<Rect>();
                    for (int j = 0; j < _keys.Count; j++)
                    {
                        for (int k = 0; k < _organaizedRects_byRowsx_X[_keys[j]].Count; k++)
                        {
                            if (_organaizedRects_byRowsx_X[_keys[j]][k].X == _XcoordinatesOfRectsinTopestRow[i])
                            {
                                _dictOfColumns[_XcoordinatesOfRectsinTopestRow[i]].Add(_organaizedRects_byRowsx_X[_keys[j]][k]);
                            }
                        }
                    }
                }
                //draw the vertical lines on the image at the X boundaries
                List<int> _Xkeys = new List<int>(_dictOfColumns.Keys);
                for (int i = 0; i < _Xkeys.Count; i++)
                {
                    Cv2.Line(ptr_MatLow_L, new Point(_Xkeys[i], 0), new Point(_Xkeys[i], ptr_MatLow_L.Height), H_GetRandom_GreenScalar(), 4);
                }
                //draw each column i a different color 
                //int _colorIndex = 0;
                //for (int i = 0; i < _Xkeys.Count; i++)
                //{
                //    _colorIndex++;
                //    if (_colorIndex > 6)
                //    {
                //        _colorIndex = 0;
                //    }
                //    Scalar _color = H_GetRandom_ColorScalar(_colorIndex);
                //    for (int j = 0; j < _dictOfColumns[_Xkeys[i]].Count; j++)
                //    {
                //        Cv2.Rectangle(ptr_MatLow_L, _dictOfColumns[_Xkeys[i]][j], _color, 2);
                //    }
                //}
                //draw the rects in the dictionary at column arg_drawupto
                if (arg_drawupto >= 0 && arg_drawupto < _Xkeys.Count)
                {
                    if (_Xkeys.Count > arg_drawupto)
                    {
                        for (int i = 0; i < _dictOfColumns[_Xkeys[arg_drawupto]].Count; i++)
                        {
                            //  Cv2.Rectangle(ptr_MatLow_L, _dictOfColumns[_Xkeys[arg_drawupto]][i], H_GetRandom_BlueScalar(), 2);
                            //_ColumnITEMS_RECTs.Add(_dictOfColumns[_Xkeys[arg_drawupto]][i]);
                            //Mat _roi = new Mat(ptr_MatLow_L, _dictOfColumns[_Xkeys[arg_drawupto]][i]);
                            //_ColumnITEMS_Mats.Add(_roi);
                        }
                    }
                }
                if (_ColumnITEMS_Mats != null) { _ColumnItemNumbers_Mats.Clear(); } else { _ColumnItemNumbers_Mats = new List<Mat>(); }
                if (_ColumnItemNumbers_RECTs != null) { _ColumnItemNumbers_RECTs.Clear(); } else { _ColumnItemNumbers_RECTs = new List<Rect>(); }
                if (_ColumnItemNumbers_Mats != null) { _ColumnItemNumbers_Mats.Clear(); } else { _ColumnItemNumbers_Mats = new List<Mat>(); }
                if (_ColumnItemNumbers_RECTs != null) { _ColumnItemNumbers_RECTs.Clear(); } else { _ColumnItemNumbers_RECTs = new List<Rect>(); }
                //build the list of mats for the columns
                // if < 4 use column 0 for Items and column 1 for numbers
                if (_Xkeys.Count == 2 || _Xkeys.Count == 3)
                {
                    _ColumnITEMS_RECTs = _dictOfColumns[_Xkeys[0]];
                    _ColumnItemNumbers_RECTs = _dictOfColumns[_Xkeys[1]];
                    //creat rois and add them to the mats
                    for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnITEMS_RECTs[i]);
                        _ColumnITEMS_Mats.Add(_roi);
                    }
                    for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i]);
                        _ColumnItemNumbers_Mats.Add(_roi);
                    }
                }
                else
                if (_Xkeys.Count == 4 || _Xkeys.Count == 5)
                {
                    _ColumnITEMS_RECTs = _dictOfColumns[_Xkeys[0]];
                    _ColumnItemNumbers_RECTs = _dictOfColumns[_Xkeys[1]];
                    _ColumnITEMS_RECTs.AddRange(_dictOfColumns[_Xkeys[2]]);
                    _ColumnItemNumbers_RECTs.AddRange(_dictOfColumns[_Xkeys[3]]);
                    //creat rois and add them to the mats
                    for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnITEMS_RECTs[i]);
                        _ColumnITEMS_Mats.Add(_roi);
                    }
                    for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i]);
                        _ColumnItemNumbers_Mats.Add(_roi);
                    }
                }
                else
                if (_Xkeys.Count == 6)
                {
                    _ColumnITEMS_RECTs = _dictOfColumns[_Xkeys[0]];
                    _ColumnItemNumbers_RECTs = _dictOfColumns[_Xkeys[1]];
                    //allso add column 3 to the items and 4 to numbers
                    _ColumnITEMS_RECTs.AddRange(_dictOfColumns[_Xkeys[2]]);
                    _ColumnItemNumbers_RECTs.AddRange(_dictOfColumns[_Xkeys[3]]);
                    _ColumnITEMS_RECTs.AddRange(_dictOfColumns[_Xkeys[4]]);
                    _ColumnItemNumbers_RECTs.AddRange(_dictOfColumns[_Xkeys[5]]);
                    //creat rois and add them to the mats
                    for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnITEMS_RECTs[i]);
                        _ColumnITEMS_Mats.Add(_roi);
                    }
                    for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i]);
                        _ColumnItemNumbers_Mats.Add(_roi);
                    }
                }
                //draw items rect in green and numbers in blue 
                for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                {
                    Cv2.Rectangle(ptr_MatLow_L, _ColumnITEMS_RECTs[i], H_GetRandom_GreenScalar(), 2);
                }
                for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                {
                    Cv2.Rectangle(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i], H_GetRandom_BlueScalar(), 2);
                }
            }
        }
        public void IdentifyTBL_makeColumnMatLists(float argpcnt_H_min, float argpcnt_H_MAX, float argpcnt_sml_W_min, float argpcnt_sml_W_MAX, float argpcnt_LRG_W_min, float argpcnt_LRG_W_MAX)
        {
            float arg_Wmin = argpcnt_sml_W_min;
            float arg_Wmax = argpcnt_LRG_W_MAX;
            float arg_Hmin = argpcnt_H_min;
            float arg_Hmax = argpcnt_H_MAX;
            //arg_Wmin = 6.227332f * ptr_MatLow_L.Width / 100.000f;
            //arg_Wmax = 77.79237f * ptr_MatLow_L.Width / 100.000f;
            //arg_Hmin = 2.075472f * ptr_MatLow_L.Height / 100.000f;
            //arg_Hmax = 5.660378f * ptr_MatLow_L.Height / 100.000f;
            int _xBoundaryMax_ofTheTBL = 0;
            int _xBoundaryMin_ofTheTBL = 0;
            int _LowestRow_NumberOfElements = 0;
            int _yBoundaryMax_ofTheTBL = 0;
            using (Mat gray_0big = new Mat())
            {
                Cv2.CvtColor(ptr_MatLow_L, gray_0big, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray_0big, gray_0big, 200, 255, ThresholdTypes.BinaryInv);
                OpenCvSharp.Point[][] contours_big;
                HierarchyIndex[] hierarchy_big;
                List<Rect> _allRects = new List<Rect>();
                Cv2.FindContours(gray_0big, out contours_big, out hierarchy_big, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                if (contours_big.Length == 0)
                {
                    return;
                }
                //sort contours LeftToRigt and bottomtotop 
                contours_big = contours_big.OrderBy(x => Cv2.BoundingRect(x).Y).ToArray();
                contours_big = contours_big.OrderBy(x => Cv2.BoundingRect(x).X).ToArray();
                for (int r = 0; r < contours_big.Length; r++)
                {
                    Rect _rect = Cv2.BoundingRect(contours_big[r]);
                    if (_rect.Height > arg_Hmin && _rect.Height < arg_Hmax && _rect.Width > arg_Wmin && _rect.Width < arg_Wmax)
                    {
                        _allRects.Add(_rect);
                    }
                }
                if (_allRects.Count == 0)
                {
                    return;
                }
                Dictionary<int, int> _dictOf_key_RectY_valueHowManyRectsonKey = new Dictionary<int, int>();
                for (int i = 0; i < _allRects.Count; i++)
                {
                    if (_dictOf_key_RectY_valueHowManyRectsonKey.ContainsKey(_allRects[i].Y))
                    {
                        _dictOf_key_RectY_valueHowManyRectsonKey[_allRects[i].Y]++;
                    }
                    else
                    {
                        _dictOf_key_RectY_valueHowManyRectsonKey[_allRects[i].Y] = 1;
                    }
                }
                //find the largest value in the dictionary
                int _largestNumberOfRects_perY = 0;
                int _numberOfLegitRowsFound = 0;
                foreach (KeyValuePair<int, int> entry in _dictOf_key_RectY_valueHowManyRectsonKey)
                {
                    if (entry.Value > _largestNumberOfRects_perY)
                    {
                        _largestNumberOfRects_perY = entry.Value;
                    }
                }//----------------------------------------------------------------------------------------------------_largestNumberOfRects_perY is defind
                //remove the keys that do not have the largest number of rects
                List<int> _keysToRemove = new List<int>();
                foreach (KeyValuePair<int, int> entry in _dictOf_key_RectY_valueHowManyRectsonKey)
                {
                    if (entry.Value != _largestNumberOfRects_perY)
                    {
                        _keysToRemove.Add(entry.Key);
                    }
                }
                for (int i = 0; i < _keysToRemove.Count; i++)
                {
                    _dictOf_key_RectY_valueHowManyRectsonKey.Remove(_keysToRemove[i]);
                }
                _numberOfLegitRowsFound = _dictOf_key_RectY_valueHowManyRectsonKey.Count;//----------------------------- _numberOfLegitRowsFound is defind
                foreach (KeyValuePair<int, int> entry in _dictOf_key_RectY_valueHowManyRectsonKey)
                {
                    EventsManagerLib.Call_LogConsole("Key = " + entry.Key + ", Value = " + entry.Value);
                }
                //use _dictOf_key_RectY_valueHowManyRectsonKey to create a dictionary of int List<Rect> where the key is the Y of the rect
                Dictionary<int, List<Rect>> _organaizedRects_byRowsx_X = new Dictionary<int, List<Rect>>();
                for (int i = 0; i < _dictOf_key_RectY_valueHowManyRectsonKey.Count; i++)
                {
                    int theYforThisRow = _dictOf_key_RectY_valueHowManyRectsonKey.ElementAt(i).Key;
                    if (!_organaizedRects_byRowsx_X.ContainsKey(theYforThisRow))
                    {
                        _organaizedRects_byRowsx_X[theYforThisRow] = new List<Rect>();
                        for (int j = 0; j < _allRects.Count; j++)
                        {
                            if (_allRects[j].Y == theYforThisRow)
                            {
                                _organaizedRects_byRowsx_X[theYforThisRow].Add(_allRects[j]);
                            }
                        }
                        //sort the rects in the list by X from lowest to highest
                        _organaizedRects_byRowsx_X[theYforThisRow] = _organaizedRects_byRowsx_X[theYforThisRow].OrderBy(x => x.X).ToList();
                    }
                    else
                    {
                    }
                }
                //draw the horizontal lines on the image at the Y boundaries
                List<int> _keys = new List<int>(_organaizedRects_byRowsx_X.Keys);
                Cv2.Line(ptr_MatLow_L, new Point(0, _keys[0]), new Point(ptr_MatLow_L.Width, _keys[0]), H_GetRandom_GreenScalar(), 4);
                if (_largestNumberOfRects_perY == 0)
                {
                    return;
                }
                if (_ColumnITEMS_RECTs != null)
                {
                    _ColumnITEMS_RECTs.Clear();
                }
                else
                {
                    _ColumnITEMS_RECTs = new List<Rect>();
                }
                if (_ColumnITEMS_Mats != null)
                {
                    _ColumnITEMS_Mats.Clear();
                }
                else
                {
                    _ColumnITEMS_Mats = new List<Mat>();
                }
                if (_ColumnItemNumbers_RECTs != null)
                {
                    _ColumnItemNumbers_RECTs.Clear();
                }
                else
                {
                    _ColumnItemNumbers_RECTs = new List<Rect>();
                }
                if (_ColumnItemNumbers_Mats != null)
                {
                    _ColumnItemNumbers_Mats.Clear();
                }
                else
                {
                    _ColumnItemNumbers_Mats = new List<Mat>();
                }
                List<int> _XcoordinatesOfRectsinTopestRow = new List<int>();
                for (int i = 0; i < _organaizedRects_byRowsx_X[_keys[0]].Count; i++)
                {
                    _XcoordinatesOfRectsinTopestRow.Add(_organaizedRects_byRowsx_X[_keys[0]][i].X);
                }
                //create a dictionary of int List<Rect> where the key is the X of the rect
                Dictionary<int, List<Rect>> _dictOfColumns = new Dictionary<int, List<Rect>>();
                for (int i = 0; i < _XcoordinatesOfRectsinTopestRow.Count; i++)
                {
                    _dictOfColumns[_XcoordinatesOfRectsinTopestRow[i]] = new List<Rect>();
                    for (int j = 0; j < _keys.Count; j++)
                    {
                        for (int k = 0; k < _organaizedRects_byRowsx_X[_keys[j]].Count; k++)
                        {
                            if (_organaizedRects_byRowsx_X[_keys[j]][k].X == _XcoordinatesOfRectsinTopestRow[i])
                            {
                                _dictOfColumns[_XcoordinatesOfRectsinTopestRow[i]].Add(_organaizedRects_byRowsx_X[_keys[j]][k]);
                            }
                        }
                    }
                }
                //draw the vertical lines on the image at the X boundaries
                List<int> _Xkeys = new List<int>(_dictOfColumns.Keys);
                for (int i = 0; i < _Xkeys.Count; i++)
                {
                    Cv2.Line(ptr_MatLow_L, new Point(_Xkeys[i], 0), new Point(_Xkeys[i], ptr_MatLow_L.Height), H_GetRandom_GreenScalar(), 4);
                }
                if (_ColumnITEMS_Mats != null) { _ColumnItemNumbers_Mats.Clear(); } else { _ColumnItemNumbers_Mats = new List<Mat>(); }
                if (_ColumnItemNumbers_RECTs != null) { _ColumnItemNumbers_RECTs.Clear(); } else { _ColumnItemNumbers_RECTs = new List<Rect>(); }
                if (_ColumnItemNumbers_Mats != null) { _ColumnItemNumbers_Mats.Clear(); } else { _ColumnItemNumbers_Mats = new List<Mat>(); }
                if (_ColumnItemNumbers_RECTs != null) { _ColumnItemNumbers_RECTs.Clear(); } else { _ColumnItemNumbers_RECTs = new List<Rect>(); }
                //build the list of mats for the columns
                // if < 4 use column 0 for Items and column 1 for numbers
                if (_Xkeys.Count == 2 || _Xkeys.Count == 3)
                {
                    _ColumnITEMS_RECTs = _dictOfColumns[_Xkeys[0]];
                    _ColumnItemNumbers_RECTs = _dictOfColumns[_Xkeys[1]];
                    //creat rois and add them to the mats
                    for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnITEMS_RECTs[i]);
                        _ColumnITEMS_Mats.Add(_roi);
                    }
                    for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i]);
                        _ColumnItemNumbers_Mats.Add(_roi);
                    }
                }
                else
                if (_Xkeys.Count == 4 || _Xkeys.Count == 5)
                {
                    _ColumnITEMS_RECTs = _dictOfColumns[_Xkeys[0]];
                    _ColumnItemNumbers_RECTs = _dictOfColumns[_Xkeys[1]];
                    _ColumnITEMS_RECTs.AddRange(_dictOfColumns[_Xkeys[2]]);
                    _ColumnItemNumbers_RECTs.AddRange(_dictOfColumns[_Xkeys[3]]);
                    //creat rois and add them to the mats
                    for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnITEMS_RECTs[i]);
                        _ColumnITEMS_Mats.Add(_roi);
                    }
                    for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i]);
                        _ColumnItemNumbers_Mats.Add(_roi);
                    }
                }
                else
                if (_Xkeys.Count == 6)
                {
                    _ColumnITEMS_RECTs = _dictOfColumns[_Xkeys[0]];
                    _ColumnItemNumbers_RECTs = _dictOfColumns[_Xkeys[1]];
                    //allso add column 3 to the items and 4 to numbers
                    _ColumnITEMS_RECTs.AddRange(_dictOfColumns[_Xkeys[2]]);
                    _ColumnItemNumbers_RECTs.AddRange(_dictOfColumns[_Xkeys[3]]);
                    _ColumnITEMS_RECTs.AddRange(_dictOfColumns[_Xkeys[4]]);
                    _ColumnItemNumbers_RECTs.AddRange(_dictOfColumns[_Xkeys[5]]);
                    //creat rois and add them to the mats
                    for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnITEMS_RECTs[i]);
                        _ColumnITEMS_Mats.Add(_roi);
                    }
                    for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                    {
                        Mat _roi = new Mat(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i]);
                        _ColumnItemNumbers_Mats.Add(_roi);
                    }
                }
                //draw items rect in green and numbers in blue 
                for (int i = 0; i < _ColumnITEMS_RECTs.Count; i++)
                {
                    Cv2.Rectangle(ptr_MatLow_L, _ColumnITEMS_RECTs[i], H_GetRandom_GreenScalar(), 2);
                }
                for (int i = 0; i < _ColumnItemNumbers_RECTs.Count; i++)
                {
                    Cv2.Rectangle(ptr_MatLow_L, _ColumnItemNumbers_RECTs[i], H_GetRandom_BlueScalar(), 2);
                }
            }
        }
    }

}
