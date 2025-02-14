#include "Files.h"
#include "iostream"
#include "cmath"

void Append_files(std::string& files, std::fstream& file_arch){
    std::ifstream append_file(files);

    uint64_t count = 0;

    unsigned long long append_file_size = 0;

    char* input;
    if(!append_file.is_open()){
        std::cerr<<"File is not open";
        exit(EXIT_FAILURE);
    }

    file_header head;

    if(files.size() > name_size){
        std::cerr<<"Name File exceeds size";
        exit(EXIT_FAILURE);
    }

    for(int i = 0; i < files.size(); ++i){
        head.file_name[i] = files[i];
    }

    for(int i = files.size(); i < name_size ; ++i){
        head.file_name[i] = '\0';
    }

    file_arch.write(head.file_name, name_size);

    append_file.seekg(0, std::ios_base::end);

    append_file_size = append_file.tellg();


    for(int i = 0; i < 8; ++i){
        head.file_size[i] = 0xFF & (append_file_size >> (56 - i * 8));
    }

    file_arch.write(head.file_size, 8);

    for(int i = 0; i < 8; ++i){
        head.file_size[i] = 0xFF & (block_size >> (56 - i * 8));
    }

    file_arch.write(head.file_size, 8);

    append_file.seekg(0, std::ios_base::beg);

    uint8_t informations_byte[block_size];



    while(append_file.read(input, 1)){
        if(count == block_size){
            wrapper sup = Insert_control_bits(informations_byte, count);
            file_arch.write(reinterpret_cast<char*>(sup.output_arr), sup.file_size / 8 + sup.file_size % 8);
            delete sup.output_arr;
            count = 0;

        }

        informations_byte[count] = *input;

        file_arch.write(input, 1);
        ++append_file_size;
        ++count;
    }



    if(count != 0) {
        int8_t* sup = Insert_control_bits(informations_byte, count).output_arr;
        file_arch.write(reinterpret_cast<char*>(sup), 2);
        delete sup;
    }
    append_file.close();
}

wrapper Insert_control_bits(const uint8_t* informations, uint64_t count){//непосредственно кодирование
    uint64_t control_vector = 0;

    int8_t shift = 2;

    int sup_k = 2;

    int sup_n = static_cast<int>(std::pow(2, sup_k));

    for(uint64_t i = 0; i < count; ++i){
        for(int j = 0; j < 8; ++j) {
            if (((informations[i / 8] >> (j % 8)) & 1) == 1) {
                if (i + shift == sup_n) {
                    shift++;
                    sup_k++;
                    sup_n = std::pow(2, sup_k);
                }
                control_vector ^= i + shift;
            }
        }
    }

    wrapper output;
    output.output_arr = new int8_t[shift/8 + shift % 8];

    for(int8_t i = 0; i < shift/8 + shift % 8; ++i){
        output.output_arr[i] = control_vector >> (8 * i) & 0xFF;
    }

    return output;
}
