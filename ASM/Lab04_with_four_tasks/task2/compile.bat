@echo on
MASM.EXE /ZI lr05-2-1.asm,,,;
MASM.EXE /ZI lr05-2-2.asm,,,;
link.exe /CO lr05-2-1.OBJ lr05-2-2.OBJ,,,;
CV.EXE LR05-2-1.EXE
