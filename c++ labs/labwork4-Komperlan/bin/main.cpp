#include <functional>
#include <lib/ArgParser.h>
#include <iostream>
#include <numeric>
#include <sstream>


struct Options {
    bool sum = false;
    bool mult = false;
};

int main(int argc, char** argv) {
//    Options opt;
//    std::vector<int> values;
//
//    ArgumentParser::ArgParser parser("Program");
//    parser.AddIntArgument("N").MultiValue(1).Positional().StoreValues(values);
//    parser.AddFlag("sum", "add args").StoreValue(opt.sum);
//    parser.AddFlag("mult", "multiply args").StoreValue(opt.mult);
//    parser.AddHelp('h', "help", "Program accumulate arguments");
//
//    if(!parser.Parse(argc, argv)) {
//        std::cout << "Wrong argument" << std::endl;
//        std::cout << parser.HelpDescription() << std::endl;
//        return 1;
//    }
//
//    if(parser.Help()) {
//        std::cout << parser.HelpDescription() << std::endl;
//        return 0;
//    }
//
//    if(opt.sum) {
//        std::cout << "Result: " << std::accumulate(values.begin(), values.end(), 0) << std::endl;
//    } else if(opt.mult) {
//        std::cout << "Result: " << std::accumulate(values.begin(), values.end(), 1, std::multiplies<int>()) << std::endl;
//    } else {
//        std::cout << "No one options had chosen" << std::endl;
//        std::cout << parser.HelpDescription();
//        return 1;
//    }

    ArgumentParser::ArgParser parser("My Parser");
    bool flag3;
    parser.AddFlag('a', "flag1");
    parser.AddFlag('b', "flag2").Default(true);
    parser.AddFlag('c', "flag3").StoreValue(flag3);

    parser.Parse({"app", "-ac"});

    std::cout<<parser.GetFlag("flag1")<<" ";
    std::cout<<parser.GetFlag("flag2")<<" ";
    std::cout<<flag3;
}
