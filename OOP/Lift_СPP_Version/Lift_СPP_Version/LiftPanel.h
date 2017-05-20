#pragma once
#include "st.h"
namespace Lift_СPP_Version
{
	ref class LiftPanel
	{
		private:
			/// <summary>
			/// Панель управления лифтом изнути
			/// </summary>
			Panel^ Contol_panel;
			/// <summary>
			/// Кнопри открытия и закрития дверей для нажатия на панель управления лифтом изнути
			/// </summary>
			Button^ button_close;
			Button^ button_open;
			Button^ button_call;
			/// <summary>
			/// Кнопки с номерами этажей для нажатия на панель управления лифтом изнути
			/// </summary>	
			array<Button^>^ buttons;

		public: 
			event CallLiftOnFloor^ CallLift;
			event OpenDoors^ OpenD;
			event CloseDoors^ CloseD;
			LiftPanel(Panel^& Contol_panel, int n);
			~LiftPanel();

		private:
			void button_call_Click(Object^ sender, EventArgs^ e);
			void button_close_Click(Object^ sender, EventArgs^ e);
			void button_open_Click(Object^ sender, EventArgs^ e);
			void MyLift_Click(Object^ sender, EventArgs^ e);

			//Кусок макета
			void LiftPanel::CreateForm();

		public:
			//сброс кнопки этажа
			void reset(int i);
	};

}