PUBLIC M_Unsigned2

CodeSeg SEGMENT PUBLIC
	ASSUME CS:CodeSeg
	
M_Unsigned2	PROC NEAR
	PUSH	BP
	MOV		BP,SP
	PUSH	AX
	PUSH	DX
	PUSH	SI
	MOV		AX,[BP+4]
	MOV		SI,16
		
	M1:
		MOV		DH,0
		SHL		AX,1 ; в СF значение сдвинутого бита
		JNC		M2
		INC		DH
		JMP		M4
	M2:	
		DEC		SI
	JNZ		M1
	M3:
		MOV		DH,0
		SHL		AX,1
		JNC		M4
		INC		DH
	M4:
		MOV		DL,'0'
		ADD		DL,DH
		PUSH	AX
		MOV		AH,2
		INT		21h
		POP		AX
		DEC		SI
	JNZ		M3
	
	POP		SI	
	POP		DX
	POP		AX
	POP		BP
	RET
M_Unsigned2 ENDP

CodeSeg ENDS
END