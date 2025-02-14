#pragma once
#include "iostream"
#include "vector"

enum types {bool_type = 0, string_type = 1, int_type = 2, help_type = 3};

struct Argument{
    Argument &MultiValue(size_t min_args);

    Argument &MultiValue();

    Argument &Positional();

    virtual Argument& Default(const bool default_values){
    }

    virtual Argument& Default(const char*){
    }

    virtual Argument& StoreValues(std::vector<int>&) {
    }

    virtual Argument& StoreValue(std::string&){
    }

    virtual Argument& StoreValue(bool&){
    }

    virtual std::string& Get_string(){
    }

    virtual std::vector<int>& Get_int(){
    }

    virtual std::vector<int>* Get_out_int(){
        return nullptr;
    }

    virtual bool& Get_bool(){
    }

    virtual std::string* Get_string_out(){
        return nullptr;
    }

    virtual bool* Get_out_bool(){
        return nullptr;
    }

    std::string fullname;
    std::string help_message;
    char short_name = 0;

    char type;
    bool is_out = false;

    int* input_arg_count;
    int* min_argument_size;
    bool* is_positional;

    std::string short_argument_name;
    std::string long_argument_name;
};

struct Int_argument: public Argument{
    Int_argument(): Argument(){};

    std::vector<int>& Get_int() override{
        return int_value;
    }
    std::vector<int>* Get_out_int() override{
        return int_vector_out;
    }

    std::vector<int>* int_vector_out;
    std::vector<int> int_value;
    Argument& StoreValues(std::vector<int> &store_values) override;
};

struct String_argument: public Argument{
    String_argument(): Argument(){};

    std::string string_value;
    std::string* string_out;

    std::string& Get_string() override{
        return (string_value);
    };
    std::string* Get_string_out() override{
        return string_out;
    };

    Argument& Default(const char* default_values) override;
    Argument& StoreValue(std::string &store_values) override;
};

struct Bool_argument: public Argument{
    Bool_argument(): Argument(){};

    bool* bool_out;
    bool bool_value;

    bool& Get_bool() override{
        return bool_value;
    };

    bool* Get_out_bool() override{
        return bool_out;
    }

    Argument& Default(const bool default_value) override;
    Argument& StoreValue(bool &store_values) override;
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

        bool GetFlag(const std::string& get_name);

        int GetIntValue(const std::string& get_name);

        int GetIntValue(const std::string& name, int index);

    private:
        int last_index = 0;
        int input_arg_count = 0;
        std::vector<Argument*> args_names;
        std::string name;
        std::string main_help_message;
        int min_arguments_size = 0;
        bool is_positional = false;
        bool have_help = false;

        int GetInt(int index, const std::string& get_name);

        void ParseMultiFlagArg(int index, std::string& sup);

};

}

