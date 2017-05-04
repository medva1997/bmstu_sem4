using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace KG_LABA6
{
    class FillPoligons
    {
       
        /// <summary>
        /// Цвет закраски
        /// </summary>
        private Color DrawColor;

        /// <summary>
        /// Цвет границы
        /// </summary>
        private Color BorderColor;

        /// <summary>
        /// Где мы рисуем
        /// </summary>
        private Bitmap bitmap;
        
        /// <summary>
        /// Picturebox на который выводтся bitmap
        /// </summary>
        private System.Windows.Forms.PictureBox pb;
        
        /// <summary>
        /// Интервал для задержки
        /// </summary>
        private int interval;
        /// <summary>
        /// Стек для хранения затравочных пикселей
        /// </summary>
        private Stack<Point> stack;

        /// <summary>
        /// Сверяет цвет выбранного пикселя с заданным
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <param name="color">Цвет для проверки</param>
        /// <returns>true если совпадает</returns>
        private bool CheckColor(int X, int Y, Color color)
        {
            bool flag = false;
           
            if ((X >= 0) && (X < bitmap.Width) && (Y >= 0) && (Y < bitmap.Height))
            {
                flag= bitmap.GetPixel(X, Y).ToArgb() == color.ToArgb();                
            }            
            return flag;
        }

        /// <summary>
        /// Устанавливает пиксель с указанным цветом
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private int AddPoint(int X, int Y, Color color)
        {
            if ((X >= 0) && (X < bitmap.Width) && (Y >= 0) && (Y < bitmap.Height))
            {
                bitmap.SetPixel(X, Y, color);               
            }          
            return 0;
        }

        /// <summary>
        /// Поиск затравочного пикселя 
        /// </summary>
        /// <param name="Xl">Левая граница</param>
        /// <param name="Xr">Правая граница</param>
        /// <param name="Y">Y</param>
        private void SearchZatr(int Xl, int Xr, int Y)
        {
            int X = Xl;
            do
            {
                int Fl = 0;
                //ищем грань идя направо
                while ((X <= Xr) &&
                    (!CheckColor(X, Y,DrawColor)) &&
                    (!CheckColor(X, Y, BorderColor)))
                {
                    X++;
                    Fl = 1;
                }

                if (Fl == 1)
                {
                    if ((X == Xr) &&
                    (!CheckColor(X, Y,DrawColor)) &&
                    (!CheckColor(X, Y,BorderColor)))
                    {
                        stack.Push(new Point(X, Y));
                    }
                    else
                    {//Если оказались на гране то смещаемся на один левее
                        stack.Push(new Point(X-1, Y));
                    }
                }

                X++;
            }
            while(X-1<Xr);
        }

        /// <summary>
        /// Свойсво возвращающее битмап
        /// </summary>
        public Bitmap GetBitmap
        {
            get
            {
                return bitmap;
            }
        }

        /// <summary>
        /// посторчная заливка
        /// </summary>
        /// <param name="start">Затравочный пиксель</param>
        private void Fill(Point start)
        {            
            stack = new Stack<Point>();
            stack.Push(start);

            while (stack.Count != 0)
            {
                Point temp = stack.Pop();
                int Xz=temp.X;
                int Y=temp.Y;
                
                if (CheckColor(Xz, Y,DrawColor))
                {
                    continue;
                }

                //идем направо до границы
                int X = Xz ;
                do
                {
                    AddPoint(X, Y, DrawColor);
                    X++;
                }
                while (!CheckColor(X, Y,BorderColor));

                int Xr = X - 1;
                //дем налево до границы
                X = Xz;
                do
                {
                    AddPoint(X, Y, DrawColor);
                    X--;
                }
                while (!CheckColor(X, Y, BorderColor));

                int Xl = X + 1;
                //Ищем новые затавочные точки
                SearchZatr(Xl, Xr, Y - 1);
                SearchZatr(Xl, Xr, Y + 1);

                //задержка и ее отключение
                if (interval != 0)
                {
                    System.Threading.Thread.Sleep(interval);
                    pb.Image = bitmap;
                    pb.CancelAsync();
                    pb.Refresh();
                }
            }
        }

        /// <summary>
        /// Очень мощный :(  конструктор
        /// </summary>
        /// <param name="pb"> Окно для отображения</param>
        /// <param name="StartPoint">Писксель для затравки</param>
        /// <param name="deltatime">Задерка</param>
        /// <param name="Draw">Цвет для заливки</param>
        /// <param name="Border">Цвет границ</param>

        public FillPoligons(ref System.Windows.Forms.PictureBox pb,Point StartPoint, int deltatime, Color Draw, Color Border)
        {
            interval = deltatime;
            this.pb = pb;
            bitmap = (Bitmap) pb.Image.Clone();
            
            DrawColor = Draw;
            BorderColor = Border;

            //Рисуем границы чтобы не уйти за край
            Pen pen = new Pen(BorderColor, 1);
            var graphics = Graphics.FromImage(bitmap);            
            graphics.DrawRectangle(pen, 0, 0, bitmap.Size.Width-1,bitmap.Size.Height-1);

            //Вызов алгоритма
            Fill(StartPoint);
            
        }

    }
}
