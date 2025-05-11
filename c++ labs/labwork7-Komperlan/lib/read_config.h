#include <fstream>
#include <string>
#include "list"
#include "iostream"

struct config {
    int Days;
    std::list<std::string> Cities;
};

config ReadConfig();