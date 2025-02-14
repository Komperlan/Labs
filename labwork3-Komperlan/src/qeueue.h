#include<iostream>
#include<string>
#include<vector>

class Queue{
private:
    struct Node {
        Pointer key;
        Node *next = nullptr;
        Node *prev = nullptr;
    };
    Node* Head;
    Node* Tail;
    int size = 0;

public:

    int Size(){
        return size;
    }

    Pointer back(){
        return Head->key;
    }

    void push_back(const Pointer arg){
        Node* node = new Node;
        if(size <= 0) {
            Head = node;
            Head->key = arg;
            Tail = Head;
        }else {
            node->key = arg;
            node->prev = Head;
            Head->next = node;

            Head = node;

        }

        size++;
    }


    void pop_back(){
        if(size == 1){
            delete Head;
            Head = Tail = nullptr;
            size--;
            return;
        }

        Head = Head->prev;
        delete Head->next;
        size--;
    }

};
