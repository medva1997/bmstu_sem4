.386
.model FLAT,PASCAL
PUBLIC START
extern COUNT:near, SetCler:near, A_B:near

.DATA
S 	DD 0,0,0,0
S1 	DD 00001111000011110000111100001111B,1010101B 
           ;0-38 - ������� ������
S2 	DD 00111100001111000011110000111100B,1000001B 
           ;0-38 - ������� ������
L DD 39;= ����� ������
N	DD	37;���� ������ ��������
P	DD	1 ;� ����������� �� ���� ����������

.CODE
START:
	PUSH	OFFSET S1
	PUSH	L
	CALL	COUNT
   
   PUSH		OFFSET	S1
   PUSH		N
   PUSH		P
   CALL SetCler
   
   PUSH		OFFSET	S2
   PUSH		N
   PUSH		P
   CALL SetCler
   
   
   CALL A_B
RET
END
