using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lift
{
    class Floor
    {
        private Panel panel;
        private int floor_index;
        private Button butt_up;
        private Button butt_down;
        private Label floor_label;
        private Label lift_label;

        public event CallLiftOnFloor CallUp;
        public event CallLiftOnFloor CallDown;

        public Floor(ref Panel pn, int Number_floor)
        {
            panel = pn;
            #region form


            this.floor_label = new System.Windows.Forms.Label();
            this.lift_label = new System.Windows.Forms.Label();
            this.butt_up = new System.Windows.Forms.Button();
            this.butt_down = new System.Windows.Forms.Button();
          
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lift_label);
            this.panel.Controls.Add(this.butt_down);
            this.panel.Controls.Add(this.butt_up);
            this.panel.Controls.Add(this.floor_label);
            this.panel.TabIndex = 0;
            this.panel.Size = new System.Drawing.Size(100, 100);
            panel.BackColor = Color.White;
            
            // 
            // floor_label
            // 
            this.floor_label.AutoSize = true;
            this.floor_label.Location = new System.Drawing.Point(3, 23);
            this.floor_label.Name = "floor_label";
            this.floor_label.Size = new System.Drawing.Size(35, 13);
            this.floor_label.TabIndex = 0;
            this.floor_label.Text = "floor_label";
            // 
            // butt_up
            // 
            this.butt_up.Location = new System.Drawing.Point(6, 40);
            this.butt_up.Name = "butt_up";
            this.butt_up.Size = new System.Drawing.Size(32, 23);
            this.butt_up.TabIndex = 1;
            this.butt_up.Text = "up";
            this.butt_up.UseVisualStyleBackColor = true;
            // 
            // butt_down
            // 
            this.butt_down.Location = new System.Drawing.Point(6, 69);
            this.butt_down.Name = "butt_down";
            this.butt_down.Size = new System.Drawing.Size(45, 23);
            this.butt_down.TabIndex = 2;
            this.butt_down.Text = "Down";
            this.butt_down.UseVisualStyleBackColor = true;
            // 
            // lift_label
            // 
            this.lift_label.AutoSize = true;
            this.lift_label.Location = new System.Drawing.Point(35, 10);
            this.lift_label.Name = "lift_label";
            this.lift_label.Size = new System.Drawing.Size(35, 13);
            this.lift_label.TabIndex = 3;
            this.lift_label.Text = "0";
            #endregion

            floor_index = Number_floor;
            floor_label.Text = (floor_index+1).ToString();

            butt_down.Click += butt_down_Click;
            butt_up.Click += butt_up_Click;
        }

        private void butt_up_Click(object sender, EventArgs e)
        {
            butt_up.BackColor = Color.Red;
            CallUp(floor_index);
        }

        private void butt_down_Click(object sender, EventArgs e)
        {
            butt_down.BackColor = Color.Red;
            CallDown(floor_index);
        }

        public void ChangeLiftPositionLabel(int i)
        {
            lift_label.Text = (i + 1).ToString();
        }

        //Уровень для проверки на прибытия на этаж
        public int GetHight()
        {
            return panel.Location.Y;
        }

        public void resetDown()
        {
            butt_down.BackColor = Control.DefaultBackColor;
        }

        public void resetUP()
        {
            butt_up.BackColor = Control.DefaultBackColor;
        }

        public bool UpVisible
        {
            set
            {
                butt_up.Visible = value;
            }
        }

        public bool DownVisible
        {
            set
            {
                butt_down.Visible = value; ;
                 
            }
        }
    }
}
