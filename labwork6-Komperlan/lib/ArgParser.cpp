#include "ArgParser.h"
#include "vector"
#include "string"
#include <algorithm>

using namespace ArgumentParser;

Argument AddArg(const char type, const std::string& fullname, const char short_name,
                const std::string& help_mess, int& size,int& min_size){
    Argument sup_arg;

    sup_arg.type = type;
    sup_arg.min_argument_size = &min_size;
    sup_arg.input_arg_count = &size;
    sup_arg.fullname = fullname;
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

size_t Argument:: Size(){
    switch (type) {
        case 0:
            return 1;
        case 1:
            return string_value.size();
        case 2:
            return int_value.size();
    }
}

int ArgParser::GetInt(int index, const std::string& get_name){
    for(const auto& arg : args_names){
        if(arg.fullname == get_name){
            return arg.int_value[index];
        }
    }
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

void ArgParser::ParseMultiFlagArg(int index){
    args_names[index].string_value[0].erase(0, 1);
    while(args_names[index].string_value[0] != "\0"){
        for(int sup_index = 0; sup_index < args_names.size(); ++sup_index){
            if(args_names[index].string_value[0][0] == args_names[sup_index].short_name){
                args_names[sup_index].bool_value =  !args_names[sup_index].bool_value;
                if(args_names[sup_index].is_out){
                    *args_names[sup_index].bool_out = args_names[sup_index].bool_value;
                }
                break;
            }
        }
        args_names[index].string_value[0].erase(0, 1);

    }
}

void Erase_arg_name(const std::string& input_arg, Argument& arg){
    if(input_arg.find(arg.long_argument_name) != -1) {
        arg.string_value[0].erase(0, (arg.long_argument_name.size() + 1));

    } else{
        arg.string_value[0].erase(0,arg.short_argument_name.size() + 1);
    }
}

Argument& Argument:: Positional(){
    this->is_positional = true;
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
    this->int_value.resize(0);
    return *this;
}

Argument& Argument:: StoreValue(std::vector<std::string>& store_values){
    this->string_out = &store_values;
    this->is_out = true;

    return *this;
}

Argument& Argument:: StoreValue(bool& store_value){
    this->bool_out = &store_value;
    this->is_out = true;

    return *this;
}

Argument& Argument:: StoreValues(std::vector<int>& store_values){
    this->int_vector_out = &store_values;
    this->is_out = true;

    return *this;
}

Argument& Argument::StoreValues(std::vector<std::string>& store_values){
    this->arr_string_out = &store_values;
    this->is_out = true;

    return *this;
}


bool ArgParser:: Parse(const std::vector<std::string>& args){
    for(const auto& arg : args){
        for(int args_base_count = 0; args_base_count < args_names.size(); ++args_base_count){
            if(arg.find(args_names[args_base_count].short_argument_name ) == -1 &&
                    arg.find(args_names[args_base_count].long_argument_name) ==  -1) {

                if(!(args_names[args_base_count].is_positional)) continue;
                args_names[args_base_count].string_value.push_back(arg);
                if(args_names[args_base_count].is_out) {
                    *args_names[args_base_count].string_out = args_names[args_base_count].string_value;
                }
                input_arg_count++;
                continue;
            }

            input_arg_count++;



            switch (args_names[args_base_count].type){
                case bool_type:
                    if( arg.find(args_names[args_base_count].long_argument_name) != -1) {
                        args_names[args_base_count].bool_value = !args_names[args_base_count].bool_value;
                        break;
                    }
                    args_names[args_base_count].string_value.push_back(arg);
                    ParseMultiFlagArg(args_base_count);
                    break;
                case string_type:
                    args_names[args_base_count].string_value.push_back(arg);
                    Erase_arg_name(arg,  args_names[args_base_count]);
                    if(args_names[args_base_count].is_out) *args_names[args_base_count].string_out = args_names[args_base_count].string_value;
                    break;
                case int_type:
                    args_names[args_base_count].string_value.push_back(arg);
                    Erase_arg_name(arg,  args_names[args_base_count]);
                    args_names[args_base_count].int_value.push_back(StringToInt(args_names[args_base_count].string_value[0]));
                    if(args_names[args_base_count].is_out) *args_names[args_base_count].int_vector_out =  args_names[args_base_count].int_value;
                    break;
            }

            break;
        }
    }
    if(input_arg_count < min_arguments_size) return false;

    return true;
}

Argument& Argument:: Default (const char* default_values){
    this->string_value[0] = default_values;
    *this->input_arg_count = 1;

    return *this;
}

Argument& Argument:: Default (const bool default_value){
    this->bool_value = default_value;
    *this->input_arg_count = 1;

    return *this;
}


Argument& ArgParser:: AddFlag(const char short_arg, const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(0, string_arg,
                                short_arg, " ", input_arg_count, min_arguments_size));
    return args_names.back();
}

Argument& ArgParser:: AddFlag(const char short_arg, const std::string& string_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(0, string_arg,
                                short_arg, help_mess, input_arg_count, min_arguments_size));
    return args_names.back();
}

Argument& ArgParser:: AddFlag(const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(0, string_arg,
                                ' ', " ", input_arg_count, min_arguments_size));

    return args_names.back();
}


Argument& ArgParser:: AddStringArgument(const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(1, string_arg,
                                ' ', " ", input_arg_count, min_arguments_size));

    return args_names.back();
}

Argument& ArgParser:: AddStringArgument(const char short_arg, const std::string& string_arg){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(1, string_arg,
                                short_arg, "", input_arg_count, min_arguments_size));

    return args_names.back();
}

Argument& ArgParser:: AddStringArgument(const char short_arg, const std::string& string_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(1, string_arg,
                                short_arg, help_mess, input_arg_count, min_arguments_size));

    return args_names.back();
}

bool ArgParser:: Help() const{
    return have_help;
}

ArgParser& ArgParser:: AddHelp(const char short_arg, const std::string& string_arg, const std::string& help_messages){
    last_index++;
    have_help = true;
    main_help_message = help_messages;
    min_arguments_size = 1;
    args_names.push_back(AddArg(3, string_arg, short_arg,
                                "Display this help and exit", input_arg_count, min_arguments_size));

    return *this;
}

Argument& ArgParser:: AddIntArgument(const std::string& int_name_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(2, int_name_arg,
                                ' ', help_mess, input_arg_count, min_arguments_size));

    return args_names.back();
}

Argument& ArgParser:: AddIntArgument(const std::string& int_name_arg){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(2, int_name_arg,
                                ' ', "", input_arg_count, min_arguments_size));

    return args_names.back();
}

Argument& ArgParser:: AddIntArgument(const char short_arg, const std::string& int_name_arg){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(2, int_name_arg,
                                short_arg, "", input_arg_count, min_arguments_size));

    return args_names.back();
}

Argument& ArgParser:: AddIntArgument(const char short_arg, const std::string& int_name_arg, const std::string& help_mess){
    ++last_index;
    min_arguments_size = 1;
    args_names.push_back(AddArg(2, int_name_arg,
                                short_arg, help_mess, input_arg_count, min_arguments_size));

    return args_names.back();
}

std::string ArgParser:: GetStringValue(const std::string& get_name){
    for(int args_index = 0; args_index < args_names.size(); ++args_index){
        if(args_names[args_index].fullname == get_name){
            return args_names[args_index].string_value[0];
        }
    }
    std::cerr<<"String value is not found";
    exit(EXIT_FAILURE);
}

std::vector<std::string> ArgParser:: GetStringValues(const std::string& get_name){
    for(int args_index = 0; args_index < args_names.size(); ++args_index){
        if(args_names[args_index].fullname == get_name){
            return args_names[args_index].string_value;
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
        output_string += args_names[count].short_argument_name + " " +
        args_names[count].long_argument_name + " " + args_names[count].help_message + '\n';
    }
    return output_string;
}

bool ArgumentParser::ArgParser:: GetFlag(const std::string& get_name) {
    for(const auto& arg: args_names){
        if(arg.fullname == get_name){
            return arg.bool_value;
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
