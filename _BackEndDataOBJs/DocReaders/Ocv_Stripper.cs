using System;
using OpenCvSharp;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;
using Rect = OpenCvSharp.Rect;
using VCI_Forms_SPN._GLobalz;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public class Ocv_Stripper : IDisposable
    {
        Mat _Strip_FarRightColumn;

        public Ocv_Stripper(Mat argFullimage, Rect argRoiRect)
        {

            string strMat = argFullimage.ToString();
            string strRect = argRoiRect.ToString();

            int widthOftheMat = argFullimage.Width;

            int widthOftheRect = argRoiRect.Width;

            EventsManagerLib.Call_LogConsole(" strMat INCLASS: " + strMat);
            EventsManagerLib.Call_LogConsole(" strRect INCLASS: " + strRect);
            EventsManagerLib.Call_LogConsole("widthOftheMat INCLASS =  " + widthOftheMat + "     widthOftheRect INCLASS =  " + widthOftheRect);
            bool error = false;
            if (argFullimage == null)
            {
                error = true;
                throw new ArgumentNullException("argFullimage");

            }
            if (argRoiRect == null)
            {
                error = true;
                throw new ArgumentNullException("argRoiRect");
            }
            if (error)
            {
                return;
            }
            //set the strip to the right of the roi
            _Strip_FarRightColumn = argFullimage[argRoiRect];
        }

        public void Display_Strip()
        {
            if (_Strip_FarRightColumn == null)
            {
                return;
            }
            // Cv2.ImShow("Strip", _Strip_FarRightColumn);
            // Cv2.WaitKey();
        }

        public void Dispose()
        {
            if (_Strip_FarRightColumn != null)
            {
                _Strip_FarRightColumn.Dispose();
            }
        }
    }
}
