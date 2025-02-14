#include "library/BST.h"
#include "iostream"
#include "vector"
#include "list"
#include "algorithm"
#include "map"

int main(){
    using namespace std_dlc;
    BST<int, std::allocator<int>> Tree;

    Tree.insert(8);
    Tree.insert(4);
    Tree.insert(6);
    Tree.insert(5);
    Tree.insert(7);
    Tree.insert(2);
    Tree.insert(1);
    Tree.insert(3);
    Tree.insert(20);
    Tree.insert(25);
    Tree.insert(15);
    Tree.insert(13);
    Tree.insert(17);
    Tree.insert(30);
    Tree.insert(29);
    Tree.insert(23);
    Tree.insert(24);
    Tree.insert(22);
    Tree.insert(31);

    //Tree.traverse(InOrderIterator<int>());

    std::cout<<Tree.erase(20).second;

    int* info = Tree.traverse(InOrderIterator<int>());

    for(int x = 0; Tree.size()> x; ++x) {
        std::cout<<info[x]<<" ";
    }

    //std::cout<<Tree.size()<<" ";

    //int* w = Tree.traverse(InOrderIterator<int>());

    //std::cout<<w[0]<<w[1];
}