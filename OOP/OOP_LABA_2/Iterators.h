//
// Created by alexey on 17.04.17.
//

#ifndef OOP_LABA_2_ITERATORS_H
#define OOP_LABA_2_ITERATORS_H

#include "st.h"
#include "MyList.h"
#include "base_iterator.h"

namespace laba {

    template<typename ValueType>
    class const_list_iterator : public base_iterator<ValueType> {
    public:
        const_list_iterator(const const_list_iterator&);

        const ValueType& operator*() const;

        const ValueType* operator->() const;

        const ValueType& get() const;

        friend class MyList<ValueType>;

    private:
        const_list_iterator(Node<ValueType>*);
    };
}


namespace laba {
    template<typename ValueType>
    class list_iterator : public base_iterator<ValueType> {
    public:
        list_iterator(const list_iterator&);

        ValueType& operator*();

        const ValueType& operator*() const;

        ValueType* operator->();

        const ValueType* operator->() const;

        ValueType& get();

        const ValueType& get() const;

        friend class MyList<ValueType>;


    private:
        Node<ValueType>* getptr() {
            return this->ptr;
        }

        list_iterator(Node<ValueType>*);
    };


}


#include "iterators_code.h"

#endif //OOP_LABA_2_ITERATORS_H
