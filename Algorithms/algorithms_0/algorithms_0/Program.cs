using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace algorithms_0
{
    internal class Program
    {
        public static double Func(double x,double y)
        {
            return (Math.Exp(Math.Pow(x,3))-Math.Exp(y)*(Math.Pow(x,6)-2*Math.Pow(x,3)*y-2*Math.Pow(x,3)+y*y+2*y+2));
        }

        public static double Square(double left_h, double right_h,double step)
        {
            return ((left_h + right_h) / 2 * step);
        }

        public static double Searchy(double x, double left, double right)
        {

            while (Func(x,left)*Func(x,right)>0)
            {
                left--;
                right++;
            }
            while (Math.Abs(right-left)>0.000001)
            {
                double mid = (right + left) / 2;
                double fmid = Func(x, mid);
                if (Math.Abs(fmid) < 0.000001)
                    return mid;
                if ((fmid*Func(x,left))<0)
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }

            }
            if (Math.Abs(Func(x, left)) > Math.Abs(Func(x, right)))
                return right;
            return left;

            /*while (Math.Abs(Func(x, (right+left)/2)) > 0.00001)
            {
                if (Func(x,left)*Func(x,(right+left)/2) < 0)
                    right = (right+left)/2;
                else if(Func(x,(right+left)/2)*Func(x,right) < 0)
                    left = (right+left)/2;
            }

            return (right+left)/2;
            */

        }

        public static void Main(string[] args)
        {
            double a = 0;
            double b = 2;
            int n = 2000; //количесво разбиений
            double step = (b - a) / n;
            double square = 0;

            double left_h = Searchy(a,-10,10);
            double right_h;
            for (double i = a+step; i <= b; i += step)
            {
                right_h=Searchy(i,-10,10);

                square += Square(left_h,right_h,step);
                left_h = right_h;
            }
            Console.WriteLine(square);
        }
    }
}