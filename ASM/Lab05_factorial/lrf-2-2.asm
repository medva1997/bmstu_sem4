PUBLIC factorial


CSEG SEGMENT PARA PUBLIC 'CODE'
	assume CS:CSEG
factorial proc near

	push bp
	mov bp, sp
	mov cx, [bp+4]
	mov ax, 1
M:
	mul cx
	loop M

	mov bx, word ptr[bp+6]
	mov word ptr[bx], ax

	pop bp
	ret
factorial endp
CSEG ENDS
END
