//
// Created by alexey on 01.04.17.
//

#include "MyList.h"
template<typename T>
void MyList<T>::free_all() {
    Clear();
}

template<typename T>
bool  MyList<T>::Compare(const MyList &other)
{
    if(getSize()!=other.getSize())
        return  false;

    Node<T> *current = head;
    Node<T> *current2 = other.head;

    while (current) {

        if(current->object!=current2->object)
            return  false;

        current=current->next;
        current2=current2->next;
    }
    return  true;
}

template<typename T>
bool  MyList<T>::operator == (const  MyList& other)
{
    return Compare(other) ;
}

template<typename T>
bool  MyList<T>::operator != (const  MyList& other)
{
    return !Compare(other);
}

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
    decSize();

}


#include <cstdarg>
template<typename T>
MyList<T>::MyList(size_t n, ...) {

    setSize(0);
    head = NULL;
    tail = NULL;
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
    this->free_all();
    Merge(other);
}

template<typename T>
MyList<T> &MyList<T>::operator=(MyList &&other)            // Перемещение
{
    this->free_all();

    this->head=other.head;
    this->tail=other.tail;
    this->setSize(other.getSize());

    other.setSize(0);
    other.head=NULL;
    other.tail=NULL;

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

template<typename T>
MyList<T> &MyList<T>::operator<<(const T &elem) {
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

    incSize();
    
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

    delete tail;

    if (head != tail) {
        tail = temp;
    } else {
        head = tail = NULL;
    }

    decSize();
    
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

    incSize();
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


    delete(head);

    if (head != tail) {
        head = temp;
    } else {
        head = tail = NULL;
    }

    void decSize();
}

//-------------------------------------------------------------------------------------------------
// Insert element into list by index
template<typename T>
void MyList<T>::InsertAt(const T &elem, int index) {
    if (index < 0 || index > getSize()) {
        return;
    }

    if (index == 0) {
        AddFirst(elem);
        return;
    }

    if (index == getSize()) {
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
    if (index < 0 || index > getSize() - 1) {
        return;
    }

    if (index == 0) {
        RemoveFirst();
        return;
    }

    if (index == getSize() - 1) {
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
    setSize(0);
}

//-------------------------------------------------------------------------------------------------


template<typename T>
bool MyList<T>::IsEmpty() {
    return isEmpty();
}

template<typename T>
int MyList<T>::getCount() const {
    return getSize();
}

template<typename T>
Node<T> *MyList<T>::getHead() const {
    return head;
}

template<typename T>
Node<T> *MyList<T>::getTail() const {
    return tail;
}


