using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift
{
    class LiftBrain
    {
        //Вызванные кнопки вверх
        bool[] upcalls;
        //Вызванные кнопки вниз
        bool[] downcalls;
        //выбранные этажи в кабине
        bool[] cabincalls;
        //Этаж на котором находится кабина
        int cabinlevel;
        int N;

        Status st;

        enum Status
        {
            GoUp,
            GoDown,
            DontMove           
        } 
        public LiftBrain(ref bool[] upcalls,ref bool[] downcalls, ref bool[] cabincalls, ref int cabinlevel)
        {
            this.upcalls = upcalls;
            this.downcalls = downcalls;
            this.cabincalls = cabincalls;
            this.cabinlevel = cabinlevel;
            this.N = upcalls.Length;

            st = Status.DontMove;
        }

        bool NeedGoDownUseUp()
        {
            for (int i = cabinlevel; i <N; i++)
            {
                if (upcalls[i] == true || cabincalls[i] == true)
                {
                    return true;
                }
            }
            return false;
        }

        bool NeedGoDownUseDown()
        {
            for (int i = cabinlevel; i < N; i++)
            {
                if (downcalls[i] == true || cabincalls[i] == true)
                {
                    return true;
                }
            }
            return false;
        }

        bool NeedGoUpUseUp()
        {
            for (int i = cabinlevel; i >=0; i--)
            {
                if (upcalls[i] == true || cabincalls[i] == true)
                {
                    return true;
                }
            }
            return false;
        }

        bool NeedGoUpUseDown()
        {
            for (int i = cabinlevel; i >= 0; i--)
            {
                if (downcalls[i] == true || cabincalls[i] == true)
                {
                    return true;
                }
            }
            return false;
        }

        Status CountNextCommand()
        {
            if (st == Status.GoDown)
            {
                if (NeedGoDownUseDown() == true)
                    return Status.GoDown;
                if (NeedGoDownUseUp() == true)
                    return Status.GoUp;
                if (NeedGoUpUseUp() == true) 
                    return Status.GoDown;
                if (NeedGoUpUseDown() == true) 
                    return Status.GoUp;
                
                //st= Status.DontMove;                    
            }

            if (st == Status.GoUp)
            {
                if (NeedGoDownUseDown() == true) 
                    return Status.GoUp;
                if (NeedGoDownUseUp() == true) 
                    return Status.GoUp;
                if (NeedGoUpUseUp() == true) 
                    return Status.GoDown;
                if (NeedGoUpUseDown() == true) 
                    return Status.GoDown;
                //st= Status.DontMove; 
            }

            
                if (NeedGoUpUseUp() == true)
                    return Status.GoDown;
                if (NeedGoUpUseDown() == true)
                    return Status.GoDown;
                if (NeedGoDownUseDown() == true) 
                    return Status.GoUp;
                if (NeedGoDownUseUp() == true)
                    return Status.GoUp;

            return Status.DontMove;
        }

        public int Command()
        {
            st = CountNextCommand();
            switch (st)
            {
                case (Status.DontMove): return 0;
                case (Status.GoUp): return -1;
                case (Status.GoDown): return 1;
            }
            return 0;

        }

        public int SetCabinLevel
        {
            set
            {
                cabinlevel = value;
            }
        }
    }
}
