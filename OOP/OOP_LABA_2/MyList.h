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
class MyList : public BaseForList{


    friend std::ostream& operator << (std::ostream& os, const  MyList& list)
    {
        Node<T> *current = list.head;
        for (size_t i = 0; i < list.getSize(); i++)
        {
            os << current->object << " ";
            current=current->next;
        }

        return os;
    }

private:
    Node<T> *head;
    Node<T> *tail;

public:
    MyList() {

        head = NULL;
        tail = NULL;
    }

    MyList(const MyList &other);            // Конструктор копирования
    MyList(MyList &&other);                 // Конструктор перемещения
    MyList(size_t n, ...);		            // Задание перечислением
    ~MyList();                              // Деструктор

    MyList &operator=(const MyList &other); // Копирование
    MyList &operator=(MyList &&other);      // Перемещение

    void pushBack(const T &elem);           // Добавляет новый последний элемент
    MyList &operator+=(const T &elem);      // Добавляет новый последний элемент
    MyList &operator<<(const T &elem);      // Добавляет новый последний элемент

    void pushFront(const T &elem);          //Добавляет новый первый элемент

    bool popBack();                         //Удаляет последний элемент

    bool popFront();                        //Удаляет первый элемент

    void merge(const MyList &other);        // Добавление списка (к исходному)
    MyList &operator+=(const MyList &other);// Добавление списка (к исходному)

    void remove(const T &elem);             // Удаление элемента
    MyList &operator-=(const T &elem);    // Удаление элемента

    bool operator==(const MyList &other);   //Проверка равенства списков
    bool operator!=(const MyList &other);   //Проверка равенства списков


private:


    bool Compare(const MyList &other);    //Проверка равенства списков

    void Remove(const T &elem);             // Удаление элемента

    void AddLast(const T &elem);            // Добавляет новый последний элемент

    void RemoveLast();                      //Удаляет последний элемент

    void AddFirst(const T &elem);           //Добавляет новый первый элемент

    void RemoveFirst();                     //Удаляет первый элемент

    bool IsEmpty();                         //Проверка на пустоту списка

    void Clear();                           //Очистка списка

    void InsertAt(const T &elem, int index);//Вставка по индексу

    void RemoveAt(int index);               //удаление по индексу

    void InsertAfter(const T &elem, struct Node<T> *insert_after); //установка за указателем

    void Merge(const MyList &other);        //Добавление списка (к исходному)

public:
    int getCount() const;                   //Свойство получения кол-во элементов

    Node<T> *getHead() const;               //Получить указатель на голову

    Node<T> *getTail() const;               //Получить указель на конец

    void free_all();                         //Очистка списка
};



#endif //OOP_LABA_2_MYLIST_H
