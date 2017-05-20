using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace KG_LABA8
{
    class Parallel
    {
        private List<Point> polynom;
        private Line NearestSide;

        struct Line
        {
            public Point first;
            public Point second;

            public Line(Point a, Point b)
            {
                first = a;
                second = b;
            }
        };

        public Point SearchNearest(ref List<Point> polynom,Point s, Point p)
        {
            this.polynom = polynom;
            findNearSide(p);
            Point newp=new Point(p.X,p.Y);
            calculateParalPoint(s, ref newp);
            return newp;

        }


        private double distanse(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        private double findDistanse(Point a, Point b, Point p)
        {
            double ab, bp, ap;
            ab = distanse(a, b);
            bp = distanse(p, b);
            ap = distanse(a, p);
            if (bp * bp + ab * ab < ap * ap)
            {
                return bp;
            }
            if (bp * bp > ab * ab + ap * ap)
            {
                return ap;
            }
            double d = Math.Abs((b.Y - a.Y) * p.X - (b.X - a.X) * p.Y + b.X * a.Y - b.Y * a.X) / ab;
            return d;
        }

        private void findNearSide(Point p)
        {
            int minside = 0;
            double mindistance = findDistanse(polynom[0], polynom[1], p);
            int n = polynom.Count - 1;
            for (int i = 0; i < n; i++)
            {
                double tmp = findDistanse(polynom[i], polynom[i + 1], p);
                if (tmp < mindistance)
                {
                    mindistance = tmp;
                    minside = i;
                }
            }
            NearestSide = new Line(polynom[minside], polynom[minside + 1]);
        }

        private void calculateParalPoint(Point s, ref Point p)
        {
            if (NearestSide.first.X == NearestSide.second.X)
            {
                p.X = s.X;
                return;
            }
            double k = (NearestSide.first.Y - NearestSide.second.Y) / (double)
                       (NearestSide.first.X - NearestSide.second.X);
            p.Y = (int)(Math.Round((p.X - s.X) * k) + s.Y);
        }
    }
}
