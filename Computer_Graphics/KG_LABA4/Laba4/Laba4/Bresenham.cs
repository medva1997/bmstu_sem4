using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Laba4
{
    class Bresenham:BaseWorker
    {
        //OK
        protected override void drawCircle(ref Bitmap bitmap, int xc, int yc, int r)
        {
            int pk = 3 - 2 * r;
            int x = 0, y = r;
            AddPoint(ref bitmap, xc, yc, x, y, drawcolor);
            AddPoint(ref bitmap, xc, yc, y, x, drawcolor);     
            while (x < y)
            {
                if (pk <= 0)
                {
                    pk = pk + (4 * x) + 6;
                    ++x;
                    AddPoint(ref bitmap, xc, yc, x, y, drawcolor);
                    AddPoint(ref bitmap, xc, yc, y, x, drawcolor);
                }
                else
                {
                    pk = pk + (4 * (x - y)) + 10;
                    ++x;
                    --y;
                    AddPoint(ref bitmap, xc, yc, y, x, drawcolor);
                    AddPoint(ref bitmap, xc, yc, x, y, drawcolor);
                }
            }
        }
        
        //+-My
        protected void bresenhamElipse(ref Bitmap bitmap, int x0, int y0, int RX, int RY)
        {
            int a2 = RX *RX;
            int b2 = RY* RY;
            int fa2 = 4 * a2;
            int fb2 = 4 * b2;

            int x, y, sigma;
            x = 0;
            y = RY;
            sigma = (2 * b2) + (a2 * (1 - 2 * RY));

            for (x = 0; b2 * x <= a2 * y; x++)
            {
                AddPoint(ref bitmap, x0, y0, x, y, drawcolor);
               
                if (sigma >= 0)
                {
                    sigma += fa2 * (1 - y);
                    y--;
                }
                sigma += b2 * ((4 * x) + 6);
            }
            for (x = RX, y = 0, sigma = 2 * a2 + b2 * (1 - 2 * RX); a2 * y <= b2 * x; y++)
            {
                AddPoint(ref bitmap, x0, y0, x, y, drawcolor);
                if (sigma >= 0)
                {
                    sigma += fb2 * (1 - x);
                    x--;
                }
                sigma += a2 * ((4 * y) + 6);
            }

        }
        
        
        //+-OK Natasha
        protected override void drawEllipse(ref Bitmap bitmap, int cx, int cy, int rx, int ry)
        {
            bresenhamElipse(ref bitmap, cx, cy, rx, ry);
            return;            

           
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int r2y2 = 2 * ry2;
            int r2x2 = 2 * rx2;
            int x = 0, y = ry;

            int d = rx2 + ry2 - r2x2 * y;
            int d1, d2;

            while (y >= 0)
            {
                AddPoint(ref bitmap, cx, cy, x, y, drawcolor); 

                if (d < 0)
                {
                    d1 = 2 * d + r2x2 * y - 1;
                    if (d1 > 0)
                    {
                        y -= 1;
                        x += 1;
                        d += r2y2 * x + ry2 + rx2 - r2x2 * y;
                    }
                    else
                    {
                        x += 1;
                        d += r2y2 * x + ry2;
                    }
                }
                else if (d == 0)
                {
                    x += 1;
                    y -= 1;
                    d += r2y2 * x + ry2 + rx2 - r2x2 * y;
                }
                else
                {
                    d2 = 2 * d - r2y2 * x - 1;
                    if (d2 < 0)
                    {
                        y -= 1;
                        x += 1;
                        d += r2y2 * x + ry2 + rx2 - r2x2 * y;
                    }
                    else
                    {
                        y -= 1;
                        d += rx2 - r2x2 * y;
                    }
                }
            } //end while
            
           
        }
       
      }
}
