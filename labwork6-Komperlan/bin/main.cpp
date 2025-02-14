#include "lib/Files.h"
#include "lib/ArgParser.h"
#include "lib/Merge.h"
#include "lib/Information_about_files.h"

using namespace ArgumentParser;

void Char_to_string(char** argv, int max_count, std::vector<std::string>& output){
    for(int i = 1; i < max_count; ++i){
        output.push_back(static_cast<std::string>(argv[i]));
    }
}

int main(int argc, char** argv){
    std::vector<std::string> filenames;

    ArgParser parser("My Parser");

    parser.AddFlag('c',"create");
    parser.AddStringArgument('f', "file");
    parser.AddFlag('l', "list");
    parser.AddFlag('x', "extract");
    parser.AddFlag('a', "append");
    parser.AddFlag('d', "delete");
    parser.AddFlag('A', "concatenate");
    parser.AddStringArgument("files").StoreValue(filenames).Positional();
    parser.AddIntArgument('s', "Size");

    std::vector<std::string> args;

    Char_to_string(argv, argc, args);

    parser.Parse(args);

    std::string archiv_name = parser.GetStringValue("file") + ".haf";

    std::fstream archiv;

    archiv.open(archiv_name,  std::fstream::in | std::fstream::out | std::fstream::app);

    block_size = 8188;//parser.GetIntValue("Size");

    if(!archiv.is_open()){
        std::cerr<<"File is not open";
        exit(EXIT_FAILURE);
    }

    if(parser.GetFlag("delete")) {
        if(parser.GetFlag("append") || parser.GetFlag("concatenate")){
            std::cerr<<"Incorrecteble arguments. Please input only 1 positional argument";
        }
        for (int files = 0; files < filenames.size(); ++files) {
            Delete_files(filenames[files], archiv);
            Remake_arch(archiv, filenames[files], archiv_name);
            archiv_name += "1";
            archiv.open(archiv_name);
        }

        if(archiv.tellg() == 1){
            archiv.close();
            Delete(archiv_name + "1");
        }
    }

    if(parser.GetFlag("append")) {
        if(parser.GetFlag("concatenate")){
            std::cerr<<"Incorrecteble arguments. Please input only 1 positional argument";
        }

        archiv.seekg(0, std::ios_base::end);
        for (int files = 0; files < filenames.size(); ++files) {
            Append_files(filenames[files], archiv);

        }
    }

    if(parser.GetFlag("concatenate")) {
        Merge_arch(filenames, archiv);
    }

    if(parser.GetFlag("list")) {
        Give_info(archiv);
    }
}