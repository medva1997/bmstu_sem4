#include "LiftBrain.h"
namespace Lift_ÑPP_Version
{
	LiftBrain::LiftBrain(bool upcalls[], bool downcalls[],  bool cabincalls[], int n)
    {
        this->upcalls = upcalls;
        this->downcalls = downcalls;
        this->cabincalls = cabincalls;
        this->cabinlevel = cabinlevel;
        this->N = n;
        st = DontMove;
    }

	bool LiftBrain::NeedGoDownUseUp()
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

    bool LiftBrain::NeedGoDownUseDown()
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

    bool LiftBrain::NeedGoUpUseUp()
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

    bool LiftBrain::NeedGoUpUseDown()
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

	Brain_command LiftBrain::Command(int cabinlevel)
	{
		st=NextCommand(cabinlevel);
		NewCommand(st);
		return st;
	}

	Brain_command LiftBrain::NextCommand(int cabinlevel)
    {
		this->cabinlevel=cabinlevel;
        if (st == GoDown)
        {
            if (NeedGoDownUseDown() == true)
                return GoDown;
            if (NeedGoDownUseUp() == true)
                return GoUp;
            if (NeedGoUpUseUp() == true) 
                return GoDown;
            if (NeedGoUpUseDown() == true) 
                return GoUp;
                
            //st= DontMove;                    
        }

        if (st == GoUp)
        {
            if (NeedGoDownUseDown() == true) 
                return GoUp;
            if (NeedGoDownUseUp() == true) 
                return GoUp;
            if (NeedGoUpUseUp() == true) 
                return GoDown;
            if (NeedGoUpUseDown() == true) 
                return GoDown;
            //st= DontMove; 
        }

            
            if (NeedGoUpUseUp() == true)
                return GoDown;
            if (NeedGoUpUseDown() == true)
                return GoDown;
            if (NeedGoDownUseDown() == true) 
                return GoUp;
            if (NeedGoDownUseUp() == true)
                return GoUp;

        return DontMove;
	}
}


