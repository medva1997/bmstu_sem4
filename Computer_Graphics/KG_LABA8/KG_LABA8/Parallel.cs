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


        private void GenerateLines(List<Line> lines_list)
        {
            for(int i=0; i<polynom.Count-1; i++)
            {
                lines_list.Add(new Line(polynom[i], polynom[i + 1]));
                lines_list.Add(new Line(polynom[i+1], polynom[i]));
            }          
        }

        private double CountK(Line line)
        {
            double K = 0;

            if (line.first.X != line.second.X)
                K = (line.second.Y - line.first.Y)*1.0 / (line.second.X - line.first.X);
            else
                K = Math.Pow(10, 30);
            return K;
        }

        private List<double> CountKForLines(List<Line> lines_list)
        {
            List<double> K = new List<double>();

            for (int i = 0; i < lines_list.Count; i++)
            {
                K.Add(CountK(lines_list[i])); 
                //K.Add(CountK(new Line(lines_list[i].second,lines_list[i].first)));
            }
            return K;
        }


        public Point SearchNearest(List<Point> polynom, Point s, Point p)
        {
            this.polynom = polynom;
            this.polynom.Add(polynom[0]);
            Point newp = new Point(p.X, p.Y);
            List<Line> lines_list = new List<Line>();
            GenerateLines(lines_list);
            Line current_line = new Line(s, p);
            
            List<double> K = CountKForLines(lines_list);

            double current_k=0;
            if (Math.Abs(s.X - p.X) < 2)
            {
                current_k = Math.Pow(10, 30);
            }
            else
            {
                current_k = CountK(current_line);
            }

            double closer_k = K[0];

            for (int i = 1; i < K.Count; i++)
            {
                if (Math.Abs(current_k - K[i]) < Math.Abs(current_k - closer_k))
                {
                    closer_k = K[i];
                }
            }

            this.polynom.RemoveRange(polynom.Count - 1, 1);

            lines_list.Clear();
            K.Clear();
            if (closer_k == Math.Pow(10, 30))
            {
                newp.X = s.X;
                return newp;
            }

            if (closer_k < 1)
            {
                double b = s.Y - closer_k * s.X;

                newp.Y = (int)(closer_k * newp.X+b);
            }
            else
            {
                double b = s.Y - closer_k * s.X;

                newp.Y = (int)(closer_k * newp.X + b);
            }
                     
            return newp;
        }
        
      
    }
}
