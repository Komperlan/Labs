#include <iostream>
#include <string>
#include<iterator>
#include <fstream>

struct Parsing{
    int argc;
    char** argv;
    int* n_lines;
    char* sep_ind;
    bool* tail_flag_ind;
    std::string* filename_ind;
    bool *line_flag_ind;
};

struct File_arg{
    std:: string filename;
    char sep;
    int n;
    bool lineflag;
    bool tailflag;
};

int Separate(const char argv[],char* sep_ind);

int ChangeCharToInt(const char argv[], bool large);

int Find_end_of_array(const char argv[]);

void ParsingAll(struct Parsing for_parsing);

char Trans(char arg);

int My_strcmp(const char* s1, const char* s2, long long count);

int ParsingLines(int* n_lines,char argv[], bool large);

void UsingFile(struct File_arg fileuse);


int main(int argc, char* argv[]) {
    int n = -1;
    int* n_lines = &n;

    std::string last_line;
    std::string filename = "";
    std::string *filename_ind = &filename;

    bool tail_flag = false;
    bool* tail_flag_ind = &tail_flag;
    bool line_flag = false;
    bool* line_flag_ind = &line_flag;

    char sep = '\n';
    char* sep_ind = &sep;

    struct Parsing for_parser = {.argc = argc, .argv = argv, .n_lines = n_lines, .sep_ind = sep_ind, .tail_flag_ind = tail_flag_ind, .filename_ind = filename_ind, .line_flag_ind = line_flag_ind};

    ParsingAll(for_parser); //парсим аргументы
    struct File_arg filing = {.filename = filename, .sep = sep, .n = n, .lineflag = line_flag, .tailflag = tail_flag };
    std::cout<<filename<<" "<<sep<<" "<<n<<" "<<line_flag<<" "<<tail_flag;
    UsingFile(filing);
}

int Find_end_of_array(const char argv[]){
    int count=0;
    while(argv[count] != '\0'){
        count++;
    }

    return count;
}

int My_strcmp(const char* s1, const char* s2, long long count) {
    while ((count > 0) and (*s1 != '\0') and (*s1 == *s2)) {
        s1++;
        s2++;
        count--;
    }
    if (count == 0)
        return 0;
    return (*s1 > *s2) - (*s2 > *s1);
}

void UsingFile(struct File_arg fileuse) {
    std::string line;
    std::ifstream file;

    file.open(fileuse.filename);

    if (!file.is_open()) { //проверка на нахождение файла
        std::cerr << "Error! file is not open";
        std::exit(EXIT_FAILURE);
    }

    int file_line = 0;

    char sim;

    file.seekg(0, std::ios_base::end);

    long long file_size = file.tellg();

    file.seekg(0, std::ios_base::beg);

    for (long long i = 0; i < file_size; i++) { //считываем файл в первый раз, чтобы узнать кол-во строк
        sim = static_cast<char> (file.get());
        if (sim == fileuse.sep) {
            file_line++;
        }
    }

    if (file_line == 0) {
        file_line = 1;
    }

    if (!fileuse.lineflag) { //проверка на ввод кол-ва строк
        fileuse.n = file_line;
    } else {
        if (fileuse.n < 1) {
            std::cerr << "Error! number of rows < 1";
            std::exit(EXIT_FAILURE);
        }
        if (fileuse.n > file_line) {
            std::cerr << "Error! number of rows > file rows";
            std::exit(EXIT_FAILURE);
        }
    }

    file.clear();
    file.seekg(0, std::ios_base::beg);

    int line_count = 0;

    if (!fileuse.tailflag) {  // могут быть очень длинные строки,  поэтому проверяем по чарово
        for (int i = 0; i < file_size; i++) {
            sim = static_cast<char>(file.get());
            if (sim == fileuse.sep) {
                line_count++;
            }
            if (line_count == fileuse.n) {
                break;
            }
            std::cout << sim;
        }
    } else {
        for (long long i = 0; i < file_size; i++) {
            sim = static_cast<char>(file.get());
            if (sim == fileuse.sep) {
                line_count++;
            }
            if (line_count >= file_line - fileuse.n) {
                std::cout << sim;
            }
            if(line_count == file_line){
                break;
            }
        }
    }
}

int ChangeCharToInt(const char* argv, bool large){
    int numbers = 0;
    if(large) {
        for (int i = sizeof(argv); i > 0; i++) {
            if (argv[i] > '0' and argv[i] <= '9') {
                numbers = numbers * 10 + (argv[i] - '0');
            } else {
                break;
            }
        }
    } else {
        for (int i = 0; i > -1; i++) {
            if (argv[i] > '0' and argv[i] <= '9') {
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

int Separate(const char argv[], char* sep_ind) {  //отделяем числа от строки
    const char kAsciiSymbolBackslash = 92;
    const char kAsciiSymbolApostroph = 39;

    int size = Find_end_of_array(argv);

    if (size == 15 && argv[0] == kAsciiSymbolApostroph && argv[2] == kAsciiSymbolApostroph) {
        *sep_ind = argv[13];
    } else {
        if (size == 3 && argv[0] == kAsciiSymbolApostroph && argv[2] == kAsciiSymbolApostroph) {
            *sep_ind = argv[1];
        } else {
            if (size == 16 && argv[13] == kAsciiSymbolBackslash && argv[12] == kAsciiSymbolApostroph && argv[15] == kAsciiSymbolApostroph) {
                if (Trans(argv[14]) != argv[14]) {
                    *sep_ind = Trans(argv[14]);
                } else{
                    std::cerr << "Error! incorrect -d argument input";
                    std::exit(EXIT_FAILURE);
                }

            } else {
                if(size == 4 && argv[1] == kAsciiSymbolBackslash && argv[0] == kAsciiSymbolApostroph && argv[3] == kAsciiSymbolApostroph) {
                    *sep_ind = Trans(argv[2]);
                }else{std::cerr << "Error! incorrect -d argument input";
                    std::exit(EXIT_FAILURE);
                }
            }
        }
    }
    return -1;
}

char Trans(char arg){ //функция для перевода знаков по табличке ascii
    switch(arg){
        case '0':
            return 0;
        case 'a':
            return 7;
        case 'b':
            return 8;
        case 't':
            return 9;
        case 'n':
            return 10;
        case 'f':
            return 12;
        case 'r':
            return 13;
        default:
            return arg;
    }
}


void ParsingAll(struct Parsing for_parsing) { //Функция распознавания аргументов
    int argc = for_parsing.argc;
    char** argv = for_parsing.argv;
    int* n_lines = for_parsing.n_lines;
    char *sep_ind = for_parsing.sep_ind;
    bool* tail_flag_ind = for_parsing.tail_flag_ind;
    std::string* filename_ind = for_parsing.filename_ind;
    bool * line_flag_ind = for_parsing.line_flag_ind;

    int lines_flag = 0;
    int delimiter_flag = 0;

    for (int i = 1; i < argc; i++) {
        std::string args = argv[i];
        if (lines_flag == 1){ //если раньше был -l, то принимаем число
            lines_flag = ParsingLines(n_lines, argv[i], false);
            *line_flag_ind = true;
        } else {
            if(delimiter_flag ==1){ //Если раньше был -d
                delimiter_flag = Separate(argv[i], sep_ind);
            } else {
                if(My_strcmp(argv[i], "--delimiter=", 12)== 0){
                    if (delimiter_flag == 0) { //отделяем числа от строки
                        delimiter_flag = Separate(argv[i], sep_ind);
                    } else {
                        std::cerr<< "Error! please input only 1 -d argument";
                        std::exit(EXIT_FAILURE);
                    }
                } else {
                    if (My_strcmp(argv[i], "-d", 2)==0) { //Если введён аргумент -d, то поднимаем флаг,
                        if(delimiter_flag == 0) {
                            delimiter_flag = 1;
                        } else {
                            std::cerr<< "please input only 1 -d argument";
                            std::exit(EXIT_FAILURE);
                        }
                    } else {
                        int kLinesDelimetr = 8;
                        if(My_strcmp(argv[i], "--lines=", kLinesDelimetr)==0){
                            if (lines_flag == 0) {//отделяем числа от строки
                                lines_flag = ParsingLines(n_lines, argv[i], true);
                                *line_flag_ind = true;
                            } else {
                                std::cerr<< "please input only 1 -l argument";
                                std::exit(EXIT_FAILURE);
                            }
                        } else {
                            if (My_strcmp(argv[i], "-l", sizeof(argv[i])) == 0) {//Если введён аргумент -l, то поднимаем флаг,
                                //std::cout<<My_strcmp(argv[i], "-l", sizeof(argv))<<" "<<argv[i]<<"\n";
                                if (lines_flag == 0) {
                                    lines_flag = 1;
                                    *line_flag_ind = true;
                                } else {
                                    std::cerr<< "please input only 1 -l argument";
                                    std::exit(EXIT_FAILURE);
                                }
                            } else {
                                if (My_strcmp(argv[i], "-t", sizeof(argv[i])) == 0 or My_strcmp(argv[i], "--tail", sizeof(argv[i])) == 0) {
                                    *tail_flag_ind = true;
                                } else {
                                    *filename_ind = argv[i];
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
