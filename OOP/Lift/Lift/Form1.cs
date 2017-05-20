using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lift
{
    public partial class Form1 : Form
    {
        private Floor[] floors;
        public Form1()
        {
            InitializeComponent();
        }

        private MyLift lift;
        private void Form1_Shown(object sender, EventArgs e)
        {
            int h = panel1.Height;
            floors=new Floor[h/100];
            for (int i = 0; i < h / 100; i++)
            {
                Panel p = new Panel();
                panel1.Controls.Add(p);
                p.Location = new Point(4, (h / 100 - i - 1) * 100 + (h / 100 - i - 1) * 4);
                floors[i] = new Floor(ref p, i);
            }

            

            
            lift = new MyLift(ref panel2, ref panel3, ref floors);


        }
    }
}
