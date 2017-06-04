SSTACK	Segment Para Stack 'Stack'
		DB 64 dup(?)
SSTACK	ENDS

DSEG	Segment Para Public 'Data'
	Mas	DB 1,2,3,4,5
		DB 5,6,7,8,9
		DB 10,11,12,13,14
		DB 15,16,17,18,19
		DB 20,21,22,23,24
		DB 10,13,'$'
	MAX	DB  0
	INDEX DB 0
Dseg	ENDS

PAGE
CSEG	Segment Para Public 'Code'
	Assume CS:CSEG,DS:DSEG,SS:SSTACK

output:
		MOV CX, 0
		MOV BX, 0					
MYLOOP1:	 
			 MOV AX,CX
			 ADD AX,5
			 ADD AX,5
			 ADD AX,5
			 ADD AX,5
			 ADD AX,5
			 ADD AX,CX
			 MOV DI, AX

			 MOV AX,CX
			 ADD AX,5
			 ADD AX,5
			 ADD AX,5
			 ADD AX,5
			 ADD AX,5	
			 ADD AX,5		
			 MOV BX, AX

	MYLOOP0: MOV CL, Mas[DI]
			 CMP CL, MAX
			 JA SETMAX
			 JB SKIP
		SETMAX: MOV MAX,CL
		SKIP:   INC DI
			CMP DI,BX
			JB MYLOOP0
		INC CX
			
		CMP DI, 25
		JB MYLOOP1
		JE EXIT


	
	EXIT:	ret	


BEGIN:

		MOV AX,DSEG
		MOV DS, AX

		call output

		MOV DX, OFFSET Mas

		MOV AH, 9
		INT 21h

		MOV AH,4ch
		INT 21h
CSEG	ENDS
	END BEGIN