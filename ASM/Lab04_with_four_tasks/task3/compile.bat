@echo on
MASM.EXE /ZI lr05-3-1.asm,,,;
link.exe /CO lr05-3-1.OBJ,,,;
CV.EXE LR05-3-1.EXE
