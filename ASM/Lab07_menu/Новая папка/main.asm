EXTRN	M_MenuPrint:NEAR
EXTRN	M_NumInput:NEAR
EXTRN	M_Unsigned2:NEAR
EXTRN	M_Signed2:NEAR

PUBLIC NewLine

DataSeg	SEGMENT PUBLIC
	FuncM	DW	M_MenuPrint,M_NumInput,M_Unsigned2,M_Signed2
	X		DW	1
	Ent		DB	'> ','$'
DataSeg	ENDS

CodeSeg	SEGMENT PUBLIC
	ASSUME CS:CodeSeg,DS:DataSeg,SS:StackSeg
	
StackSeg	SEGMENT STACK
	DW	128h	DUP (?)
StackSeg	ENDS
	
NewLine	PROC NEAR
	PUSH	AX
	PUSH	DX
	MOV		AH,2
	MOV		DL,10
	INT		21h
	MOV		DL,13
	INT		21h
	POP		DX
	POP		AX
	RET
NewLine	ENDP
	
MAIN:
	MOV AX,DataSeg
	MOV DS,AX
	
	Print:	
		CALL	FuncM
	
	MenuLoop:
		CALL	NewLine
		MOV		AH,9
		MOV		DX,OFFSET Ent
		INT		21h
		
		MOV		AH,8
		INT		21h
		MOV		AH,2
		MOV		DL,AL
		INT		21h

		CALL	NewLine
		MOV		BL,AL
		SUB		BL,'0'
		
		CMP		BL,8
		JE		EndProg
		
		ADD		BL,BL
		MOV		BH,0
		
		CMP		BL,2
		JBE		SkipPush
		PUSH	X			;пуш Х если вызывается функция печати числа
	SkipPush:
		CALL	FuncM[BX]
		
		CMP		BL,2
		JBE		SkipPop
		ADD		SP,2		;поп Х
	SkipPop:
		CMP		BL,2
		JNE		MenuLoop
		MOV		X,AX
		
	JMP		MenuLoop
	
	EndProg:
		MOV		AH,4Ch
		MOV		AL,0
		INT		21h
CodeSeg ENDS
	END MAIN
		


	
