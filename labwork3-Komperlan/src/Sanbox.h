#pragma once
#include <cstdint>

uint64_t** Create_heap(const uint64_t x_min, const uint64_t x_max, const uint64_t y_min, const uint64_t y_max);

uint64_t** Avalanche(uint64_t** field, uint64_t* y_max, uint64_t* x_max);