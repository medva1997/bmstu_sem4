;Функция CopyStr должна копировать последовательность из L байтов, заданную параметром s1,
; в область памяти, заданную параметром s2, и возвращать указатель на начало этой области.
;   char *CopyStr(INT *s1, INT *s2, int L);

.386
.model FLAT,C
PUBLIC CopyStr
.CODE
CopyStr PROC

	; соглашение
	PUSH EBP		; сохранение значение предыдущей функции 
	MOV EBP,ESP		; записываем текущую позицию в стеке 
	PUSH EBX		; сохраняем прежние

	MOV EСX,[EBP+16] ;
	MOV ESI,[EBP+12] ;
	MOV EDI,[EBP+8]  ; 
	
	CLD		;df=0
		CMP ESI,EDI ; если подали одну и ту же строку
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
	; соглашение
	POP EBX
	MOV ESP,EBP
	POP EBP
	 
	RET
 CopyStr ENDP
END
