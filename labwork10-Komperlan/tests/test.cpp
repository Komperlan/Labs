#include <gtest/gtest.h>
#include "library/TTaskSheduler.h"
#include "math.h"

TEST(TaskTest, Test1) {
    TaskSheduler scheduler;

    float a = 1;
    float b = -2;

    auto id1 = scheduler.add([](float a, float c){return -4 * a * c;}, a, b);

    ASSERT_EQ(scheduler.getResult<float>(id1), 8);
}

TEST(TaskTest, LeftRightTest) {
    TaskSheduler scheduler;

    float a = 1;
    float b = -2;

    auto id1 = scheduler.add([](float a, float b){return -4 * a * b;}, a, b);
    auto id2 = scheduler.add([](float a, float b){return a * a + b;}, a, b);
    auto id3 = scheduler.add([](float a, float b){return a + b;}, scheduler.getFutureResult<float>(id1), scheduler.getFutureResult<float>(id2));

    ASSERT_EQ(scheduler.getResult<float>(id3), 7);
}

TEST(TaskTest, LeftTest) {
    TaskSheduler scheduler;

    float a = 1;
    float b = -2;

    auto id1 = scheduler.add([](float a, float b){return -4 * a * b;}, a, b);
    auto id3 = scheduler.add([](float a, float b){return a + b;}, scheduler.getFutureResult<float>(id1), 1);

    ASSERT_EQ(scheduler.getResult<float>(id3), 9);
}

TEST(TaskTest, SimpleTest) {
    TaskSheduler scheduler;

    float a = 3;

    auto id1 = scheduler.add([](float a){return a * a;}, a);

    ASSERT_EQ(scheduler.getResult<float>(id1), 9);
}

TEST(TaskTest, SimpleTestMult) {
    TaskSheduler scheduler;

    float a = 1;
    float b = -2;

    auto id1 = scheduler.add([](float a, float b){return -4 * a * b;}, a, b);
    auto id3 = scheduler.add([](float a){return a*a;}, scheduler.getFutureResult<float>(id1));

    ASSERT_EQ(scheduler.getResult<float>(id3), 64);
}

TEST(TaskTest, QuadroTest1) {
    TaskSheduler scheduler;

    float a = 1;
    float b = -2;
    float c = 3;

    auto id1 = scheduler.add([](float a, float c){return -4 * a * c;}, a, c);
    auto id2 = scheduler.add([](float b, float v){return b * b + v;}, b, scheduler.getFutureResult<float>(id1));
    auto id3 = scheduler.add([](float b, float d){return -b + std::sqrt(d);}, b, scheduler.getFutureResult<float>(id2));
    auto id4 = scheduler.add([](float b, float d){return -b - std::sqrt(d);}, b, scheduler.getFutureResult<float>(id2));
    auto id5 = scheduler.add([](float a, float v){return v/(2*a);}, a, scheduler.getFutureResult<float>(id3));
    auto id6 = scheduler.add([](float a, float v){return v/(2*a);}, a, scheduler.getFutureResult<float>(id4));

    ASSERT_EQ(scheduler.getResult<float>(id1), -12);
    ASSERT_EQ(scheduler.getResult<float>(id2), -8);

    std::stringstream left;
    std::stringstream right;
    std::string left_str;
    std::string right_str;

    left<<(scheduler.getResult<float>(id3));
    right<<std::sqrt(-1);
    left>>left_str;
    right>>right_str;
    ASSERT_EQ(left_str, right_str); // NAN
}

TEST(TaskTest, QuadroTest2) {
    TaskSheduler scheduler;

    float a = 5;
    float b = 3;
    float c = -26;

    auto id1 = scheduler.add([](float a, float c) { return -4 * a * c; }, a, c);

    auto id2 = scheduler.add([](float b, float v) { return b * b + v; }, b, scheduler.getFutureResult<float>(id1));

    auto id3 = scheduler.add([](float b, float d) { return -b + std::sqrt(d); }, b,
                             scheduler.getFutureResult<float>(id2));

    auto id4 = scheduler.add([](float b, float d) { return -b - std::sqrt(d); }, b,
                             scheduler.getFutureResult<float>(id2));

    auto id5 = scheduler.add([](float a, float v) { return v / (2 * a); }, a, scheduler.getFutureResult<float>(id3));

    auto id6 = scheduler.add([](float a, float v) { return v / (2 * a); }, a, scheduler.getFutureResult<float>(id4));

    ASSERT_EQ(scheduler.getResult<float>(id1), 520);
    ASSERT_EQ(scheduler.getResult<float>(id2), 529);
    ASSERT_EQ(scheduler.getResult<float>(id3), 20);
    ASSERT_EQ(scheduler.getResult<float>(id4), -26);
    ASSERT_EQ(scheduler.getResult<float>(id5), 2);
    ASSERT_EQ(scheduler.getResult<float>(id6), static_cast<float>(-2.6));
}