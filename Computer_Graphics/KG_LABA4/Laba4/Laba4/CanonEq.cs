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
            //x^2+y^2=R^2            
            int a2 =RX * RX;
            double deltax = 0, deltay=RX;
            int dx, dy;
            int step = 1;

            //производня для остановки цикла
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
        //Bad version (NOT IN USE)
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

        //Worked version
        protected override void drawEllipse(ref Bitmap bitmap, int cx, int cy, int rx, int ry)
        {
            //x^2/a^2+y^2/b^2=1
            
            int rx2 = rx * rx;//a^2
            int ry2 = ry * ry;//b^2

            //Производная при y`=-1 , является границей для оптимального рисования
            //y`=-b/a*x/sqrt(a^2-x^2)
            int rdel2 =(int)Math.Round(rx2 / Math.Sqrt(rx2 + ry2));           

            int x = 0, y = 0;
            double m = (double)ry / rx;//b/a
            for (x = 0; x <= rdel2; x++)
            {
                y = (int)Math.Round(Math.Sqrt(rx2 - x * x) * m);  //y=b/a*sqrt(a^2-x^2)
                AddPoint(ref bitmap, cx, cy, x, y, drawcolor);             
            }

            //Производная , является границей для оптимального рисования
            rdel2 = (int)Math.Round(ry2 / Math.Sqrt(rx2 + ry2));
            m = 1 / m;//переворачиваем m

            for (y = 0; y <= rdel2; y++)
            {
                x = (int)Math.Round(Math.Sqrt(ry2 - y * y) * m);//аналогично выше
                AddPoint(ref bitmap, cx, cy, x, y, drawcolor);
            }
          
        }
    }
}
