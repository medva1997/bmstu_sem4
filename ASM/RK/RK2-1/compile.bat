@echo on
MASM.EXE /ZI RK3.asm,,,;
link.exe /CO RK3.OBJ,,,;
CV.EXE RK3.EXE