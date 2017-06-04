EXTRN	M_MenuPrint:NEAR
EXTRN	M_NumInput:NEAR


PUBLIC NewLine

DataSeg	SEGMENT PUBLIC
	FuncM	DW	M_MenuPrint,M_NumInput
	X		DW	1
	Ent		DB	'Choose from 0..7: ','$'
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
		CALL	FuncM ;вызов печати меню
	
	MenuLoop:
	
		CALL	NewLine ;перенос стрки
		
		;вывод приглашения ввести пункт меню
		MOV		AH,9
		MOV		DX,OFFSET Ent
		INT		21h
		
		
		;ввод и вывод пукта меню
		MOV		AH,1
		INT		21h

		CALL	NewLine ;перенос стрки
		
		MOV		BL,AL
		SUB		BL,'0'
		
		CMP BL,8	;menu >8
		JG Print
		
		CMP BL,0	;menu <0
		JL Print
		
		
		CMP		BL,8			;проверка на выход
		JE		EndProg			;выход
		
		SHL		BL,1			;умножение на 2 для того чтобы работа индексация
		MOV		BH,0
		
		CMP		BL,2			; menu >=2
		JBE		SkipPush
		PUSH	X				;пуш Х если вызывается функция печати числа
		
	SkipPush:
		CALL	FuncM[BX]		;вызов выбранной функции
		
		CMP		BL,2
		JBE		SkipPop		
		ADD		SP,2			;поп Х
		
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
		


	
