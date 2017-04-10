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

    int getSize() const;
    void incSize();
    void decSize();
    void setSize(int n);

private:
    int CurrentSize;
};

#endif //OOP_LABA_2_BASEFORLIST_H
