#include "fstream"
#include "iostream"
#include "Files.h"

void Give_info(std::fstream& arch){
    char shift[8];
    while(!arch.tellg()){
        char name[64];
        arch.read(name, 64);

        arch.read(shift, 8);

        uint64_t size = To_uint64(reinterpret_cast<unsigned char*>(shift));

        std::cout<<"Name = "<<name<<" Size = "<<size<<'\n';

        arch.seekg(static_cast<long long>(size / 2), std::ios_base::cur);
        arch.seekg(static_cast<long long>(size / 2), std::ios_base::cur);
        arch.seekg(static_cast<long long>(size % 2 + ((size / block_size + size % block_size > 0) * 2)), std::ios_base::cur);
    }
}