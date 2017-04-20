//
// Created by alexey on 17.04.17.
//

#ifndef OOP_LABA_2_BASE_ITERATOR_H
#define OOP_LABA_2_BASE_ITERATOR_H

#include "st.h"
#include "Node.h"
#include "MException.h"

namespace laba {


        template<typename ValueType>
        class base_iterator {

        public:
            base_iterator(){};
            base_iterator(const base_iterator &);

            virtual ~base_iterator();

            base_iterator &operator=(const base_iterator &);

            base_iterator &operator++();

            base_iterator operator++(int);

            base_iterator &operator--();

            base_iterator operator--(int);

            bool operator==(const base_iterator &rhs);

            bool operator!=(const base_iterator &rhs);

            bool check();

            const ValueType & GetVaLue()const ;

        protected:
            Node<ValueType> *ptr;
            base_iterator(Node<ValueType> *);
        };

}
#include "base_iterator_code.h"
#endif

