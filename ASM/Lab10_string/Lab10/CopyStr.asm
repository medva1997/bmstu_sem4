;������� CopyStr ������ ���������� ������������������ �� L ������, �������� ���������� s1,
; � ������� ������, �������� ���������� s2, � ���������� ��������� �� ������ ���� �������.
;   char *CopyStr(INT *s1, INT *s2, int L);

.386
.model FLAT,C
PUBLIC CopyStr
.CODE
CopyStr PROC

	; ����������
	PUSH EBP		; ���������� �������� ���������� ������� 
	MOV EBP,ESP		; ���������� ������� ������� � ����� 
	PUSH EBX		; ��������� �������

	MOV E�X,[EBP+16] ;
	MOV ESI,[EBP+12] ;
	MOV EDI,[EBP+8]  ; 
	
	CLD		;df=0
		CMP ESI,EDI ; ���� ������ ���� � �� �� ������
		JE EXIT 
		JA M1

		;p2 < p1
		STD ; df=1
		ADD EDI, ECX
		ADD ESI, ECX 
		DEC EDI
		DEC ESI
	M1: 
		REP MOVSB

	EXIT:
	; ����������
	POP EBX
	MOV ESP,EBP
	POP EBP
	 
	RET
 CopyStr ENDP
END
