#pragma once
#include <cinttypes>
#include <iostream>
#include <string>

struct int2023_t {
    // implement
    static const short base = 253;// это баааза это основа так сказать база
    static const short size = 256;

    uint8_t number[base];

    int2023_t(){
        for(int i = 0; i < base; i++){
            number[i] = 0;
        }
    }

    int2023_t(const int2023_t& num){
        for(int i = 0; i < base; i++){
            number[i] = num.number[i];
        }
    }

    int2023_t& operator=(const int2023_t& num){
        for(int i = 0; i < base; i++){
            number[i] = num.number[i];
        }
        return *this;
    }
};

static_assert(sizeof(int2023_t) <= 253, "Size of int2023_t must be no higher than 253 bytes");

int2023_t from_int(int32_t i);

int2023_t from_string(std::string string_main);

int2023_t operator+(const int2023_t& lhs, const int2023_t& rhs);

int2023_t operator-(const int2023_t& lhs, const int2023_t& rhs);

int2023_t operator*(const int2023_t& lhs, const int2023_t& rhs);

int2023_t operator/(const int2023_t& lhs, const int2023_t& rhs);

bool operator==(const int2023_t& lhs, const int2023_t& rhs);

bool operator!=(const int2023_t& lhs, const int2023_t& rhs);

std::ostream& operator<<(std::ostream& stream, const int2023_t& value);