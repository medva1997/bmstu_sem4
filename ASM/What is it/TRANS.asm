TITLE         ÌÇß  ÌÎÄÓËÜ 1 ËÀÁÎÐÀÒÎÐÍÀß ÐÀÁÎÒÀ 2 

     SSTACK     SEGMENT PARA STACK  'STACK'
                DB   64 DUP('ÑÒÅÊ____')
     SSTACK     ENDS

     DSEG          SEGMENT  PARA PUBLIC 'DATA'
     MAS	   DB      'ABCDEFGH'
	 TEMP	   DB		0
     DSEG          ENDS

SUBTTL         ÎÑÍÎÂÍÀß ÏÐÎÃÐÀÌÌÀ
PAGE
     CSEG      SEGMENT PARA PUBLIC 'CODE'
               ASSUME CS:CSEG,DS:DSEG,SS:SSTACK

     START     PROC FAR
               MOV  AX,DSEG
               MOV  DS,AX

			   MOV DI,1				;j = 1
	
	LOOPST:	  
			mov AL,MAS[DI]		;key = A[j]
			mov TEMP,AL				;key = A[j]
			MOV SI,DI				;i = j-1
			DEC SI					;i = j-1

			SMALLLOOP:				;while
				CMP SI, 0			;i > 0
				JB ENDSMALLLOP	    ;i > 0
				MOV AL,MAS[SI]		;A[i] < key
				CMP AL,TEMP			;A[i] < key
				JA ENDSMALLLOP		;A[i] < key
				MOV MAS[SI+1],AL	;A[i + 1] = A[i]
				DEC SI				;i = i - 1
				CMP SI, 8 			;i > 0
				JA ENDSMALLLOP	    ;i > 0
				JMP SMALLLOOP 
			ENDSMALLLOP:
				MOV AL,TEMP			;A[i+1] = key
				MOV MAS[SI+1],AL	;A[i+1] = key
				
				INC DI
				CMP DI,8
				JB LOOPST 

     M6:       MOV  AH,4CH
               MOV  AL,0
               INT 21H
     START     ENDP

     CSEG      ENDS
               END  START