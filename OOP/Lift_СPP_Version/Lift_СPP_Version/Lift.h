#pragma once
#include "st.h"
#include "Cabine.h"
#include "Floor.h"
#include "LiftPanel.h"
#include "LiftBrain.h"
namespace Lift_�PP_Version
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
		// ���������� ������� ���������� ������ �����
        void c_pan_CallLift(int i);	
        // ���������� ������� ������� ����� ����� �� �����
		void CallFromFloorUp(int i);
        // ���������� ������� ������� ������ ���� �� �����
        void CallFromFloorDown(int i);

		//����� ����� �� �������� �� ���� ��� �������� ����
		void downreset(int i);
		//����� ����� �� �������� �� ���� ��� �������� �����
		void upreset(int i);
		
		//�������� �������� ������������ �� �����
        void checher(int i);
	};

}