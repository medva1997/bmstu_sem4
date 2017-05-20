#pragma once
#include "st.h"
namespace Lift_СPP_Version
{
	//Класс этажа
	ref class Floor
	{
		private:
			//Панель где рисуются все элементы относящиеся к этому этажу
			Panel^ floor_panel;
			int floor_index;
			//Кнопка вызова лифта наверх
			Button^ butt_up;
			//Кнопка вызова лифта вниз
			Button^ butt_down;
			//Подпись с указанием номера этажа 
			Label^ floor_label;
			//Подпись местоположения лифта
			Label^ lift_label;

		public: 
			//Событие просьбы открыть двери по кнопке на панели
			event CallLiftOnFloor^ CallUp;
			//Событие простьбы закрыть двери по кнопке на панели
			event CallLiftOnFloor^ CallDown;	

			Floor(Panel^& pn, int Number_floor);   
			~Floor();

			//изменение указалеля положения лифта
			void ChangeLiftPositionLabel(int i);

			//Уровень для проверки на прибытия на этаж
			int GetHight();

			//Сброс цвета кнопки вниз
			void resetDown();
			//Сброс цвета кнопки вверх
			void resetUP();

			//Видимость кнопки вверх
			void UpVisible(bool value);
			//Видимость кнопки вниз
			void DownVisible(bool value);

		private: 
			void butt_up_Click(Object^ sender, EventArgs^ e);
			void butt_down_Click(Object^ sender, EventArgs^ e);

			//Кусок макета
			void CreateForm();
	};
}
