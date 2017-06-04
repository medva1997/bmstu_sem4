@echo on
MASM.EXE /ZI laba.asm,,,;
link.exe /CO LABA.OBJ,,,;
CV.EXE LABA.EXE