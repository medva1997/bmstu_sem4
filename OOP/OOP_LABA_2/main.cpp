#include <iostream>
#include "MyList.h"
#include "MyList.cpp"

#include "MyIterator.h"

int main() {


    MyList<int> list;
    MyList<int> list2(2,10000,20000);
    list=list2;

    std::cout<<list<<std::endl;
    if(list==list2)
        std::cout<<"true"<<std::endl;

    list2.pushFront(10);
    list2<<10;
    list2+=11;
    list2.popBack();
    list2.popFront();
    list.merge(list2);
    list+=list2;
    list.remove(10);
    list-=10;


    if(list!=list2)
        std::cout<<"true"<<std::endl;


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

    ///////////////////////////////////////////////////////////
    MyIterator<int> iter1(list), iter2(list2);
    bool eq = false;
    try
    {
        eq = iter1.equal(iter2);
    }
    catch(MExceptionBase& exc)
    {
        std::cout << "Error: " << exc.what() << std::endl;
    }
    /////////////////////////////////////////////////////////

    int val;
    --iter1;

    try
    {
        val = iter1.value();
    }
    catch (MExceptionBase& exc)
    {
        std::cout << "Error: " << exc.what() << std::endl;
    }


    std::cout << "Hello, World!" << std::endl;

}