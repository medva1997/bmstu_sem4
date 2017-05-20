using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_LABA7
{



    public partial class Form1 : Form
    {

        List<Point> points;
        Rectangle rect;
        bool draw_rect=false;
        
        

        /// <summary>
        /// отклонение мыши по оси X
        /// </summary>
        private const int MouseErrorX = 20;
        /// <summary>
        /// отклонение мыши по оси Y
        /// </summary>
        private const int MouseErrorY = 50;

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
            //rect.Location = new Point(-1, -1);

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            if (ModifierKeys == Keys.Shift)
            {
                if (rect.Location == new Point(0, 0))
                {
                    rect.Location = coordinates;
                }
                else
                {
                    int dx = rect.Location.X - coordinates.X;
                    int dy = rect.Location.Y - coordinates.Y;
                    rect.Size = new Size(Math.Abs(dx), Math.Abs(dy));
                    if (dx > 0)
                    {
                        rect.Location = new Point(rect.Location.X - dx,rect.Location.Y);
                    }

                    if (dy > 0)
                    {
                        rect.Location = new Point(rect.Location.X, rect.Location.Y-dy);
                    }
                    draw_rect = true;
                }
                return;
            }


            if (MouseButtons.Left == ((MouseEventArgs)e).Button)
            {
                if (ModifierKeys == Keys.Control)
                {
                    if (points.Count % 2 == 1)
                    {
                        points.Add(ControlKeyLine(points[points.Count - 1],coordinates));
                    }
                }
                else
                {

                    points.Add(coordinates);
                }
            }

            DrawAll();

        }

        private void DrawAll()
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Black, 1);
            pictureBox1.Refresh();

            for (int i = 0; i < points.Count / 2; i++)
            {
                g.DrawLine(pen, points[i * 2], points[i * 2 + 1]);
            }

            if(draw_rect==true)
                g.DrawRectangle(pen, rect);

        }

        /// <summary>
        /// При нажатой клавише ctrl, возвряшает кординаты токни на оси ох или oy
        /// </summary>
        /// <param name="lastpoint">Последняя введенная пользователем точка</param>
        /// <returns></returns>
        private Point ControlKeyLine(Point lastpoint, Point mouse)
        {
            Point p = new Point();
            int deltaX = lastpoint.X - mouse.X ;
            int deltaY = lastpoint.Y - mouse.Y ;
            if (deltaX != 0)
            {
                double tang = deltaY / deltaX;
                if (Math.Abs(tang) < 1)
                {
                    p = new Point(mouse.X , lastpoint.Y);
                }
                else
                {
                    p = new Point(lastpoint.X, mouse.Y);
                }
            }
            else
            {
                p = new Point(lastpoint.X, mouse.Y );
            }
            return p;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            DrawAll();
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Black, 1);

            if (ModifierKeys == Keys.Shift)
            {

                int dx = rect.Location.X - e.X;
                int dy = rect.Location.Y - e.Y;
                int X = rect.Location.X;
                int Y = rect.Location.Y;
                
                if (dx > 0)
                {
                    X -= dx;
                }

                if (dy > 0)
                {
                    Y -= dy;
                }

                g.DrawRectangle(pen, X, Y, Math.Abs(dx), Math.Abs(dy));
            }

            if (points.Count % 2 == 1)
            {
                if (ModifierKeys == Keys.Control)
                {
                    g.DrawLine(pen, points[points.Count - 1], ControlKeyLine(points[points.Count - 1], new Point(e.X, e.Y)));
                }
                else
                {
                    if (ModifierKeys != Keys.Shift)
                        g.DrawLine(pen, points[points.Count - 1], new Point(e.X, e.Y));
                }                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g=pictureBox1.CreateGraphics();
            Algo alg=new Algo(points,rect, ref g);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int X=0, Y=0, W=0, H=0;
            try
            {
                X = Convert.ToInt32(textBox1.Text);
                Y = Convert.ToInt32(textBox2.Text);
                W = Convert.ToInt32(textBox3.Text);
                H = Convert.ToInt32(textBox4.Text);
                draw_rect = true;
            }
            catch
            {

                MessageBox.Show("Error");
                return;
            }
            rect.Location = new Point(X, Y);
            rect.Size=new Size(W,H);


            int X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;
            points = new List<Point>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
               
                try
                {
                    X1 = Convert.ToInt32(row.Cells[0].Value);
                    Y1 = Convert.ToInt32(row.Cells[1].Value);
                    X2 = Convert.ToInt32(row.Cells[2].Value);
                    Y2 = Convert.ToInt32(row.Cells[3].Value);

                    points.Add(new Point(X1, Y1));
                    points.Add(new Point(X2, Y2));

                }
                catch {
                    MessageBox.Show("Error");
                    return;
                }
            }

            DrawAll();

        }
    }
}