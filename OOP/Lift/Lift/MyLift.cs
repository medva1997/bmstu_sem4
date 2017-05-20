using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lift
{
    delegate void CallLiftOnFloor(int i);    
    delegate void LiftchangeFloor(int i);
    delegate void CloseDoors();
    delegate void OpenDoors();    

    class MyLift
    {
        /// <summary>
        /// Массив объектов этажей
        /// </summary>
        Floor[] floors;
        /// <summary>
        /// Шахта лифта
        /// </summary>
        Panel Shachta;
        
        /// <summary>
        /// кабина лифта
        /// </summary>
        Rectangle cabin;
        Graphics g;

        LiftPanel c_pan;
        //флаг открытости дверей
        bool is_open;
        //таймер для смещения лифта
        Timer t;

       
        bool[] upcalls;
        bool[] downcalls;
        bool[] cabincalls;

        LiftBrain brains;
        /// <summary>
        /// событие изменения этажа
        /// </summary>
        public event LiftchangeFloor ChangeFloor;
        

        public MyLift(ref Panel pn, ref Panel Contol_panel, ref Floor[] floors)
        {
            this.floors = floors;
            
            Shachta = pn;
            g = pn.CreateGraphics();
            cabin.Location = new Point(0, 0);
            cabin.Size = new Size(pn.Width, 100);
            is_open = false;
            DrawCabine();           

            t = new Timer();
            t.Interval = 200;
            t.Tick += t_Tick;

            upcalls = new bool[floors.Length];
            downcalls = new bool[floors.Length];
            cabincalls = new bool[floors.Length];

            for (int i = 0; i < floors.Length; i++)
            {
                upcalls[i] = false;
                downcalls[i] = false;
                cabincalls[i] = false;

                floors[i].CallDown += CallFromFloorDown;
                floors[i].CallUp += CallFromFloorUp;
                ChangeFloor += floors[i].ChangeLiftPositionLabel;
            }

            floors[0].DownVisible = false;
            floors[floors.Length - 1].UpVisible = false;

            brains=new LiftBrain(ref upcalls, ref downcalls ,ref cabincalls, ref cabinlevel);
            c_pan = new LiftPanel(ref Contol_panel, floors.Length);
            c_pan.CloseD += c_pan_CloseD;
            c_pan.OpenD += c_pan_OpenD;
            c_pan.CallLift += c_pan_CallLift;

            ChangeFloor+=checher;

            CheckIsCabineOnFloor();
            t.Start();
            
        }

        // Обработчик события внцтренней панели лифта
        void c_pan_CallLift(int i)
        {
            if (flag == 0 && i == cabinlevel)
            {
                stopping();
                c_pan.reset(i);         
                return;
            }

            cabincalls[i] = !cabincalls[i];
            if (flag == 0)
            {
               
                flag= brains.Command();
                
            }
        }

        // Обработчик события нажития кнпки вверх на этаже
        public void CallFromFloorUp(int i)
        {
            //MessageBox.Show("UP "+i);
            if (flag == 0 && i == cabinlevel)
            {
                stopping();
                floors[i].resetUP();
                return;
            }
           

            upcalls[i] = true;
            if (flag == 0)
            {
                flag = brains.Command();
            }

           
        }

        // Обработчик события нажатия кнопки вниз на этаже
        public void CallFromFloorDown(int i)
        {          
            if (flag == 0 && i == cabinlevel)
            {
                stopping();
                floors[i].resetDown(); ;
                return;
            }
           
            downcalls[i] = true;
            if (flag == 0)
            {
                flag = brains.Command();
            }
        }

        int flag = 0;//0-stop, 1-down, -1-up


        void downreset(int i)
        {
            downcalls[i] = false;
            cabincalls[i] = false;
            floors[i].resetDown();
            c_pan.reset(i);
            stopping();
        }

        void upreset(int i)
        {
            upcalls[i] = false;
            cabincalls[i] = false;
            floors[i].resetUP();
            c_pan.reset(i);
            stopping();
        }

        void checher(int i)
        {
           
            if (flag == 1)//вниз
            {
                
                if (downcalls[i] == true || cabincalls[i] == true)
                {
                    downreset(i);
                    flag = brains.Command();
                    return;                    
                }
                if (upcalls[i] == true || cabincalls[i] == true)
                {
                    upreset(i);
                    flag = brains.Command();
                    return;   
                }
            }
           if(flag==-1)
           {                
                if (upcalls[i] == true || cabincalls[i]==true)
                {
                    upreset(i);
                }

                if (downcalls[i] == true || cabincalls[i] == true)
                {
                    downreset(i);
                    flag = brains.Command();
                    return;
                }
            }

            
            if (flag == 0)
            {
                if (upcalls[i] == true || cabincalls[i] == true || downcalls[i] == true)
                {
                    upcalls[i] = false;
                    floors[i].resetUP();
                    downreset(i);
                }      
            }

            flag= brains.Command();
           
            int a;
            
        }

        int next_flag = 0;


        

        void stopping()
        {
            flag = 0;
            is_open = true;
            DrawCabine();
            t.Tick -= t_Tick;
            t.Tick+=t2_Tick;
        }

        int counter = 0;
        void t2_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 5)
            {
                t.Tick += t_Tick;
                t.Tick -= t2_Tick;


                flag = brains.Command();
                is_open = false;
                DrawCabine();
                counter = 0;
            }
        }
        
        void t_Tick(object sender, EventArgs e)
        {
            int dy=0;
            switch (flag)
            {
                case (0): dy = 0; DrawCabine(); return; 
                case (1): dy=10; break;
                case (-1): dy = -10; break;

            }

            cabin.Location = new Point(cabin.Location.X, cabin.Location.Y + dy);
            if (cabin.Location.Y <= 0)
            {
                cabin.Location = new Point(cabin.Location.X, 0);
                flag = 1;
            }

            if (cabin.Location.Y > ((floors.Length-1)*100+(floors.Length-1)*4))
            {
                cabin.Location = new Point(cabin.Location.X, ((floors.Length - 1) * 100 + (floors.Length - 1) * 4));
                flag = -1;
            }

            DrawCabine();
            CheckIsCabineOnFloor();
        }

        int cabinlevel;

        void CheckIsCabineOnFloor()
        {
            for (int i = 0; i < floors.Length; i++)
            {
                int level = floors[i].GetHight();
                if (Math.Abs(cabin.Location.Y -level )<5)
                {
                    cabinlevel = i;
                    brains.SetCabinLevel = cabinlevel;
                    ChangeFloor(i);
                    break;
                }
            }
        }

        void c_pan_OpenD()
        {
            is_open = true;
            DrawCabine();
        }

        void c_pan_CloseD()
        {
            is_open = false;
            DrawCabine();
        }

        private void DrawCabine()
        {
            Brush br = new SolidBrush(Color.Blue);
            Shachta.Refresh();
            if (is_open == true)
            {
                g.FillRectangle(br, cabin);
                Brush br2 = new SolidBrush(Color.White);
                g.FillRectangle(br2, cabin.Location.X + 15, cabin.Location.Y, cabin.Width-30,cabin.Height);

                Random rnd = new Random();
                
                draw_stick_figure((cabin.Location.X + cabin.Width) / 2, cabin.Location.Y + cabin.Height-30, Color.Black, rnd.Next(1,10));
            }
            else
            {
                g.FillRectangle(br, cabin);
            }
        }

        private void draw_stick_figure(int x, int y, Color color, int  legs)
        {
             Pen p=new Pen(color,1);
             Brush br2 = new SolidBrush(color);

                // Голова
                g.FillEllipse(br2, 1 + x, y, 10, 10);
                // Ноги, 1-раздвинуты, остальное сдвинуты
                if (legs > 5)
                 {  
                        g.DrawLine(p,5 + x, 20 + y, 10 + x, 30 + y);
                        g.DrawLine(p, 5 + x, 20 + y, x, 27 + y);
                 }
             else
                {
                     g.DrawLine(p, 5 + x, 20 + y, 5 + x, 30 + y);
                     g.DrawLine(p, 5 + x, 20 + y, 5 + x, 30 + y);
                }
                // Тело
                g.DrawLine(p,5 + x, 20 + y, 5 + x, 10 + y);
                // Руки
                g.DrawLine(p,5 + x, 10 + y, 9 + x, 20 + y);
                g.DrawLine(p,5 + x, 10 + y, 1 + x, 20 + y);
         }
    }
}
