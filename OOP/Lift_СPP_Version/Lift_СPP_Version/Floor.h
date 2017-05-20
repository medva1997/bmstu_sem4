#pragma once
#include "st.h"
namespace Lift_�PP_Version
{
	//����� �����
	ref class Floor
	{
		private:
			//������ ��� �������� ��� �������� ����������� � ����� �����
			Panel^ floor_panel;
			int floor_index;
			//������ ������ ����� ������
			Button^ butt_up;
			//������ ������ ����� ����
			Button^ butt_down;
			//������� � ��������� ������ ����� 
			Label^ floor_label;
			//������� �������������� �����
			Label^ lift_label;

		public: 
			//������� ������� ������� ����� �� ������ �� ������
			event CallLiftOnFloor^ CallUp;
			//������� �������� ������� ����� �� ������ �� ������
			event CallLiftOnFloor^ CallDown;	

			Floor(Panel^& pn, int Number_floor);   
			~Floor();

			//��������� ��������� ��������� �����
			void ChangeLiftPositionLabel(int i);

			//������� ��� �������� �� �������� �� ����
			int GetHight();

			//����� ����� ������ ����
			void resetDown();
			//����� ����� ������ �����
			void resetUP();

			//��������� ������ �����
			void UpVisible(bool value);
			//��������� ������ ����
			void DownVisible(bool value);

		private: 
			void butt_up_Click(Object^ sender, EventArgs^ e);
			void butt_down_Click(Object^ sender, EventArgs^ e);

			//����� ������
			void CreateForm();
	};
}
