#pragma once
#include "st.h"
namespace Lift_СPP_Version
{
	

	ref class Cabine
	{
	private:
		/// Шахта лифта        
		Panel^ Shakhta;
		/// <summary>
        /// кабина лифта
        /// </summary>
		System::Drawing::Rectangle cabin;
		//чем рисуем кабину
        Graphics^ g;
		//Количесво этажей
		int N;
		//счетчик таймеров до закрытия дверей
		int counter;
		//текущие команды
		Cabine_command status;
		Brain_command command;
		//на каком этаже сейчас кабина
		int cabinlevel;
		//таймар для перерисовки
		Timer^ t;

	public:
		//событие достижения нового этажа
		event LiftchangeFloor^ ChangeFloor;

	public:
		Cabine(Panel^& Shakhta,int N);
		void DrawCabine();
		//Остановка кабины для погрузки или выгрузки
		void stopping();
	private:
		//таймер закрытия дверей
        void t2_Tick(Object^ sender, EventArgs^ e);
		//таймер движения кабины
        void t_Tick(Object^ sender, EventArgs^ e);

	public:
		//проверка достижения нового этажа, кидает событие
		void CheckIsCabineOnFloor();
		//для управления дверями
        void c_pan_OpenD();
        void c_pan_CloseD();
		//получает уведомление о изменении команды в направление движения лифта
		void brain_new_command(int new_command);
	};

}

