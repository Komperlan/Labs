#pragma once
#include "vector"
#include "string"
#include "cstdint"
#include "array"
#include "fstream"

static uint64_t block_size;

static const int16_t name_size = 64;

struct file_header{
    char file_name[64];
    char file_size[8] ;// max size file = 12 exabytes
};

struct wrapper{
    int8_t* output_arr;
    int8_t file_size = 0;// max size file = 12 exabytes
};

uint64_t To_uint64(unsigned char* info);

wrapper Insert_control_bits(const uint8_t* informations, uint64_t count);

void Delete_files(std::string& filename, std::fstream& archiv);

void Append_files(std::string& file_name, std::fstream& file_arch);

void Fix_errors(uint8_t* information, int8_t* controls, const int8_t* old_controls, std::fstream& archiv);

void Remake_arch(std::fstream& arch_in, std::string& file_name, std::string archiv_name);

void Delete(std::string name);