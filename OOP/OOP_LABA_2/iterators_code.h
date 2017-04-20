//
// Created by alexey on 17.04.17.
//

#ifndef OOP_LABA_2_ITERATORS_CODE_H_H
#define OOP_LABA_2_ITERATORS_CODE_H_H

#include "Iterators.h"
#include "base_iterator.h"

namespace laba {

    template<typename ValueType>
    const ValueType& const_list_iterator<ValueType>::operator*() const {
        return this->ptr->getObject();
    }

    template<typename ValueType>
    const ValueType& const_list_iterator<ValueType>::get() const {
        return this->ptr->getObject();
    }


    template<typename ValueType>
    const ValueType* const_list_iterator<ValueType>::operator->() const {

        return this->ptr;
    }

    template<typename ValueType>
    const_list_iterator<ValueType>::const_list_iterator(const const_list_iterator& iter) {
        this->ptr = iter.ptr;
    }

    template<typename ValueType>
    const_list_iterator<ValueType>::const_list_iterator(Node <ValueType>* ptr) {
        this->ptr = ptr;
    }


    template<typename ValueType>
    ValueType& list_iterator<ValueType>::operator*() {
        return this->ptr->getObject();

    }

    template<typename ValueType>
    ValueType& list_iterator<ValueType>::get() {
        return this->ptr->getObject();

    }

    template<typename ValueType>
    ValueType* list_iterator<ValueType>::operator->() {
        return this->ptr->getObject();
    }

    template<typename ValueType>
    const ValueType& list_iterator<ValueType>::operator*() const {
        return *this->ptr->getObject();
    }

    template<typename ValueType>
    const ValueType& list_iterator<ValueType>::get() const {
        return *this->ptr->getObject();
    }

    template<typename ValueType>
    const ValueType* list_iterator<ValueType>::operator->() const {

        return this->ptr->getObject();

    }

    template<typename ValueType>
    list_iterator<ValueType>::list_iterator(const list_iterator& iter) { this->ptr = iter.ptr; }

    template<typename ValueType>
    list_iterator<ValueType>::list_iterator(Node <ValueType>* ptr) { this->ptr = ptr; }


}
#endif //OOP_LABA_2_ITERATORS_CODE_H_H
