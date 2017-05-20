#pragma once

namespace Lift_ÑPP_Version {

	using namespace System::Windows::Forms;
	using namespace System::Drawing;
	using namespace System;

	typedef int Brain_command ;
	typedef int Cabine_command ;

	delegate void CallLiftOnFloor(int i); 
	delegate void LiftchangeFloor(int i);
	delegate void CloseDoors();
	delegate void OpenDoors();
	delegate void ChangeBrainCommand(int i);

	enum Brain_Status
    {
        GoUp,
        GoDown,
        DontMove           
    };

	enum Cabine_Status
    {
        NeedOpen,
        Opening,
        Opened,
		Closing,
		NeedClose,
		Closed
    };

	
}