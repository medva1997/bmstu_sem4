PUBLIC M_NumInput


DataSeg SEGMENT PUBLIC
	Num	DB	13,10,'OK',13,10,'$'
	Ent		DB	'Input number: ','$'	
DataSeg ENDS

CodeSeg SEGMENT PUBLIC
	ASSUME CS:CodeSeg
	
;функция ввода числа
M_NumInput	PROC	NEAR
	PUSH	BX
	PUSH	DX
	PUSH	SI
	
	MOV		AX,0
	MOV		BX,0
	
	;приглашение к вводу
	MOV		AH,9
	MOV		DX,OFFSET Ent
	INT		21h
	
	MOV		DX,0
	
	InputLoop:
	
		;ввод символа
		MOV		AH,1
		INT		21h
		
		CMP		AL,13	;Enter
		JE		SymbEnter
		
		CMP		AL,45	;-
		JNE		SymbNumber
		
		MOV		SI,1  ; minus flag
		JMP		InputLoop
		
	SymbNumber:
		PUSH	AX		;кладем в стек обрабатываемую цифру
		MOV		AX,BX	;AX = уже обработанное
		MOV		BX,10	;ВХ = 10
		MUL		BX		;АХ = АХ*ВХ
		POP		BX		;ВХ = обрабатываемая цифра(символ)
		SUB		BL,'0'	
		MOV		BH, 0	
		ADD		BX,AX	;ВХ = обработанное на данном этапе число
	JMP		InputLoop
	
	SymbEnter:
		MOV		AX,BX
		CMP		SI,1
		JNE		EndProc
		NEG		AX
		
	EndProc:
		PUSH	AX
		
	POP		AX
	POP		SI
	POP		DX
	POP		BX
	RET
M_NumInput ENDP		

CodeSeg ENDS

END