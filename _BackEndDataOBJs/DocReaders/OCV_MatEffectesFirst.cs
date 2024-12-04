using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = OpenCvSharp.Point;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
using static VCI_Forms_SPN._GLobalz.ColorHelpers;
using VCI_Forms_SPN._GLobalz;
using OpenCvSharp;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public class OCV_MatEffectesFirst
    {
        OCV_TempDataObj _TempDataObj;
        Mat OriginalMatCloned = null;

        int widthOf_ZoomedMat = 10;
        int heightOf_ZoomedMat = 10;

        int widthOf_Matat1x = 10;
        int heightOf_Matat1x = 10;
        EffectManager myEffectsManager;
 
        public OCV_MatEffectesFirst(OCV_TempDataObj argTempDataObj, EffectManager arg_mateffectmngr)
        {
            _TempDataObj = argTempDataObj;
            myEffectsManager = arg_mateffectmngr;
        }
        ~OCV_MatEffectesFirst()
        {
        }

        public void DO_OCV_EFFECTS(Mat arg_zoomedMat, Mat arg_matat1x)
        {

            if (arg_zoomedMat == null || arg_matat1x == null || myEffectsManager == null) { return; }


            myEffectsManager.ApplyEffects(arg_zoomedMat);

            Cv2.PutText(arg_zoomedMat, "zoomed mat : ", new Point(10, 10), HersheyFonts.HersheySimplex, 0.5, H_GetRandom_RedScalar(), 1);
            Cv2.PutText(arg_matat1x, "mat at 1x : ", new Point(10, 10), HersheyFonts.HersheySimplex, 0.5, H_GetRandom_RedScalar(), 1);

        }
    }

    public class EffectManager
    {
        private List<IMatEffect> effects;

        public EffectManager()
        {
            effects = new List<IMatEffect>();
        }

        public EffectManager(List<IMatEffect> argListOfEffects)
        {
            effects = argListOfEffects;
            if (effects == null)
            {
                effects = new List<IMatEffect>();

            }
        }
        public void AddEffect(IMatEffect effect)
        {
            if (effects == null)
            {
                effects = new List<IMatEffect>();
            }
            effects.Add(effect);
        }

        public void RemoveEffect(IMatEffect effect)
        {
            if (effects == null)
            {
                return;
            }
            effects.Remove(effect);
        }

        public void ApplyEffects(Mat image)
        {
            if (effects == null)
            {
                return;
            }
            int count = effects.Count;
            if (count == 0)
            {
                return;
            }
            for (int i = 0; i < count; i++)
            {
                effects[i].ApplyEffect(image);
            }

        }
    }
}
