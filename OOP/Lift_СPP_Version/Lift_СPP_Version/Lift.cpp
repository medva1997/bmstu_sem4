#include "Lift.h"

namespace Lift_СPP_Version
{
	Lift::Lift(Panel^& floors_panel,Panel^& Shakhta, Panel^& controns_panel)
		{
			n=floors_panel->Height/100;
			cabine=gcnew Cabine(Shakhta,n);
			floors=gcnew array<Floor^>(n);
			
			for (int i = 0; i < n; i++)
            {
                Panel^ p = gcnew Panel();
                floors_panel->Controls->Add(p);
                p->Location = Point(4, (n - i - 1) * 100 + (n - i - 1) * 4);
                floors[i] = gcnew Floor(p, i);
            }

			controls_lift=gcnew LiftPanel(controns_panel,n);

			upcalls = new bool[n];
            downcalls = new bool[n];
            cabincalls = new bool[n];

			brain=gcnew LiftBrain(upcalls,downcalls,cabincalls,n);
			for (int i = 0; i < n; i++)
            {
                upcalls[i] = false;
                downcalls[i] = false;
                cabincalls[i] = false;

				floors[i]->CallDown += gcnew CallLiftOnFloor(this,&Lift::CallFromFloorDown);
                floors[i]->CallUp += gcnew CallLiftOnFloor(this,&Lift::CallFromFloorUp);
				cabine->ChangeFloor += gcnew LiftchangeFloor(floors[i],&Floor::ChangeLiftPositionLabel);
            }
			
            floors[0]->DownVisible( false);
            floors[n-1]->UpVisible ( false);
			controls_lift->OpenD  += gcnew OpenDoors(cabine,&Cabine::c_pan_OpenD);
			controls_lift->CloseD  += gcnew CloseDoors(cabine,&Cabine::c_pan_CloseD);
			controls_lift->CallLift+=gcnew CallLiftOnFloor(this,&Lift::c_pan_CallLift);
			brain->NewCommand+=gcnew ChangeBrainCommand(cabine,&Cabine::brain_new_command);
			cabinlevel=n-1;
			cabine->ChangeFloor+= gcnew LiftchangeFloor(this,&Lift::checher);
			cabine->CheckIsCabineOnFloor();
		}

	// Обработчик события внутренней панели лифта
        void Lift::c_pan_CallLift(int i)
        {
			if (command == Brain_Status::DontMove && i == cabinlevel)
            {
                cabine->stopping();
                controls_lift->reset(i);         
                return;
            }

            cabincalls[i] = !cabincalls[i];

            if (command == Brain_Status::DontMove)
            {
               
               command = brain->Command(cabinlevel);
                
            }
        }
	
        // Обработчик события нажития кнпки вверх на этаже
		void Lift::CallFromFloorUp(int i)
        {
            //MessageBox.Show("UP "+i);
            if (command == Brain_Status::DontMove && i == cabinlevel)
            {
                cabine->stopping();
                floors[i]->resetUP();
                return;
            }
           

            upcalls[i] = true;
            if (command == Brain_Status::DontMove)
            {
                command = brain->Command(cabinlevel);
            }

           
        }

        // Обработчик события нажатия кнопки вниз на этаже
        void Lift::CallFromFloorDown(int i)
        {          
            if (command == Brain_Status::DontMove && i == cabinlevel)
            {
                cabine->stopping();
                floors[i]->resetDown(); ;
                return;
            }
           
            downcalls[i] = true;
            if (command == Brain_Status::DontMove)
            {
				command = brain->Command(cabinlevel);
            }
        }



		void Lift::downreset(int i)
        {
            downcalls[i] = false;
            cabincalls[i] = false;
            floors[i]->resetDown();
            controls_lift->reset(i);
            cabine->stopping();
        }

        void Lift::upreset(int i)
        {
            upcalls[i] = false;
            cabincalls[i] = false;
            floors[i]->resetUP();
            controls_lift->reset(i);
            cabine->stopping();
        }

		void Lift::checher(int i)
        {
			cabinlevel=i;
			if (command == Brain_Status::GoDown)//вниз
            {
                
                if (downcalls[i] == true || cabincalls[i] == true)
                {
                    downreset(i);
					command = brain->Command(cabinlevel);
                    return;                    
                }
                if (upcalls[i] == true || cabincalls[i] == true)
                {
                    upreset(i);
					command = brain->Command(cabinlevel);
                    return;   
                }
            }

			if(command==Brain_Status::GoUp)
			   {                
					if (upcalls[i] == true || cabincalls[i]==true)
					{
						upreset(i);
					}

					if (downcalls[i] == true || cabincalls[i] == true)
					{
						downreset(i);
						command = brain->Command(cabinlevel);
						return;
					}
				}

            
			if (command == Brain_Status::DontMove)
            {
                if (upcalls[i] == true || cabincalls[i] == true || downcalls[i] == true)
                {
                    upcalls[i] = false;
                    floors[i]->resetUP();
                    downreset(i);
                }      
            }

            command = brain->Command(cabinlevel);
            
        }

}
