#include <gtest/gtest.h>
#include "library/MyRanges.h"
#include "library/wrapper.h"
#include "deque"

using namespace MyRange;

TEST(AdapterTestSuite, filter_test1) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::deque<int> d = {1,2,3,4,5,6};
    std::set<int> s = {1,2,3,4,5,6};

    std::stringstream output_v;
    std::stringstream output_d;
    std::stringstream output_s;

    for(auto filtred_v : v | filter([](int i){return i % 2 == 1;})){
        output_v<<filtred_v<<" ";
    }

    for(auto filtred_v : d | filter([](int i){return i % 2 == 1;})){
        output_d<<filtred_v<<" ";
    }

    for(auto filtred_v : d | filter([](int i){return i % 2 == 1;})){
        output_s<<filtred_v<<" ";
    }

    ASSERT_EQ(output_v.str(), "1 3 5 ");
    ASSERT_EQ(output_d.str(), "1 3 5 ");
    ASSERT_EQ(output_s.str(), "1 3 5 ");
}

TEST(AdapterTestSuite, filter_test2) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::deque<int> d = {1,2,3,4,5,6};
    std::set<int> s = {1,2,3,4,5,6};

    std::stringstream output_v;
    std::stringstream output_d;
    std::stringstream output_s;

    for(auto filtred_v : v | filter([](int i){return ((i * i)  < 10);})){
        output_v<<filtred_v<<" ";
    }

    for(auto filtred_v : d | filter([](int i){return ((i * i)  < 10);})){
        output_d<<filtred_v<<" ";
    }

    for(auto filtred_v : d | filter([](int i){return ((i * i)  < 10);})){
        output_s<<filtred_v<<" ";
    }

    ASSERT_EQ(output_v.str(), "1 2 3 ");
    ASSERT_EQ(output_d.str(), "1 2 3 ");
    ASSERT_EQ(output_s.str(), "1 2 3 ");
}

TEST(AdapterTestSuite, transform_test1) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | transform([](int i){return (i * i);})){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "1 4 9 16 25 36 ");
}

TEST(AdapterTestSuite, transform_test2) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | transform([](int i){return (i + i);})){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "2 4 6 8 10 12 ");
}

TEST(AdapterTestSuite, transform_test3) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | transform([](int i){return (i + 1);})){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "2 3 4 5 6 7 ");
}

TEST(AdapterTestSuite, take_test) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | take(2)){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "1 2 ");
}


TEST(AdapterTestSuite, drop_test) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | MyRange::drop(2)){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "3 4 5 6 ");
}

TEST(AdapterTestSuite, drop_take_test) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | MyRange::drop(1) | MyRange::take(3)){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "2 3 ");
}

TEST(AdapterTestSuite, drop_take_test2) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::stringstream output;

    for(auto filtred_v : v | MyRange::drop(1) | MyRange::take(2)){
        output<<filtred_v<<" ";
    }

    ASSERT_EQ(output.str(), "2 ");
}

TEST(AdapterTestSuite, reverse_test) {
    std::vector<int> v = {1,2,3,4,5,6};
    std::deque<int> d = {1,2,3,4,5,6};

    std::stringstream output_v;
    std::stringstream output_d;

    for(auto filtred_v : v | reverse()){
        output_v<<filtred_v<<" ";
    }

    for(auto filtred_v : d | reverse()){
        output_d<<filtred_v<<" ";
    }


    ASSERT_EQ(output_v.str(), "6 5 4 3 2 1 ");
    ASSERT_EQ(output_d.str(), "6 5 4 3 2 1 ");
}

TEST(AdapterTestSuite, associative_key_test) {
    std::map<int, int> h = {{10, 1},{20, 2}, {30, 3}, {40, 4}, {50, 5}, {60, 6}};

    std::stringstream output_m;

    for(auto filtred_v : h | MyRange::values()){
        output_m<<filtred_v<<" ";
    }


    ASSERT_EQ(output_m.str(), "1 2 3 4 5 6 ");
}