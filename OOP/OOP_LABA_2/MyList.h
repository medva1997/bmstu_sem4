//
// Created by alexey on 01.04.17.
//

#ifndef OOP_LABA_2_MYLIST_H
#define OOP_LABA_2_MYLIST_H

#include "BaseForList.h"
#include <iostream>
#include "MException.h"
#include "Node.h"


namespace laba {


    template<typename T>
    class MyList : public laba_core::BaseForList {
    public:
        MyList() {

            head = nullptr;
            tail = nullptr;
        }

        const T& getHead() const;

        const T& getTail() const;


        explicit MyList(const MyList<T>& other);            // Конструктор копирования
        MyList(MyList<T>&& other);                 // Конструктор перемещения
        explicit MyList(size_t n, ...);            // Задание перечислением
        MyList(T* array, int n);

        ~MyList();                              // Деструктор

        MyList& operator=(const MyList& other); // Копирование
        MyList& operator=(MyList&& other);      // Перемещение

        void pushBack(const T& elem);           // Добавляет новый последний элемент
        MyList& operator+=(const T& elem);      // Добавляет новый последний элемент
        MyList& operator<<(const T& elem);      // Добавляет новый последний элемент

        void pushFront(const T& elem);          //Добавляет новый первый элемент

        const T& popBack();                         //Удаляет последний элемент
        const T& popFront();                        //Удаляет первый элемент



        // итераторы
        list_iterator<T> begin();

        list_iterator<T> end();

        const_list_iterator<T> begin() const;

        const_list_iterator<T> end() const;

        T* ToArray(int* size);


        void merge(const MyList& other);        // Добавление списка (к исходному)
        MyList& operator+=(const MyList& other);// Добавление списка (к исходному)

        void remove(const T& elem);             // Удаление элемента
        MyList& operator-=(const T& elem);      // Удаление элемента

        bool Equal(const MyList& other);

        bool operator==(const MyList& other);   //Проверка равенства списков
        bool operator!=(const MyList& other);   //Проверка равенства списков

        void clear();                         //Очистка списка


        template<typename VT>
        friend std::ostream& operator<<(std::ostream& os, const MyList<VT>& list);

        template<typename VT>
        friend std::istream& operator>>(std::istream& os, MyList<VT>& list);

        void InsertAfter(const T& elem, list_iterator<T>& insert_after); //установка за указателем

        void remove(list_iterator<T>& insert_after); //удаление под указателем и смещение его вперед


        //friend class list_iterator;


    private:


        bool Compare(const MyList& other);    //Проверка равенства списков

        void Remove(const T& elem);             // Удаление элемента

        void AddLast(const T& elem);            // Добавляет новый последний элемент

        void RemoveLast();                      //Удаляет последний элемент

        void AddFirst(const T& elem);           //Добавляет новый первый элемент

        void RemoveFirst();                     //Удаляет первый элемент

        void Clear();                           //Очистка списка


        void InsertAfter(const T& elem, struct Node<T>* insert_after); //установка за указателем


        void Merge(const MyList& other);        //Добавление списка (к исходному)

    public:

    private:
        Node<T>* head;
        Node<T>* tail;
    };


    using std::ostream;

    template<typename VT>
    ostream& operator<<(std::ostream& os, const MyList<VT>& list) {
        const_list_iterator<VT> it1 = list.begin();
        for (; it1 != list.end(); ++it1) {
            os << *it1 << " ";
        }
        return os;
    }


    using std::istream;

    template<typename VT>
    istream& operator>>(std::istream& os, MyList<VT>& list) {
        VT elem;
        os >> elem;
        list.pushBack(elem);
        return os;
    }

}


#include "MyList_code.h"

#endif //OOP_LABA_2_MYLIST_H
