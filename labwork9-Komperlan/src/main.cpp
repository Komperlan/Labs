#include "ranges"
#include "vector"
#include "iostream"
#include "algorithm"
#include "library/MyRanges.h"
#include "map"
#include "tuple"
#include "set"

using namespace MyRange;


int main() {
    std::vector<int> v = {1,2,3,4,5,6};
    std::vector<int> g = {7,8,9,10,11,12};
    std::map<int, int> h = {{1, 4}, {8, 6}, {15, 9}};

    auto zazz = h | MyRange::values();

    auto s = zazz.begin();

    auto* p = &s;

    auto qw = *p;

    decltype(qw)::iterator_category;

    for(auto filtred_v : h | MyRange::values() | MyRange::filter([](int i){return i % 2 == 0;}) | MyRange::transform([](int i){return i*i;}) | MyRange::take(0)){
        std::cout<<filtred_v<<" ";
    }
}