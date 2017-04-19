using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Laba4
{
    class Tester
    {
        public Bitmap Start(int p_wigth, int p_height, bool flag)
        {
            Bitmap pic_bitmap = new Bitmap(p_wigth, p_height);
            var graphics = Graphics.FromImage(pic_bitmap);  
            int N = 5;
            int offset = 20;
            int[] time_array = new int[N];
            PointF[] RecCorner = new PointF[N];
            Size[] SizeCorner = new Size[N];
            Color[] colors = new Color[N];

            BaseWorker[] alg = new BaseWorker[N];
            alg[0] = new StandartWorker();
            alg[1] = new Bresenham();
            alg[2] = new MidPoint();
            alg[3] = new CanonEq();
            alg[4] = new ParamEq();

            for (int i = 0; i < N; i++)
            {
                if (flag)
                {
                    time_array[i] = alg[i].TestTimeElipse();
                    DrawText("Эллипсы", ref graphics, new PointF(p_wigth/2, 0));
                }
                else
                {
                    time_array[i] = alg[i].TestTimeCircle();
                    DrawText("Окружности", ref graphics, new PointF(p_wigth/2, 0));
                }
            }

           

            int max = (int)(time_array.Max() * 1.3);

           
            
            int wigth = (int)((p_wigth - (N+2) * offset) / N);
            int pos = 0;
            
            for (int i = 0; i < N; i++)
            {
                SizeCorner[i].Height = time_array[i] * p_height / max;
                SizeCorner[i].Width = wigth;
                RecCorner[i].X = pos;
                pos += wigth + offset;
                RecCorner[i].Y = p_height - SizeCorner[i].Height;
               
            }

             
            colors[0] = Color.Yellow;
            colors[1] = Color.Silver;
            colors[2] = Color.Green;
            colors[3] = Color.Pink;
            colors[4] = Color.Blue;

            string[] names = { "Стандартный \n c установкой точек", "Брезенхэм", "Средней точки", "Канонической", "Параметрическое" };
             

            for (int i = 0; i < N; i++)
            {
                SolidBrush brush = new SolidBrush(colors[i]);
                graphics.FillRectangle(brush, RecCorner[i].X, RecCorner[i].Y, SizeCorner[i].Width, SizeCorner[i].Height);
                DrawText(names[i], ref graphics, time_array[i], RecCorner[i]);
            }
           
            

           
            return pic_bitmap;
        }

        private void DrawText(string name, ref Graphics g,int time,PointF rect )
        {
            Pen myPen = new Pen(Color.Red, 3);
            Font drawFont = new Font("Times New Roman", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(name + "\n (" + Convert.ToString(time) + ")", drawFont, drawBrush, rect.X, rect.Y - 50);


        }

        private void DrawText(string name, ref Graphics g, PointF rect)
        {
            Pen myPen = new Pen(Color.Red, 3);
            Font drawFont = new Font("Times New Roman", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(name , drawFont, drawBrush, rect.X, rect.Y + 50);
        }

    }
}
