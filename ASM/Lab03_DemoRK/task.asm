;��������� ��������� ����������� �� ������� ����  A[6]
;� ���������� ���������� � ������� B[6] ���� �����, ������� 8.
;���������� (������������ � � ����� � �� ����������)  ���������� � ����
;����������

TITLE        PK1
     SSTACK     SEGMENT PARA STACK  'STACK'
                DB   64 DUP('����____')
     SSTACK     ENDS

     DSEG          SEGMENT  PARA PUBLIC 'DATA'
	A    DW    1,8,16,25,32,6
	B    DW    0,0,0,0,0,0
    K    DW    ?
	P    DW    ?
     DSEG          ENDS

SUBTTL         �������� ���������
PAGE
     CSEG      SEGMENT PARA PUBLIC 'CODE'
               ASSUME CS:CSEG,DS:DSEG,SS:SSTACK

     START     PROC FAR
               MOV  BX,DSEG
               MOV  DS,AX
			   MOV SI,0
			   MOV K,0
			   MOV CX,6

		M2:		MOV BX,A[SI]
				TEST BX,11B
				JNZ M3
				MOV BX, A[SI]
				MOV DI,K
				MOV B[DI], BX
				INC  K

		M3:		INC  SI				
				LOOP M2
		
     M5:       MOV  AH,4CH
               MOV  AL,0
               INT 21H
     START     ENDP

     CSEG      ENDS
               END  START