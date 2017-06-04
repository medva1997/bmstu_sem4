@echo on
MASM.EXE /ZI lr05-4-1.asm,,,;
MASM.EXE /ZI lr05-4-2.asm,,,;
link.exe /CO lr05-4-1.OBJ lr05-4-2.OBJ,,,;
CV.EXE LR05-4-1.EXE
