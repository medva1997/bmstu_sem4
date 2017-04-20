//
// Created by alexey on 17.04.17.
//

#ifndef OOP_LABA_2_BASE_ITERATOR_CODE_H
#define OOP_LABA_2_BASE_ITERATOR_CODE_H

#include "base_iterator.h"

namespace laba {

    template<typename ValueType>
    base_iterator<ValueType>::base_iterator(const base_iterator <ValueType>& iter) {
        this->ptr = iter.ptr;
    }

    template<typename ValueType>
    base_iterator<ValueType>::base_iterator(Node <ValueType>* ptr) {
        this->ptr = ptr;
    }

    template<typename ValueType>
    base_iterator<ValueType>::~base_iterator() {
        this->ptr = nullptr;
    }

    template<typename ValueType>
    base_iterator <ValueType>& base_iterator<ValueType>::operator=(const base_iterator <ValueType>& iter) {
        if (this != &iter) {
            this->ptr = iter.ptr;
        }
        return *this;
    }

    template<typename ValueType>
    base_iterator <ValueType>& base_iterator<ValueType>::operator++() {
        if (this->ptr != nullptr)
            this->ptr = this->ptr->getNext();
        return *this;
    }

    template<typename ValueType>
    base_iterator <ValueType> base_iterator<ValueType>::operator++(int) {
        base_iterator temp = *this;
        this->operator++();
        return temp;
    }


    template<typename ValueType>
    base_iterator <ValueType>& base_iterator<ValueType>::operator--() {
        if (this->ptr != nullptr)
            this->ptr = this->ptr->getPrev();

        return *this;
    }

    template<typename ValueType>
    base_iterator <ValueType> base_iterator<ValueType>::operator--(int) {
        base_iterator temp = *this;
        this->operator--();
        return temp;
    }


    template<typename ValueType>
    bool base_iterator<ValueType>::operator==(const base_iterator <ValueType>& rhs) {
        return this->ptr->getObject() == rhs.ptr->getObject();
    }

    template<typename ValueType>
    bool base_iterator<ValueType>::operator!=(const base_iterator <ValueType>& rhs) {
        return this->ptr->getObject() != rhs.ptr->getObject();
    }

    template<typename ValueType>
    bool base_iterator<ValueType>::check() {
        if (this->ptr != nullptr)
            return true;
        else
            return false;

    }

    template<typename ValueType>
    const ValueType& base_iterator<ValueType>::GetVaLue() const {
        return ptr->getObject();

    }

}
#endif //OOP_LABA_2_BASE_ITERATOR_CODE_H
