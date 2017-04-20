//
// Created by alexey on 18.04.17.
//

#ifndef OOP_LABA_2_NODE_H
#define OOP_LABA_2_NODE_H

#include "st.h"
namespace laba {
    template<typename T>
    class Node {
    private:
        T object;
        Node *next;
        Node *prev;

    public:
        Node(const T &elem)
                : object(elem), prev(nullptr), next(nullptr) {}

        ~Node() {
            ~object;
            prev = nullptr;
            next = nullptr;
        }

        T& getObject() const {
            return const_cast<T&>(object);
        }


        Node *getNext() const {
            return next;
        }

        void setNext(Node *next) {
            Node::next = next;
        }

        Node *getPrev() const {
            return prev;
        }

        void setPrev(Node *prev) {
            Node::prev = prev;
        }
    };
}

#endif //OOP_LABA_2_NODE_H
