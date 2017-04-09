//
// Created by alexey on 01.04.17.
//

#ifndef OOP_LABA_2_MYLIST_H
#define OOP_LABA_2_MYLIST_H

#include "BaseForList.h"
#include <iostream>
#include "MException.h"
// List node
template<typename T>
struct Node {
    T object;
    struct Node *next = NULL;
    struct Node *prev = NULL;

    Node(const T &elem) {
        object = elem;
        prev = NULL;
        next = NULL;
    }
};


template<typename T>
class MyList {

private:

    int count = 0;
    Node<T> *head;
    Node<T> *tail;

public:
    MyList() {
        count = 0;
        head = NULL;
        tail = NULL;
    }

    MyList(const MyList &other);    // Конструктор копирования
    MyList(MyList &&other);        // Конструктор перемещения
    //MyList(size_t n, ...);		// Задание множества перечислением
    ~MyList();                        // Деструктор

    MyList(size_t n, ...);            // Задание  перечислением
    MyList &operator=(const MyList &other);        // Копирование
    MyList &operator=(MyList &&other);            // Перемещение

    // Добавляет новый последний элемент
    void pushBack(const T &elem);

    MyList &operator+=(const T &elem);

    //Добавляет новый первый элемент
    void pushFront(const T &elem);

    //Удаляет последний элемент
    bool popBack();

    //Удаляет первый элемент
    bool popFront();

    // Добавление списка (к исходному)
    void merge(const MyList &other);

    MyList &operator+=(const MyList &other);

    // Удаление элемента
    void remove(const T& elem);
    MyList& operator -= (const T& elem);

private:

    void Remove(const T& elem);
    void AddLast(const T &elem);

    void RemoveLast();

    void AddFirst(const T &elem);

    void RemoveFirst();

    bool IsEmpty();

    void Clear();

    int Length();

    void InsertAt(const T &elem, int index);

    void RemoveAt(int index);

    void InsertAfter(const T &elem, struct Node<T> *insert_after);

    void Merge(const MyList &other);

public:
    int getCount() const;

    Node<T> *getHead() const;

    Node<T> *getTail() const;

public:
    void free_all() {
        Clear();
    }
};

// Удаление элемента
template<typename T>
void MyList<T>::remove(const T& elem){ Remove(elem); }
template<typename T>
MyList<T>& MyList<T>::operator -= (const T& elem)
{
    Remove(elem);
}

template<typename T>
void MyList<T>::Remove(const T& elem)
{
    if (IsEmpty()) {
        return;
    }

    Node<T> *current = head;
    while ((current->object!=elem)&&(current!=tail)) {
        current=current->next;
    }
    if(current==head)
    {
        RemoveFirst();
        return;
    }
    if(current==tail)
    {
        if(current->object==tail->object)
        RemoveLast();
        return;
    }

    current->prev->next=current->next;
    current->next->prev=current->prev->next;
    delete current;
    --count;

}

template<typename T>
MyList<T>::MyList(size_t n, ...) {

    /*if (n < 0) {
        //throw MExcInvalidSize();
    }

    va_list va;
    va_start(va, n);

    for (size_t i = 0; i < n; i++) {
        T el = va_arg(va, T);
        this->pushBack(el);
    }

    va_end(va);*/

}

template<typename T>
MyList<T> &MyList<T>::operator=(const MyList &other)        // Копирование
{
    this->free_all();
    Merge(other);
}

template<typename T>
MyList<T> &MyList<T>::operator=(MyList &&other)            // Перемещение
{
    this->free_all();
    Merge(other);
    other.free_all();
}

template<typename T>
MyList<T>::MyList(const MyList &other) {
    this->free_all();
    Merge(other);
}

template<typename T>
MyList<T>::MyList(MyList &&other) {
    this->free_all();
    Merge(other);
    other.free_all();
}

template<typename T>
MyList<T>::~MyList() {
    free_all();
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
    Node<T> *head2 = other.getHead();
    Node<T> *tail2 = other.getTail();
    while (head2 != tail2) {
        this->pushBack(head2->object);
        head2 = head2->next;
    }
    this->pushBack(head2->object);
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

//Удаляет последний элемент
template<typename T>
bool MyList<T>::popBack() {
    RemoveLast();
}

//Удаляет первый элемент
template<typename T>
bool MyList<T>::popFront() {
    RemoveFirst();
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

    if(!node)
        throw  MExcMemoryAlloc();

    if (tail) {
        tail->next = node;
    }

    node->next = NULL;
    node->prev = tail;
    tail = node;

    if (head == NULL) {
        head = node;
    }

    ++count;
}

//-------------------------------------------------------------------------------------------------
// Removing element from the end of list
template<typename T>
void MyList<T>::RemoveLast() {
    if (head == NULL) {
        return;
    }

    Node<T> *temp = tail->prev;

    if (head != tail) {
        temp->next = NULL;
    }

    free(tail->object);
    free(tail);

    if (head != tail) {
        tail = temp;
    } else {
        head = tail = NULL;
    }

    --count;
}

//-------------------------------------------------------------------------------------------------
// Add element to the beginning of list
template<typename T>
void MyList<T>::AddFirst(const T &elem) {
    Node<T> *node = new Node<T>(elem);

    if (head) {
        head->prev = node;
    }

    node->prev = NULL;
    node->next = head;
    head = node;

    if (tail == NULL) {
        tail = node;
    }

    ++count;
}

//-------------------------------------------------------------------------------------------------
// Remove element from the beginning of list
template<typename T>
void MyList<T>::RemoveFirst() {
    if (head == NULL) {
        return;
    }

    Node<T> *temp = head->next;

    if (head != tail) {
        temp->prev = NULL;
    }

    free(head->object);
    free(head);

    if (head != tail) {
        head = temp;
    } else {
        head = tail = NULL;
    }

    --count;
}

//-------------------------------------------------------------------------------------------------
// Insert element into list by index
template<typename T>
void MyList<T>::InsertAt(const T &elem, int index) {
    if (index < 0 || index > count) {
        return;
    }

    if (index == 0) {
        AddFirst(elem);
        return;
    }

    if (index == count) {
        AddLast(elem);
        return;
    }

    struct Node<T> *current = head;
    for (int i = 0; i < index; ++i) {
        current = current->next;
    }

    Node<T> *node = new Node<T>(elem);

    node->next = current;
    node->prev = current->prev;
    current->prev->next = node;
    current->prev = node;
}

template<typename T>
void MyList<T>::InsertAfter(const T &elem, Node<T> *insert_after) {
    if (insert_after == tail) {
        AddLast(elem);
        return;
    }
    if (insert_after == NULL) {
        AddFirst(elem);
        return;
    }

    Node<T> *node = new Node<T>(elem);


    node->next = insert_after->next;
    node->prev = insert_after;
    insert_after->next = node;
    node->next->prev = node;


}

//-------------------------------------------------------------------------------------------------
// Remove element from list by index
template<typename T>
void MyList<T>::RemoveAt(int index) {
    if (index < 0 || index > count - 1) {
        return;
    }

    if (index == 0) {
        RemoveFirst();
        return;
    }

    if (index == count - 1) {
        RemoveLast();
        return;
    }

    struct Node<T> *current = head;
    for (int i = 0; i < index; ++i) {
        current = current->next;
    }

    current->next->prev = current->prev;
    current->prev->next = current->next;

    free(current->object);
    free(current);
}

//-------------------------------------------------------------------------------------------------
// Delete list
template<typename T>
void MyList<T>::Clear() {
    if (IsEmpty()) {
        return;
    }

    Node<T> *current = head;
    while (current) {
        Node<T> *next = current->next;
        //std::cout << current->object << std::endl;
        delete current;
        current = next;
    }
    head = tail = NULL;
    count = 0;
}

//-------------------------------------------------------------------------------------------------
template<typename T>
int MyList<T>::Length() {
    return count;
}

template<typename T>
bool MyList<T>::IsEmpty() {
    return count == 0;
}

template<typename T>
int MyList<T>::getCount() const {
    return count;
}

template<typename T>
Node<T> *MyList<T>::getHead() const {
    return head;
}

template<typename T>
Node<T> *MyList<T>::getTail() const {
    return tail;
}


#endif //OOP_LABA_2_MYLIST_H
