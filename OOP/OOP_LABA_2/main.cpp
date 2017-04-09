#include <iostream>
#include "MyList.h"
#include "MyIterator.h"

int main() {


    MyList<int> list;
    MyList<int> list2;
    list.pushFront(10);
    list.pushBack(10);
    list.pushBack(11);
    list.pushBack(12);

    list2.pushFront(2);
    list2.pushBack(10);
    list2.pushBack(11);
    list2.pushBack(12);
    list2.pushFront(2);

    list+=3;
    list+=list2;

    std::cout << "Iterator: ";

    for (MyIterator<int> iter(list); iter.valid(); iter.next())
        std::cout << iter.value()<< " ";

    std::cout << std::endl;

    list.free_all();


    std::cout << "Hello, World!" << std::endl;

}