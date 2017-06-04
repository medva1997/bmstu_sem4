// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//
#include "stdafx.h"
extern "C" void START(void);


int _tmain(int argc, _TCHAR* argv[])
{
	_asm
	{
		call START
	}

	return 0;
}

