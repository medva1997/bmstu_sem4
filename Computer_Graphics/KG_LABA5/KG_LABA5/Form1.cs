using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KG_LABA5
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Многоугольники
        /// </summary>
        List<List<Point>> Polygons;

        bool InputEnded = false;

        /// <summary>
        /// отклонение мыши по оси X
        /// </summary>
        int MouseErrorX = 20;

        /// <summary>
        /// отклонение мыши по оси Y
        /// </summary>
        int MouseErrorY = 50;

        Graphics g;
        Pen pen;

        public Form1()
        {
            InitializeComponent();
            
            Polygons = new List<List<Point>>();
            Polygons.Add(new List<Point>());
            //BoundaryTypeInput = false;
            g = pictureBox1.CreateGraphics();
            pen = new Pen(Color.Black, 1);
            textBox1.Text = "5";
            checkBox1.Checked = true;
            /*
            Polygons[0].Add(new Point(404, 201));
            Polygons[0].Add(new Point(498, 429));
            Polygons[0].Add(new Point(540, 317));
            Polygons[0].Add(new Point(577, 399));
            Polygons[0].Add(new Point(529, 212));
            Polygons[0].Add(new Point(677, 305));
            Polygons[0].Add(new Point(421, 181));
            Polygons[0].Add(new Point(465, 100));
            Polygons[0].Add(new Point(434, 333));
            Polygons.Add(new List<Point>());

            Polygons[1].Add(new Point(788, 305));
            Polygons[1].Add(new Point(778, 342));
            Polygons[1].Add(new Point(834, 416));
            Polygons.Add(new List<Point>());

            Polygons[2].Add(new Point(540, 573));
            Polygons[2].Add(new Point(503, 506));
            Polygons[2].Add(new Point(683, 508));
            Polygons[2].Add(new Point(627, 572));
            Polygons.Add(new List<Point>());

            Polygons[3].Add(new Point(111, 472));
            Polygons[3].Add(new Point(71, 373));
            Polygons[3].Add(new Point(224, 374));
            Polygons.Add(new List<Point>());

            Polygons[4].Add(new Point(133, 471));
            Polygons[4].Add(new Point(114, 415));
            Polygons[4].Add(new Point(212, 427));
            Polygons.Add(new List<Point>());

            Polygons[5].Add(new Point(66,136 ));
            Polygons[5].Add(new Point(192,264 ));
            Polygons[5].Add(new Point(152,183 ));
            Polygons[5].Add(new Point(221, 258));
            Polygons[5].Add(new Point(189, 176));
            Polygons[5].Add(new Point(237, 244));
            Polygons[5].Add(new Point(216, 173));
            Polygons[5].Add(new Point(272, 242));
            Polygons[5].Add(new Point(239, 163));
            Polygons[5].Add(new Point(290, 241));
            Polygons[5].Add(new Point(290, 161));
            Polygons[5].Add(new Point(221, 161));
            Polygons[5].Add(new Point(221, 105));
            Polygons.Add(new List<Point>());

            Polygons[6].Add(new Point(235, 50));
            Polygons[6].Add(new Point(265, 24));
            Polygons[6].Add(new Point(457, 24));
            Polygons[6].Add(new Point(457, 60));
            Polygons[6].Add(new Point(429, 60));
            Polygons[6].Add(new Point(420, 77));
            Polygons[6].Add(new Point(393, 77));
            Polygons[6].Add(new Point(379, 99));
            Polygons[6].Add(new Point(356, 77));
            Polygons[6].Add(new Point(285, 77));
            Polygons[6].Add(new Point(269, 50));
            Polygons.Add(new List<Point>());

            Polygons[7].Add(new Point(760, 90));
            Polygons[7].Add(new Point(706, 170));
            Polygons[7].Add(new Point(887, 185));
            Polygons.Add(new List<Point>());


            Polygons[8].Add(new Point(806, 158));
            Polygons[8].Add(new Point(753, 154));
            Polygons[8].Add(new Point(769, 129));
            
           
            Polygons.Add(new List<Point>());
            Polygons[1].Add(new Point(296, 614));
            Polygons[1].Add(new Point(413, 325));
            Polygons[1].Add(new Point(505, 615));
            Polygons[1].Add(new Point(578, 329));
            Polygons[1].Add(new Point(697, 605));
            Polygons[1].Add(new Point(732, 297));
            Polygons[1].Add(new Point(483, 110));
            Polygons[1].Add(new Point(178, 271));
            Polygons.Add(new List<Point>());

            Polygons[2].Add(new Point(273, 264));
            Polygons[2].Add(new Point(294, 349));
            Polygons[2].Add(new Point(422, 272));
            Polygons.Add(new List<Point>());

            Polygons[3].Add(new Point(496, 202));
            Polygons[3].Add(new Point(541, 285));
            Polygons[3].Add(new Point(651, 297));
            Polygons.Add(new List<Point>());
             
            Polygons.Add(new List<Point>());
             * */

        }




        /// <summary>
        /// Сохранение точки в список
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MouseButtons.Left == ((MouseEventArgs)e).Button)
            {
                if (ModifierKeys == Keys.Control)
                {
                    Polygons[Polygons.Count - 1].Add(ControlKeyLine(Polygons[Polygons.Count - 1][Polygons[Polygons.Count - 1].Count - 1]));
                }
                else
                {
                    Polygons[Polygons.Count - 1].Add(new Point(MousePosition.X - MouseErrorX, MousePosition.Y - MouseErrorY));
                }

            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                if (IsRight(Polygons[Polygons.Count - 1]) == false)
                    Polygons[Polygons.Count - 1].Reverse();
                Polygons.Add(new List<Point>());
            }

            DrawAllLines();


            RecreteDataGridView1();


        }

        private int check(Point p1, Point p2, Point p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            
        }

      

        private bool IsRight(List<Point> list)
        {
            int minindex = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].X < list[minindex].X)
                    minindex = i;
            }

            if (list.Count() == 0)
                return true;
            if (check(list[minindex], list[(list.Count()+minindex - 1)% list.Count()], list[(minindex + 1) %list.Count()]) > 0)
                return true;
            else
                return false;
        }


        private void UsePointsFromDataGridView1()
        {
            List<List<Point>> newpoints = new List<List<Point>>();
            this.dataGridView1.Sort(this.dataGridView1.Columns["Poligon"], ListSortDirection.Ascending);

            int listcount = -1;
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                

                if (dataGridView1.Rows[i].Cells[2].Value.ToString() != (listcount + 1).ToString())
                {
                    newpoints.Add(new List<Point>());
                    listcount++;
                }

                int X = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                int Y = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                newpoints[newpoints.Count - 1].Add(new Point(X, Y));

            }
            newpoints.Add(new List<Point>());

            Polygons = newpoints;
            //отчистка экрана
            pictureBox1.Refresh();

            //рисуем все уже сохраненные точки
            DrawAllLines();

        }

        private void RecreteDataGridView1()
        {
            int count = 0;
            dataGridView1.Rows.Clear();

            //Этот цикл нельзя оьъединять со следущим так как добавлениее строки очищает таблицу
            for (int i = 0; i < Polygons.Count; i++)
            {
                for (int j = 0; j < Polygons[i].Count; j++)
                    dataGridView1.Rows.Add();
            }

            for (int i = 0; i < Polygons.Count; i++)
            {
                List<Point> temp = Polygons[i];
                for (int j = 0; j < temp.Count; j++)
                {
                    dataGridView1.Rows[count].Cells[0].Value = temp[j].X;
                    dataGridView1.Rows[count].Cells[1].Value = temp[j].Y;
                    dataGridView1.Rows[count].Cells[2].Value = i + 1;
                    count++;
                }

            }
        }

        /// <summary>
        /// При нажатой клавише ctrl, возвряшает кординаты токни на оси ох или oy
        /// </summary>
        /// <param name="lastpoint">Последняя введенная пользователем точка</param>
        /// <returns></returns>
        private Point ControlKeyLine(Point lastpoint)
        {
            Point p = new Point();
            int deltaX = lastpoint.X - MousePosition.X + MouseErrorX;
            int deltaY = lastpoint.Y - MousePosition.Y + MouseErrorY;
            if (deltaX != 0)
            {
                double tang = deltaY / deltaX;
                if (Math.Abs(tang) < 1)
                {
                    p = new Point(MousePosition.X - MouseErrorX, lastpoint.Y);
                }
                else
                {
                    p = new Point(lastpoint.X, MousePosition.Y - MouseErrorY);
                }
            }
            else
            {
                p = new Point(lastpoint.X, MousePosition.Y - MouseErrorY);
            }
            return p;
        }

        /// <summary>
        /// Рисует линии к мыши из последней и первой точке
        /// </summary>
        /// <param name="list"></param>
        private void DrawLinesToMouse(List<Point> list)
        {

            if (list.Count > 0)
            {
                if (ModifierKeys == Keys.Control)
                {
                    g.DrawLine(pen, list[list.Count - 1], ControlKeyLine(list[list.Count - 1]));
                    g.DrawLine(pen, ControlKeyLine(list[list.Count - 1]), list[0]);
                }
                else
                {
                    g.DrawLine(pen, list[list.Count - 1], new Point(MousePosition.X - MouseErrorX, MousePosition.Y - MouseErrorY));
                    g.DrawLine(pen, new Point(MousePosition.X - MouseErrorX, MousePosition.Y - MouseErrorY), list[0]);
                }
            }
        }

        /// <summary>
        /// Отрисовка всех сохраненных линий
        /// </summary>
        private void DrawAllLines()
        {
            int i;
            for (i = 0; i < Polygons.Count() - 1; i++)
            {
                if (Polygons[i].Count > 1)
                {
                    g.DrawLines(pen, Polygons[i].ToArray());
                    g.DrawLine(pen, Polygons[i][0], Polygons[i][Polygons[i].Count - 1]);
                }

            }
            //последний многоуголиник
            if (Polygons[i].Count > 1)
            {
                g.DrawLines(pen, Polygons[i].ToArray());
                if (InputEnded == true)
                    g.DrawLine(pen, Polygons[i][0], Polygons[i][Polygons[i].Count - 1]);
            }


        }

        /// <summary>
        /// Создание линий при движении мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //отчистка экрана
            pictureBox1.Refresh();

            //рисуем все уже сохраненные точки
            DrawAllLines();

            //Рисует линии к мыши из последней и первой точке            
            DrawLinesToMouse(Polygons[Polygons.Count() - 1]);


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UsePointsFromDataGridView1();
        }
        FillPoligons pol;

        private void button1_Click(object sender, EventArgs e)
        {
            GC.Collect();
            int delay=0;
            try
            {
                delay=Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Плохая задержка");
                return;
            }

            pol = new FillPoligons(pictureBox1.Size, Color.Red);
            pol.Worker(ref pictureBox1, Polygons, checkBox1.Checked, delay);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UsePointsFromDataGridView1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<List<Point>> newpoints = new List<List<Point>>();
            newpoints.Add(new List<Point>());


            Polygons = newpoints;
            //отчистка экрана
            pictureBox1.Image = new Bitmap(100, 100);
            pictureBox1.Refresh();

           
        }
    }
        
}
