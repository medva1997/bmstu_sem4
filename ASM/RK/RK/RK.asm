     SSTACK     SEGMENT PARA STACK  'STACK'
                DB   64 DUP('ярей____')
     SSTACK     ENDS

     DSEG          SEGMENT  PARA PUBLIC 'DATA'
	 m2		DB	  "CEGD"     
     DSEG          ENDS

	 SUBTTL         MAIN
PAGE
     CSEG      SEGMENT PARA PUBLIC 'CODE'
               ASSUME CS:CSEG,DS:DSEG,SS:SSTACK

     BEGIN     PROC FAR   
               PUSH  DS 
               MOV  AX,0
               PUSH  AX
			   MOV  AX,DSEG
               MOV  DS,AX

			   			  
			   mov  CX,4
			   mov  SI,0

			   mov  AL,m2[3]         
		M1:    xchg AL,m2[SI]
			   INC  SI
			   LOOP M1
				
    
               RET
     BEGIN     ENDP

     CSEG      ENDS
               END  BEGIN
