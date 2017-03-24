#ifndef MEMORY
#define MEMORY

#include <stdlib.h>

void ClearMemory(void *ptr);

void *AllocMemory(int size_type, size_t count);

#endif
