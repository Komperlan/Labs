#include <iostream>
#include "../src/main.h"
#include "../src/Sanbox.h"
#include "bmp.h"
#include <string>
#include<fstream>
#include <algorithm>
#include "qeueue.h"
#include <cstdint>
#include <iostream>
#include <chrono>


int ChangeCharToInt(const char argv[], bool large);

int ParsingLines(int* n_lines,char argv[], bool large);

int MyStrcmp(const char* string_1, const char* string_2, int64_t count);

void ParseAllArgs(struct Parsing_arg parsing_args);

int main(int argc, char* argv[]) {
    auto begin = std::chrono::steady_clock::now();

    std::string filename;
    std::string direction;


    int fps;
    int iteration;

    const uint16_t kShortMax = 65535/2;

    Parsing_arg arguments = {.argc = argc, .argv = argv, .fps = &fps, .filename_ind = &filename, .direction = &direction, .max_iteration = &iteration};

    ParseAllArgs(arguments);

    std::ifstream file(filename);

    if (!file.is_open()) {
        std::cerr << "Error! file is not open";
        std::exit(EXIT_FAILURE);
    }

    int file_size =0;

    uint64_t x_min = kShortMax * 2 + 1;
    uint64_t y_min = kShortMax * 2 + 1;
    uint64_t x_max = 0;
    uint64_t y_max = 0;

    uint64_t x;
    uint64_t y;
    uint64_t grains;

    Queue stack;

    while (file >> x >> y >> grains) {
        x_max = std::max(x_max, x);
        y_max = std::max(y_max, y);
        x_min = std::min(x_min, x);
        y_min = std::min(y_min, y);
        stack.push_back({x, y, grains});
        file_size++;
    }

    std::string string;

    file.close();

    uint64_t** field = Create_heap(x_min, x_max, y_min, y_max);


    for(int64_t sands = 0; sands < file_size; sands++){
        field[stack.back().y - y_min][stack.back().x - x_min] = stack.back().num_sand;
        stack.pop_back();
    }

    y_max -= y_min;
    x_max -= x_min;


    uint64_t frame_count = 0;


    for(int iterations = 0; iterations < iteration; iterations++) {
        field = Avalanche(field, &y_max, &x_max);
    }


    for(uint64_t row = 0; row <= y_max - y_min; ++row){
        std::free(field[row]);
    }
    std::free(field);   

    auto end = std::chrono::steady_clock::now();

    auto elapsed_ms = std::chrono::duration_cast<std::chrono::milliseconds>(end - begin);
    std::cout << "The time: " << elapsed_ms.count() << " ms\n";

}

int ChangeCharToInt(const char* argv, bool large){
    int numbers = 0;
    if(large) {
        for (int i = sizeof(argv); i > 0; ++i) {
            if (argv[i] >= '0' and argv[i] <= '9') {
                numbers = numbers * 10 + (argv[i] - '0');
            } else {
                break;
            }
        }
    } else {
        for (int i = 0; i > - 1; ++i) {
            if (argv[i] >= '0' and argv[i] <= '9') {
                numbers = numbers * 10 + (argv[i] - '0');
            } else {
                break;
            }
        }
    }
    return numbers;
}

int ParsingLines(int* n_lines, char argv[], bool large){
    int n_args = ChangeCharToInt(argv, large);
    if(n_args > 0){
        *n_lines = n_args;
    } else {
        std::cerr<<"Error! please input correct -l number argument";
        std::exit(EXIT_FAILURE);
    }
    return -1;
}

int MyStrcmp(const char* string_1, const char* string_2, int64_t count) {//Сравнение строк
    while ((count > 0) and (*string_1 != '\0') and (*string_1 == *string_2)) {
        ++string_1;
        ++string_2;
        --count;
    }
    if (count == 0)
        return 0;

    return (*string_1 > *string_2) - (*string_2 > *string_1);
}

void ParseAllArgs(struct Parsing_arg parsing_args) { //Функция распознавания аргументов



    int file_flag = 0; //флаги т.к нужны 3 состояния 0 l ещё не найден, 1 l найден но число ещё не обработанно, -1 l найден и обработан
    int frame_flag = 0;
    int file_direction_flag = 0;
    int iteration_flag = 0;


    for (int i = 1; i < parsing_args.argc; ++i) {
        std::string args = parsing_args.argv[i];
        if (iteration_flag == 1){
            iteration_flag = ParsingLines(parsing_args.max_iteration, parsing_args.argv[i], false);
        } else if(file_direction_flag == 1){ //Если раньше был -o
                *parsing_args.direction = args;
                file_direction_flag = -1;
        } else if(frame_flag == 1){
            frame_flag = ParsingLines(parsing_args.fps, parsing_args.argv[i], false);
        }else if(file_flag == 1){
            *parsing_args.filename_ind = args;
            file_flag = -1;
        }else if(MyStrcmp(parsing_args.argv[i], "--output", 8)== 0){
            if (file_direction_flag == 0) { //отделяем числа от строки
                args.erase(0, 8);
                file_direction_flag = -1;
                *parsing_args.direction = args;
            } else {
                std::cerr<< "Error! please input only 1 -o argument";
                std::exit(EXIT_FAILURE);
            }
        } else if (MyStrcmp(parsing_args.argv[i], "-o", 2)==0) { //Если введён аргумент -d, то поднимаем флаг,
            if(file_direction_flag == 0) {
                file_direction_flag = 1;
            } else {
                std::cerr<< "please input only 1 -d argument";
                std::exit(EXIT_FAILURE);
            }
        } else {
            int kLinesDelimetr = 11;
            if(MyStrcmp(parsing_args.argv[i], "--max-iter=", kLinesDelimetr)==0){
                if (iteration_flag == 0) {//отделяем числа от строки
                    iteration_flag = ParsingLines(parsing_args.max_iteration, parsing_args.argv[i], true);
                    iteration_flag = -1;
                } else {
                    std::cerr<< "please input only 1 -m argument";
                    std::exit(EXIT_FAILURE);
                }
            } else if (MyStrcmp(parsing_args.argv[i], "-m", sizeof(parsing_args.argv[i])) == 0) {//Если введён аргумент -l, то поднимаем флаг,
                    if (iteration_flag == 0) {
                        iteration_flag = 1;
                    } else {
                        std::cerr<< "please input only 1 -m argument";
                        std::exit(EXIT_FAILURE);
                    }
            } else if(MyStrcmp(parsing_args.argv[i], "-f", 2) == 0){
                    if(frame_flag != 0){
                        std::cerr<< "please input only 1 -m argument";
                        std::exit(EXIT_FAILURE);
                    } else if(frame_flag == -1){
                        std::cerr<< "please input only 1 -m argument";
                        std::exit(EXIT_FAILURE);
                    }else {
                        frame_flag = 1;
                    }

            } else if(MyStrcmp(parsing_args.argv[i], "--freq", 6) == 0){
                if(frame_flag != 0){
                    std::cerr<< "please input only 1 -m argument";
                    std::exit(EXIT_FAILURE);
                } else if(frame_flag == -1){
                    std::cerr<< "please input only 1 -m argument";
                    std::exit(EXIT_FAILURE);
                }else {
                    frame_flag = ParsingLines(parsing_args.fps, parsing_args.argv[i], true);
                }
            } else if(MyStrcmp(parsing_args.argv[i], "--input", 7)== 0) {
                if (file_flag == 0) { //отделяем числа от строки
                    args.erase(0, 7);
                    file_flag = -1;
                    *parsing_args.direction = args;
                } else {
                    std::cerr << "Error! please input only 1 -o argument";
                    std::exit(EXIT_FAILURE);
                }
            } if(MyStrcmp(parsing_args.argv[i], "-i", 2) == 0){
                if (file_flag == 0) { //отделяем числа от строки
                    file_flag = 1;
                } else {
                    std::cerr << "Error! please input only 1 -o argument";
                    std::exit(EXIT_FAILURE);
                }
            }
        }
    }
}
