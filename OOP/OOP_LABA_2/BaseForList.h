//
// Created by alexey on 01.04.17.
//

#ifndef OOP_LABA_2_BASEFORLIST_H
#define OOP_LABA_2_BASEFORLIST_H


#include <cstddef>
#include "st.h"

namespace laba {
    namespace laba_core {
        class BaseForList {

        public:
            explicit BaseForList();

            virtual ~BaseForList();

            bool is_empty() const;

            size_t size() const;

        protected:
            size_t element_count;
        };
    }
}


#endif //OOP_LABA_2_BASEFORLIST_H
