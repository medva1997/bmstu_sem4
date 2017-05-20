#pragma once
#include "st.h"
namespace Lift_�PP_Version
{
	ref class LiftBrain
	{
	private:
		//��������� ������ �����
		bool* upcalls;
		//��������� ������ ����
		bool* downcalls;
		//��������� ����� � ������
		bool* cabincalls;
		//���� �� ������� ��������� ������
		int cabinlevel;
		//����������� ������
		int N;
		Brain_command st;

	public:
		//������� ����� �������
		event ChangeBrainCommand^ NewCommand;

		LiftBrain(bool upcalls[], bool downcalls[],  bool cabincalls[], int n);

		//��������� ����� �������  �������� � ������� �� ���������
		Brain_command Command(int cabinlevel);

	private:
		bool NeedGoDownUseUp();
		bool NeedGoDownUseDown();
		bool NeedGoUpUseUp();
		bool NeedGoUpUseDown();
		Brain_command NextCommand(int cabinlevel);
		
	};

}