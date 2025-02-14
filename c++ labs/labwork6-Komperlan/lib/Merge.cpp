#include "Merge.h"

void Merge_arch(std::vector<std::string>& files, std::fstream& arch){
    char sup_byte;
    for(int i = 0; i < files.size(); ++i){
        std::fstream file(files[i]);
        while(!file.eof()){
            file>>sup_byte;
            arch<<sup_byte;
        }
    }
}