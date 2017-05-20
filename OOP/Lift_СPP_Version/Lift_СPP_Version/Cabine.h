#pragma once
#include "st.h"
namespace Lift_�PP_Version
{
	

	ref class Cabine
	{
	private:
		/// ����� �����        
		Panel^ Shakhta;
		/// <summary>
        /// ������ �����
        /// </summary>
		System::Drawing::Rectangle cabin;
		//��� ������ ������
        Graphics^ g;
		//��������� ������
		int N;
		//������� �������� �� �������� ������
		int counter;
		//������� �������
		Cabine_command status;
		Brain_command command;
		//�� ����� ����� ������ ������
		int cabinlevel;
		//������ ��� �����������
		Timer^ t;

	public:
		//������� ���������� ������ �����
		event LiftchangeFloor^ ChangeFloor;

	public:
		Cabine(Panel^& Shakhta,int N);
		void DrawCabine();
		//��������� ������ ��� �������� ��� ��������
		void stopping();
	private:
		//������ �������� ������
        void t2_Tick(Object^ sender, EventArgs^ e);
		//������ �������� ������
        void t_Tick(Object^ sender, EventArgs^ e);

	public:
		//�������� ���������� ������ �����, ������ �������
		void CheckIsCabineOnFloor();
		//��� ���������� �������
        void c_pan_OpenD();
        void c_pan_CloseD();
		//�������� ����������� � ��������� ������� � ����������� �������� �����
		void brain_new_command(int new_command);
	};

}

