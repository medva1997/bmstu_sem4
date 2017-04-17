using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba4
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private BitmapZoomer Zoomer;
        private Bitmap viever;

        public Form1()
        {
            InitializeComponent();
        }


        private bool GetParams(out int X, out int Y, out int RX, out int RY)
        {
            X = Y = RX = RY = 0;
            try
            {
                X = Convert.ToInt32(textBox2.Text);
                Y = Convert.ToInt32(textBox3.Text);
                RX = Convert.ToInt32(textBox4.Text);
                if(textBox5.Text!="")
                    RY = Convert.ToInt32(textBox5.Text);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        private bool Getspectr(out int RX, out int RY, out int DRX, out int N)
        {
            DRX = N = RX = RY = 0;
            try
            {
                DRX = Convert.ToInt32(textBox7.Text);
                N = Convert.ToInt32(textBox6.Text);
                RX = Convert.ToInt32(textBox9.Text);
                if (textBox8.Text != "")
                    RY = Convert.ToInt32(textBox8.Text);
                return true;
            }
            catch
            {
                return false;
            }
        }      

        private void Form1_Shown(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            Zoomer = new BitmapZoomer(pictureBox1.Size, 100, 100);
            //цвет фона
            button1.BackColor = Color.White;
            //цвет линий.
            button2.BackColor = Color.Black;
            ClearBitmap(button1.BackColor);
            //zoom
            textBox1.Text = "1";
            textBox5.Enabled = false;
            textBox8.Enabled = false;

            textBox2.Text = "200";
            textBox3.Text = "200";
            textBox4.Text = "50";
            textBox5.Text = "70";


            textBox9.Text = "100";
            textBox8.Text = "70";
            textBox7.Text = "5";
            textBox6.Text = "10";
            radioButton6.Checked = true;

            

        }

        

        #region Colors
        //Получить цвет для линий.
        private Color GetLineColor
        {
            get
            {
                if (checkBox1.Checked == true)
                    return button1.BackColor;
                else
                    return button2.BackColor;
            }
        }

        //цвет фона
        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            colorDialog1.AllowFullOpen = true;
            button1.BackColor = colorDialog1.Color;
            ClearBitmap(button1.BackColor);
        }

        //цвет линий
        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            colorDialog1.AllowFullOpen = true;
            button2.BackColor = colorDialog1.Color;
        }

        //сброс фона
        private void button3_Click(object sender, EventArgs e)
        {
            ClearBitmap(button1.BackColor);
            checkBox1.Checked = false;
        }

        //сброс фона
        private void ClearBitmap(Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            graphics.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
            pictureBox1.Image = bitmap;

        }
        #endregion

        #region Zooming
        //масштаб
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Zoomer.setZoomFactor(textBox1.Text);
            if (viever != null)
                viever.Dispose();
            viever = Zoomer.zoom(ref bitmap);
            pictureBox1.Image = viever;
            
        }
        //Left
        private void button4_Click(object sender, EventArgs e)
        {
            if (viever != null)
                viever.Dispose();
            viever=Zoomer.Left(ref bitmap);
            pictureBox1.Image = viever;
            
        }

        //Up
        private void button5_Click(object sender, EventArgs e)
        {
            if (viever != null)
                viever.Dispose();
            viever = Zoomer.Up(ref bitmap);
            pictureBox1.Image = viever;
            
        }

        //Right
        private void button7_Click(object sender, EventArgs e)
        {
            if (viever != null)
                viever.Dispose();
            viever = Zoomer.Right( ref bitmap);
            pictureBox1.Image = viever;
            
           
        }

        //Down
        private void button6_Click(object sender, EventArgs e)
        {
            if (viever != null)
                viever.Dispose();
            viever = Zoomer.Down(ref bitmap);
            pictureBox1.Image = viever;

        }
        #endregion 

        //выключение полей при окружности
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = false;
            textBox8.Enabled = false;
        }

        //включение полей при элипсе
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = true;
            textBox8.Enabled = true;
        }

        private BaseWorker GetObjectByType()
        {
            if (radioButton1.Checked)
                return new StandartWorker();
            if (radioButton2.Checked)
                return new Bresenham();
            if (radioButton3.Checked)
                return new MidPoint();
            if (radioButton4.Checked)
                return new CanonEq();
            if (radioButton5.Checked)
                return new ParamEq();            
            return new StandartWorker();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BaseWorker st = GetObjectByType();

            st.SetColor=GetLineColor;
            int X,Y,RX, RY;
            GetParams(out X, out Y, out RX, out RY);
            if (radioButton7.Checked)
                st.DrawCircle(ref bitmap, X, Y, RX);
            else
                st.DrawEllipse(ref bitmap, X, Y, RX, RY);

            pictureBox1.Image = Zoomer.zoom( ref bitmap);
            checkBox1.Checked = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int Y=pictureBox1.Height/2;
            int X=pictureBox1.Width/2;
            int xr,yr,drx,dry,N;

            Getspectr(out xr, out  yr, out  drx, out  N);
            dry = yr * drx / xr;

            BaseWorker st = GetObjectByType();
            st.SetColor = GetLineColor;

            if (!radioButton7.Checked)
                st.drawSpecrte(ref bitmap, X, Y, xr, yr, drx, dry, N);
            else
                st.drawSpecrteCircle(ref bitmap, X, Y, xr, yr, drx, N);

            pictureBox1.Image = Zoomer.zoom(ref bitmap);
            checkBox1.Checked = false;
        }

    
    }
}
