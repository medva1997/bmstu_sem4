using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG_LABA9
{
    struct Line
    {
        public bool empty;
        public Point p1;
        public Point p2;

        public Line(Point a, Point b)
        {
            empty = false;

            if (a == b) //todo
            {
                empty = true;
                p1 = a;
                p2 = b;

            }
            if ((a.X > b.X) || (a.X == b.X && a.Y > b.Y))
            {
                p2 = a;
                p1 = b;
            }
            else
            {
                p1 = a;
                p2 = b;
            }
           
        }

        public bool isEqual(Line s)
        {
            if (p1==s.p1 && p2==s.p2)
                return true;
            if (p1==s.p2 && p2==s.p1)
                return true;
            return false;
        }
    };

    struct Vector
    {
        public int x;
        public int y;
        public int z;

        public Vector(int a, int b, int c = 0)
        {
            x = a;
            y = b;
            z = c;
        }
        public Vector(Point end, Point start)
        {
            x = end.X - start.X;
            y = end.Y - start.Y;
            z = 0;
        }

        public Vector(Line line)
        {
            

            x = line.p2.X - line.p1.X;
            y = line.p2.Y - line.p1.Y;
            z = 0;
        }
    };

    class DNO
    {
       

        public static void VectorMult(Vector a, Vector b, ref Vector res)
        {
            res.x = a.y * b.z - a.z * b.y;
            res.y = a.z * b.x - a.x * b.z;
            res.z = a.x * b.y - a.y * b.x;
        }

        public static int ScalarMultiplication(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// проверка знака числа
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int SIGN(int x)
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
        public static int IsConvexPolygon(List<Point> poligon)
        {

            poligon.Add(poligon[0]);


            Vector a = new Vector(poligon[1], poligon[0]);
            Vector b = new Vector();
            Vector tmp = new Vector();
            int n = poligon.Count;

            int res = 0;

            for (int i = 1; i < n - 1; i++)
            {
                b = new Vector(poligon[i + 1], poligon[i]);
                VectorMult(a, b, ref tmp);

                if (res == 0)
                    res = SIGN(tmp.z);

                if (res != SIGN(tmp.z))
                {
                    if (tmp.z != 0)
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

            poligon.RemoveAt(n - 1);



            return res;
        }

        /// <summary>
        /// Поиск нормалей 
        /// </summary>
        /// <param name="polygon">набор вершин многоугольника</param>
        /// <param name="obhod">направление обхода</param>
        /// <param name="normVect">набор векторов</param>
        public static void FindNormVectors(List<Point> polygon, int obhod, ref List<Vector> normVect)
        {
            int n = polygon.Count - 1;
            Vector b = new Vector();

            for (int i = 0; i < n; i++)
            {
                b = new Vector(polygon[i + 1], polygon[i]);

                if (obhod == -1)
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

        public static List<Point> CloneList(List<Point> original)
        {
            List<Point> clone = new List<Point>();
            for (int i = 0; i < original.Count; i++)
            {
                clone.Add(original[i]);
            }
            return clone;
        }

        public static List<Line> CloneList(List<Line> original)
        {
            List<Line> clone = new List<Line>();
            for (int i = 0; i < original.Count; i++)
            {
                clone.Add(original[i]);
            }
            return clone;
        }

        // Distanse between two points
        public static double Distanse(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        // Distanse between line and point
        public static double FindDistanse(Point a, Point b, Point p)
        {
            double ab;
            ab = DNO.Distanse(a, b);
            double d = Math.Abs((b.Y - a.Y) * p.X - (b.X - a.X) * p.Y + b.X * a.Y - b.Y * a.X) / ab;
            return d;

        }

        //nearest line to point
        public static Point[] FindNearSide(Point p, List<Point> poligone1)
        {
            Point[] side = new Point[2];
            int minside = 0;
            side[0] = poligone1[0];
            side[1] = poligone1[1];
            double mindistance = DNO.FindDistanse(poligone1[0], poligone1[1], p);
            int n = poligone1.Count;
            double tmp;
            for (int i = 0; i < n - 1; i++)
            {
                tmp = DNO.FindDistanse(poligone1[i], poligone1[i + 1], p);
                if (tmp < mindistance)
                {
                    mindistance = tmp;
                    minside = i;
                    side[0] = poligone1[i];
                    side[1] = poligone1[i + 1];
                }
            }

            tmp = DNO.FindDistanse(poligone1[n - 1], poligone1[0], p);
            if (tmp < mindistance)
            {
                mindistance = tmp;
                minside = n - 1;
                side[0] = poligone1[n - 1];
                side[1] = poligone1[0];
            }

            return side;

        }

    }
}
