;������� DlinaStroki ������ ���������� ����� ������ ASCIIZ, �������� ���������� s.
;int DlinaStroki(char *s);

.386
.model FLAT,C
PUBLIC DlinaStroki
.CODE
DlinaStroki PROC

	; ����������
	PUSH EBP		; ���������� �������� ���������� ������� 
	MOV EBP,ESP		; ���������� ������� ������� � ����� 
    PUSH ESP		; ��������� �������

	PUSH EDI
	


	
	MOV EDI,[EBP+8] ; ������� ����� ������ S, �� �������� 12 (������ �����)
	
	XOR EAX,EAX		;��������� EAX ��� ������ 0
	MOV ECX,-1		; ffffffff
	REPNE SCASB
	NOT ECX			
	DEC ECX
	MOV EAX,ECX

	; ����������

	
	POP EDI

	POP ESP
	MOV ESP,EBP
	POP EBP
	 
	 RET 
DlinaStroki ENDP
END