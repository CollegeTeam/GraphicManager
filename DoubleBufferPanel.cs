using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicManager
{
    public class DoubleBufferPanel : System.Windows.Forms.Panel
    {
        public DoubleBufferPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                          System.Windows.Forms.ControlStyles.UserPaint |
                          System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }

}
