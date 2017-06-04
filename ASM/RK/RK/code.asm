     SSTACK     SEGMENT PARA STACK  'STACK'
                DB   64 DUP('ярей____')
     SSTACK     ENDS

     DSEG          SEGMENT  PARA PUBLIC 'DATA'
     m2		DB	"CEGD"
     DSEG          ENDS

PAGE
     CSEG      SEGMENT PARA PUBLIC 'CODE'
               ASSUME CS:CSEG,DS:DSEG,SS:SSTACK

     BEGIN     PROC FAR
               PUSH  DS ; 
               MOV  AX,0
               PUSH  AX

               MOV  AX,DSEG
               MOV  DS,AX

	       mov  CX,4

               mov  AL,m2[3]
               mov  SI,0

	L1:    xchg AL,m2[SI]
               INC  SI
	       LOOP L1
	       
               RET
     BEGIN     ENDP

     CSEG      ENDS
               END  BEGIN
