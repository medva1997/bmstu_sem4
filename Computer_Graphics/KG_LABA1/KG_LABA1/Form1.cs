/* 
 * Medvedev Alexey 2017 year 
 * На плоскасти дано множество точек
 * Найти такой треугольник с вершинами в таких точках
 * для которых разность площадей описанной и вписаной окружностей минимальна
 * 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_LABA1
{
    public partial class Form1 : Form
    {
        List<Point> drawlist;
        

        public Form1()
        {
            InitializeComponent();
            drawlist=new List<Point>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Автомарическая нумерация
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr; 
        }


        //TODO повтор кода
        private void button1_Click(object sender, EventArgs e)
        {

            int number_of_rows = dataGridView1.RowCount; //количество строк в таблице
            bool flag = true;                            //Флаг корректности данных пользователя
            double x, y; // временные переменные для хранения данных строки
            List<Point> point_list = new List<Point>();
            for(int i=0;i<number_of_rows; i++)
            {
                try
                {
                    x = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                    y = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                }
                catch
                {
                    MessageBox.Show("Найдено некорректно заполненое поле в строке "+(i+1).ToString());
                    flag = false;
                    break;
                }

                if ((dataGridView1.Rows[i].Cells[0].Value == null) ^ (dataGridView1.Rows[i].Cells[1].Value == null))
                {
                    MessageBox.Show("Найдено некорректно заполненое поле в строке " + (i + 1).ToString());
                    flag=false;
                    break;
                }
                
                point_list.Add(new Point(x,y));                
            }

            if (flag != false)
                worker(point_list);



            point_list.Clear();
            GC.Collect();
        }

        struct answer
        {
            
            public int id1, id2, id3;
            public double min_delta;
            public answer(int id1, int id2, int id3, double min_delta)
            {
                this.id1 = id1;
                this.id2 = id2;
                this.id3 = id3;
                this.min_delta = min_delta;
            }
        }

        const double EPS = 0.0001;

        private void worker(List<Point> point_list)
        {
            int couter = point_list.Count-1;
            double tmp_area = point_list[0].AreaDelta(point_list[1], point_list[2]);
            answer ans=new answer(0,1,2, tmp_area); 
            for (int i = 0; i < couter; i++)
            {
                for (int j = i+1; j < couter; j++)
                {
                    for (int k = j+1; k < couter; k++)
                    {
                        tmp_area = point_list[i].AreaDelta(point_list[j], point_list[k]);
                        if((tmp_area>EPS)&&(tmp_area<ans.min_delta))
                            ans = new answer(i,j,k, tmp_area);
                    }
                }
            }

            if (couter< 3)
            {
                MessageBox.Show("мало точек");
                return;
            }
            
            drawlist.Clear();
            drawlist.Add(point_list[ans.id1]);
            drawlist.Add(point_list[ans.id2]);
            drawlist.Add(point_list[ans.id3]);

            
            //Вычисляем прямоугольник для описанной окружности
            PointF tcentre = BigCircle(drawlist[0].GetPointF, drawlist[1].GetPointF, drawlist[2].GetPointF);
            float radius = (float)(drawlist[0].GetLength(drawlist[0], new Point((double)tcentre.X, (double)tcentre.Y)));
            PointF upcorner = new PointF(tcentre.X - radius, tcentre.Y + radius);
            PointF downcorner = new PointF(tcentre.X + radius, tcentre.Y - radius);

            drawlist.Add(new Point(tcentre.X, tcentre.Y));
            drawlist.Add(new Point(upcorner.X, upcorner.Y));
            drawlist.Add(new Point(downcorner.X,downcorner.Y));



            //dataGridView1.Rows.Clear();
            string str = point_list[0].AreaDelta(point_list[1], point_list[2]).ToString();
            if (str == "не число")
            {
                MessageBox.Show("Вырожденный случай");
                return;
            }
            if (str == "∞")
            {
                MessageBox.Show("Вырожденный случай");
                return;
            }
          
            DrawPicture(drawlist,  panel4);
                    
            PrintAnswer(drawlist);
            
        }
    

        //отрисовка рисунка
        private void DrawPicture(List<Point> p_list,  Panel panel)
        {
            
            int Count = p_list.Count;
            if (Count < 3)
                return;

            //поиск минимумов и максимумов среди точек
            int max_y, max_x, min_x, min_y;
            max_x = min_x = p_list[0].GetXint;
            max_y = min_y = p_list[0].GetYint;
            
            foreach (Point next in p_list)
            {
                min_x = Math.Min(min_x, next.GetXint);
                min_y = Math.Min(min_y, next.GetYint);
                max_x = Math.Max(max_x, next.GetXint);
                max_y = Math.Max(max_y, next.GetYint);
            }

            //Небольшой костыль для восстановления радиуса
            float radius = (float)(p_list[0].GetLength(p_list[0],p_list[3]));
            PointF upcorner = new PointF(p_list[3].GetXfloat - radius, p_list[3].GetYfloat + radius); 
            
            //настройка маштабира
            XYConverter conv = new XYConverter(new PointF(min_x, min_y), new PointF(max_x, max_y), panel.Size, 15);
           
            Graphics g = panel.CreateGraphics();
            
            Pen myPen = new Pen(Color.Red, 3);
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, panel.Width, panel.Height);
            
            //Рисуем подписи
            for (int i = 0; i < 3; i++)
            {
                g.DrawString("(" + p_list[i].GetXint.ToString() + "," + p_list[i].GetYfloat.ToString() + ")", drawFont, drawBrush, conv.GetPointWithMargin(p_list[i]));
            }
                    

            //рисуем треугольник
            g.DrawLine(myPen, conv.GetPointF(p_list[0]), conv.GetPointF(p_list[1]));
            g.DrawLine(myPen, conv.GetPointF(p_list[0]), conv.GetPointF(p_list[2]));
            g.DrawLine(myPen, conv.GetPointF(p_list[1]), conv.GetPointF(p_list[2]));
            //вписанная окружность
            g.DrawEllipse(myPen, InscribedCircle(conv.GetPointF(p_list[0]), conv.GetPointF(p_list[1]), conv.GetPointF(p_list[2])));
            //описанная окружность
            g.DrawEllipse(myPen, conv.GetRectangleF(upcorner,radius));
            panel.Update();
          
        }
               
        //Печать ответа
        private void PrintAnswer(List<Point> p_list)
        {
            string p0 = "(" + p_list[0].GetXfloat.ToString() + "," + p_list[0].GetYfloat.ToString() + ")";
            string p1 = "(" + p_list[1].GetXfloat.ToString() + "," + p_list[1].GetYfloat.ToString() + ")";
            string p2 = "(" + p_list[2].GetXfloat.ToString() + "," + p_list[2].GetYfloat.ToString() + ")";
            string answer = "Треугольник образованный точками с координатами " + p0 + ", " + p1 + ", " + p2 +
                " имеет наименьшую разность площадей вписанной и описанной окружностей равную " + p_list[0].AreaDelta(p_list[1], p_list[2])+
                " кв. единиц.";

           textBox1.Text = answer;
        }

        //Событие резайза
        private void Form1_Resize(object sender, EventArgs e)
        {
           
            DrawPicture(drawlist, panel4);
         
        }

        //вписанная окружность
        private RectangleF InscribedCircle(PointF pt1, PointF pt2, PointF pt3)
        {
            //Rectangle result = new Rectangle();
            //Векторы сторон
            PointF p1 = new PointF(pt2.X - pt1.X, pt2.Y - pt1.Y);
            PointF p2 = new PointF(pt3.X - pt2.X, pt3.Y - pt2.Y);
            PointF p3 = new PointF(pt1.X - pt3.X, pt1.Y - pt3.Y);
            //Длины векторов сторон
            float l1 = (float)Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y),
                l2 = (float)Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y),
                l3 = (float)Math.Sqrt(p3.X * p3.X + p3.Y * p3.Y);
            //единичные векторы сторон
            PointF ep1 = new PointF(p1.X / l1, p1.Y / l1),
                ep2 = new PointF(p2.X / l2, p2.Y / l2),
                ep3 = new PointF(p3.X / l3, p3.Y / l3);
            //векторы биссектрис. Считаем только для углов 2 и 3
            PointF b2 = new PointF(-ep1.X + ep2.X, -ep1.Y + ep2.Y),
                b3 = new PointF(ep3.X - ep2.X, ep3.Y - ep2.Y);
            //Вычисляем точку пересечения биссектрис. Биссектрисы заданы векторами b2 и b3
            //и проходят через точки pt2 и pt3 соответственно. 
            //Вспомогательные переменные для удобства записи
            float k2 = b2.Y / b2.X, k3 = b3.Y / b3.X;
            //Координаты пересечения биссектрис. Формулы выводятся из уравнения прямой 
            //по точке и направляющему вектору
            float x = (k2 * pt2.X - k3 * pt3.X + pt3.Y - pt2.Y) / (k2 - k3), y = k2 * (x - pt2.X) + pt2.Y;
            //Радиус вписанной окружности.
            //Расстояние от точки пересечения до любой из сторон
            //Считаем расстояние до стороны p1
            float radius = (float)(Math.Abs(x * p1.Y - y * p1.X - pt1.X * p1.Y + pt1.Y * p1.X) / Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y));
            return new RectangleF(x - radius, y - radius, 2 * radius, 2 * radius);
        }

        //описанная окружность
        private PointF BigCircle(PointF pt1, PointF pt2, PointF pt3)
        {
            float a=0, b=0;
            if (BigCounter(pt1, pt2, pt3, ref a, ref  b) == 0) { return new PointF(a, b); }
            if (BigCounter(pt1, pt3, pt2, ref a, ref  b) == 0) { return new PointF(a, b); }
            if (BigCounter(pt2, pt1, pt3, ref a, ref  b) == 0) { return new PointF(a, b); }
            if (BigCounter(pt2, pt3, pt1, ref a, ref  b) == 0) { return new PointF(a, b); }
            if (BigCounter(pt3, pt1, pt2, ref a, ref  b) == 0) { return new PointF(a, b); }
            if (BigCounter(pt3, pt2, pt1, ref a, ref  b) == 0) { return new PointF(a, b); }

            return new PointF(0, 0);
        }

        //расчет описанной окружности
        private int BigCounter(PointF pt1, PointF pt2, PointF pt3, ref float a, ref float b)
        {
            float x12 = pt1.X - pt2.X;
            float x23 = pt2.X - pt3.X;
            float x31 = pt3.X - pt1.X;

            float y12 = pt1.Y - pt2.Y;
            float y23 = pt2.Y - pt3.Y;
            float y31 = pt3.Y - pt1.Y;

            float z1 = pt1.X * pt1.X + pt1.Y * pt1.Y;
            float z2 = pt2.X * pt2.X + pt2.Y * pt2.Y;
            float z3 = pt3.X * pt3.X + pt3.Y * pt3.Y;

            float zx = y12 * z3 + y23 * z1 + y31 * z2;
            float zy = x12 * z3 + x23 * z1 + x31 * z2;
            float z = x12 * y31 - y12 * x31;

            if (z == 0)
            {
                a = 0;
                b = 0;
                return 1;
            }
            a = -zx / (2 * z);
            b = zy / (2 * z);

            return 0;
        }

    }
}
