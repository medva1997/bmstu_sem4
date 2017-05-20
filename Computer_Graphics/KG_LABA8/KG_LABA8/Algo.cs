using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG_LABA8
{
    class Algo
    {
        private Graphics g;
        private Color color;

        struct Line 
        {
                public Point p1;
                public Point p2;
                
                public Line(Point a, Point b) {
                    p1 = a;
                    p2 = b;
                }
        };

        struct Vector {
           public int x;
           public int y;
           public int z;
           
           public Vector(int a, int b, int c = 0) {
                x = a;
                y = b;
                z = c;
            }
           public Vector(Point end, Point start) {
                x = end.X - start.X;
                y = end.Y - start.Y;
                z = 0;
            }
        };

        void VectorMult(Vector a, Vector b, ref Vector res) {
            res.x = a.y * b.z - a.z * b.y;
            res.y = a.z * b.x - a.x * b.z;
            res.z = a.x * b.y - a.y * b.x;
        }

        int ScalarMult(Vector a, Vector b) {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// проверка знака числа
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int  SIGN(int x) 
        {
            if (x < 0)
                return -1;
            if (x == 0)
                return 0;
            if (x > 0)
                return 1;

            return 1;
        }

        /// <summary>
        /// Определение выпуклости многоугольника
        /// </summary>
        /// <param name="poligon">Множество вершин</param>
        /// <returns>0-невыпуклый иначе выпуклый</returns>
        private int IsConvexPolygon(List<Point> poligon) 
        {
                Vector a=new Vector(poligon[1], poligon[0]);
                Vector b;
                int n = poligon.Count-1;
                Vector tmp = new Vector(); ;
                int res = 0;

                for(int i = 1; i < n; i++) 
                {
                    b = new Vector(poligon[i + 1], poligon[i]);
                    VectorMult(a, b, ref tmp);

                    if(res == 0)
                        res = SIGN(tmp.z);

                    if(res != SIGN(tmp.z))
                    {
                        if(tmp.z==0)
                            return 0;
                    }                   
                    a = b;
                }

                b = new Vector(poligon[0], poligon[n]);
                VectorMult(a, b, ref tmp);

                if (res == 0)
                    res = SIGN(tmp.z);

                if (res != SIGN(tmp.z))
                {
                    if (tmp.z == 0)
                        return 0;
                }
                a = b;

                return res;
            }

        /// <summary>
        /// Поиск нормалей 
        /// </summary>
        /// <param name="poligon">набор вершин многоугольника</param>
        /// <param name="obhod">направление обхода</param>
        /// <param name="normVect">набор векторов</param>
        private void findNormVectorsToSide(List<Point> poligon, int obhod, ref List<Vector> normVect) 
        {
            int n = poligon.Count -1;
            Vector b=new Vector();;
            for(int i = 0; i < n; i++) {
                b =new Vector(poligon[i+1], poligon[i]);
                if(obhod == -1)
                    normVect.Add(new Vector(b.y, -b.x));
                else
                    normVect.Add(new Vector(-b.y, b.x));
            }
            //Фикс1
            b = new Vector(poligon[0], poligon[n]);
            if (obhod == -1)
                normVect.Add(new Vector(b.y, -b.x));
            else
                normVect.Add(new Vector(-b.y, b.x));
             
        }

       private Point P(double t, Point p1, Point p2) {
            Point tmp = new Point(); 
            tmp.X = p1.X + (int)(Math.Round((p2.X - p1.X) * t));
            tmp.Y = p1.Y + (int)(Math.Round((p2.Y - p1.Y) * t));
            return tmp;
        }

        /// <summary>
        /// отсечение отрезка
        /// </summary>
        /// <param name="polynom"></param>
        /// <param name="normVect"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
       int CutSegment(List<Point> polynom, List<Vector> normVect, ref Point p1, ref Point p2, ref bool visible)
       {
           visible = false;
           //Фикс2 здесь
           int n = polynom.Count;

           Vector D, W;
           int Dsk, Wsk;
          
           double tbot = 0, ttop = 1;
           double t;
           D = new Vector(p2, p1);

           //цикл отсечения отрезка по всем граням
           for (int i = 0; i < n; i++)
           {
               W = new Vector(p1, polynom[i]);

               Dsk = ScalarMult(D, normVect[i]);
               Wsk = ScalarMult(W, normVect[i]);

               if (Dsk == 0)
               {
                   if (Wsk < 0)
                       return 0;
               }
               else
               {
                   t = -Wsk / (double)Dsk;

                   if (Dsk > 0)
                   {
                       if (t > 1)
                           return 0;
                       else
                       {
                           tbot = Math.Max(tbot, t);
                       }
                   }
                   else
                   {
                       if (t < 0)
                           return 0;
                       else
                       {
                           ttop = Math.Min(ttop, t);
                       }
                   }
               }
           }

           if (tbot <= ttop)
           {
               Point tmp = P(tbot, p1, p2);
               p2 = P(ttop, p1, p2);
               p1 = tmp;
               //qDebug() << "ok";
               visible = true;
           }
           return 0;
       }

       int SimpleAlgo(List<Point> poligon, List<Line> lines)
       {
           //poligon.Add(poligon[0]);
           //проверка ны выпуклость
           int obhod = IsConvexPolygon(poligon);
           if (obhod == 0)
               return 1;

           //поизк нормалей
           List<Vector> normVect = new List<Vector>(); 
           findNormVectorsToSide(poligon, obhod, ref normVect);

           bool visible = true;

           for (int i = 0; i < lines.Count; i++)
           {
               Line s = lines[i];

               //Pen pen2 = new Pen(Color.Blue, 1);
               //g.DrawLine(pen2, s.p1, s.p2);
               //отсечение линии
               CutSegment(poligon, normVect, ref s.p1, ref s.p2, ref visible);
               
               //отрисовка отсечения
               if (visible)
               {
                   Pen pen = new Pen(color, 2);
                   g.DrawLine(pen, s.p1, s.p2);
               }
           }
           return 0;
       }



        public  Algo(List<Point> poligon, List<Point> points, Color color, Graphics g)
        {
            this.g = g;
            this.color = color;
            List<Line> lines=new List<Line>();
            for (int i = 0; i < points.Count / 2; i++)
            {
                lines.Add(new Line( points[i * 2], points[i * 2 + 1]));
            }
           
            SimpleAlgo(poligon, lines);
        }
    }
}
