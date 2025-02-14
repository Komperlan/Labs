#include <gtest/gtest.h>
#include "library/BST.h"
#include "algorithm"

using namespace std_dlc;

TEST(BSTTestSuite, target_input_test) {
    BST<int> Tree;
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

    ASSERT_EQ(*Tree.begin(), 8);
}

TEST(BSTTestSuite, Pre_order_traverse_test) {
    BST<int> Tree;
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

    int* info = Tree.traverse(PreOrderIterator<int>());

    ASSERT_EQ(info[0], 8);
    ASSERT_EQ(info[1], 4);
    ASSERT_EQ(info[2], 2);
    ASSERT_EQ(info[3], 1);
    ASSERT_EQ(info[4], 3);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 5);
    ASSERT_EQ(info[7], 7);
    ASSERT_EQ(info[8], 20);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 13);
    ASSERT_EQ(info[11], 17);
    ASSERT_EQ(info[12], 25);
    ASSERT_EQ(info[13], 23);
    ASSERT_EQ(info[14], 22);
    ASSERT_EQ(info[15], 24);
    ASSERT_EQ(info[16], 30);
    ASSERT_EQ(info[17], 29);
    ASSERT_EQ(info[18], 31);
}

TEST(BSTTestSuite, Post_order_traverse_test) {
    BST<int> Tree;
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

    int* info = Tree.traverse(PostOrderIterator<int>());

    ASSERT_EQ(info[0], 1);
    ASSERT_EQ(info[1], 3);
    ASSERT_EQ(info[2], 2);
    ASSERT_EQ(info[3], 5);
    ASSERT_EQ(info[4], 7);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 4);
    ASSERT_EQ(info[7], 13);
    ASSERT_EQ(info[8], 17);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 22);
    ASSERT_EQ(info[11], 24);
    ASSERT_EQ(info[12], 23);
    ASSERT_EQ(info[13], 29);
    ASSERT_EQ(info[14], 31);
    ASSERT_EQ(info[15], 30);
    ASSERT_EQ(info[16], 25);
    ASSERT_EQ(info[17], 20);
    ASSERT_EQ(info[18], 8);
}

TEST(BSTTestSuite, In_order_traverse_test) {
    BST<int> Tree;
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

    int* info = Tree.traverse(InOrderIterator<int>());

    ASSERT_EQ(info[0], 1);
    ASSERT_EQ(info[1], 2);
    ASSERT_EQ(info[2], 3);
    ASSERT_EQ(info[3], 4);
    ASSERT_EQ(info[4], 5);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 7);
    ASSERT_EQ(info[7], 8);
    ASSERT_EQ(info[8], 13);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 17);
    ASSERT_EQ(info[11], 20);
    ASSERT_EQ(info[12], 22);
    ASSERT_EQ(info[13], 23);
    ASSERT_EQ(info[14], 24);
    ASSERT_EQ(info[15], 25);
    ASSERT_EQ(info[16], 29);
    ASSERT_EQ(info[17], 30);
    ASSERT_EQ(info[18], 31);
}

TEST(BSTTestSuite, In_order_traverse_test2) {
    BST<int> Tree;

    Tree.insert(1);
    Tree.insert(2);
    Tree.insert(3);
    Tree.insert(4);
    Tree.insert(7);
    Tree.insert(6);
    Tree.insert(5);
    Tree.insert(8);
    Tree.insert(9);
    Tree.insert(10);

    int* info = Tree.traverse(InOrderIterator<int>());

    ASSERT_EQ(info[0], 1);
    ASSERT_EQ(info[1], 2);
    ASSERT_EQ(info[2], 3);
    ASSERT_EQ(info[3], 4);
    ASSERT_EQ(info[4], 5);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 7);
    ASSERT_EQ(info[7], 8);
    ASSERT_EQ(info[8], 9);
    ASSERT_EQ(info[9], 10);
}

TEST(BSTTestSuite, Copy_constructor_test) {
    BST<int> Tree;
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

    BST<int> Tree2(Tree.begin(), Tree.end());


    int* info = Tree2.traverse(PreOrderIterator<int>());

    ASSERT_EQ(info[0], 8);
    ASSERT_EQ(info[1], 4);
    ASSERT_EQ(info[2], 2);
    ASSERT_EQ(info[3], 1);
    ASSERT_EQ(info[4], 3);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 5);
    ASSERT_EQ(info[7], 7);
    ASSERT_EQ(info[8], 20);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 13);
    ASSERT_EQ(info[11], 17);
    ASSERT_EQ(info[12], 25);
    ASSERT_EQ(info[13], 23);
    ASSERT_EQ(info[14], 22);
    ASSERT_EQ(info[15], 24);
    ASSERT_EQ(info[16], 30);
    ASSERT_EQ(info[17], 29);
    ASSERT_EQ(info[18], 31);
}

TEST(BSTTestSuite, contains_count_find_up_low_bound_test) {
    BST<int> Tree;
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

    ASSERT_EQ(*Tree.find(31), 31);
    ASSERT_EQ(Tree.count(1), 1);
    ASSERT_EQ(Tree.contains(1), 1);
    ASSERT_EQ(*Tree.lower_bound(22), 20);
    ASSERT_EQ(*Tree.upper_bound(22), 23);
}

TEST(BSTTestSuite, merge_test) {
    BST<int> Tree;
    Tree.insert(8);
    Tree.insert(4);
    Tree.insert(6);
    Tree.insert(5);
    Tree.insert(7);
    Tree.insert(2);
    Tree.insert(1);
    Tree.insert(3);

    BST<int> Tree2;

    Tree2.insert(20);
    Tree2.insert(25);
    Tree2.insert(15);
    Tree2.insert(13);
    Tree2.insert(17);
    Tree2.insert(30);
    Tree2.insert(29);
    Tree2.insert(23);
    Tree2.insert(24);
    Tree2.insert(22);
    Tree2.insert(31);

    Tree.merge(Tree2);

    int* info = Tree.traverse(PreOrderIterator<int>());

    ASSERT_EQ(info[0], 8);
    ASSERT_EQ(info[1], 4);
    ASSERT_EQ(info[2], 2);
    ASSERT_EQ(info[3], 1);
    ASSERT_EQ(info[4], 3);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 5);
    ASSERT_EQ(info[7], 7);
    ASSERT_EQ(info[8], 20);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 13);
    ASSERT_EQ(info[11], 17);
    ASSERT_EQ(info[12], 25);
    ASSERT_EQ(info[13], 23);
    ASSERT_EQ(info[14], 22);
    ASSERT_EQ(info[15], 24);
    ASSERT_EQ(info[16], 30);
    ASSERT_EQ(info[17], 29);
    ASSERT_EQ(info[18], 31);
}

TEST(BSTTestSuite, size_test) {
    BST<int> Tree;
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

    ASSERT_EQ(Tree.size(), 19);
}

TEST(BSTTestSuite, reverse_test) {
    BST<int> Tree;
    Tree.insert(8);
    Tree.insert(4);
    Tree.insert(6);
    Tree.insert(5);
    Tree.insert(7);
    Tree.insert(2);
    Tree.insert(1);
    Tree.insert(3);

    BST<int> Tree2;

    Tree2.insert(20);
    Tree2.insert(25);
    Tree2.insert(15);
    Tree2.insert(13);
    Tree2.insert(17);
    Tree2.insert(30);
    Tree2.insert(29);
    Tree2.insert(23);
    Tree2.insert(24);
    Tree2.insert(22);
    Tree2.insert(31);

    Tree.merge(Tree2);

    int* info = Tree.reverse_traverse();

    ASSERT_EQ(info[18], 8);
    ASSERT_EQ(info[17], 4);
    ASSERT_EQ(info[16], 2);
    ASSERT_EQ(info[15], 1);
    ASSERT_EQ(info[14], 3);
    ASSERT_EQ(info[13], 6);
    ASSERT_EQ(info[12], 5);
    ASSERT_EQ(info[11], 7);
    ASSERT_EQ(info[10], 20);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[8], 13);
    ASSERT_EQ(info[7], 17);
    ASSERT_EQ(info[6], 25);
    ASSERT_EQ(info[5], 23);
    ASSERT_EQ(info[4], 22);
    ASSERT_EQ(info[3], 24);
    ASSERT_EQ(info[2], 30);
    ASSERT_EQ(info[1], 29);
    ASSERT_EQ(info[0], 31);
}

TEST(BSTTestSuite, std_find_test) {
    BST<int> Tree;
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

    ASSERT_EQ(*(std::find(Tree.begin(), Tree.end(), 31)), 31);
}

TEST(BSTTestSuite, std_is_sorted_test) {
    BST<int> Tree;
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

    ASSERT_EQ(std::is_sorted(Tree.begin(), Tree.end()), false);
    ASSERT_EQ(std::is_sorted(Tree.begin(InOrderIterator<int>()), Tree.end(InOrderIterator<int>())), true);
}

TEST(BSTTestSuite, allocator_test) {
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

    int* info = Tree.traverse(PreOrderIterator<int>());

    ASSERT_EQ(info[0], 8);
    ASSERT_EQ(info[1], 4);
    ASSERT_EQ(info[2], 2);
    ASSERT_EQ(info[3], 1);
    ASSERT_EQ(info[4], 3);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 5);
    ASSERT_EQ(info[7], 7);
    ASSERT_EQ(info[8], 20);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 13);
    ASSERT_EQ(info[11], 17);
    ASSERT_EQ(info[12], 25);
    ASSERT_EQ(info[13], 23);
    ASSERT_EQ(info[14], 22);
    ASSERT_EQ(info[15], 24);
    ASSERT_EQ(info[16], 30);
    ASSERT_EQ(info[17], 29);
    ASSERT_EQ(info[18], 31);
}

TEST(BSTTestSuite, erase_test) {
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

    Tree.erase(20);

    int* info = Tree.traverse(InOrderIterator<int>());

    ASSERT_EQ(info[0], 1);
    ASSERT_EQ(info[1], 2);
    ASSERT_EQ(info[2], 3);
    ASSERT_EQ(info[3], 4);
    ASSERT_EQ(info[4], 5);
    ASSERT_EQ(info[5], 6);
    ASSERT_EQ(info[6], 7);
    ASSERT_EQ(info[7], 8);
    ASSERT_EQ(info[8], 13);
    ASSERT_EQ(info[9], 15);
    ASSERT_EQ(info[10], 17);
    ASSERT_EQ(info[11], 22);
    ASSERT_EQ(info[12], 23);
    ASSERT_EQ(info[13], 24);
    ASSERT_EQ(info[14], 25);
    ASSERT_EQ(info[15], 29);
    ASSERT_EQ(info[16], 30);
    ASSERT_EQ(info[17], 31);
}