#include "memory.h"

void ClearMemory(void *ptr) {
    if (ptr != NULL)
        free(ptr);
}

void *AllocMemory(int size_type, size_t count) {
    return malloc(count * size_type);
}
