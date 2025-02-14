#include "number.h"
#include<string>

int2023_t zero_is_zero(int2023_t number){
    int count = 0;
    int base =  int2023_t().base;
    for(int i = 0; i < base; i++){
        if(number.number[i] == 0){
            count++;
        }
    }
    if(count == base - 1 && number.number[base - 1] == 128){
        number.number[base - 1] = 0;
    }
    return number;
}

void Upheaval (char* string, int lenght){
    int count = lenght - 1;

    char sup_string[lenght];

    while(string[count--] == '0');


    for(int i = 0; i < lenght ; i++){
        if(i > count){
            sup_string[lenght - i - 1] = string[i];
        }
    }

    for(int i = 0; i < count; i++){
        sup_string[i] = '0';
    }

    for(int i = 0; i < lenght; i++){
        string[i] = sup_string[i];
        //std::cout<<string[i];
    }

}

bool find_more_number(int2023_t lhs, int2023_t rhs){
    for(int i = int2023_t().base - 1; i >= 0; --i){
        if(lhs.number[i] > rhs.number[i]){
            return true;
        }
        if(rhs.number[i] > lhs.number[i]){
            return false;
        }
    }
    return true;
}

bool equal(const int2023_t lhs, const int2023_t rhs){
    for(int i = int2023_t().base - 1; i >= 0; --i){
        if(lhs.number[i] != rhs.number[i]){
            return false;
        }
    }
    return true;
}

int strcmp(const char* string_left, const char* string_right){
    while(*string_left != '\0' && *string_right != '\0'){
        ++string_left;
        ++string_right;
    }
    return (*string_left > *string_right) + (*string_left < *string_right);
}

int Strlen(const char string[]){
    int c = 0;
    while(string[c++] != '\0');
    return --c;
}

int2023_t from_int(int32_t changing_number) {//change int to int2023_t
    int2023_t number;
    static const int size = int2023_t().size;
    int count = 0;
    static const int base = int2023_t().base;
    if(changing_number < 0){
        number.number[base - 1] = 128;
    }


    while(changing_number){
        number.number[count++] = static_cast<int8_t>(abs(changing_number % size));
        changing_number /= size;
    }
    return number;
}

int2023_t from_string(std:: string string_main) {// change string to int2023_t
    std::string string_sup = "";
    const int base = int2023_t().base;

    int count = 0;

    const short kAsciiNumberZero = 48;
    int2023_t number;
    int num = 0;
    int sup_num;

    uint8_t Todd_Howard[base];
    for(int i = 0; i < base; i++){
        Todd_Howard[i] = 0;
    }

    if(string_main[0] == '-'){
        Todd_Howard[base-1] = 128;
    }

    static const int size = int2023_t().size;

    while(string_main != ""){
        for(int i = 0; i < string_main.size(); i++){
            if(string_main[i] == '-'){
                continue;
            }
            sup_num = 0;
            num = num * 10 + static_cast<int>(string_main[i] - kAsciiNumberZero);

            if(num / size != 0 || num == size || i == string_main.size()-1){
                for(int j = 0; j<=10; j++){
                    if(size * j > num){
                        sup_num = --j;

                        break;
                    }
                }
                num %= size;
            }
            if(i>1) {
                string_sup.push_back(static_cast<uint8_t>(sup_num + kAsciiNumberZero));
            }
        }
        //std::cout<<string_sup<<" "<<string_main<<'\n';
        Todd_Howard[count++] = static_cast<uint8_t>(num);
        string_main = string_sup;
        //::cout<<string_main<<" ";
        //std::cout<<string_main;
        string_sup="";
        num = 0;
    }

    for(int i = 0; i < base; i++){
        number.number[i] += Todd_Howard[i];
    }

    return number;
}

int2023_t operator+(const int2023_t& lhs, const int2023_t& rhs) {
    int2023_t sup_num_lhs = lhs;
    int2023_t sup_num_rhs = rhs;
    int2023_t number;

    int next_base = 0;
    int sum_num;
    static const int size = int2023_t().size;
    static const int base = int2023_t().base;

    if((lhs.number[base-1] < size / 2 && rhs.number[base-1] < size / 2) || (lhs.number[base-1] >= size / 2 && rhs.number[base-1] >= size / 2)) {
        if((lhs.number[base-1] >= size / 2 && rhs.number[base-1] >= size / 2)){
            number.number[base - 1] = static_cast<uint8_t>(lhs.number[base - 1]);
        }
        for (int i = 0; i < base; i++) {
            sum_num = lhs.number[i] + rhs.number[i] + next_base;
            if (sum_num >= size) {
                sum_num = sum_num % (size);
                next_base = 1;
            } else {
                next_base = 0;
            }
            number.number[i] += static_cast<uint8_t>(sum_num);
        }
    } else if(lhs.number[base - 1] < size / 2){
        sup_num_rhs.number[base - 1] += size / 2;
        number = lhs - sup_num_rhs;
    }else{
        sup_num_lhs.number[base - 1] += size / 2;
        number = rhs - sup_num_lhs;
    }

    return number;
}

int2023_t operator-(const int2023_t& lhs, const int2023_t& rhs) {
    int2023_t sup_num_lhs = lhs;
    int2023_t sup_num_rhs = rhs;

    static const int size = int2023_t().size;
    static const int base = int2023_t().base;
    int2023_t number;
    int sup_dot = 0;

    if(sup_num_lhs.number[base-1] >= size / 2 && sup_num_rhs.number[base - 1] >= size / 2){
        sup_num_rhs.number[base - 1] += size/2;
        number = sup_num_rhs + lhs;
    }else if((sup_num_lhs.number[base - 1] >= (size / 2) && rhs.number[base - 1] < (size / 2)) || (lhs.number[base - 1] < (size / 2) && rhs.number[base - 1] >= (size / 2))) {
        if(sup_num_rhs.number[base - 1] >= (size / 2)) {
            sup_num_rhs.number[base - 1] -= size / 2;
            number = sup_num_lhs + sup_num_rhs;
        }else{
            sup_num_rhs.number[base - 1] -= size / 2;
            number =  sup_num_rhs + sup_num_lhs;
        }
    }else {
        if (find_more_number(sup_num_lhs, sup_num_rhs)) {
            for (int i = 0; i < base; i++) {
                if (sup_num_rhs.number[i] > sup_num_lhs.number[i]) {
                    for (int j = i + 1; j < base; ++j) {
                        if (sup_num_lhs.number[j] != 0) {
                            sup_num_lhs.number[j]--;
                            sup_dot = size;
                            break;
                        }
                        sup_num_lhs.number[j]--;
                    }
                }
                number.number[i] = sup_num_lhs.number[i] + sup_dot - sup_num_rhs.number[i];
                sup_dot = 0;
            }
        } else {
            number = rhs - lhs;
            number.number[base-1] += size/2;
        }
    }
    return number;
}

int2023_t operator*(const int2023_t& lhs, const int2023_t& rhs) {
    int2023_t number;

    static const int size = int2023_t().size;
    static const int base = int2023_t().base;

    int sup_num;
    int next_base = 0;


    if(find_more_number(lhs, rhs)) {
        for (int i = 0; i < base - 1; i++) {
            for (int j = 0; j < base - 1; j++) {
                if(i + j > base - 1){
                    break;
                }
                sup_num = next_base + static_cast<int>(lhs.number[j]) * static_cast<int>(rhs.number[i]) + number.number[j + i];

                next_base = sup_num / size;
                sup_num %= size;
                number.number[j + i] = sup_num;
            }

        }
        if((lhs.number[base - 1] >= size / 2)){
            number.number[base - 1] += size / 2;
        }
        if((rhs.number[base - 1] >= size / 2)){
            number.number[base - 1] += size / 2;
        }
    } else {
        number = rhs * lhs;
    }



    return number;
}

int2023_t operator/(const int2023_t& lhs, const int2023_t& rhs) {
    int2023_t number;
    int2023_t sup_lhs_number = lhs;
    int2023_t sup_rhs_number = rhs;

    const int base = int2023_t().base;
    const int size = int2023_t().size;

    if(lhs.number[base - 1] >= size / 2){
        sup_lhs_number.number[base - 1] += size / 2;
    }
    if(rhs.number[base - 1] >= size / 2){
        sup_rhs_number.number[base - 1] += size / 2;
    }

    bool flag = false;


    for (int i = base - 1; i >= 0; --i) {
        while (find_more_number(sup_lhs_number, (number * sup_rhs_number))) {
            if (equal((number*sup_rhs_number), sup_lhs_number)) {
                number.number[i] += 1;
                break;
            }


            if(number.number[i] == size - 1){
                flag = true;
            }

            number.number[i] += 1;

            if((i == base - 1) && number.number[i] == 128){
                number.number[i] = 1;
                break;
            }

            if(((number.number[i] == sup_lhs_number.number[i])) && flag){
                number.number[i] += 1;
                break;
            }

            if(number*rhs == from_int(0) && rhs != from_int(0)) {
                break;
            }
        }
        flag = false;
        number.number[i]--;
    }
    if(lhs.number[base - 1] >= size / 2){
        number.number[base - 1] += size/2;
    }
    if(rhs.number[base - 1] >= size / 2){
        number.number[base - 1] += size/2;
    }
    number = zero_is_zero(number);
    return number;
}


bool operator==(const int2023_t& lhs, const int2023_t& rhs) {
    for(int i = 0; i < int2023_t().base; i++){
        if(lhs.number[i] != rhs.number[i]){
            return false;
        }
    }
    return true;
}

bool operator!=(const int2023_t& lhs, const int2023_t& rhs) {
    for(int i = 0; i < int2023_t().base; i++){
        if(lhs.number[i] != rhs.number[i]){
            return true;
        }
    }
    return false;
}


std::ostream& operator<<(std::ostream& stream, const int2023_t& value) {
    int num;

    for(int i = int2023_t().base-1; i >= 0; --i) {
        num = value.number[i];
        stream<<"("<<num<<")";
    }

    return stream;
}

int2023_t int2023_t::operator()(int pow){
    int2023_t number = from_int(1);
    for(int i = 0; i < pow; i++){
        number = number * (*this);
    }
    return number;
}