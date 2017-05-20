#pragma once
#include "st.h"
namespace Lift_СPP_Version
{
	ref class LiftBrain
	{
	private:
		//Вызванные кнопки вверх
		bool* upcalls;
		//Вызванные кнопки вниз
		bool* downcalls;
		//выбранные этажи в кабине
		bool* cabincalls;
		//Этаж на котором находится кабина
		int cabinlevel;
		//Количесство этажей
		int N;
		Brain_command st;

	public:
		//Событие новой команды
		event ChangeBrainCommand^ NewCommand;

		LiftBrain(bool upcalls[], bool downcalls[],  bool cabincalls[], int n);

		//Генератор новой команды  движения и события ее изменения
		Brain_command Command(int cabinlevel);

	private:
		bool NeedGoDownUseUp();
		bool NeedGoDownUseDown();
		bool NeedGoUpUseUp();
		bool NeedGoUpUseDown();
		Brain_command NextCommand(int cabinlevel);
		
	};

}