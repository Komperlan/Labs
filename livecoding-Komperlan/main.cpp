/*
    Использовать исключительно для отладки. Комитить изменения этого файла запрещено!
*/

#include "task_1.h"
#include "task_2.h"
#include "iostream"
#include "vector"
#include "list"

int main(int, char**){
    std::list l = {1, 2, 3, 4, 5};
    std::vector v = {'a', 'b', 'c', 'd'};

     auto arr = pythonzip (l, v);

     auto s = MyIter(l.begin(), v.begin());

     //std::cout<<(*s).first;

    for(auto value : pythonzip(l, v)) {
        std::cout << value.first << " " << value.second << std::endl;
    }
}
