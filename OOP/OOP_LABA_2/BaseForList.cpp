//
// Created by alexey on 01.04.17.
//

#include "BaseForList.h"

BaseForList::BaseForList() : CurrentSize(0){}

int BaseForList::getSize() const {
    return CurrentSize;
}

void BaseForList::decSize(){
    CurrentSize--;
}

void BaseForList::incSize(){
    CurrentSize++;
}

void BaseForList::setSize(int n){
    CurrentSize=n;
}


bool BaseForList::isEmpty() const {
    return (CurrentSize == 0);
}