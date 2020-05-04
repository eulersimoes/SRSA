using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COCKPIT
{
    public class ModelFpv
    {
        CaptureCam.CaptureCam device = new CaptureCam.CaptureCam();

        public void Capturar(PictureBox pic)
        {
            device.Capturar(ref pic);
        }
    }
}
