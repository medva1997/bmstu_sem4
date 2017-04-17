using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Laba4
{
    class ParamEq:BaseWorker
    {
        

        protected override void drawCircle(ref Bitmap bitmap, int Xc, int Yc, int RX)
        {
            double d = 1.0 / RX;
            for (double t = Math.PI / 4.0; t >= 0; t -= d)
            {
                int dx = (int)Math.Round(RX * Math.Cos(t));
                int dy = (int)Math.Round(RX * Math.Sin(t));

                AddPoint(ref bitmap, Xc, Yc, dx, dy, drawcolor);
                AddPoint(ref bitmap, Xc, Yc, dy, dx, drawcolor);                
            }

        }

       
        protected override void drawEllipse(ref Bitmap bitmap, int Xc, int Yc, int RX, int RY)
        {
            double deltax = 0;
            double deltay = 0;
            int dx = 0, dy = 0;
            double d = 1.0 / Math.Max(RX, RY);

            for (double t = Math.PI / 2.0; t >= 0; t -= d)
            {
                deltax = RX * Math.Cos(t);
                deltay = RY * Math.Sin(t);
                dx = (int)Math.Round(deltax);
                dy = (int)Math.Round(deltay);

                AddPoint(ref bitmap, Xc, Yc, dx, dy, drawcolor);
            }
        }
    }
}
