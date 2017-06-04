PUBLIC M_MenuPrint

EXTRN NewLine:NEAR

DataSeg	SEGMENT PUBLIC
	Menu	DB	10,13,'	MENU',10,13
			DB	'0. Print menu',10,13
			DB	'1. Input number',10,13
			DB	'2. Num as unsigned bin',10,13
			DB	'3. Num as bin',10,13
			DB	'4. Num as unsigned dec',10,13
			DB	'5. Num as dec',10,13
			DB	'6. Num as unsigned hex',10,13
			DB	'7. Num as hex',10,13
			DB	'8. Exit','$'
DataSeg ENDS

CodeSeg	SEGMENT PUBLIC
	ASSUME CS:CodeSeg, DS:DataSeg
	
;вывод меню на экран
M_MenuPrint	PROC NEAR
	PUSH	AX
	PUSH	DX
	
	MOV		AH,9
	MOV	DX, OFFSET Menu	
	INT		21h
	CALL	NewLine		
	
	POP		DX
	POP		AX
	RET
	
M_MenuPrint ENDP

CodeSeg ENDS
END 