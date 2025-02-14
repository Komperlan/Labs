#include "Files.h"
#include "cmath"
#include <iostream>
#include <cstdio>
#include "math.h"



std::string Get_filename(std::fstream& open_file);

void Check_integrity_file(uint8_t* information, const int8_t* controls, uint64_t size, std::fstream& arch);

uint64_t To_uint64(unsigned char* info);

void Delete_files(std::string& filename, std::fstream& archiv){

    archiv.seekg(0, std::ios_base::beg);
    archiv.seekp(0, std::ios_base::beg);

    std::string name;
    name = Get_filename(archiv);

    while(name != filename) {

        if(archiv.eof()){
            std::cerr<<"File is not found";
        }

        char* shift_sup;

        archiv.read(shift_sup, 8);

        unsigned long long shift = To_uint64(reinterpret_cast<uint8_t*>(shift_sup));

        archiv.seekg(static_cast<long long>(shift / 2), std::ios_base::cur);
        archiv.seekg(static_cast<long long>(shift / 2), std::ios_base::cur);
        archiv.seekg(static_cast<long long>(shift % 2), std::ios_base::cur);
        name = Get_filename(archiv);
    }


    std::ofstream file(name);

    char Byte;

    uint8_t sup[8];
    uint64_t size;

    archiv.read(reinterpret_cast<char*>(sup), 8);

    archiv.read(reinterpret_cast<char*>(sup), 8);

    size = To_uint64(sup);

    block_size = To_uint64(sup);

    int8_t control_size = static_cast<int8_t>(std::log2(block_size));

    uint8_t information[block_size];

    uint64_t size_count = 0;

    for(unsigned long long pos = 0; pos < (size / block_size + ((size % block_size) > 0)); ++pos){
        for(int i = 0; i < block_size; ++i) {
            if(size_count == size){
                break;
            }

            size_count++;
            archiv.read(&Byte, 1);
            information[i] = Byte;
        }

        char control[control_size];
        archiv.read(control, control_size);

        if(pos != (size / block_size + (size % block_size) & 1)){
            Check_integrity_file(information, reinterpret_cast<int8_t*>(control), block_size, archiv);
        }else{
            Check_integrity_file(information, reinterpret_cast<int8_t*>(control), static_cast<int16_t>(size % block_size), archiv);
        }

        file.write(reinterpret_cast<char*>(information), block_size);
    }
}

std::string Get_filename(std::fstream& open_file){
    char filename[name_size];
    open_file.read(filename, name_size);

    return filename;
}

void Check_integrity_file(uint8_t* information, const int8_t* controls, uint64_t size, std::fstream& arch){

    wrapper packet = Insert_control_bits(information, size);

    int8_t* sup = packet.output_arr;

    for(int i = 0; i < packet.file_size; ++i){
        if(controls[i / 8] >> (i % 8) != sup[i / 8] >> (i % 8)){
            Fix_errors(information, sup, controls, arch);
        }
    }

    delete packet.output_arr    ;
}

void Fix_errors(uint8_t* information, int8_t* control, const int8_t* old_controls, std::fstream& archiv){
    int count = 0;
    uint64_t pos = 0;
    for(int8_t i = 0; i < 16; ++i){
        if((old_controls[i / 8] >> (i % 8)) & 1 != (control[i / 8] >> (i % 8)) & 1){
            count++;
            pos += std::pow(2, i) - 1;
        }
    }

    if(count == 1){
        return;
    }else{
        information[pos / 8] ^= static_cast<uint8_t> (std::pow(2, pos % 8));
        archiv.seekg(-static_cast<int64_t>(block_size / 2), std::ios_base::cur);
        archiv.seekg(-static_cast<int64_t>(block_size / 2), std::ios_base::cur);
        archiv.write(reinterpret_cast<char*>(information), block_size);
    }
}

uint64_t To_uint64(unsigned char* info){
    int64_t output = 0;
    for(int i = 0; i < 8; ++i){
        output = (output << 8);
        output = (output | static_cast<int64_t>(info[i]));
    }

    return output;
}

void Remake_arch(std::fstream& arch_in, std::string& file_name, std::string archiv_name){
    std::fstream archiv_out(archiv_name + "1", std::fstream::in | std::fstream::out | std::fstream::app);

    arch_in.seekg(0, std::ios_base::beg);

    auto name_in_arch = Get_filename(arch_in);

    char Info[block_size];

    char Byte;

    char Number_uint64_t[8];

    while(file_name != name_in_arch){
        arch_in.read(Number_uint64_t, 8);

        uint64_t size = To_uint64(reinterpret_cast<unsigned char*>(Number_uint64_t));

        archiv_out.write(Number_uint64_t, 8);

        for(uint64_t i = 0; i < size; ++i){
            if(i % block_size == 0){
                for(int j = 0; j < 2; ++j){
                    arch_in.read(&Byte, 1);
                    archiv_out.write(&Byte, 1);
                }
            }

            arch_in.read(Info, block_size);
            archiv_out.write(Info, block_size);
        }
        for(int j = 0; j < 2; ++j){
            arch_in.read(Info, block_size);
            archiv_out.write(Info, block_size);
        }
        name_in_arch = Get_filename(arch_in);
    }

    if(archiv_out.tellg() == 0){

        archiv_out.close();
        Delete(archiv_name + "1");
    }

    arch_in.close();
    Delete(archiv_name);
}

void Delete(std::string name){
    char sup[name.size() + 1];
    for(int i = 0; i < name.size() + 1; ++i){
        sup[i] = name[i];
        if(i == name.size()){
            sup[i] = '\0';
        }
    }
    remove(sup);
}