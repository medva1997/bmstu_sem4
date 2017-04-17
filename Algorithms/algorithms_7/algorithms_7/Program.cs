using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms_7
{
    class Program
    {
        const int MAX_STEPS =5000;
       

        static bool DoubleEqual(double compare, double with)
        {
            if (Math.Abs(compare - with) < 0.000001)
                return true;
            return false;
        }

        //функция для полинома Лежандра
        static double funcLegendre(double x, int n)
        {           
            double p_prev = 1, p_curr = x;
            if (n == 0)
                return p_prev;
            for (int i = 1; i < n; i++)
            {
                double res = ((2.0 * i + 1.0) / (i + 1.0)) * x * p_curr - (i / (i + 1.0)) * p_prev;
                p_prev = p_curr;
                p_curr = res;
            }
            return p_curr;
        }           

        static bool Dichotomy(Func<double,int,double> myfunc, int n, double alpha, double a, double b, ref double x) //func(x,n)
        {
            x = 0;
            if (a == b)
            {
                return true;
            }
            if (((myfunc(a, n)-alpha) * (myfunc(b, n)-alpha)) > 0)
		        return false;
	        double c;

            //не срываемся в бесконечный луп, а повторяем M шагов
	        for (int i=0; i<MAX_STEPS; i++)
	        {
		        c = (a+b)/2;

                if (Math.Abs(myfunc(c, n)-alpha) < 0.000001)
		        {
			        x = c;
			        return true;
		        }

                if ((myfunc(a, n)-alpha) * (myfunc(c, n)-alpha) < 0)
		        {
			        b = c;
			        continue;
		        }
                if ((myfunc(c, n)-alpha) * (myfunc(b, n)-alpha) < 0)
			        a = c;
	        }
	        return false;
        }

        
        //возвращает массив из n корней полинома Лежандра
        static List<double> SolveLegendre(int n)
        {
            int count; //количество уже найденных корней
            int parts = n;  //количество отрезков дл¤ поиска дихотомией
            double[] roots = new double[n]; //список из n корней; они всегда будут
            do
            {
                count = 0;
                parts *= 3; //если не смогли найти n корней, то увеличиваем число разбиений
                double step = 2.0 / parts; //чем больше корней надо найти, тем меньше шаг между отрезками дихотомии
                double a = -1;
                double b = a + step;
                for (int i = 0; i < parts; i++) //проходимся по всем отрезкам дихотомией
                {
                  
                    if (Dichotomy(new Func<double,int,double>(funcLegendre), n, 0, a, b, ref roots[count]))
                    {                        
                        count++;
                    }
                    if (count == n) //не крутимся в цикле, если уже все нашли
                        break;

                    a += step;
                    b += step;
                }
            }
            while (count < n); //повторяем процесс, пока не найдем все корни

            return roots.ToList();
        }

        
        static List<double> GaussEq_Solve(List<List<double>> coeffs_OR, List<double> answers_OR, int N)
        {
            List<double> PTemp;		
            double Temp;

            //Делаем копии
            List<List<double>> coef = coeffs_OR; //матрица коэффициентов
            List<double> answ = answers_OR; //столбец "ответов"

            //спускаемся по диагонали матрицы коэффициентов
            for (int k = 0; k < N; k++)
            {
                //если "верхнее" (тоесть диагональное) число в этом столбце ноль, то мен¤ем строку матрицы с другой строкой,
                //в которой в этом столбце нул¤ нет
                //при этом считаем что не ноль , если его нет - то это какая-то хрень и ¤ не хочу устраивать проверку на это здесь ^^
                if (DoubleEqual(coef[k][k], 0))
                {
                    int Row = 0; ;
                    for (int i = k; i < N; i++)
                        if (!DoubleEqual(coef[i][k], 0))
                        {
                            Row = i;
                            break;
                        }

                    //нашли номер строки без нуля, меняем еЄ местами со строкой с нулем
                    PTemp = coef[Row];
                    coef[Row] = coef[k];
                    coef[k] = PTemp;
                    Temp = answ[Row];
                    answ[Row] = answ[k];
                    answ[k] = Temp;
                }

                //k-ю строку делим на k элемент
                Temp = coef[k][k];
                for (int i = 0; i < N; i++)
                {
                    coef[k][i] /= Temp;
                }
                answ[k] /= Temp;

                //от предыдущих строк отнимаем k-ю строку умноженную на k-й элемент этой предыдущей строки
                for (int i = 0; i < k; i++)
                {
                    Temp = coef[i][k];
                    for (int j = 0; j < N; j++)
                    {
                        coef[i][j] -= coef[k][j] * Temp;
                    }
                    answ[i] -= answ[k] * Temp;
                  
                }

                //от оставшихся строк отнимаем k-ю строку умноженную на k-й элемент этой оставшейся строки
                for (int i = k + 1; i < N; i++)
                {
                    Temp = coef[i][k];
                    for (int j = 0; j < N; j++)
                    {
                        coef[i][j] -= coef[k][j] * Temp;
                    }
                    answ[i] -= answ[k] * Temp;
                    
                }
            }

            coef.Clear();
            return answ;
        }

        //находит массив Ai исходя из n узлов и найденных ti корней Лежандра
        static List<double> searchA(List<double> t, int n)
        {
            List<List<double>> tCoeffs = new List<List<double>>();  //матрица коэффициентов из ti
            List<double> answers = new List<double>();//столбец ответов

            for (int k = 0; k < n; k++)
            {
                tCoeffs.Add(new List<double>());
                for (int i = 0; i < n; i++)             //заполн¤ем строки коэффициентов
                    tCoeffs[k].Add(Math.Pow(t[i], k));

                if (k % 2 == 1) //заполн¤ем столбец ответов
                    answers.Add(0);
                else
                    answers.Add(2.0 / (k + 1));
            }

            return GaussEq_Solve(tCoeffs, answers, n);
        }
        
        
        static double integrateByGauss(double a, double b, int n, Func<double, double> myFunc)
        {
            if (DoubleEqual(a, b))  //интеграл на нулевом промежутке равен нулю
                return 0.0;

            List<double> t = SolveLegendre(n); //находим ti и Ai
            List<double> A = searchA(t,n);
           
            double integlal_value = 0;
            for (int i = 0; i < n; i++)
            {
                double x_i = (b - a) / 2 * t[i] + (b + a) / 2;
                integlal_value += A[i] * myFunc(x_i);
            }
            integlal_value *= (b - a) / 2;
            return integlal_value;
        }

        static double funcError(double t)
        {
            return Math.Exp(-(t * t)/2);
        }

       

       static bool MYDichotomy(Func<double, double> myfunc, int n, double alpha, double a, double b, ref double x) //func(x,n)
       {
           x = a;
           if (alpha == 0)
               return true;

           double c=b*2;
           double abs_alpha=Math.Abs(alpha);
           int i;
           for (i = 0; i < MAX_STEPS; i++)
           {               
               double intg = integrateByGauss(a, b, n, myfunc);
               double intg2 = integrateByGauss(a, c, n, myfunc);
               double abs_itg = Math.Abs(intg);
               double abs_itg2 = Math.Abs(intg2);

               if (Math.Abs(abs_itg - abs_alpha) < 0.000001)
               {
                   if (alpha < 0)
                       b = -b;
                   x =b;
                   return true;
               }

               if((abs_itg<abs_alpha) &&(abs_itg2>abs_alpha))
               {
                   b = (b + c) / 2;
                   continue;
               }

               if ((abs_itg < abs_alpha) && (abs_itg2 < abs_alpha))
               {
                   b = (b + c) / 2;
                   c = b * 2;
                   continue;
               }

               if ((abs_itg > abs_alpha) && (abs_itg2 > abs_alpha))
               {
                   b = b-(c - b) / 2;
                   c = b* 2 ;
                   continue;
               }
           }
           return false;
       }

        static void Main(string[] args)
        {
            while (1==1)
            {
                double a = 0, alpha;
                int n;

                Console.Write("Укажите alpha: ");

                alpha = Convert.ToDouble(Console.ReadLine().Replace(".", ",")) * Math.Sqrt(2 * Math.PI);
                Console.Write("Укажите количество корней: ");
                n = Convert.ToInt32(Console.ReadLine());

                double x = 0;
                if (!MYDichotomy(new Func<double, double>(funcError), n, alpha, a, 1, ref x))
                    Console.WriteLine("Превышено число итераций");
                else
                    Console.WriteLine("Найденная граница интегрирования: {0:0.#####}", x);

                Console.ReadKey();
            }
           
        }
    }
}
