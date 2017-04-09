//
// Created by alexey on 01.04.17.
//

#include "BaseForList.h"

BaseForList::BaseForList() : CurrentSize(0), MaxSize(0) {}

size_t BaseForList::getCurrentSize() const {
    return CurrentSize;
}

size_t BaseForList::getMaxSize() const {
    return MaxSize;
}

bool BaseForList::isEmpty() const {
    return (CurrentSize == 0);
}