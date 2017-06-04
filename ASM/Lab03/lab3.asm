TITLE         МЗЯ  МОДУЛЬ 1 ЛАБОРАТОРНАЯ РАБОТА 2 

     SSTACK     SEGMENT PARA STACK  'STACK'
                DB   64 DUP('СТЕК____')
     SSTACK     ENDS

     DSEG          SEGMENT  PARA PUBLIC 'DATA'
     X         DB      0,1,2,3,4,5,6,7
     B         DB      1B
     K         DB      ?
     DSEG          ENDS

SUBTTL         ОСНОВНАЯ ПРОГРАММА
PAGE
     CSEG      SEGMENT PARA PUBLIC 'CODE'
               ASSUME CS:CSEG,DS:DSEG,SS:SSTACK

     START     PROC FAR
               MOV  AX,DSEG
               MOV  DS,AX

     M1:       MOV   K,2
               MOV SI,0
               MOV CX,8
               MOV AL,B
     M2:       TEST X[SI],AL
               JNZ  M3
               DEC  K
               JZ   M4
     M3:       INC  SI
               LOOP M2
     M4:       ADD  SI,'0'
               MOV  AH,2
               MOV  DX,SI
     M5:       INT  21H
     M6:       MOV  AH,4CH
               MOV  AL,0
               INT 21H
     START     ENDP

     CSEG      ENDS
               END  START
