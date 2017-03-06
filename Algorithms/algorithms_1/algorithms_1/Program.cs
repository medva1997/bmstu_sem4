using System;
using System.Collections.Generic;

namespace algorithms_1
{
    internal class Program
    {

        public struct data
        {
            public double YValue, DeltaXvalue;
            public double Xleft,Xright;

            public data(double y,double left, double rigth, double F)
            {
                this.YValue = y;
                this.Xleft = left;
                this.Xright = rigth;
                this.DeltaXvalue = F;

            }
        }

        public static double func(double x)
        {
            return Math.Sin( x);
        }

        public static void Main(string[] args)
        {

            Console.WriteLine("Введите начальное значение x для генерации таблицы: ");
            double xStart = -1;
            //double xStart = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите конечное значение x для генерации таблицы: ");
            //double xEnd = 8;
            double xEnd= Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите шаг x для генерации таблицы: ");
            //double xStep = 1;
            double xStep = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите x для вычисления результата: ");
            //double xSearch = 1.5;

            double xSearch = Convert.ToDouble(Console.ReadLine().Replace(',','.'));

            Console.Write("Введите степень полинома: ");
            //int N=4 ;
            int N= Convert.ToInt32(Console.ReadLine())+1;

            List<data> fullyTable=new List<data>();
            List<data> yTable=new List<data>();
            List<double> xTable=new List<double>();
            generate_matrix(xStart,xEnd,xStep,xTable,fullyTable);

            int stindex = 0;
            while ((stindex < fullyTable.Count)&&(fullyTable[stindex].Xleft<xSearch))
            {
                stindex++;

            }
            stindex = stindex - N / 2;

            if (stindex < 0)
                stindex = 0;
            if (stindex+N/2 >= fullyTable.Count)
                stindex = fullyTable.Count - N;

            Console.WriteLine();
            Console.WriteLine("Для решения будут использованы следующие узлы");
            Console.WriteLine("  X\t\t Y");
            for (int i = stindex,j=0;  j<N ; j++,i++)
            {
                yTable.Add(fullyTable[i]);
                Console.WriteLine("{0:0.000} \t\t{1:0.000}", fullyTable[i].Xleft, fullyTable[i].YValue);

            }

            double rez = 0;
            Console.WriteLine();
            Console.Write("Полином:\n\tY("+xSearch+")=");
            while (yTable.Count!=0)
            {
                for (int i = 0; i < yTable.Count; i++)
                {
                    //Console.WriteLine(yTable[i].YValue+"          "+yTable[i].DeltaXvalue);
                }
                Console.Write("({0:0.000}*{1:0.000})",yTable[0].YValue,yTable[0].DeltaXvalue);
                rez += +yTable[0].YValue * yTable[0].DeltaXvalue;
                if (yTable.Count != 1)
                {
                    Console.Write("+");
                }
                yTable = Сounter(yTable,xStep,xSearch);
            }
            Console.WriteLine();
            Console.WriteLine("Рассчитанное значение: {0:0.0000000}",rez);
            Console.WriteLine("Реальное значение:     {0:0.0000000}",func(xSearch));
            Console.WriteLine("Погрешность:           {0:0.0000000}%",Math.Abs((rez-func(xSearch))/func(xSearch))*100);


/*

            List<double> FullyTable=new List<double>();
            List<double> yTable=new List<double>();
            List<double> FullxTable=new List<double>();
            List<double> xTable=new List<double>();

            generate_matrix(xStart,xEnd,xStep,FullxTable,FullyTable);
            int stindex = 0;
            while (FullxTable[stindex]<xSearch)
            {
                stindex++;

            }
            stindex = stindex - N / 2;

            if ((stindex + N) >= (FullxTable.Count - 1))
                stindex = FullxTable.Count - 1 - N;

            if (stindex < 0)
                stindex = 0;

            Console.WriteLine();
            Console.WriteLine("Для решения будут использованы следующие узлы");
            Console.WriteLine("  X\t\t Y");

            for (int i = stindex,j=0;  j<N ; j++,i++)
            {
                yTable.Add(FullyTable[i]);
                xTable.Add(FullxTable[i]);
                Console.WriteLine("{0:0.000} \t\t{1:0.000}", FullxTable[i], FullyTable[i]);

            }

            Console.WriteLine();
            Console.Write("Полином :\n\tY("+xSearch+")=");
            double rez = Newton2(xSearch,N,xTable,yTable);

            Console.WriteLine();
            Console.WriteLine("Рассчитанное значение: {0:0.0000000}",rez);
            Console.WriteLine("Реальное значение:       {0:0.0000000}",func(xSearch));
            Console.WriteLine("Погрешность:           {0:0.0000000}%",Math.Abs((rez-func(xSearch))/func(xSearch))*100);

*/
            //Console.ReadLine();
        }

//VAR1
        public static void generate_matrix(double xStart, double xEnd, double step,List<double> xTable,List<data> yTable)
        {
            for(double i=xStart;i<=xEnd;i+=step)
            {
                xTable.Add(i);
                yTable.Add(new data(func(i),i,i,step));
            }
        }

        public static double Getproiz(double xleft, double xrigth, double step, double xSearch)
        {
            double rez = 1;
            for (double i = xleft; i <xrigth; i += step)
            {
                rez *= (xSearch - i);
            }
            return rez;
        }

        public static  List<data> Сounter( List<data> yTable,double step, double xSearch)
        {
            List<data> newYTable=new List<data>();
            for (int i = 0; i < yTable.Count-1; i++)
            {
                double tempy = (yTable[i].YValue - yTable[i + 1].YValue) / (yTable[i].Xleft - yTable[i + 1].Xright);
                double tempf = Getproiz(yTable[i].Xleft, yTable[i + 1].Xright, step, xSearch);
                newYTable.Add(new data(tempy,yTable[i].Xleft , yTable[i + 1].Xright,tempf));
            }
            return newYTable;
        }


//VAR2
        public static void generate_matrix(double xStart, double xEnd, double step,List<double> xTable,List<double> yTable)
        {
            for(double i=xStart;i<=xEnd;i+=step)
            {
                xTable.Add(i);
                yTable.Add(func(i));
            }
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