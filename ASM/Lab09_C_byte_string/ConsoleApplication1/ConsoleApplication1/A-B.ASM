;—оставить подпрограмму с именем A_B типа
;   Procedure (var A: LONGWORD; const B:LONGWORD; L:LONGWORD)
;выполн€ющую вычитание битовых строк A:=A \ B длины L. 
.386
.model FLAT,C
PUBLIC A_B

.CODE
A_B PROC
	
  	; соглашение
	PUSH EBP		; сохранение значение предыдущей функции 
	MOV EBP,ESP		; записываем текущую позицию в стеке 
	PUSH EBX		; сохран€ем прежние

	MOV ESI,[EBP+16] ; строка A	
	MOV EBX,[EBP+12] ; строка B

	MOV EAX,[ESI] ; строка A
	MOV EDX,[EBX] ; строка B

	NOT EDX  ; строка B
	AND EAX, EDX; toA

	MOV [ESI], EAX

	ADD EBX,4	
	ADD ESI,4

	MOV EAX,[ESI] ; строка A
	MOV EDX,[EBX] ; строка B

	NOT EDX  ; строка B
	AND EAX, EDX; toA

	MOV [ESI], EAX



	NOT EBX
	AND ESI, EBX

	MOV [ESI], EAX

	POP EBX
	MOV ESP,EBP
	POP EBP
  RET 12
  A_B ENDP
END
