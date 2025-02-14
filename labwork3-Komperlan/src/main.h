#pragma once
#include "string"
#include <cstdint>

struct Pointer{
    uint64_t x;
    uint64_t y;
    uint64_t num_sand;
};

struct Parsing_arg{
    int argc;
    char** argv;
    int* fps;
    std::string* filename_ind;
    std::string* direction;
    int* max_iteration;
};
