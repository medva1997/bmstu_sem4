// Lab11.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "iostream"
using namespace std;
extern "C"  char *CopyStr(char *s1, char *s2, int L);
extern "C" int DlinaStroki(char *s);


int _tmain(int argc, _TCHAR* argv[])
{
		char* s0="xaxaxa";
		//char s1[]="xaxaxa";
		//char* s2=new char[20];
		//char* s2=calloc(20,0)
		int L=DlinaStroki(s0);
		cout<<L<<endl;
		int tmp;
		//cin>>tmp;
		char* s1= (char*) calloc(20,sizeof(char));
		CopyStr(s1,s0,L);

		printf("%s",s1);
		//free(s1);
		cin>>tmp;
	
	return 0;

}

