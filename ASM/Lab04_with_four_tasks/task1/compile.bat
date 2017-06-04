@echo on
MASM.EXE /ZI lr05-1-1.asm,,,;
MASM.EXE /ZI lr05-1-2.asm,,,;
link.exe /CO lr05-1-1.OBJ lr05-1-2.OBJ,,,;
CV.EXE LR05-1-1.EXE
