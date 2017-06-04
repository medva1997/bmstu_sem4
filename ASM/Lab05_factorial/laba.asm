.286
STK SEGMENT PARA STACK 'STACK'
	db 100 dup(0)
STK ENDS

DSEG SEGMENT PARA PUBLIC 'DATA'
	X db 5
	F db ?
DSEG ENDS

CSEG SEGMENT PARA PUBLIC 'CODE'
	assume CS:CSEG, DS:DSEG, SS:STK
	
factor proc near
	PUSH BP
	
	MOV  BP, sp
	MOV  DX,[BP+4] ; get N		
	DEC DX
	JNE M1
	MOV AX,1
	
	POP BP
	RET
M1:
	PUSH DX
	CALL factor
	ADD SP,2
	
	MOV DX,[BP+4]
	MUL DX	
	POP BP	
	RET	
factor endp	
	
main:
	MOV ax, DSEG
	MOV ds, ax
	
	push 3
	
	call factor

	add sp, 2
	
	
	
	MOV DX, AX
	ADD DX,48
	MOV AH,2
	int 21h
	
	MOV ax, 4ch
	int 21h
CSEG ENDS

END main
