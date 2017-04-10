//
// Created by alexey on 09.04.17.
//

#ifndef OOP_LABA_2_MYITERATOR_H
#define OOP_LABA_2_MYITERATOR_H

#include "MyList.h"
#include "MException.h"

template<typename T>
class MyIterator {

public:
    MyIterator(const MyList<T> &list);

    ~MyIterator();

    void UseFront();

    void UseBack();

    const T &value() const;

    const T &operator*() const;

    bool valid() const;

    bool next();

    bool operator++();

    bool prev();

    bool operator--();

    bool equal(const MyIterator &other) const;

    bool operator==(const MyIterator &other) const;

    bool operator!=(const MyIterator &other) const;

private:
    const MyList<T> *plist;
    const Node<T> *pnode;
    int cur;

};


#endif //OOP_LABA_2_MYITERATOR_H
