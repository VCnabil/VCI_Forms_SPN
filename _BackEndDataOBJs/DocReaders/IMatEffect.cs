using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public interface IMatEffect
    {
        // bool IsEffectEnabled { get; set; }
        void ApplyEffect(Mat image);
    }
}
