using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_LABA8
{
    public partial class Form1 : Form
    {

        List<Point> poligone;
        List<Point> points;

        public Form1()
        {
            InitializeComponent();
            line_color.BackColor = Color.Black;
            poligon_color.BackColor = Color.Black;
            cut_color.BackColor = Color.Red;

            points = new List<Point>();
            poligone = new List<Point>();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            if (ModifierKeys == Keys.Shift || radioButton1.Checked)
            {
                poligone.Add(coordinates);                
                DrawAll();
                RecreteDataGridViews();
                return;
            }


            //обычные линии
            if (MouseButtons.Left == ((MouseEventArgs)e).Button)
            {
                if (ModifierKeys == Keys.Control || radioButton3.Checked)
                {
                    if (points.Count % 2 == 1)
                    {
                        points.Add(ControlKeyLine(points[points.Count - 1], coordinates));
                    }
                    else
                    {
                        points.Add(coordinates);
                    }

                }
                else
                {
                    if (ModifierKeys == Keys.Alt || radioButton4.Checked)
                    {
                        if (points.Count % 2 == 1)
                        {
                            Parallel parar = new Parallel();
                            //parar.SearchNearest(ref poligone,coordinates);
                            List<Point> temp_poligone = new List<Point>();
                            for (int i = 0; i < poligone.Count; i++)
                            {
                                temp_poligone.Add(poligone[i]);
                            }
                            points.Add(parar.SearchNearest(temp_poligone,points[points.Count - 1], coordinates));
                            temp_poligone.Clear();
                        }
                        else
                        {
                            points.Add(coordinates);
                        }
                    }
                    else
                    {
                        points.Add(coordinates);
                    }
                }

                
            }
            RecreteDataGridViews();
            DrawAll();
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

        private void DrawAll()
        {
            bmt = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmt);
            Pen penline = new Pen(line_color.BackColor, 1);
            Pen penpoligone = new Pen(poligon_color.BackColor, 1);
            pictureBox1.Refresh();

            for (int i = 0; i < points.Count / 2; i++)
            {
                g.DrawLine(penline, points[i * 2], points[i * 2 + 1]);
            }

            if (poligone.Count() > 1) 
                g.DrawPolygon(penpoligone, poligone.ToArray());

            pictureBox1.BackgroundImage = bmt;

        }

        private void poligon_color_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            poligon_color.BackColor = colorDialog1.Color;
            DrawAll();
        }

        private void line_color_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            line_color.BackColor = colorDialog1.Color;
            DrawAll();
        }

        private void cut_color_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            cut_color.BackColor = colorDialog1.Color;
            DrawAll();
        }

        private void RecreteDataGridViews()
        {
          
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            //Этот цикл нельзя оьъединять со следущим так как добавлениее строки очищает таблицу
            for (int i = 0; i < points.Count / 2; i++)
            {
                    dataGridView1.Rows.Add();
            }

            for (int i = 0; i < points.Count / 2; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = points[i * 2].X;
                dataGridView1.Rows[i].Cells[1].Value = points[i * 2].Y;
                dataGridView1.Rows[i].Cells[2].Value = points[i * 2 + 1].X;
                dataGridView1.Rows[i].Cells[3].Value = points[i * 2 + 1].Y;
            }


            for (int i = 0; i < poligone.Count ; i++)
            {
                dataGridView2.Rows.Add();
            }

            for (int i = 0; i < poligone.Count ; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = poligone[i].X;
                dataGridView2.Rows[i].Cells[1].Value = poligone[i].Y;
                
            }
            

        }

        private void read_button_Click(object sender, EventArgs e)
        {
            points = new List<Point>();
            poligone = new List<Point>();
            int X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    X1 = Convert.ToInt32(row.Cells[0].Value);
                    Y1 = Convert.ToInt32(row.Cells[1].Value);
                    X2 = Convert.ToInt32(row.Cells[2].Value);
                    Y2 = Convert.ToInt32(row.Cells[3].Value);


                    if (X1 == 0 && X2 == 0 && Y1 == 0 && Y2 == 0)
                    {
                        
                    }
                    else
                    {
                        points.Add(new Point(X1, Y1));
                        points.Add(new Point(X2, Y2));
                    }

                }
                catch
                {
                    MessageBox.Show("Error");
                    return;
                }
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                try
                {
                    X1 = Convert.ToInt32(row.Cells[0].Value);
                    Y1 = Convert.ToInt32(row.Cells[1].Value);
                    if (X1 == 0 && X2 == 0)
                    {

                    }
                    else
                    {
                        poligone.Add(new Point(X1, Y1));
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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

          
            pictureBox1.Refresh();
            Graphics g = pictureBox1.CreateGraphics();
            Pen penline = new Pen(line_color.BackColor, 1);

            if (points.Count % 2 == 1)
            {
                if (ModifierKeys == Keys.Control || radioButton3.Checked)
                {
                    g.DrawLine(penline, points[points.Count - 1], ControlKeyLine(points[points.Count - 1], new Point(e.X, e.Y)));
                }
                else
                {
                    if (ModifierKeys == Keys.Alt || radioButton4.Checked)
                    {
                        if (points.Count % 2 == 1)
                        {
                            Parallel parar = new Parallel();
                            //parar.SearchNearest(ref poligone,coordinates);
                            List<Point> temp_poligone = new List<Point>();
                            for (int i = 0; i < poligone.Count; i++)
                            {
                                temp_poligone.Add(poligone[i]);
                            }
                            g.DrawLine(penline, points[points.Count - 1], parar.SearchNearest( temp_poligone, points[points.Count - 1], new Point(e.X, e.Y)));
                            temp_poligone.Clear();
                        }
                        
                    }
                    else
                    {
                        if (ModifierKeys != Keys.Shift)
                            g.DrawLine(penline, points[points.Count - 1], new Point(e.X, e.Y));
                    }
                }
            }
        }

        Bitmap bmt;
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmt);
            List<Point> temp_poligone = new List<Point>();
            for (int i = 0; i < poligone.Count; i++ )
            {
                temp_poligone.Add(poligone[i]);
            }
            Algo ag=new Algo(temp_poligone,points,cut_color.BackColor,g);
            temp_poligone.Clear();
            pictureBox1.BackgroundImage = bmt;
            g.Flush();
            pictureBox1.Refresh();
            //DrawAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            points = new List<Point>();
            poligone = new List<Point>();
            RecreteDataGridViews();
            DrawAll();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            bmt = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }



    }
}
