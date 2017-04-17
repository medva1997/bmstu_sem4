﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Laba4
{
    class BaseWorker
    {
        protected Color drawcolor = Color.Black;

        public Color SetColor
        {
            set { drawcolor = value; }
        }

        protected int AddPoint(ref Bitmap bitmap, int Xc, int Yc, int dx, int dy, Color color)
        {
            AddPoint(ref bitmap, Xc + dx, Yc + dy, color);
            AddPoint(ref bitmap, Xc - dx, Yc + dy, color);
            AddPoint(ref bitmap, Xc + dx, Yc - dy, color);
            AddPoint(ref bitmap, Xc - dx, Yc - dy, color);
            
            return 0;
        }
        protected int AddPoint(ref Bitmap bitmap, int X, int Y, Color color)
        {

            if ((X >= 0) && (X < bitmap.Width) && (Y >= 0) && (Y < bitmap.Height))
            {
                bitmap.SetPixel(X, Y, color);
            }
            return 0;
        }

        protected virtual void drawCircle(ref Bitmap bitmap, int X, int Y, int RX) { }

        protected virtual void drawEllipse(ref Bitmap bitmap, int X, int Y, int RX, int RY) { }

        public void  DrawCircle(ref Bitmap bitmap, int X, int Y, int RX)
        {
            drawCircle(ref  bitmap, X, Y, RX);
        }

        public void DrawEllipse(ref Bitmap bitmap, int X, int Y, int RX, int RY)
        {
            drawEllipse(ref  bitmap, X, Y, RX, RY);
        }


        public void drawSpecrte(ref Bitmap bitmap, int X, int Y, int RX, int RY, int drx, int dry,int N)
        {
            int rx = RX;
            int ry = RY;            
            for (int i = 0; i < N; i++)
            {
                drawEllipse(ref bitmap, X, Y, rx, ry);
                rx += drx;
                ry += dry;
            }
           
        }

        public void drawSpecrteCircle(ref Bitmap bitmap, int X, int Y, int RX, int RY, int drx, int N)
        {
            int rx = RX;
            for (int i = 0; i < N; i++)
            {
                drawCircle(ref bitmap, X, Y, rx);
                rx += drx;
            }
           
        }
    }
}
