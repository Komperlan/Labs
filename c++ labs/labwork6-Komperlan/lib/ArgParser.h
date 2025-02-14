#pragma once
#include "iostream"
#include "vector"

enum types {bool_type = 0, string_type = 1, int_type = 2, help_type = 3};

struct Argument;

struct Int_argument{
    std::vector<int>* int_vector_out;
    std::vector<int> int_value;
};

struct String_argument{
    std::vector<std::string> string_value;
    std::vector<std::string>* string_out;
    std::vector<std::string>* arr_string_out;
};

struct Bool_argument{
    bool* bool_out;
    bool bool_value = false;
};

struct Argument: public Int_argument, String_argument, Bool_argument{
    Argument &MultiValue(size_t min_args);

    Argument &MultiValue();

    Argument &Positional();

    Argument& StoreValues(std::vector<int>& store_values);
    Argument& StoreValues(std::vector<std::string>& store_values);
    Argument& Default(const char* default_values);
    Argument& StoreValue(std::vector<std::string>& store_values);
    Argument& Default(const bool default_value);
    Argument& StoreValue(bool &store_values);

    size_t Size();


    std::string fullname;
    std::string help_message;
    char short_name = 0;

    char type;

    int* input_arg_count;
    int* min_argument_size;
    bool is_positional = false;

    bool is_out = false;

    std::string short_argument_name;
    std::string long_argument_name;

};


namespace ArgumentParser {

    class ArgParser {
    public:
        explicit ArgParser (const std::string& from_users_name);

        bool Parse(const std::vector<std::string>& args);

        Argument& AddFlag(char short_arg, const std::string& string_arg);

        Argument& AddFlag(char short_arg, const std::string& string_arg, const std::string& help_mess);

        Argument& AddFlag(const std::string& string_arg);


        Argument& AddStringArgument(const std::string& string_arg);

        Argument& AddStringArgument(char short_arg, const std::string& string_arg);

        Argument& AddStringArgument(char short_arg, const std::string& string_arg, const std::string& help_mess);

        bool Help() const;

        ArgParser& AddHelp(char short_arg, const std::string& string_arg, const std::string& Help_message);

        Argument& AddIntArgument(const std::string& name);

        Argument& AddIntArgument(const std::string& name, const std::string& help_mess);

        Argument& AddIntArgument(char short_arg, const std::string& int_name);

        Argument& AddIntArgument(char short_arg, const std::string& int_name, const std::string& help_mess);


        std::string HelpDescription();

        std::string GetStringValue(const std::string& value);

        std::vector<std::string> GetStringValues(const std::string& value);

        bool GetFlag(const std::string& get_name);

        int GetIntValue(const std::string& get_name);

        int GetIntValue(const std::string& name, int index);

    private:
        int last_index = 0;
        int input_arg_count = 0;
        std::vector<Argument> args_names;
        std::string name;
        std::string main_help_message;
        int min_arguments_size = 0;
        bool have_help = false;

        int GetInt(int index, const std::string& get_name);

        void ParseMultiFlagArg(int index);

};

}

