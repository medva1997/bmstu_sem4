EXTRN factorial: near

STK SEGMENT PARA STACK 'STACK'
	db 100 dup(0)
STK ENDS

DSEG SEGMENT PARA PUBLIC 'DATA'
	X db 5
	F db ?

DSEG ENDS

CSEG SEGMENT PARA PUBLIC 'CODE'
	assume CS:CSEG, DS:DSEG, SS:STK
main:
	mov ax, DSEG
	mov ds, ax

	mov ax,X
	mov bx, offset F

	push bx

	push ax

	call factorial	

	add sp, 8

	;push 0

	mov ax, 4c00h
	int 21h
CSEG ENDS

END main
