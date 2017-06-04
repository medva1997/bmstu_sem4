;Составить подпрограмму с именем Count типа
;      function (const S: LONGWORD; L:LONGWORD):INTEGER;
;возвращающую число единиц битовой строки S длины L.
.386
.model FLAT,PASCAL
PUBLIC COUNT

.CODE

COUNT:
	PUSH	EBP
	MOV		EBP,ESP

	PUSH	EBX

	
	MOV		EAX,0
	MOV		EBX,[EBP+12];строка(первые 32)
	MOV		ECX,[EBP+8] ;длина строки (и счетчик итераций) 	
	MOV		EDX,0
CIKL:	
	BT		[EBX],EDX	;заносит значение бита в СF
	JNC		DONOTINC	;если 0 - новая итерация цикла
	INC		EAX			;иначе eax++
	
DONOTINC:
	INC		EDX			;номер бита в строке
	CMP		EDX,31
	JA		CONTINUE	;пока идем по строке
	
	MOV		EDX,0
	ADD		EBX,4		;переход к хвосту строки (оставшимся 7 битам)
CONTINUE:
	LOOP	CIKL
	

	POP		EBX


	MOV		ESP,EBP
	POP		EBP
	
	RET 8
END