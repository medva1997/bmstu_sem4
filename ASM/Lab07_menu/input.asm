PUBLIC M_NumInput


DataSeg SEGMENT PUBLIC
	Num	DB	13,10,'OK',13,10,'$'
	Ent		DB	'Input number: ','$'	
DataSeg ENDS

CodeSeg SEGMENT PUBLIC
	ASSUME CS:CodeSeg
	
;������� ����� �����
M_NumInput	PROC	NEAR
	PUSH	BX
	PUSH	DX
	PUSH	SI
	
	MOV		AX,0
	MOV		BX,0
	
	;����������� � �����
	MOV		AH,9
	MOV		DX,OFFSET Ent
	INT		21h
	
	MOV		DX,0
	
	InputLoop:
	
		;���� �������
		MOV		AH,1
		INT		21h
		
		CMP		AL,13	;Enter
		JE		SymbEnter
		
		CMP		AL,45	;-
		JNE		SymbNumber
		
		MOV		SI,1  ; minus flag
		JMP		InputLoop
		
	SymbNumber:
		PUSH	AX		;������ � ���� �������������� �����
		MOV		AX,BX	;AX = ��� ������������
		MOV		BX,10	;�� = 10
		MUL		BX		;�� = ��*��
		POP		BX		;�� = �������������� �����(������)
		SUB		BL,'0'	
		MOV		BH, 0	
		ADD		BX,AX	;�� = ������������ �� ������ ����� �����
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