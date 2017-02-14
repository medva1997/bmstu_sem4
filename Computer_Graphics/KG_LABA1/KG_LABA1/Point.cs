using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG_LABA1
{
    class Point
    {
        public double x;
        public double y;        


        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double GetLength(Point first_p,Point second_p)
        {
            return Math.Sqrt(Math.Pow(first_p.x - second_p.x, 2) + Math.Pow(first_p.y - second_p.y, 2));
        }

        //Площадь вписанной окружности
        public double AreaInside(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            double r = Math.Sqrt((p - a) * (p - b) * (p - c) / p);
            return Math.PI * r * r;
        }

        //Площадь описанной окружности
        public double AreaOutside(double a, double b, double c)
        {
            double p  = (a + b + c) / 2;
            double r = a * b * c / Math.Sqrt((p - a) * (p - b) * (p - c) * p) / 4;
            return Math.PI*r*r;
        }
       
        //Разница площади
        public double AreaDelta(Point point2, Point point3)
        {
            double a, b, c;
            //находим стороны
            a = GetLength(this, point2);
            b = GetLength(this, point3);
            c = GetLength(point2, point3);
            double arout = AreaOutside(a, b, c);
            double arin = AreaInside(a, b, c);

            return Math.Abs(arout - arin);
        }

        public int GetXint
        {
            get { return (int)x; }
        }
        public int GetYint
        {
            get { return (int)y; }
        }

        public float GetXfloat
        {
            get { return (float)x; }
        }
        public float GetYfloat
        {
            get { return (float)y; }
        }

        public System.Drawing.PointF GetPointF
        {
            get { return new System.Drawing.PointF((float)x, (float)y); }
        }


        public System.Drawing.PointF serchtchentre(Point point2, Point point3)
        {
            double ma, mb,y1,y2,y3,x1,x2,x3,xa,ya;
            y1 = this.y;
            x1 = this.x;
            y2 = point2.y;
            x2 = point2.x;
            y3 = point3.y;
            x3 = point3.x;
            ma = (y2 - y1) / (x2 - x1);
            mb = (y3 - y2) / (x3 - x2);
            xa = (ma * mb * (y1 - y3) + mb * (x1 + x2) - ma * (x2 + x3)) / (2 * (mb - ma));
            ya = -1 / ma * (x - (x1 + x2) / 2) + (y1 + y2) / 2;
            return new System.Drawing.PointF((float)xa, (float)ya);
        }

    }
}
