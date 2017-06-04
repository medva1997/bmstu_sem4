// Lab10.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "iostream"
using namespace std;
extern "C"  char *CopyStr(char *s1, char *s2, int L);
extern "C" int DlinaStroki(char *s);





int _tmain(int argc, _TCHAR* argv[])
{
		char s1[]="xaxaxa";
		char* s2=new char[20];
		int L=DlinaStroki(s1);
		cout<<L<<endl;
		int tmp;
		cin>>tmp;
		char*s3=CopyStr(s2,s1,L);
		cout<<s2<<endl;
		
		cin>>tmp;
	
	return 0;

}

