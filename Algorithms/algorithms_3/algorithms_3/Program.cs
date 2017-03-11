using System;
using System.Collections.Generic;
using System.Dynamic;

namespace algorithms_3
{
    internal class Program
    {
        public struct GeneratorData
        {
            public double Start, End, Step;

            public GeneratorData(double start=0, double end=0, double step=0)
            {
                Start = start;
                End = end;
                Step = step;
            }

            public void Input(string label)
            {
                Console.Write("Введите начальное значение "+label+" для генерации таблицы: ");
                Start = Convert.ToDouble(Console.ReadLine().Replace(',','.'));

                Console.Write("Введите конечное значение "+label+" для генерации таблицы: ");
                End= Convert.ToDouble(Console.ReadLine().Replace(',','.'));

                Console.Write("Введите шаг "+label+" для генерации таблицы: ");
                Step = Convert.ToDouble(Console.ReadLine().Replace(',','.'));
            }

        }

        public struct SearchData
        {
            public double XSearch, YSearch;
            public int NX, NY;
            public SearchData(double xSearch=2.3, double ySearch=2.3, int nx=3, int ny=3)
            {
                XSearch = xSearch;
                YSearch = ySearch;
                NX = nx+1;
                NY = ny+1;
            }

            public void Input()
            {
                Console.Write("Введите искомое X: ");
                XSearch = Convert.ToDouble(Console.ReadLine().Replace(',','.'));

                Console.Write("Введите искомое Y: ");
                XSearch = Convert.ToDouble(Console.ReadLine().Replace(',','.'));

                Console.Write("Введите степень полинома для интерполяции x: ");
                NX = Convert.ToInt32(Console.ReadLine().Replace(',','.'));

                Console.Write("Введите степень полинома для интерполяции y: ");
                NY = Convert.ToInt32(Console.ReadLine().Replace(',','.'));
            }

        }

        public struct Data
        {
            public List<double>Xlist;
            public List<double>Ylist;
            public List<List<double>>Zlist;


        }

        public static double Func(double x, double y)
        {
            return x * x + y * y;
            return Math.Sin(x) + Math.Cos(y * y) + x * x - y * y;

        }

        public static void Generator(ref  GeneratorData xValues,ref  GeneratorData yValues, ref Data initialData)
        {
            initialData.Xlist=new List<double>();
            initialData.Ylist=new List<double>();
            initialData.Zlist=new List<List<double>>();

            for (double j = yValues.Start; j < yValues.End + yValues.Step; j += yValues.Step)
            {
                initialData.Ylist.Add(j);
            }

            for (double i = xValues.Start; i < xValues.End + xValues.Step; i += xValues.Step)
            {
                initialData.Xlist.Add(i);
                List<double> line=new List<double>();
                for (double j = yValues.Start; j < yValues.End + yValues.Step; j += yValues.Step)
                {
                    line.Add(Func(i,j));
                }
                initialData.Zlist.Add(line);
            }

        }

        public static int  GetIndex(List<double> list, double value,int n)
        {
            int stindex = 0;
            while ((stindex < list.Count) && (list[stindex] < value))
            {
                stindex++;
            }
            stindex = stindex - n / 2;
            if (n % 2 == 1)
                stindex--;

            if (stindex < 0)
                stindex = 0;
            if (stindex + n >= list.Count)
            {
                stindex = list.Count - n ;

            }
            return stindex;
        }

        public static Data GetByX(ref Data initialData, ref SearchData searchData)
        {
            Data newData=new Data();
            newData.Xlist=new List<double>();
            newData.Ylist=new List<double>();
            newData.Zlist=new List<List<double>>();

            int stindex=GetIndex(initialData.Xlist, searchData.XSearch, searchData.NX);


            newData.Ylist = initialData.Ylist;
            for (int i = stindex; i < stindex+searchData.NX; i++)
            {
                    newData.Xlist.Add(initialData.Xlist[i]);
                    newData.Zlist.Add(initialData.Zlist[i]);
            }
            return newData;

        }

        public static Data GetByY(ref Data initialData, ref SearchData searchData)
        {
            Data newData=new Data();
            newData.Xlist=new List<double>();
            newData.Ylist=new List<double>();
            newData.Zlist=new List<List<double>>();

            int stindex=GetIndex(initialData.Ylist, searchData.YSearch, searchData.NY);

            newData.Xlist = initialData.Xlist;

            for (int i = stindex; i < stindex+searchData.NY; i++)
            {
                newData.Ylist.Add(initialData.Ylist[i]);
            }

            for (int i = 0; i < newData.Xlist.Count; i++)
            {
                List<double> line=new List<double>();
                for (int j = stindex; j < stindex + searchData.NY; j++)
                {
                    line.Add(initialData.Zlist[i][j]);
                }
                newData.Zlist.Add(line);
            }
            return newData;

        }



        public static void Main(string[] args)
        {
            Data initialData=new Data();


            SearchData searchData= new SearchData(1.5,1.5,1,1);
            GeneratorData xValues = new GeneratorData(0, 5, 1);
            GeneratorData yValues = new GeneratorData(0, 5, 1);

            Generator(ref  xValues, ref yValues, ref initialData);
            searchData.Input();

            //Вырезаем маленький прямоугольник
            initialData = GetByX(ref  initialData, ref searchData);
            initialData = GetByY(ref  initialData, ref searchData);

            List<double> Yanswers=new List<double>();
            for (int i = 0; i < initialData.Xlist.Count; i++)
            {
                Yanswers.Add(Newton2(searchData.YSearch,searchData.NY,initialData.Ylist,initialData.Zlist[i]));

            }
            double rez = Newton2(searchData.XSearch, searchData.NX, initialData.Xlist, Yanswers);
            Console.WriteLine("Результат: {0:0.0000}",rez);
            Console.WriteLine("Реальное значение: {0:0.000}", Func(searchData.XSearch,searchData.YSearch));
            Console.WriteLine("Погрешность:    {0:0.00000}%",Math.Abs((rez-Func(searchData.XSearch,searchData.YSearch))/Func(searchData.XSearch,searchData.YSearch))*100);

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

                //Console.Write("({0:0.000}*{1:0.000})",F,proiz);
                //домножаем разделенную разность на скобки (x-x[0])...(x-x[i-1])
                result += F*proiz;
            }
            return result;
        }
    }
}