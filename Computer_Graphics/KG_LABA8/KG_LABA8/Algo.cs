using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG_LABA8
{
    /// <summary>
    /// Алгоритм Кируса — Бека
    /// алгоритм отсечения отрезков произвольным выпуклым многоугольником. 
    /// </summary>
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

        int ScalarMultiplication(Vector a, Vector b) {
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
               
                poligon.Add(poligon[0]);
                

                Vector a=new Vector(poligon[1], poligon[0]);
                Vector b= new Vector();
                Vector tmp = new Vector(); 
                int n = poligon.Count;
                
                int res = 0;

                for(int i = 1; i < n-1; i++) 
                {
                    b = new Vector(poligon[i + 1], poligon[i]);
                    VectorMult(a, b, ref tmp);

                    if(res == 0)
                        res = SIGN(tmp.z);

                    if(res != SIGN(tmp.z))
                    {
                        if(tmp.z!=0)
                            return 0;
                    }                   
                    a = b;
                }

                b = new Vector(poligon[1], poligon[0]);
                VectorMult(a, b, ref tmp);

                if (res == 0)
                    res = SIGN(tmp.z);

                if (res != SIGN(tmp.z))
                {
                    if (tmp.z != 0)
                        return 0;
                }                

                poligon.RemoveAt(n-1);
               
               

                return res;
            }

        /// <summary>
        /// Поиск нормалей 
        /// </summary>
        /// <param name="polygon">набор вершин многоугольника</param>
        /// <param name="obhod">направление обхода</param>
        /// <param name="normVect">набор векторов</param>
        private void FindNormVectors(List<Point> polygon, int obhod, ref List<Vector> normVect) 
        {
            int n = polygon.Count -1;
            Vector b=new Vector();

            for(int i = 0; i < n; i++) {
                b =new Vector(polygon[i+1], polygon[i]);

                if(obhod == -1)
                    normVect.Add(new Vector(b.y, -b.x));
                else
                    normVect.Add(new Vector(-b.y, b.x));
            }

            //Фикс1
            b = new Vector(polygon[0], polygon[n]);
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
        /// <param name="polygon"></param>
        /// <param name="normVect"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
       int LineCutter(List<Point> polygon, List<Vector> normVect, ref Point p1, ref Point p2, ref bool visible)
       {
           visible = false;
           //Фикс2 здесь
           int n = polygon.Count;

           Vector D, W;
           int Dsk, Wsk;
          
           double tbot = 0, ttop = 1;
           double t;
           D = new Vector(p2, p1); //Вектор нашего отрезка

           //цикл отсечения отрезка по всем граням
           for (int i = 0; i < n; i++)
           {
               W = new Vector(p1, polygon[i]);//вектор соединяющий начало отезка и вершину многоугольника

               Dsk = ScalarMultiplication(D, normVect[i]); //Показывает угол и с какой стороны угол

               Wsk = ScalarMultiplication(W, normVect[i]);// Видимость для парелльных

               //Если D И сторона параллельны
               if (Dsk == 0)
               {
                   //За пределами 
                   if (Wsk < 0)
                       return 0;
               }
               else
               {
                   //Если T [0 до 1] то точка пересечения на отрезке
                   t = -Wsk / (double)Dsk;

                   //точка пересения ближе к начало
                   //верктор D и N В одну сторону
                   if (Dsk > 0)
                   {
                       //Точка пересечния вне отрезка
                       if (t > 1)
                           return 0;
                       else
                       {
                           //отрезок P направлен с внутренней на внешнюю сторону ребра E. 
                           //В этом случае параметр tA заменяется на tE, если tE > tA.
                           tbot = Math.Max(tbot, t);
                       }
                   }
                   else
                   {
                       if (t < 0)
                           return 0;
                       else
                       {
                           //отрезок P направлен с внешней на внутреннюю сторону ребра E. 
                           //В этом случае параметр tB заменяется на tE, если tE < tB.
                           ttop = Math.Min(ttop, t);
                       }
                   }
               }
           }
           //заданная параметрами t* часть отрезка P видима
           if (tbot <= ttop)
           {
               Point tmp = P(tbot, p1, p2);
               p2 = P(ttop, p1, p2);
               p1 = tmp;
               visible = true;
           }
           return 0;
       }

       int Algoritm(List<Point> polygon, List<Line> lines)
       {
           //poligon.Add(poligon[0]);
           //проверка ны выпуклость
           int direction = IsConvexPolygon(polygon);
           if (direction == 0)
               return 1;

           //поизк нормалей
           List<Vector> normVect = new List<Vector>(); 
           FindNormVectors(polygon, direction, ref normVect);

           bool visible = true;

           for (int i = 0; i < lines.Count; i++)
           {
               Line s = lines[i];
               //отсечение линии
               LineCutter(polygon, normVect, ref s.p1, ref s.p2, ref visible);
               
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
           
            int code=Algoritm(poligon, lines);
            if (code == 1)
                System.Windows.Forms.MessageBox.Show("Похоже он не выпуклый");
        }
    }
}
