#include <iostream>
#include "MException.h"
#include "Iterators.h"

using std::cin;
using std::cout;
using std::endl;
using namespace laba;

int main() {

    try {
        MyList<int> list;
        MyList<int> list2(2, 10000, 20000);
        list = list2;
        //cin>>list;

        cout << list << endl;
        if (list == list2)
            cout << "true" << endl;

        list2.pushFront(10);
        list2 << 10;
        list2 += 11;
        list2.popBack();
        list2.popFront();
        list.merge(list2);
        list += list2;
        list.remove(10);
        list -= 10;


        if (list != list2)
            cout << "true" << endl;


        list2.pushFront(2);
        list2.pushBack(10);
        cout << list << endl;
        cout << "Iterator: "<< endl;


        list_iterator<int> iter = list.begin();
        iter++;
        list.InsertAfter(27, iter);
        cout << "Hello, World!" << endl;

        iter = list.begin();
        for (; iter.check(); ++iter) {
            cout << *iter << " ";
        }

        iter = list.begin();
        iter++;
        list.InsertAfter(25,iter);
        iter=list.end();
        list.remove(iter);

        int *arr=list.ToArray();
        cout<<endl<<arr[0]<<" "<<arr[2]<<" hi "<<endl;

        ///////////////
        ++iter;
    }
    catch (MExceptionBase& exc)
    {
        cout << "Error: " << exc.what() << endl;
    }
    cout << "Hello, World!" << endl;

}