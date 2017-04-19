using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Laba4
{
    class MidPoint:BaseWorker
    {

        protected override void drawCircle(ref Bitmap bitmap, int Xc, int Yc, int RX)
        {

            int x = 0;
            int y = RX;
            int p = 1 - RX;
            AddPoint(ref bitmap, Xc, Yc, x, y, drawcolor);
            AddPoint(ref bitmap, Xc, Yc, y, x, drawcolor);

            while (x < y)
            {
                x++;
                if (p < 0)
                {
                    p += 2 * x + 1;
                }
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }

                AddPoint(ref bitmap, Xc, Yc, x, y, drawcolor);
                AddPoint(ref bitmap, Xc, Yc, y, x, drawcolor);

            }

        }

        //Used
        protected override void drawEllipse(ref Bitmap bitmap, int cx, int cy, int rx, int ry)
        {
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int r2y2 = 2 * ry2;
            int r2x2 = 2 * rx2;
            
            int rdel2 =(int)(rx2 / Math.Sqrt(rx2 + ry2)); //производная для ограничения


            int x = 0;
            int y = ry;

            int df = 0;
            int f = (int)(ry2 - rx2 * y + 0.25 * rx2 + 0.5);

            int delta = -r2x2 * y;
            for (x = 0; x <= rdel2; x += 1)
            {               

                AddPoint(ref bitmap, cx, cy, x, y, drawcolor); 
                if (f >= 0)
                {
                    y -= 1;
                    delta += r2x2;
                    f += delta;
                }
                df += r2y2; ;
                f += df + ry2;
            }


            delta = r2y2 * x ;            
            f +=(int) (-ry2 * (x + 0.75) - rx2 * (y - 0.75));
            df = -r2x2 * y;           

            
            for (; y >= 0; y -= 1)
            {
               
                AddPoint(ref bitmap, cx, cy, x, y, drawcolor); 

                if (f < 0)
                {
                    x += 1;
                    delta += r2y2;
                    f += delta;
                }
                df += r2x2;
                f += df + rx2;
            }
        }


        protected  void drawEllipse1(ref Bitmap bitmap, int Xc, int Yc, int Rx, int Ry)
        {
            

            int Rx2 = Rx*Rx;
            int Ry2 = Ry*Ry;
            int twoRx2 = 2 * Rx2;
            int twoRy2 = 2 * Ry2;
            int p;
            int x = 0;
            int y = Ry;
            int px = 0;
            int py = twoRx2 * y;
            AddPoint(ref bitmap, Xc, Yc, x, y, drawcolor);    
                          
            p = (int)Math.Round(Ry2 - (Rx2*Ry) + (0.25) * Rx2);

            while(px < py){
                x++;
                px += twoRy2;
                if(p < 0)                
                    p += Ry2 + px;               
                else
                {
                    y--;
                    py -= twoRx2;
                    p += Ry2 + px - py;
                }
                AddPoint(ref bitmap, Xc, Yc, x, y, drawcolor);   
            }
            p = (int)Math.Round(Ry2 * (x + 0.5)*(x + 0.5) + Rx2 * (y - 1)*(y - 1) - Rx2 * Ry2);

            while(y > 0){

                y--;
                py -= twoRx2;
                if(p > 0){
                    p += Rx2 - py;
                }else{

                    x++;
                    px += twoRy2;
                    p += Rx2 - py + px;
                }
                AddPoint(ref bitmap, Xc, Yc, x, y, drawcolor);
            }

        }
        protected  void drawEllipse2(ref Bitmap bitmap, int Xc, int Yc, int RX, int RY)
        {
            
            int a = RX;
            int b = RY;
            int ab2 = a * a * b * b,
                         a2 = a * a,
                         b2 = b * b;
            int xt = 0, yt = b;
            
            double f = b2 + a2 * (yt - 0.5) * (yt - 0.5) - ab2;
            double border = a2 / Math.Sqrt(b2 + a2);
            
            while (xt <= border)
            {                
                AddPoint(ref bitmap, Xc, Yc, xt, yt, drawcolor);               
                xt += 1;
                if (f > 0)
                {
                    yt -= 1;
                    f += a2 * (-2 * yt);
                }
                f += b2 * (2 * xt + 1);
            }

            f += 0.75 * (a2 - b2) - (b2 * xt + a2 * yt);
            while (yt >= 0)
            {
                AddPoint(ref bitmap, Xc, Yc, xt, yt, drawcolor); 

                yt -= 1;
                if (f < 0)
                {
                    xt += 1;
                    f += b2 * (2 * xt);
                }
                f += a2 * (-2 * yt + 1);
            }
        }
    }
}
