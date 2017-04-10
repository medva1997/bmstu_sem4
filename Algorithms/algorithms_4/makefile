FLAGS := -std=c99  -ggdb -Wall -Werror -pedantic
main: main.o
	gcc -o $@ $^
	./main
.o: %.c 
	gcc  $(FLAGS) -c $<

clean:
	rm *.o main 
