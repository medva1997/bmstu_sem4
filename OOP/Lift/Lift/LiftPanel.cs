using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lift
{
    class LiftPanel
    {
        /// <summary>
        /// Панель управления лифтом изнути
        /// </summary>
        Panel Contol_panel;
        /// <summary>
        /// Кнопри открытия и закрития дверей для нажатия на панель управления лифтом изнути
        /// </summary>
        Button button_close, button_open, button_call;
        /// <summary>
        /// Кнопри с номерами этажей для нажатия на панель управления лифтом изнути
        /// </summary>
        Button[] buttons;

        public event CallLiftOnFloor CallLift;
        public event OpenDoors OpenD;
        public event CloseDoors CloseD;

        public LiftPanel(ref Panel Contol_panel, int n)
        {
            this.Contol_panel = Contol_panel;

            buttons = new Button[n];
            button_close = new Button();
            button_open = new Button();
            button_call = new Button();
            Label label1 = new Label();

            #region form
            // 
            // Contol_panel
            // 
            this.Contol_panel.Controls.Add(this.button_close);
            this.Contol_panel.Controls.Add(this.button_open);
            this.Contol_panel.Controls.Add(this.button_call);
            this.Contol_panel.Controls.Add(label1);
            this.Contol_panel.Name = "Contol_panel";
            this.Contol_panel.TabIndex = 1;
            this.Contol_panel.BackColor = Color.White;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(42, 4);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 13);
            label1.TabIndex = 0;
            label1.Text = "Управление";

            // 
            // button_open
            // 
            this.button_open.Location = new System.Drawing.Point(127, 25);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(42, 23);
            this.button_open.TabIndex = 3;
            this.button_open.Text = "Open";
            this.button_open.UseVisualStyleBackColor = true;


            this.button_call.Location = new System.Drawing.Point(127, 80);
            this.button_call.Name = "button_call";
            this.button_call.Size = new System.Drawing.Size(42, 23);
            this.button_call.TabIndex = 3;
            this.button_call.Text = "Call";
            this.button_call.UseVisualStyleBackColor = true;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(127, 54);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(42, 23);
            this.button_close.TabIndex = 4;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            // 
            #endregion

            for (int i = 0; i < n; i++)
            {

                buttons[i] = new Button();
                this.Contol_panel.Controls.Add(buttons[i]);

                this.buttons[i].Location = new System.Drawing.Point(4, (n - i - 1) * 25 + 4 * (n - i - 1) + 25);
                this.buttons[i].Size = new System.Drawing.Size(42, 23);
                this.buttons[i].TabIndex = i;
                this.buttons[i].Text = (i + 1).ToString();
                this.buttons[i].UseVisualStyleBackColor = true;
                this.buttons[i].Click += MyLift_Click;
            }
            button_call.Click += button_call_Click;
            button_open.Click += button_open_Click;
            button_close.Click += button_close_Click;

        }

        void button_call_Click(object sender, EventArgs e)
        {
            Console.Beep(1000, 100);
        }
        void button_close_Click(object sender, EventArgs e)
        {
            CloseD();
        }

        void button_open_Click(object sender, EventArgs e)
        {
            OpenD();
        }

        void MyLift_Click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            int _numb = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == temp)
                {
                    _numb = i;
                }
            }
            if (buttons[_numb].BackColor == Color.Red)
                reset(_numb);
            else
                buttons[_numb].BackColor = Color.Red;
            CallLift(_numb);
        }

        public void reset(int i)
        {
            buttons[i].BackColor = Control.DefaultBackColor;
        }

    }
}
