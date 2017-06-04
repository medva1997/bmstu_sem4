PUBLIC	M_Signed2
EXTRN	M_Unsigned2:NEAR

CodeSeg	SEGMENT	PUBLIC
	ASSUME	CS:CodeSeg
	
M_Signed2	PROC	NEAR
		PUSH	AX
		PUSH	BP
		MOV		BP,SP
		MOV		AX,[BP+4]
		CMP		AX,0
		JGE		Pozitive	;если число положительное, то печатаем его, если нет, то печатаем минус
		PUSH	AX
		MOV		AH,2
		MOV		DL,'-'
		INT		21h
		POP		AX
		NEG		AX
	Pozitive:
		PUSH	AX
		CALL	M_Unsigned2
		ADD		SP,2
		
	POP		BP
	POP		AX
	RET
M_Signed2 ENDP
CodeSeg	ENDS
END
	