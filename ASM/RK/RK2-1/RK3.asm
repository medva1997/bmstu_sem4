SSTACK	Segment Para Stack 'Stack'
		DB 64 dup(?)
SSTACK	ENDS

DSEG	Segment Para Public 'Data'
	MasA DB 0,2,3,4,5
	MasB DB 7,2,1,8,5
	MasC DB 5 dup(' ')
		 DB 10,13,'$'
	N DW 0	
Dseg	ENDS

CSEG	Segment Para Public 'Code'
	Assume CS:CSEG,DS:DSEG,SS:SSTACK
	
	compare proc near
	
		mov si,0
		bigloop: 
			mov di,0
			mov AL, MasA[si]
		smallloop:
			cmp MasB[DI], AL
			JNE neravn
			JE raven
			raven: 
				MOV BX,SI
				MOV SI, N
				ADD AX,48
				MOV MasC[SI],AL
				INC SI
				MOV N,SI
				MOV SI,BX
			neravn:	
				INC DI
				CMP DI, 5
				JNE smallloop
				INC SI
				CMP SI, 5
				JNE bigloop	
		
		
		ret
	compare  endp
	


	
BEGIN:
		MOV AX,DSEG
	    MOV DS, AX
	    	    
	    call  compare
		
		
		mov SI,0		
		MOV DX, OFFSET MasC
		MOV AH, 9
		INT 21h
		
		MOV AX, N
		ADD AX,48
		MOV N, AX
		
		MOV DX, N
		MOV AH, 2
		INT 21h
		
		
		MOV AH,4ch
		INT 21h
CSEG	ENDS
	END BEGIN