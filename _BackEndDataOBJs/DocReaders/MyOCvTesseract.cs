using OpenCvSharp;
using System.Drawing;
using OpenCvSharp.Extensions;
using System.Collections.Generic;
using Tesseract;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public class MyOCvTesseract
    {
        public MyOCvTesseract()
        {
            List_Items = new List<string>();
            List_ItemNumbers = new List<string>();
        }
        public string ReadTextFromMat(Mat srcMat)
        {
            if (srcMat == null)
            {
                return "No Mat to read";
            }
            Bitmap bitmapImage = srcMat.ToBitmap(); // Use the extension method to convert Mat to Bitmap
            string text = "";
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = PixConverter.ToPix(bitmapImage))
                {
                    using (var page = engine.Process(img))
                    {
                        text = page.GetText();
                    }
                }
            }
            return text;
        }
        public string ReadTextFromListOfMats(List<Mat> srcMats)
        {
            if (srcMats == null)
            {
                return "No Mat to read";
            }
            string text = "";
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                foreach (var mat in srcMats)
                {
                    Bitmap bitmapImage = mat.ToBitmap(); // Use the extension method to convert Mat to Bitmap
                    using (var img = PixConverter.ToPix(bitmapImage))
                    {
                        using (var page = engine.Process(img))
                        {
                            string temp = page.GetText();
                            //if temp is not just spaces 
                            if (!string.IsNullOrWhiteSpace(temp))
                            {
                                text += temp + "^";
                            }
                        }
                    }
                    // text += "\n";
                }
            }
            return text;
        }
        List<string> List_Items;
        List<string> List_ItemNumbers;
        public void ReadTextFromListOfMats_IntoLists(List<List<Mat>> srcMats)
        {
            if (srcMats == null)
            {
                return;
            }
            if (List_ItemNumbers == null)
            {
                List_ItemNumbers = new List<string>();
            }
            else
            {
                List_ItemNumbers.Clear();
            }
            if (List_Items == null)
            {
                List_Items = new List<string>();
            }
            else
            {
                List_Items.Clear();
            }
            List<Mat> _list0 = srcMats[0];
            List<Mat> _list1 = srcMats[1];
            int _list0Count = _list0.Count;
            for (int i = 0; i < _list0Count; i++)
            {
                string text0 = "";
                string text1 = "";
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    Bitmap bitmapImage = _list0[i].ToBitmap(); // Use the extension method to convert Mat to Bitmap
                    Bitmap bitmapImage2 = _list1[i].ToBitmap(); // Use the extension method to convert Mat to Bitmap
                    using (var img = PixConverter.ToPix(bitmapImage))
                    {
                        using (var page = engine.Process(img))
                        {
                            text0 = page.GetText();
                            //if temp is not just spaces 
                            if (!string.IsNullOrWhiteSpace(text0))
                            {
                                List_Items.Add(text0);
                            }
                        }
                    }
                    using (var img = PixConverter.ToPix(bitmapImage2))
                    {
                        using (var page = engine.Process(img))
                        {
                            text1 = page.GetText();
                            //if temp is not just spaces 
                            if (!string.IsNullOrWhiteSpace(text1))
                            {
                                List_ItemNumbers.Add(text1);
                            }
                        }
                    }
                }
            }
        }
        public List<string> GetList_Items()
        {
            return List_Items;
        }
        public List<string> GetList_ItemNumbers()
        {
            return List_ItemNumbers;
        }
    }
}
