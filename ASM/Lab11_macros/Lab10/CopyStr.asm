;Функция CopyStr должна копировать последовательность из L байтов, заданную параметром s1,
; в область памяти, заданную параметром s2, и возвращать указатель на начало этой области.
;   char *CopyStr(INT *s1, INT *s2, int L);

.386
.model FLAT,C
PUBLIC CopyStr
.CODE

PROLOG MACRO LIST 
	PUSH EBP		
	MOV EBP,ESP	
    IRP A,<LIST>
    IFIDN <A>,<F>
      PUSHF
    ELSE
      PUSH A
    ENDIF
	MOV ECX,[EBP+16] ;len
	MOV ESI,[EBP+12] ;p2
	MOV EDI,[EBP+8]  ;p1
  ENDM
ENDM

EPILOG MACRO LIST 		
	
    IRP A,<LIST>
    IFIDN <A>,<F>
      POPF
    ELSE
      POP A
    ENDIF
  ENDM
  MOV ESP,EBP
  POP EBP
ENDM

CopyStr PROC

	PROLOG<ESI,EDI,F>
	

	

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
		

		 EPILOG<F,EDI,ESI>
		RET
	 
	RET
 CopyStr ENDP
END