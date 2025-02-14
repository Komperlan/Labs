#include "iostream"
#include "library/TTaskSheduler.h"
#include "functional"
#include "map"
#include "math.h"

int main() {
    TaskSheduler scheduler;

    float a = 1;
    float b = -2;
    float c = 3;

    auto id1 = scheduler.add([](float a, float c){return -4 * a * c;}, a, c);

    scheduler.executeALL();

    std::cout << "x1 = " << scheduler.getResult<int>(id1) << std::endl;
}