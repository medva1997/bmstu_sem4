;Функция DlinaStroki должна возвращать длину строки ASCIIZ, заданную параметром s.
;int DlinaStroki(char *s);

.386
.model FLAT,C
PUBLIC DlinaStroki
.CODE
DlinaStroki PROC

	; соглашение
	PUSH EBP		; сохранение значение предыдущей функции 
	MOV EBP,ESP		; записываем текущую позицию в стеке 
    PUSH ESP		; сохраняем прежние

	PUSH EDI
	


	
	MOV EDI,[EBP+8] ; Заносим адрес строки S, со смещение 12 (начало слова)
	
	XOR EAX,EAX		;обнуление EAX ДЛЯ ПОИСКА 0
	MOV ECX,-1		; ffffffff
	REPNE SCASB
	NOT ECX			
	DEC ECX
	MOV EAX,ECX

	; соглашение

	
	POP EDI

	POP ESP
	MOV ESP,EBP
	POP EBP
	 
	 RET 
DlinaStroki ENDP
END