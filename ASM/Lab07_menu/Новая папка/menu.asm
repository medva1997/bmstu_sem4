PUBLIC M_MenuPrint

EXTRN NewLine:NEAR

DataSeg	SEGMENT PUBLIC
	Menu	DB	10,13,'	MENU','$'
	Menu0	DB	'0.Print menu','$'
	Menu1	DB	'1.Input number','$'
	Menu2	DB	'2.Num as unsigned bin','$'
	Menu3	DB	'3.Num as signed bin','$'
	Menu4	DB	'4.Num as unsigned dec','$'
	Menu5	DB	'5.Num as signed dec','$'
	Menu6	DB	'6.Num as unsigned hex','$'
	Menu7	DB	'7.Num as signed hex','$'
	Menu8	DB	'8.Exit','$'
DataSeg ENDS

CodeSeg	SEGMENT PUBLIC
	ASSUME CS:CodeSeg, DS:DataSeg
	
;вывод меню на экран
M_MenuPrint	PROC NEAR
	PUSH	AX
	PUSH	DX
	
	MOV		AH,9
	;MOV	DX,OFFSET Menu
	LEA		DX,Menu
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu0
	LEA		DX,Menu0
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu1
	LEA		DX,Menu1
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu2
	LEA		DX,Menu2
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu3
	LEA		DX,Menu3
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu4
	LEA		DX,Menu4
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu5
	LEA		DX,Menu5
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu6
	LEA		DX,Menu6
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu7
	LEA		DX,Menu7
	INT		21h
	CALL	NewLine
	;MOV	DX,OFFSET Menu8
	LEA		DX,Menu8
	INT		21h
	
	POP		DX
	POP		AX
	RET
M_MenuPrint ENDP

CodeSeg ENDS
END 