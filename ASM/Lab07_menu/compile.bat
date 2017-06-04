@echo on
MASM.EXE /ZI main.asm,,,;
MASM.EXE /ZI menu.asm,,,;
MASM.EXE /ZI input.asm,,,;

link.exe /CO MAIN.OBJ  MENU.OBJ  INPUT.OBJ  signed2.OBJ uns2.OBJ,,,;
MAIN.EXE