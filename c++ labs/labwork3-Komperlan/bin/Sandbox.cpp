#include "../src/Sanbox.h"
#include "../src/bmp.h"
#include <cstdint>
#include "iostream"
#include <cstring>

void Delete_field(uint64_t** field, uint64_t height){
    for(unsigned long row = 0; row <= height; ++row){
        std::free(field[row]);
    }

    std::free(field);
}

void Show_error_and_exit(const char* error_message) {
    std::cerr << error_message;
    exit(EXIT_FAILURE);
}


uint64_t** Create_heap(const uint64_t x_min, const uint64_t x_max, const uint64_t y_min, const uint64_t y_max){
    uint64_t** field;

    if(! (field = (static_cast <uint64_t**> (malloc(sizeof(uint64_t*) * (y_max - y_min + 1)))))){
        Show_error_and_exit("destroing malloc");
    }

    for(uint16_t i = 0; i <= (y_max - y_min); ++i){
        if(! (field[i] = static_cast <uint64_t*> (malloc(sizeof (uint64_t) * (x_max - x_min + 1))))){
            Show_error_and_exit("destroing malloc");
        }
    }
    for(uint16_t i = 0; i <= (y_max - y_min); ++i){
        std::memset(field[i], 0, (x_max - x_min + 1) * sizeof (uint64_t));
    }

    return field;
}

uint64_t** Avalanche(uint64_t** field, uint64_t* y_max, uint64_t* x_max){
    const int8_t sand_dropped = 4;

    uint64_t x_min = 0;
    uint64_t y_min = 0;

    uint64_t old_y_max;
    uint64_t old_x_max;
    uint64_t old_y_min;
    uint64_t old_x_min;

    bool need_expend = false;

    uint64_t** avalanche_field = Create_heap(x_min, *x_max, y_min, *y_max);

    for (uint64_t y = 0; y <= *y_max - y_min; ++y) {
        for (uint64_t x = 0; x <= *x_max - x_min; ++x) {

            int left = 0;
            int down = 0;
            if (field[y][x] < sand_dropped) {
                continue;
            }
            field[y][x] -= sand_dropped;
            old_y_max = *y_max;
            old_x_max = *x_max;
            old_y_min = y_min;
            old_x_min = x_min;

            if (y + 1 > *y_max) {
                *y_max = y + 1;
                need_expend = true;
            }
            if (y == 0) {
                y_min = 0;
                (*y_max)++;
                need_expend = true;
                down++;
            }
            if (x + 1 > *x_max) {
                *x_max = x + 1;
                need_expend = true;
            }
            if (x == 0) {
                x_min = 0;
                *x_max += 1;
                need_expend = true;
                left++;
            }

            if (need_expend) {
                uint64_t **sup_field = Create_heap(x_min, *x_max, y_min, *y_max);
                uint64_t **sup_avelanche_field = Create_heap(x_min, *x_max, y_min, *y_max);

                for (int x_sup = 0; x_sup <= old_x_max - old_x_min; ++x_sup) {
                    for (int y_sup = 0; y_sup <= old_y_max - old_y_min; ++y_sup) {
                        sup_field[y_sup + down][x_sup + left] = field[y_sup][x_sup];
                        sup_avelanche_field[y_sup + down][x_sup + left] = avalanche_field[y_sup][x_sup];
                    }
                }

                Delete_field(avalanche_field, old_y_max - old_y_min);
                Delete_field(field, old_y_max - old_y_min);
                field = sup_field;
                avalanche_field = sup_avelanche_field;
            }

            avalanche_field[y + down - 1][x + left]++;
            avalanche_field[y + down + 1][x + left]++;
            avalanche_field[y + down][x + left - 1]++;
            avalanche_field[y + down][x + left + 1]++;
        }
    }
    for(int y_av = 0; y_av <= *y_max - y_min; ++y_av){
        for(int x_av = 0; x_av <= *x_max - x_min; ++x_av){
            field[y_av][x_av] += avalanche_field[y_av][x_av];
        }
    }

    //`std::cout<<*x_max<<" "<<*x_min<<" "<<*y_max<<" "<<*y_min<<'\n';

    Delete_field(avalanche_field, old_y_max - old_y_min);

    return field;
}