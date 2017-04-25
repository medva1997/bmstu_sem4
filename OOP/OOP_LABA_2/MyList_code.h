//
// Created by alexey on 01.04.17.
//

#include "MyList.h"
#include <cstdarg>

namespace laba {

    template<typename T>
    void MyList<T>::clear() {
        Clear();
    }

    template<typename T>
    bool MyList<T>::Compare(const MyList &other) {
        if (size() != other.size())
            return false;

        Node<T> *current = head;
        Node<T> *current2 = other.head;

        while (current) {

            if (current->getObject() != current2->getObject())
                return false;

            current = current->getNext();
            current2 = current2->getNext();
        }
        return true;
    }

    template<typename T>
    bool MyList<T>::operator==(const MyList &other) {
        return Compare(other);
    }

    template<typename T>
    bool MyList<T>::operator!=(const MyList &other) {
        return !Compare(other);
    }

// Удаление элемента
    template<typename T>
    void MyList<T>::remove(const T &elem) { Remove(elem); }

    template<typename T>
    MyList<T> &MyList<T>::operator-=(const T &elem) {
        Remove(elem);
    }

    template<typename T>
    void MyList<T>::Remove(const T &elem) {
        if (is_empty()) {
            return;
        }

        Node<T> *current = head;
        while ((current->getObject() != elem) && (current != tail)) {
            current = current->getNext();
        }
        if (current == head) {
            RemoveFirst();
            return;
        }
        if (current == tail) {
            if (current->getObject() == tail->getObject())
                RemoveLast();
            return;
        }

        current->getPrev()->setNext(current->getNext());
        current->getNext()->setPrev(current->getPrev()->getNext());
        delete current;
        element_count--;;

    }




    template<typename T>
    MyList<T>::MyList(size_t n, ...) {

        element_count=0;
        head = nullptr;
        tail = nullptr;
        if (n < 0) {
            //throw MExcInvalidSize();
        }

        va_list va;
        va_start(va, n);

        for (size_t i = 0; i < n; i++) {
            T el = va_arg(va, T);
            this->pushBack(el);
        }

        va_end(va);

    }

    template<typename T>
    MyList<T> &MyList<T>::operator=(const MyList &other)        // Копирование
    {
        this->clear();
        Merge(other);
    }

    template<typename T>
    MyList<T> &MyList<T>::operator=(MyList &&other)            // Перемещение
    {
        this->clear();

        this->head = other.head;
        this->tail = other.tail;
        this->element_count=other.size();

        other.element_count=0;
        other.head = nullptr;
        other.tail = nullptr;

    }

    template<typename T>
    MyList<T>::MyList(T* array, int n)
    {
        for (int i = 0; i < n; ++i) {
            pushBack(array[i]);
        }
    }

    template<typename T>
    T* MyList<T>::ToArray(int *s)
    {
        T* array=new T[size()];

        list_iterator<T> it1=this->begin();
        for (int i=0; it1.check();  ++it1,i++) {
            array[i]=*it1;
        }
        *s=size();
        return array;
    }

    template<typename T>
    MyList<T>::MyList(const MyList<T>& other) {
        this->clear();
        Merge(other);
    }

    template<typename T>
    MyList<T>::MyList(MyList &&other) {
        this->clear();
        Merge(other);
        other.clear();
    }

    template<typename T>
    MyList<T>::~MyList() {
        clear();
    }

//-------------------------------------------------------------------------------------------------
// Добавление списка (к исходному)
    template<typename T>
    void MyList<T>::merge(const MyList &other) {
        Merge(other);
    }

// Добавление списка (к исходному)
    template<typename T>
    MyList<T> &MyList<T>::operator+=(const MyList &other) {
        Merge(other);
    }

// Добавление списка (к исходному)
    template<typename T>
    void MyList<T>::Merge(const MyList &other) {
        Node<T> *head2 = other.head;
        Node<T> *tail2 = other.tail;
        while (head2 != tail2) {
            this->pushBack(head2->getObject());
            head2 = head2->getNext();
        }
        this->pushBack(head2->getObject());
    }
//-------------------------------------------------------------------------------------------------

// Добавляет новый последний элемент
    template<typename T>
    void MyList<T>::pushBack(const T &elem) {
        AddLast(elem);
    }

// Добавляет новый последний элемент
    template<typename T>
    MyList<T> &MyList<T>::operator+=(const T &elem) {
        AddLast(elem);
    }

    template<typename T>
    MyList<T> &MyList<T>::operator<<(const T &elem) {
        AddLast(elem);
    }

//Удаляет последний элемент
    template<typename T>
    const T& MyList<T>::popBack() {
        T temp = getTail();
        RemoveLast();
        return temp;
    }

//Удаляет первый элемент
    template<typename T>
    const T& MyList<T>::popFront() {
        T temp = getHead();
        RemoveFirst();
        return temp;
    }

//Добавляет новый первый элемент
    template<typename T>
    void MyList<T>::pushFront(const T &elem) {
        AddFirst(elem);
    }


//-------------------------------------------------------------------------------------------------
// Adding element to the end of list
    template<typename T>
    void MyList<T>::AddLast(const T &elem) {
        Node<T> *node = new Node<T>(elem);

        if (&node == nullptr)
            throw MExcMemoryAlloc();

        if (tail) {
            tail->setNext(node);
        }

        node->setNext(nullptr);
        node->setPrev(tail);
        tail = node;

        if (head == nullptr) {
            head = node;
        }

        element_count++;

    }

//-------------------------------------------------------------------------------------------------
// Removing element from the end of list
    template<typename T>
    void MyList<T>::RemoveLast() {
        if (head == nullptr) {
            return;
        }

        Node<T> *temp = tail->getPrev();

        if (head != tail) {
            temp->setNext(nullptr);
        }

        delete tail;

        if (head != tail) {
            tail = temp;
        } else {
            head = tail = nullptr;
        }

        element_count--;;

    }

//-------------------------------------------------------------------------------------------------
// Add element to the beginning of list
    template<typename T>
    void MyList<T>::AddFirst(const T &elem) {
        Node<T> *node = new Node<T>(elem);

        if (head == nullptr) {
            head->setPrev(node);
        }

        node->setPrev(nullptr);
        node->setNext(head);
        head = node;

        if (tail == nullptr) {
            tail = node;
        }

        element_count++;
    }

//-------------------------------------------------------------------------------------------------
// Remove element from the beginning of list
    template<typename T>
    void MyList<T>::RemoveFirst() {
        if (head == nullptr) {
            return;
        }

        Node<T> *temp = head->getNext();

        if (head != tail) {
            temp->setPrev(nullptr);
        }


        delete head;

        if (head != tail) {
            head = temp;
        } else {
            head = tail = nullptr;
        }

        element_count--;

    }


    template<typename T>
    void MyList<T>::InsertAfter(const T &elem, Node<T> *insert_after) {
        if (insert_after == tail) {
            AddLast(elem);

            return;
        }
        if (insert_after == nullptr) {
            AddFirst(elem);
            return;
        }

        Node<T> *node = new Node<T>(elem);


        node->setNext(insert_after->getNext());
        node->setPrev(insert_after);
        insert_after->setNext(node);
        node->getNext()->setPrev(node);
        element_count++;
    }

    template<typename T>
    void MyList<T>::InsertAfter(const T &elem, list_iterator<T>& after) {

        Node<T> *p_pos=after.getptr();

        InsertAfter(elem,p_pos);
    }

//-------------------------------------------------------------------------------------------------

    template<typename T>
    void MyList<T>::remove(list_iterator<T>& removeiter) //удаление под указателем и смещение его вперед
    {

        Node<T> *current = removeiter.getptr();
        if(current==tail)
            removeiter--;
        removeiter++;

        if(current!=tail)
            current->getNext()->setPrev(current->getPrev());
        else
            tail=  current->getPrev();

        if(current!=head)
            current->getPrev()->setNext(current->getNext());
        else
            head= current->getNext();
        delete(current);
        element_count--;
    }

//-------------------------------------------------------------------------------------------------
// Delete list
    template<typename T>
    void MyList<T>::Clear() {
        if (is_empty()) {
            return;
        }

        Node<T> *current = head;
        while (current) {
            Node<T> *next = current->getNext();
            //std::cout << current->object << std::endl;
            delete current;
            current = next;
        }
        head = tail = nullptr;
        element_count=0;
    }

//-------------------------------------------------------------------------------------------------

    template<typename T>
    const T& MyList<T>::getHead() const {
        if (head != nullptr)
            return head->getObject();
    }

    template<typename T>
    const T& MyList<T>::getTail() const {
        if (tail != nullptr)
            return tail->getObject();
    }




    template<typename T>
    list_iterator<T> MyList<T>::begin() {
        return list_iterator<T>(head);
    }

    template<typename T>
    list_iterator<T> MyList<T>::end() {
        return list_iterator<T>(tail);
    }

    template<typename T>
    const_list_iterator<T> MyList<T>::begin() const {
        return const_list_iterator<T>(head);
    }

    template<typename T>
    const_list_iterator<T>  MyList<T>::end() const {
        return const_list_iterator<T>(tail);
    }

    template<typename T>
    bool MyList<T>::Equal(const MyList &other)
    {
        return Compare(other);
    }
}
