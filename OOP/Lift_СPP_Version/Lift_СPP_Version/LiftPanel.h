#pragma once
#include "st.h"
namespace Lift_�PP_Version
{
	ref class LiftPanel
	{
		private:
			/// <summary>
			/// ������ ���������� ������ ������
			/// </summary>
			Panel^ Contol_panel;
			/// <summary>
			/// ������ �������� � �������� ������ ��� ������� �� ������ ���������� ������ ������
			/// </summary>
			Button^ button_close;
			Button^ button_open;
			Button^ button_call;
			/// <summary>
			/// ������ � �������� ������ ��� ������� �� ������ ���������� ������ ������
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

			//����� ������
			void LiftPanel::CreateForm();

		public:
			//����� ������ �����
			void reset(int i);
	};

}