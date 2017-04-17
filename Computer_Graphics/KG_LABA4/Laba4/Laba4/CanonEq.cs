using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Laba4
{
    class CanonEq:BaseWorker
    {
        //OK
        protected override void drawCircle(ref Bitmap bitmap, int Xc, int Yc, int RX)
        {         
            
            int a2 =RX * RX;
            double deltax = 0, deltay=RX;
            int dx, dy;
            int step = 1;

            int flag = (int)Math.Round(RX / Math.Sqrt(2)); ;
            for (; deltax <= flag; deltax += step)
            {
                deltay = Math.Sqrt(a2 - deltax * deltax);

                dx = (int)Math.Round(deltax);
                dy = (int)Math.Round(deltay);

                AddPoint(ref bitmap, Xc, Yc, dx, dy, drawcolor);
                AddPoint(ref bitmap, Xc, Yc, dy, dx, drawcolor);
            }          

        }
        //MY
        protected void drawEllipse1(ref Bitmap bitmap, int Xc, int Yc, int RX, int RY)
        {
            int a = RX, b = RY;
            int a2 = a * a, b2 = b * b;
            double deltax = 0, deltay = b;
            int dx, dy;
            int step = 1;

            double flag = Math.Round(a2 / Math.Sqrt(a2 + b2));
            for (; deltax <= flag; deltax += step)
            {
                deltay = Math.Sqrt(a2 * b2 - deltax * deltax * b2) / a;

                dx = (int)Math.Round(deltax);
                dy = (int)Math.Round(deltay);

                AddPoint(ref bitmap, Xc, Yc, dx, dy, drawcolor);
            }

            
            
            for (; deltay >= 0; deltay -= step)
            {
                deltax = Math.Sqrt(a2 * b2 - deltay * deltay * a2) / b;

                dx = (int)Math.Round(deltax);
                dy = (int)Math.Round(deltay);

                AddPoint(ref bitmap, Xc, Yc, dx, dy, drawcolor);
            }
            
        }

        //Natasha
        protected override void drawEllipse(ref Bitmap bitmap, int cx, int cy, int rx, int ry)
        {     
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int rdel2 =(int)Math.Round(rx2 / Math.Sqrt(rx2 + ry2));

            double m = (double)ry / rx;

            int x = 0, y = 0;

            for (x = 0; x <= rdel2; x++)
            {
                y = (int)Math.Round(Math.Sqrt(rx2 - x * x) * m);               
                AddPoint(ref bitmap, cx, cy, x, y, drawcolor);             
            }

            rdel2 = (int)Math.Round(ry2 / Math.Sqrt(rx2 + ry2));
            m = 1 / m;

            for (y = 0; y <= rdel2; y++)
            {
                x = (int)Math.Round(Math.Sqrt(ry2 - y * y) * m);
                AddPoint(ref bitmap, cx, cy, x, y, drawcolor);
            }
          
        }
    }
}
