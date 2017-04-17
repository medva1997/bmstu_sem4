using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms_6
{
    class Program
    {
            
        const int INF=1000000;

        static double F(double x)
        {
	        return Math.Exp(x);
        }

        static double FLog(double x)
        {
	        return Math.Log(x);
        }

        //Печать таблиц с отступом 
        static void print_matrix(double [,] matrix)
        {            
	        for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
        	        if (matrix[i,j] == INF)
                        Console.Write("--------\t");
          	        else
        	        {
	        	        if (matrix[i,j] - (int)matrix[i,j] == 0)
	        		        Console.Write("{0:#0.####}\t\t", (int)matrix[i,j]);
	        	        else
                            Console.Write("{0:#0.####}\t\t", matrix[i, j]);
        	        }
                }
                Console.WriteLine();
            }
        }

        //Генерация первоноачальной таблицы с 2 заполнеными столбиками
        static double[,] TableGenerate(double a, double b, int N)
        {
	        double h = (b-a)/N;
	        double[,] table = new double[N, 7];

	        for (int i = 0; i < N; i++)
	        {
		        table[i,0] = a;
		        table[i,1] = F(a);
		        a+=h;
	        }
	
	        return table;
        }


        //односторонняя 
        static double[,] OneSideDiff(double[,] table, int N)
        {
            // (y(i+1)-y(i))/(x(i+1)-x(i))   или y(i+1)-y(i)/H
	        for (int i = 0; i < N-1; i++)
		        table[i,2] = (table[i+1,1]-table[i,1])/(table[i+1,0]-table[i,0]);
	        table[N-1,2] = INF; 
	        return table;
        }

        //центральная 
        static double[,] CentralDiff(double[,] table, int N)
        {
	        //Y'n = (Y(n+1) - Y(n-1))/2h
	        table[0,3] = INF;
	        for (int i = 1; i < N-1; i++)
		        table[i,3] = (table[i+1,1]-table[i-1,1])/(table[i+1,0]-table[i-1,0]);
	        table[N-1,3] = INF;
	        return table;
        }

        // повышенная точность в граничных точках
        static double[,] EdgeNodes(double[,] table, int N)
        {
	        for (int i = 1; i < N-1; i++)
		        table[i,4] = INF;
	        table[0,4] = (-3*table[0,1] + 4*table[1,1] - table[2,1])/(table[2,0]-table[0,0]);
	        table[N-1,4] = (3*table[N-1,1] - 4*table[N-2,1] + table[N-3,1])/(table[N-1,0]-table[N-3,0]);
	        return table;
        }

        //Рунге по центральным разностям
        static double[,] RungeDiffCentre(double[,] table, int N)
        {
            double h = table[2,0] - table[0,0];
            double h2 = h * 2;

	        double [] vector1 =new double[N], vector2=new double[N];
	        for (int i = 2; i < N-2; i++)
	        {
		        vector1[i] = (table[i+1,1] - table[i-1,1])/h;
		        vector2[i] = (table[i+2,1] - table[i-2,1])/h2;
	        }

            table[0, 5] = INF;
            table[1, 5] = INF;
	        for (int i = 2; i < N-2; i++)
	        {
		        table[i,5] = vector1[i] + (vector1[i]-vector2[i])/3;
	        }            
	        table[N-2,5] = INF;
	        table[N-1,5] = INF;
	        return table;
        }

        //Рунге по односторонним разностям
        static double[,] RungeDiff(double[,] table, int N)
        {
            double h = table[1, 0] - table[0, 0];
            double h2 = h * 2;
            int p = 1;
            double[] vector1 = new double[N], vector2 = new double[N];
            
            for (int i = 0; i < N - 2; i++)
            {
		        vector1[i] = (table[i+1,1] - table[i,1]) / h;
		        vector2[i] = (table[i+2,1] - table[i,1]) / h2;
                table[i, 5] = vector1[i] + (vector1[i] - vector2[i]) / (Math.Pow(2,p) - 1);
            }

            
            for (int i = N-2; i < N; i++)
            {
		        vector1[i] = (table[i,1] - table[i-1,1]) / h;
		        vector2[i] = (table[i,1] - table[i-2,1]) / h2;
                table[i, 5] = vector1[i] + (vector1[i] - vector2[i]) / (Math.Pow(2,p) - 1);
            }          
            return table;
        }


        //Выравнивающие переменные (для экспоненты)
        static double [,]Alignment(double [,]table, int N)
        {
	        double [,]newtab = new double[N, 4];
	        for (int i = 0; i < N; i++)
	        {
		        newtab[i,0] = table[i,0];
		        newtab[i,1] = FLog(table[i,1]);
	        }
	        newtab = CentralDiff(newtab,N);
	        table[0,6] = INF;
	        for (int i = 1; i < N-1; i++)
	        {
		        if (table[i,0] != 0)
			        table[i,6] = newtab[i,3]*table[i,1];
		        else
			        table[i,6] = INF;
	        }
	        table[N-1,6] = INF;
	        
            
	        return table;
        }

       

        //котируем соседние с x строчки, n-количество нужных строк,
        static double [,] NewTable(double x, double [,]table, double [,]choose, int n, int count)
        {
	        double diffmin = Math.Abs(table[0,0]-x);
  	        int imin = 0; 
  	        for (int i = 1; i < count; i++)
     	        if  (Math.Abs(table[i,0]-x) < diffmin)
     	        {
        	        diffmin = Math.Abs(table[i,0]-x);
        	        imin = i;
     	        }
            //Console.WriteLine("imin={0}\n", imin);
  	        int nb = n/2;
  	        if (imin-nb < 0)
    	        nb = 0;
  	        else if (imin+nb >= count)
  		        nb = count-n;
  	        else
    	        nb = imin-nb;
  	        for (int i = 0; i <n; i++)
  	        {
    
    	        choose[i,0] = table[nb,0];
    	        choose[i,1] = table[nb,1];
    	        nb++;
  	        }
  	        return choose;
        }

        //разделенная разность
        static double [,]RazdRazn(double [,]choose, int n, double x)
        {
	        int k = 0;
	        for (int i =2; i < n+2; i++)
	        {
		        k++;
		        for (int j = 0; j < n+2-i; j++)
		        {
			        choose[j,i] = (choose[j,i-1] - choose[j+1,i-1])/(choose[j,0] - choose[j+k,0]);
		        }
	        }
  	        for (int i = 0; i < n+1; i++)
  		        choose[i,n+2] = x - choose[i,0];
	        return choose;
        }

        //часть сборки полинома
        static double z(double [,]choose, int ii, int n)
        {
	        double tmp = 1;
	        for (int i = 0; i <= ii; i++)
	        {
		        tmp *= choose[i,n+1];
	        }

	        double sum = 1;
	        if (tmp == 0)
	        {
		        for (int i = 0; i <= ii; i++)
			        if (choose[i,n+1] != 0)
				        sum *= choose[i,n+1];
		        return sum;
	        }

	        sum = 0;
	        for (int i = 0; i <= ii; i++)
	        {
		        sum += tmp/choose[i,n+1];
	        }
	        return sum;
        }
        
        //Собираем полином
        static double Polinom(double [,]choose, int n)
        {
	        
	        double y = choose[0,2];
	        Console.Write("({0})", y);
	        for (int i = 1; i< n-1; i++)
	        {
		        y += choose[0,2+i]*z(choose, i, n);
		        Console.Write(" + ({0:0.####})", choose[0,2+i]*z(choose, i, n));
	        }
	        
	        return y;
        }



        static void Main(string[] args)
        {
	        double a = 0.0;
	        double b = 3.2;
	        int N = 16;
	        double [,]table = TableGenerate(a,b,N);
	        table = OneSideDiff(table,N);//одностороняяя
	        table = CentralDiff(table, N);//центральная
	        table = EdgeNodes(table,N);//повышеной точности
	        table = RungeDiff(table,N);//формула рунге
	        table = Alignment(table, N); //выравнивающие

            Console.Write("X\t      Y\t\t\tOneSide\t\t Central\tEdgeNodes\tRunge\t\tAlignment\t\n");
            Console.Write("----------------------------------------------------------------------------------------------------------\n");
	        print_matrix(table);

	        Console.Write("\nInput number of nodes: ");
	        int n= Convert.ToInt32(Console.ReadLine());
	        Console.Write("Input X: ");
	        double X=Convert.ToDouble(Console.ReadLine().Replace(".", ","));
	        
	        double [,]razdraznTab = new double[n,n+2];
            table = NewTable(X,table,razdraznTab,n, N);
  	        table = RazdRazn(razdraznTab, n-1, X);

            Console.Write("X\t\tY\t\t");
  	        for (int i = 0; i < n-1; i++)
  		        Console.Write("\t\t");        
  	        Console.Write("X-xi\n");
  	        
  	        print_matrix(razdraznTab);
            Console.Write("------------------------------\nY'({0}) = ", X);
  	        double answ = Polinom(razdraznTab, n);
            Console.Write("= {1:0.######}", X, answ);
            Console.ReadKey();
        }

    }
}
