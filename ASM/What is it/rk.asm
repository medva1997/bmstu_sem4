     SSTACK     SEGMENT PARA STACK  'STACK'
                DB   64 DUP('ярей____')
     SSTACK     ENDS

     DSEG          SEGMENT  PARA PUBLIC 'DATA'
	 M1	       DB	3,6,5,4

     DSEG      ENDS

PAGE
     CSEG      SEGMENT PARA PUBLIC 'CODE'
               ASSUME CS:CSEG,DS:DSEG,SS:SSTACK

     BEGIN     PROC FAR
     
               PUSH  DS
               MOV  AX,0
               MOV	CX,3
               PUSH  AX

	       	   MOV  AX,DSEG
               MOV  DS,AX
               
               MOV  DI,0
			   MOV  BL,M1[DI]
			   
	FUNC:	   MOV AL, M1[DI+1]
			   XCHG	AL, M1[DI]
			   INC DI
			   LOOP FUNC
			   
			   ;MOV AL, M1[2]
			   ;XCHG	AL, M1[1]
			   
			   ;MOV AL, M1[3]
			   ;XCHG	AL, M1[2]
			   
			   XCHG BL, M1[3]

               RET
     BEGIN     ENDP

     CSEG      ENDS
               END  BEGIN
COMMENT @
