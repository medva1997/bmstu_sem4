using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG_LABA7
{
    class Algo
    {
        private Point upl;
        private Point downr;
        private Graphics g;

        private int[] GetT(Point p)
        {
            int[] T = new int[4];

            if (p.X < upl.X)    T[3] = 1;
            if (p.X > downr.X)  T[2] = 1;
            if (p.Y > downr.Y)  T[1] = 1;
            if (p.Y < upl.Y)    T[0] = 1;

            //Font f=new Font("Arial",10);
            //SolidBrush br=new SolidBrush(Color.Black);
            //string s ="T=" +T[0].ToString() + T[1].ToString() + T[2].ToString() + T[3].ToString();
            //g.DrawString(s, f, br, new PointF(p.X, p.Y));
            ////g.DrawLine(pen, P1, P2);
            return T;
        }

        private int GetS(int[] T)
        {
            int S = 0;
            for (int i=0; i<4; i++)
            {
                S += T[i];
            }
            return S;
            
        }

        private int GetP(int[] T1, int[] T2)
        {
            int p = 0;
            for (int i = 0; i < 4; i++)
            {
                p += T1[i] * T2[i];
            }
            return p;
        }

        enum return_codes
        {
            A,B,B1,OK
        }
     
        void setresult(ref Point R, int x, int y)
        {
            R.X = x;
            R.Y = y;
        }

        Point PP1, PP2;

        private return_codes CountR(ref Point R)
        {
            int ymin = Math.Min(upl.Y, downr.Y);
            int ymax = Math.Max(upl.Y, downr.Y);

            int xleft = Math.Min(upl.X, downr.X);
            int xright = Math.Max(upl.X, downr.X);

            double tangens = Math.Pow(10, 30);//вертикальный отрезок
            Point Q = R;

            //15
            if (PP1.X == PP2.X)
            {
                //Тангенс будет равен неопределенности
                //skip to 23
            }
            else
            {
                //16
                tangens = (PP2.Y - PP1.Y) / ((PP2.X - PP1.X)*1.0);

                #region X1
                //17
                if (Q.X < xleft) //проверка возможности пересечения с левой границей отсекателя
                {
                    //18
                    int Yp = (int)Math.Round(tangens * (xleft - Q.X) + Q.Y); //вычисление ординаты пересечения
                    //19
                    if (ymin <= Yp && Yp <= ymax) //проверка корретноости найденного значенияя
                    {
                        setresult(ref R, xleft, Yp);
                        return return_codes.A;
                    }
                }
                #endregion

                #region X2
                //20
                if (Q.X > xright) //проверка возможности пересечения с правой границей отсекателя
                {
                    //21
                    int Yp = (int)Math.Round((tangens * (xright - Q.X) + Q.Y)); //вычисление ординаты пересечения
                    //22

                    if (ymin <= Yp && Yp <= ymax) //проверка корретноости найденного значенияя
                    {
                        setresult(ref R, xright, Yp);
                        return return_codes.A;
                    }
                }
                #endregion
            }

            //23
            if (tangens == 0)
                return return_codes.B;



            #region Y1
            if (Q.Y < ymin)
            {
                int x = (int)Math.Round((ymin - Q.Y) / tangens + Q.X); //вычисление абсциссы пересечения
                if (xleft <= x && x <= xright) //проверка корретноости найденного значенияя
                {
                    setresult(ref R, x, ymin);
                    return return_codes.A;
                }
            }
            #endregion

            #region Y2
            if (Q.Y > ymax)
            {
                int x = (int)Math.Round((ymax - Q.Y) / tangens + Q.X); //вычисление абсциссы пересечения
                if (xleft <= x && x <= xright) //проверка корретноости найденного значенияя
                {
                    setresult(ref R, x, ymax);
                    return return_codes.A;
                }
            }
            #endregion

            return return_codes.B;
        }

        /// <summary>
        /// Простой алгоритм
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        private void algo2(Point P1, Point P2)
        {
            PP1 = P1;
            PP2 = P2;
            //3
            int[] T1 = GetT(P1);
            int[] T2 = GetT(P2);
            int S1 = GetS(T1);           
            int S2 = GetS(T2);

          
            //4
            int pr = 1;
            //5

            //6
            if ((S1 == 0) && (S2 == 0))
            {
                DrawLine(P1, P2, pr);
                return;
            }


            //7
            int P = GetP(T1, T2);
            //8
            if (P != 0)
            {
                pr = -1;
                DrawLine(P1, P2, pr);
                return;
            }

            //Забиваем поумолчанию
            Point R1 = P1, R2 = P1, Q = new Point(); ;
            int i = 0;

           
            //9  
            if (S1 == 0)
            {
                R1 = P1;
                Q = P2;
                i = 2;              
            }
            else
            {
                //10
                if (S2 == 0)
                {
                    R1 = P2;
                    Q = P1;
                    i = 2;                  
                }
                else
                {   //11
                    i = 1;                   
                }

            }

            return_codes code=return_codes.OK;

            //Первая итерация
            if (i == 1)
            {
                Q = P1;
                code = CountR(ref Q);
                R1= Q;
                Q = P2;
                i++;
            }
           
            ////не рисуем линию красным
            //if (code == return_codes.B)
            //{
            //    pr = -1;
            //    DrawLine(P1, P2, pr);
            //    return;
            //}

            //втораяитерация
            if (i == 2)
            {
                code = CountR(ref Q);
                R2 = Q;
            }

            //отрисовка красным
            if (code != return_codes.B)
            {
                DrawLine(R1, R2, pr);
            }
           
        }

       
        //31
        private void DrawLine(Point P1,Point P2, int flag)
        {
            if (flag == 1)
            {
                Pen pen = new Pen(Color.Red, 2);
                g.DrawLine(pen, P1, P2);
                //Draw
            }
            else
            {
                Pen pen = new Pen(Color.White, 2);
                //g.DrawLine(pen, P1, P2);
            }
        }

        
        public Algo(List<Point> points,Rectangle rect,ref Graphics g)
        {
            this.g = g;

            int x1, x2, y1, y2;
            x1 = rect.Location.X;
            y1 = rect.Location.Y;
            x2 = rect.Location.X + rect.Size.Width;
            y2 = rect.Location.Y + rect.Size.Height;

            upl = rect.Location;
            downr = new Point(x2, y2);

            for (int i = 0; i < points.Count() / 2; i++)
            {
                algo2(points[i * 2], points[i * 2 + 1]);
            }
        }
    }
}
