using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_LABA9
{
    public partial class Form1 : Form
    {
        List<Point> poligone1;
        List<Point> poligone2;
        Bitmap bmt;
        public Form1()
        {
            InitializeComponent();
            button3.BackColor = Color.Red;
            button4.BackColor = Color.Black;
            button7.BackColor = Color.Green;
        }

        

        //Draw on background all points
        private void DrawAll()
        {
            bmt = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmt);
            Pen penpoligone1 = new Pen(button3.BackColor, 1);
            Pen penpoligone2 = new Pen(button4.BackColor, 1);

            pictureBox1.Refresh();
            if (poligone1.Count() > 1)
            {
                if (radioButton1.Checked)
                {
                    for (int i = 0; i < poligone1.Count-1; i++)
                    {
                        g.DrawLine(penpoligone1, poligone1[i], poligone1[i + 1]);
                    }
                }
                else
                {
                    g.DrawPolygon(penpoligone1, poligone1.ToArray());
                }
            }
            
            if (poligone2.Count() > 1)
            {
                if (radioButton2.Checked)
                {

                    for (int i = 0; i < poligone2.Count-1; i++)
                    {
                        g.DrawLine(penpoligone2, poligone2[i], poligone2[i + 1]);
                    }
                }
                else
                {
                    g.DrawPolygon(penpoligone2, poligone2.ToArray());

                }
            }
            pictureBox1.BackgroundImage = bmt;

            
        }

        /// <summary>
        /// При нажатой клавише ctrl, возвряшает кординаты токни на оси ох или oy
        /// </summary>
        /// <param name="lastpoint">Последняя введенная пользователем точка</param>
        /// <returns></returns>
        private Point ControlKeyLine(Point lastpoint, Point mouse)
        {
            Point p = new Point();
            int deltaX = lastpoint.X - mouse.X;
            int deltaY = lastpoint.Y - mouse.Y;
            if (deltaX != 0)
            {
                double tang = deltaY / deltaX;
                if (Math.Abs(tang) < 1)
                {
                    p = new Point(mouse.X, lastpoint.Y);
                }
                else
                {
                    p = new Point(lastpoint.X, mouse.Y);
                }
            }
            else
            {
                p = new Point(lastpoint.X, mouse.Y);
            }
            return p;
        }


        //Update DataGridViews
        private void RecreteDataGridViews()
        {

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            //Этот цикл нельзя оьъединять со следущим так как добавлениее строки очищает таблицу
            for (int i = 0; i < poligone1.Count; i++)
            {
                dataGridView1.Rows.Add();
            }

            //Этот цикл нельзя оьъединять со следущим так как добавлениее строки очищает таблицу
            for (int i = 0; i < poligone2.Count; i++)
            {
                dataGridView2.Rows.Add();
            }

            for (int i = 0; i < poligone1.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = poligone1[i].X;
                dataGridView1.Rows[i].Cells[1].Value = poligone1[i].Y;
                
            }
            
            for (int i = 0; i < poligone2.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = poligone2[i].X;
                dataGridView2.Rows[i].Cells[1].Value = poligone2[i].Y;                
            }           

        }

        //Цвет первого многоугольника
        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button3.BackColor = colorDialog1.Color;
            DrawAll();
        }

        //Цвет второго многоугольника
        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button4.BackColor = colorDialog1.Color;
            DrawAll();
        }

        //Read from DataGrid1
        private void button1_Click(object sender, EventArgs e)
        {
            poligone1 = new List<Point>();
            int X1 = 0, Y1 = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    X1 = Convert.ToInt32(row.Cells[0].Value);
                    Y1 = Convert.ToInt32(row.Cells[1].Value);
                    if (X1 == 0 && Y1 == 0)
                    {

                    }
                    else
                    {
                        poligone1.Add(new Point(X1, Y1));
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                    return;
                }
            }
            DrawAll();

        }

        //Read from DataGrid2
        private void button2_Click(object sender, EventArgs e)
        {
            poligone2 = new List<Point>();
            int X1 = 0, Y1 = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                try
                {
                    X1 = Convert.ToInt32(row.Cells[0].Value);
                    Y1 = Convert.ToInt32(row.Cells[1].Value);
                    if (X1 == 0 && Y1 == 0)
                    {

                    }
                    else
                    {
                        poligone2.Add(new Point(X1, Y1));
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                    return;
                }
            }
            DrawAll();
        }

       
        //nearest vertix
        private Point NeatestVertix(Point coord)
        {
            Point rez = coord;
            int tmpVertex = 0;
            double mindistance = DNO.Distanse(poligone1[0], coord);
            int n = poligone1.Count;

            for(int i = 0; i < n; i++) {
                double tmp = DNO.Distanse(poligone1[i], coord);
                if(tmp < mindistance) {
                    mindistance = tmp;
                    tmpVertex = i;
                }
            }

            if(mindistance < 20) {
                return poligone1[tmpVertex];
            }
            else {
                return coord;
            }
            
        }
        
        

        //nearest  point on poligon1 to poligon
        private Point magnetic(Point coord)
        {
            Point rez = coord;

            if (NeatestVertix(coord) != coord)
                return NeatestVertix(coord);

            Point[] side = DNO.FindNearSide(coord,poligone1);
            Point s = side[0];



            if (side[0].X == side[1].X)
            {
                rez.X = s.X;
                return rez;
            }
            double k = (side[0].Y - side[1].Y)*1.0 / (side[0].X - side[1].X);

            if (Math.Abs(k) < 1)
            {
                rez.Y =(int) Math.Round((rez.X - s.X) * k) + s.Y;
            }
            else
            {
                rez.X =(int) Math.Round((rez.Y - s.Y) / k) + s.X;
            }

            return rez;

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            if (MouseButtons.Left == ((MouseEventArgs)e).Button)
            {
                ///параллельный осям
                if (ModifierKeys == Keys.Control)
                {
                    if (radioButton1.Checked)
                    {
                        poligone1.Add(ControlKeyLine(poligone1[poligone1.Count - 1], coordinates));
                    }

                    if (radioButton2.Checked)
                    {
                        poligone2.Add(ControlKeyLine(poligone2[poligone2.Count - 1], coordinates));
                    }

                    RecreteDataGridViews();
                    DrawAll();
                    return;
                }

                //магнитный
                if (ModifierKeys == Keys.Shift)
                {
                    if (radioButton1.Checked)
                    {
                        MessageBox.Show("Данный ввид ввода для первого многоугольника недоступен");
                    }
                    if (radioButton2.Checked)
                    {
                        poligone2.Add(magnetic(coordinates));
                    }
                    RecreteDataGridViews();
                    DrawAll();
                    return;
                }

                //обычный
                if (radioButton1.Checked)
                {
                    poligone1.Add(coordinates);
                }
                if (radioButton2.Checked)
                {
                    poligone2.Add(coordinates);
                }

                RecreteDataGridViews();
                DrawAll();
                return;


            }
            

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                if (radioButton2.Checked == true)
                    radioButton3.Checked = true;


                if(radioButton1.Checked==true)
                    radioButton2.Checked = true;
            }
            RecreteDataGridViews();
            DrawAll();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            poligone1 = new List<Point>();
            poligone2 = new List<Point>();
            bmt = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            richTextBox1.Text=
            @"Отрезок гор/верт при помощи CTRL 
Отрезок притянутый к вершине или стороне при помощи SHIFT";
            richTextBox1.Enabled = false;
        }

        //Reset 
        private void button5_Click(object sender, EventArgs e)
        {

            poligone1 = new List<Point>();
            poligone2 = new List<Point>();
            bmt = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.BackgroundImage = bmt;
            radioButton1.Checked = true;
            RecreteDataGridViews();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            Pen penpoligone1 = new Pen(button3.BackColor, 1);
            Pen penpoligone2 = new Pen(button4.BackColor, 1);
            pictureBox1.Refresh();
            Graphics g = pictureBox1.CreateGraphics();

            if (ModifierKeys == Keys.Control)
            {
                if (radioButton1.Checked && poligone1.Count>=1)
                {
                    g.DrawLine(penpoligone1, poligone1[poligone1.Count - 1], ControlKeyLine(poligone1[poligone1.Count - 1],coordinates));
                    g.DrawLine(penpoligone1, ControlKeyLine(poligone1[poligone1.Count - 1], coordinates), poligone1[0]);
                }
                if (radioButton2.Checked && poligone2.Count >= 1)
                {
                    g.DrawLine(penpoligone2, poligone2[poligone2.Count - 1], ControlKeyLine(poligone2[poligone2.Count - 1],coordinates));
                    g.DrawLine(penpoligone2, ControlKeyLine(poligone2[poligone2.Count - 1],coordinates), poligone2[0]);
                }
                return;
            }

            if (ModifierKeys == Keys.Shift)
            {
           
                if (radioButton2.Checked && poligone2.Count >= 1)
                {
                    g.DrawLine(penpoligone2, poligone2[poligone2.Count - 1],magnetic( coordinates));
                    g.DrawLine(penpoligone2, magnetic(coordinates), poligone2[0]);
                }
                return
                    ;
            }
           

                if (radioButton1.Checked && poligone1.Count > 1)
                {
                    g.DrawLine(penpoligone1, poligone1[poligone1.Count - 1], coordinates);
                    g.DrawLine(penpoligone1, coordinates, poligone1[0]);
                }
                if (radioButton2.Checked && poligone2.Count > 1)
                {
                    g.DrawLine(penpoligone2, poligone2[poligone2.Count - 1],  coordinates);
                    g.DrawLine(penpoligone2,  coordinates, poligone2[0]);
                }
            
        }

        //Цвет отсекателя
        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button7.BackColor = colorDialog1.Color;
            DrawAll();
        }


        //Start
        private void button6_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmt);
            List<Point> temp_poligone1 = DNO.CloneList(poligone1);
            List<Point> temp_poligone2 = DNO.CloneList(poligone2);
           

            Algo algo = new Algo(temp_poligone1, temp_poligone2, button7.BackColor, g);
            algo.Start();

            pictureBox1.BackgroundImage = bmt;
            g.Flush();
            pictureBox1.Refresh();
            temp_poligone1.Clear();
            temp_poligone2.Clear();

        }

      

    }
}
