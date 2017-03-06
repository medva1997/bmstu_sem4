using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;

namespace algorithms_2
{
    internal class Program
    {
        public const double Eps = 0.000001;

        public static double Func1(double x, double y)
        {
            double x3 = Math.Pow(x, 3);
            double middle = x3 * (x3 - 2 * y - 2);
            double end = y * y + 2 * y + 2;
            return Math.Exp(x3 - y) - middle - end;

        }

        public static double Func2(double x, double y)
        {
            double x2 = Math.Pow(x, 2);
            return x2 * Math.Exp(-y) + y * Math.Exp(-y) - Math.Exp(x2) * Math.Log(x2 + y);
        }

        public static double SearchyFunc1(double x, double left, double right)
        {
            double eps = (right - left) * Eps;
            while (true)
            {
                double mid = (right + left) / 2;

                if (Math.Abs(right - left) < eps * Math.Abs(mid) + Eps)
                {
                    break;
                }
                double fm = Func1(x, mid);
                if (fm == 0)
                    return mid;
                if (fm * Func1(x, right) < 0)
                    left = mid;
                else
                {
                    right = mid;
                }
            }
            return Math.Abs(Func1(x, left)) > Math.Abs(Func1(x, right)) ? right : left;
        }

        public static double SearchyFunc2(double x, double left, double right)
        {
            double eps = (right - left) * Eps;
            while (true)
            {
                double mid = (right + left) / 2;

                if (Math.Abs(right - left) < eps * Math.Abs(mid) + Eps)
                {
                    break;
                }
                double fm = Func2(x, mid);
                if (fm == 0)
                    return mid;
                if (fm * Func2(x, right) < 0)
                    left = mid;
                else
                {
                    right = mid;
                }
            }
            return Math.Abs(Func2(x, left)) > Math.Abs(Func2(x, right)) ? right : left;
        }

        public static double GetY1(double x)
        {
            return  SearchyFunc1(x, -1, 2);
        }

        public static double GetY2(double x)
        {
            return  SearchyFunc2(x, 0.1, 2);
        }


        public struct Data
        {
            public double X;
            public double Y;

            public  Data(double x, double y)
            {
                X = x;
                Y = y;
            }

        }

        public static int SortByY(Data item1, Data item2)
        {
            if (item1.Y > item2.Y)
                return 1;
            return -1;
        }

        public static List<Data> generate_matrix(double xStart, double xEnd, double step)
        {
            List<Data> table=new List<Data>();
            for(double i=xStart;i<xEnd+step;i+=step)
            {
                table.Add(new Data(i,GetY1(i)-GetY2(i)));
            }
            //table.Sort(SortByY);
            return table;
        }

        public static List<Data> Configuration(List<Data> table, double y, int n)
        {
            int stindex = 0;
            while ((stindex < table.Count) && (table[stindex].Y < y))
            {
                stindex++;

            }
            stindex = stindex - n / 2;

            if (stindex < 0)
                stindex = 0;
            if (stindex + n >= table.Count)
            {
                stindex = table.Count - n ;

            }



            List<Data> newTable =new List<Data>();

            for (int i = stindex, j = 0; j < n; j++, i++)
            {
                newTable.Add(table[i]);
            }
            return newTable;
        }



        public static void Main(string[] args)
        {
            List<Data> table = generate_matrix(0, 2, 0.1);
            double xsearch = 0;
            int n = 4;

            List<Data> confTable = Configuration(table, xsearch, n+1);
            List<double> xtable=new List<double>();
            List<double> ytable = new List<double>();

            for (int i = 0; i < confTable.Count(); i++)
            {
                xtable.Add(confTable[i].Y);
                ytable.Add(confTable[i].X);
            }

            double x=Newton2(xsearch,n, xtable, ytable);
            Console.WriteLine();
            Console.WriteLine("Результат x= {0}",x);
            double y1 = GetY1(x);
            double y2 = GetY2(x);
            Console.WriteLine("y= {0} ",y1+(y2-y1)/2);
            //Console.WriteLine("y1= {0} ",y1);
            //Console.WriteLine("y2= {0} ",y2);
        }


        public static double RazdelennayRaznost(int m, List<double> x, List<double> y)
        {
            double rezult = 0;
            for (int j = 0; j <= m; j++) //следующее слагаемое полинома
            {
                double down = 1;
                //считаем знаменатель разделенной разности
                for (int k = 0; k <= m; k++)
                {
                    if (k != j) down *= (x[j] - x[k]);
                }
                //считаем разделенную разность
                rezult += y[j] / down;
            }
            return rezult;
        }

        public  static  double Newton2(double xSerch, int n,List<double> x, List<double> y)
        {
            double result = y[0], F;

            for (int i = 1; i < n; i++)
            {
                F=RazdelennayRaznost(i, x, y);

                double proiz = 1;
                for (int k = 0; k < i; k++) proiz *= (xSerch - x[k]);

                Console.Write("({0:0.000}*{1:0.000})",F,proiz);
                //домножаем разделенную разность на скобки (x-x[0])...(x-x[i-1])
                result += F*proiz;
            }
            return result;
        }


    }
}