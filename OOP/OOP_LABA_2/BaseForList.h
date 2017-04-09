//
// Created by alexey on 01.04.17.
//

#ifndef OOP_LABA_2_BASEFORLIST_H
#define OOP_LABA_2_BASEFORLIST_H


#include <cstddef>

class BaseForList {

public:
    BaseForList();

    bool isEmpty() const;

    size_t getCurrentSize() const;

    size_t getMaxSize() const;

private:
    size_t CurrentSize;

    size_t MaxSize;

    virtual void setMaxSize(size_t newSize) = 0;
};


#endif //OOP_LABA_2_BASEFORLIST_H
