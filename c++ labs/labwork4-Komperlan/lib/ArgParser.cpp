#include "ArgParser.h"
#include "vector"
#include "string"
#include <algorithm>

using namespace ArgumentParser;

Argument AddArg(const char type, const std::string& fullname, const char short_name,
                const std::string& help_mess, bool& positional, int& size,int& min_size){
    Argument sup_arg;

    sup_arg.type = type;
    sup_arg.min_argument_size = &min_size;
    sup_arg.input_arg_count = &size;
    sup_arg.fullname = fullname;
    sup_arg.is_positional = &positional;
    sup_arg.short_name = short_name;
    sup_arg.help_message = help_mess;
    sup_arg.short_argument_name = " ";
    if(short_name != ' ') {
        sup_arg.short_argument_name = "-" + std::string{short_name};
    }

    if(fullname != " ") {
        sup_arg.long_argument_name = "--" + fullname;
    }

    return sup_arg;
}

int ArgParser::GetInt(int index, const std::string& get_name){
    for(const auto& arg : args_names){
        if(arg->fullname == get_name){
            return arg->Get_int()[index];
        }
    }
    std::cerr<<"int value is not found";
    return EXIT_FAILURE;
}

int StringToInt(const std::string_view& string){
    int kAsciiZero = 48;
    int number = 0;
    for(const auto& symbol : string){
        if(symbol < '0' || symbol > '9'){
            continue;
        }
        number = number * 10 + static_cast<int>(symbol) - kAsciiZero;
    }

    return number;
}

void ArgParser::ParseMultiFlagArg(int index, std::string& sup){
    sup.erase(0, 1);

    while(sup.size() > 0){
        for(int sup_index = 0; sup_index < args_names.size(); ++sup_index){
            if(sup[0] == args_names[sup_index]->short_name){
                args_names[sup_index]->Get_bool() =  !args_names[sup_index]->Get_bool();
                if(args_names[sup_index]->is_out){
                    *args_names[sup_index]->Get_out_bool() = args_names[sup_index]->Get_bool();
                }
                break;
            }
        }
        sup.erase(0, 1);
    }
}

void Erase_arg_name(const std::string& input_arg, Argument& arg){
    if(input_arg.find(arg.long_argument_name) != -1) {
        arg.Get_string().erase(0, (arg.long_argument_name.size() + 1));

    } else{
        arg.Get_string().erase(0,arg.short_argument_name.size() + 1);
    }
}

Argument& Argument:: Positional(){
    *this->is_positional = true;
    return *this;
}


ArgParser:: ArgParser (const std::string& from_users_name){
    name = from_users_name;
}

Argument&  Argument:: MultiValue (size_t min_args){
    *min_argument_size += static_cast<int>(min_args);
    return *this;
}

Argument&  Argument:: MultiValue (){
    this->Get_int().resize(0);
    return *this;
}

Argument& String_argument:: StoreValue(std::string& store_values){
    this->string_out = &store_values;
    this->is_out = true;

    return *this;
}

Argument& Bool_argument:: StoreValue(bool& store_value)  {
    this->bool_out = &store_value;
    this->is_out = true;

    return *this;
}



Argument& Int_argument:: StoreValues(std::vector<int>& store_values){
    this->int_vector_out = &store_values;
    this->is_out = true;

    return *this;
}



bool ArgParser:: Parse(const std::vector<std::string>& args){
    for(const auto& arg : args){
        for(int args_base_count = 0; args_base_count < args_names.size(); ++args_base_count){
            if(arg.find(args_names[args_base_count]->short_argument_name ) == -1 &&
                    arg.find(args_names[args_base_count]->long_argument_name) ==  -1) {

                if(! is_positional || ((arg[0] < '0' || arg[0] > '9') && is_positional)){
                    continue;
                }

                args_names[args_base_count]->Get_int().push_back(StringToInt(arg));
                *args_names[args_base_count]->Get_out_int() =  args_names[args_base_count]->Get_int();
                input_arg_count++;
                continue;
            }


            input_arg_count++;
            std::string sup = arg;

            switch (args_names[args_base_count]->type){
                case bool_type:
                    if( arg.find(args_names[args_base_count]->long_argument_name) != -1) {
                        args_names[args_base_count]->Get_bool() = !args_names[args_base_count]->Get_bool();
                        if(args_names[args_base_count]->is_out){
                            *args_names[args_base_count]->Get_out_bool() = args_names[args_base_count]->Get_bool();
                        }
                        break;
                    }
                    ParseMultiFlagArg(args_base_count, sup);
                    break;
                case string_type:
                    args_names[args_base_count]->Get_string() = arg;
                    Erase_arg_name(arg,  *args_names[args_base_count]);
                    if(args_names[args_base_count]->is_out) *args_names[args_base_count]->Get_string_out() = args_names[args_base_count]->Get_string();
                    break;
                case int_type:
                    if(arg.find(args_names[args_base_count]->long_argument_name) != -1) {
                        sup.erase(0, (args_names[args_base_count]->long_argument_name.size() + 1));

                    } else{
                        sup.erase(0, (args_names[args_base_count]->short_argument_name.size() + 1));
                    }
                    args_names[args_base_count]->Get_int().push_back(StringToInt(sup));
                    if(args_names[args_base_count]->is_out) *args_names[args_base_count]->Get_out_int() =  args_names[args_base_count]->Get_int();
                    break;
            }

            break;
        }

    }
    if(input_arg_count < min_arguments_size) return false;

    return true;
}

Argument& String_argument:: Default (const char* default_values){
    this->string_value = default_values;
    *this->input_arg_count = 1;

    return *this;
}

Argument& Bool_argument:: Default (const bool default_value){
    this->bool_value = default_value;
    *this->input_arg_count = 1;

    return *this;
}


Argument& ArgParser:: AddFlag(const char short_arg, const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new Bool_argument;
    *sup = AddArg(0, string_arg,
                  short_arg, " ", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);
    return *args_names.back();
}

Argument& ArgParser:: AddFlag(const char short_arg, const std::string& string_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new Bool_argument;
    *sup = AddArg(0, string_arg,
                  short_arg, help_mess, is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);
    return *args_names.back();
}

Argument& ArgParser:: AddFlag(const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new Bool_argument;
    *sup = AddArg(0, string_arg,
                  ' ', " ", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}


Argument& ArgParser:: AddStringArgument(const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new String_argument;
    *sup = AddArg(1, string_arg,
                  ' ', " ", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

Argument& ArgParser:: AddStringArgument(const char short_arg, const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new String_argument;
    *sup = AddArg(1, string_arg,
                  short_arg, "", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

Argument& ArgParser:: AddStringArgument(const char short_arg, const std::string& string_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new String_argument;
    *sup = AddArg(1, string_arg,
                   short_arg, help_mess, is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

bool ArgParser:: Help() const{
    return have_help;
}

ArgParser& ArgParser:: AddHelp(const char short_arg, const std::string& string_arg, const std::string& help_messages){
    last_index++;
    have_help = true;
    main_help_message = help_messages;
    min_arguments_size = 1;
    Argument* sup = new Argument;
    *sup = AddArg(3, string_arg, short_arg,
                  "Display this help and exit", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *this;
}

Argument& ArgParser:: AddIntArgument(const std::string& int_name_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new Int_argument;
    *sup = AddArg(2, int_name_arg,
                  ' ', help_mess, is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

Argument& ArgParser:: AddIntArgument(const std::string& int_name_arg){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new Int_argument;
    *sup = AddArg(2, int_name_arg,
                  ' ', "", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

Argument& ArgParser:: AddIntArgument(const char short_arg, const std::string& int_name_arg){
    ++last_index;
    min_arguments_size = 1;
    Argument* sup = new Int_argument;
    *sup = AddArg(2, int_name_arg,
                  short_arg, "", is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

Argument& ArgParser:: AddIntArgument(const char short_arg, const std::string& int_name_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;

    Argument* sup = new Int_argument;
    *sup = AddArg(2, int_name_arg,
                 short_arg, help_mess, is_positional, input_arg_count, min_arguments_size);
    args_names.push_back(sup);

    return *args_names.back();
}

std::string ArgParser:: GetStringValue(const std::string& get_name){
    for(int args_index = 0; args_index < args_names.size(); ++args_index){
        if(args_names[args_index]->fullname == get_name){
            return args_names[args_index]->Get_string();
        }
    }
    std::cerr<<"String value is not found";
    exit(EXIT_FAILURE);
}


std::string ArgParser:: HelpDescription(){
    std::string output_string;
    output_string += name + '\n';
    output_string += main_help_message + '\n';
    for(int count = 0; count < args_names.size(); ++count){
        output_string += args_names[count]->short_argument_name + " " +
        args_names[count]->long_argument_name + " " + args_names[count]->help_message + '\n';
    }
    return output_string;
}

bool ArgumentParser::ArgParser:: GetFlag(const std::string& get_name) {
    for(const auto& arg: args_names){
        if(arg->fullname == get_name){
            return arg->Get_bool();
        }
    }
    std::cerr<<"flag value is not found";
    return EXIT_FAILURE;
}

int ArgParser:: GetIntValue(const std::string& get_name) {
    return GetInt(0, get_name);
}

int ArgParser:: GetIntValue(const std::string& get_name, const int index) {
    return GetInt(index, get_name);
}
