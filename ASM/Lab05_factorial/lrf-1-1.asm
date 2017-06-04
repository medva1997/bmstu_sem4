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
	
factorial proc near
	push bp
	mov  bp, sp
	mov ax ,[bp+4] ; get N
	
	ret
	
factorial endp	

	
main:
	mov ax, DSEG
	mov ds, ax
	mov ax,X
	push ax
	
	call factorial	

	add sp, 4
	
	mov ax, 4ch
	int 21h
CSEG ENDS

END main
