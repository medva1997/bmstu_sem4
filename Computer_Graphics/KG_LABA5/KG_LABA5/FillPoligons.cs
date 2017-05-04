using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace KG_LABA5
{
    class FillPoligons
    {
        /// <summary>
        /// Где мы рисуем
        /// </summary>
        private Bitmap bitmap;

        /// <summary>
        /// Цвет для отрисовки
        /// </summary>
        private Color drawcolor;

        /// <summary>
        /// список с экстремумами
        /// </summary>
        private List<Point> extremums;

        /// <summary>
        /// Kрайняя левая и правая вершины
        /// </summary>      
        border[] bordermaxmix;
        struct border
        {
            int xl,xr;
            public border(int XL, int XR)
            {
                xl = 100000;
                xr = -1;
            }
            public void newborder(int X)
            {
                if (xl > X)
                    xl = X;
                if (xr < X)
                    xr = X;
            }

            public bool Used
            {
                get
                {
                    if ((xr != -1)) 
                        return true;
                    else 
                        return false;
                }
            }
            public int XL
            {
                get{return xl;}
            }
            public int XR
            {
                get { return xr; }
            }
        }

        /// <summary>
        /// добавление точки на картинку с учетом закрашенности уже этой точки
        /// </summary>
        /// <param name="p">Point для закраски</param>
        /// <param name="color">Цвет</param>
        /// <returns></returns>
       private void AddPoint(ref Point p, Color color)
        {
           
            if ((p.X >= 0) && (p.X < bitmap.Width) && (p.Y >= 0) && (p.Y < bitmap.Height))
            {
                Color co = bitmap.GetPixel(p.X, p.Y);
                
                if (co.ToArgb() == drawcolor.ToArgb())
                {
                    bitmap.SetPixel(p.X +1, p.Y, drawcolor);
                }
                else
                    bitmap.SetPixel(p.X, p.Y, color);
           }         
        }

       private int AddPoint(int X,int  Y, Color color)
       {
            
           if ((X >= 0) && (X < bitmap.Width) && (Y >= 0) && (Y < bitmap.Height))
           {
               bitmap.SetPixel(X, Y, color);               
           }


           return 0;
       }

       private void Swap<T>(ref T x, ref T y)
       {
           T temp = x;
           x = y;
           y = temp;
       }

       /// <summary>
       /// Рисует линию со всеми точками по алгоритму Брезентхема INT
       /// </summary>
       /// <param name="pointfrom">откуда</param>
       /// <param name="pointto">куда</param>
       private void drawborder_full(Point pointfrom, Point pointto)
       {
           int dx = (int)(Math.Abs(pointto.X - pointfrom.X));
           int dy = (int)(Math.Abs(pointto.Y - pointfrom.Y));
           int stepx = Math.Sign(pointto.X - pointfrom.X);
           int stepy = Math.Sign(pointto.Y - pointfrom.Y);

            int flag;
            if (dy > dx)
            {
                int tmp = dx;
                dx = dy;
                dy = tmp;
                flag = 1;
            }
            else
                flag = 0;
            int f1 = 2 * dy - dx;
            int x = pointfrom.X;
            int y = pointfrom.Y;
            for (int i = 0; i < dx; i++)
            {                   
                AddPoint(x,y,Color.Black);                   
                if (f1 >= 0)
                {
                    if (flag == 1)
                        x += stepx;
                    else
                        y += stepy;
                    f1 -= 2 * dx;
                }
                if (flag == 1)
                    y += stepy;
                else
                    x += stepx;
                f1 += 2 * dy;
            }
            AddPoint(x, y, Color.Black); 

       }
       
        /// <summary>
       /// Рисует линию только с левыми y на каждой строке по алгоритму Брезентхема INT
        /// </summary>
        /// <param name="pointfrom">откуда</param>
        /// <param name="pointto">куда </param>
       private void drawborder(Point pointfrom, Point pointto)
       {
          

           int dx = (int)(Math.Abs(pointto.X - pointfrom.X));
           int dy = (int)(Math.Abs(pointto.Y - pointfrom.Y));
           int stepx = Math.Sign(pointto.X - pointfrom.X);
           int stepy = Math.Sign(pointto.Y - pointfrom.Y);
          

           int flag;
           if (dy > dx)
           {
               Swap<int>(ref dx, ref dy);               
               flag = 1;
           }
           else
               flag = 0;
          
           int f1 = 2 * dy - dx;
           int lasty = pointfrom.Y;

           Point calculated = new Point(pointfrom.X, pointfrom.Y);
           bordermaxmix[calculated.Y].newborder(calculated.X);
         

           //проверка вершины на экстремум
           if (extremums.Contains(calculated) == true)
           {
               //2 точки если экстремум
               AddPoint(ref calculated, drawcolor);
               AddPoint(ref calculated, drawcolor);
               
           }
           else
           {
               //одна точка еесли не экстремум
               AddPoint(ref calculated, drawcolor);               
           }

           for (int i = 0; i < dx; i++)
           {
               if (lasty != calculated.Y)
               {
                   bordermaxmix[calculated.Y].newborder(calculated.X);                  
                   lasty = calculated.Y;
                   AddPoint(ref calculated, drawcolor);
               }
               

               if (f1 >= 0)
               {
                   if (flag == 1)
                       calculated.X += stepx;
                   else
                       calculated.Y += stepy;
                  
                   f1 -= 2 * dx;
               }
               if (f1 < 0)
               {
                   if (flag == 1)
                       calculated.Y += stepy;
                   else
                       calculated.X += stepx;

               }
               f1 += 2 * dy;
               
               if (calculated.Y == pointto.Y)
                   return;
               
           }                    
           
       }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="bit_size"> размер исходного изображения</param>
        /// <param name="color"> цвет линий </param>
       public FillPoligons(Size bit_size, Color color)
       {
           bitmap = new Bitmap(bit_size.Width, bit_size.Height);
           drawcolor = color;
           //bordersarray = new List<int>[bit_size.Height];

           bordermaxmix = new border[bit_size.Height];

           for (int i = 0; i < bit_size.Height; i++)
               //bordersarray[i] = new List<int>();
               bordermaxmix[i] = new border(0,0);

           extremums = new List<Point>();
       }

    
        /// <summary>
        /// Поиск экстемумов
        /// </summary>
        /// <param name="polig">один многоугольник</param>
       public void SearchMaxes(List<Point>polig)
       {
           int y0, y1, y2;
           for (int i = 0; i < polig.Count(); i++)
           {
              
               y0 = polig[i].Y;
               y1 = polig[(polig.Count()+i - 1) % polig.Count()].Y;
               y2 = polig[(i + 1)%polig.Count()].Y;


               if ((y0 < y1 && y0 < y2) || (y0 >= y1 && y0 >= y2))
               {
                   extremums.Add(polig[i]);

               }
           }   
       }
        
        /// <summary>
        /// отрисовка границ с не со всеми точками на линиях и поиск экстремумов 
        /// </summary>
        /// <param name="Polygons"></param>
       public void  DrawBorders(ref List<List<Point>> Polygons)
       {
           for (int i = 0; i < Polygons.Count() - 1; i++)
           {
               if (Polygons[i].Count > 1)
               {
                   SearchMaxes(Polygons[i]); 
                   for (int j = 0; j < Polygons[i].Count; j++)
                   {
                       drawborder(Polygons[i][j], Polygons[i][(j + 1)%Polygons[i].Count]);      
                   }                  
                   
               }
           }
       }
        
        /// <summary>
        /// отрисовка всех границ
        /// </summary>
        /// <param name="Polygons"></param>
       public void  DrawBorders_Full(ref List<List<Point>> Polygons)
       {
           for (int i = 0; i < Polygons.Count() - 1; i++)
           {
               if (Polygons[i].Count > 1)
               {
                   for (int j = 0; j < Polygons[i].Count ; j++)
                   {
                       drawborder_full(Polygons[i][j], Polygons[i][(j + 1) % Polygons[i].Count]);                       
                   }
                              
               }
           }           
       }

       public void Worker(ref System.Windows.Forms.PictureBox pb, List<List<Point>> Polygons, bool borderflag, int delay)
       {
           
           DrawBorders(ref Polygons);
           pb.Image = bitmap;
           int index = 0;
          
           bitmap = FillPoligon(ref index);
          
           pb.Image = bitmap;
           while (index < pb.Height)
           {
               bitmap =FillPoligon(ref index);
               pb.Image = bitmap;

               if (delay != 0)
               {
                   pb.CancelAsync();
                   pb.Refresh();
                   System.Threading.Thread.Sleep(delay);
               }
           }

           if (borderflag == true)
           {
               DrawBorders_Full(ref Polygons);              
               pb.Image = bitmap;
           }           
       }

       

       protected void FillLine(int index)
       {
           if (bordermaxmix[index].Used == false)
               return;         
           int xl = bordermaxmix[index].XL;
           int xr = bordermaxmix[index].XR;
          
           bool flag = false;
           for (int i = xl; i < xr; i++)
           {
               if (bitmap.GetPixel(i, index).ToArgb() == drawcolor.ToArgb())
               {                     
                   flag = !flag;                   
               }

               if (flag == true)
               {
                   AddPoint(i, index, drawcolor);
               }                        
            }
        }

       
        /// <summary>
        /// заполнение внутренней части
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Bitmap FillPoligon(ref int index)
        {
            for (int i = index; i < bitmap.Height; i++)
            {
                if (bordermaxmix[i].Used)
                {
                    index = i+1;
                    FillLine(i);
                    return bitmap;
                }
            }

            index = bitmap.Height + 1;
            return bitmap;
        }
       
      
        
    
    }
}
