#pragma once
#include "st.h"
#include "Cabine.h"
#include "Floor.h"
#include "LiftPanel.h"
#include "LiftBrain.h"
namespace Lift_СPP_Version
{

	ref class Lift
	{
	private:
		Cabine^ cabine;
		array<Floor^>^ floors;
		LiftPanel^ controls_lift;
		LiftBrain^ brain;

		bool* upcalls;
		bool* downcalls;
		bool* cabincalls;

		int n;
		int cabinlevel;
		Brain_command command;

	public:
		Lift(Panel^& floors_panel,Panel^& Shakhta, Panel^& controns_panel);

	private:
		// Обработчик события внутренней панели лифта
        void c_pan_CallLift(int i);	
        // Обработчик события нажития кнпки вверх на этаже
		void CallFromFloorUp(int i);
        // Обработчик события нажатия кнопки вниз на этаже
        void CallFromFloorDown(int i);

		//Сброс этажа по прибытию на него при движении вниз
		void downreset(int i);
		//Сброс этажа по прибытию на него при движении вверх
		void upreset(int i);
		
		//Проверка нужности остановиться на этаже
        void checher(int i);
	};

}