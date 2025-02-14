#pragma once
#include "cstdint"
#include "iostream"

class Base{
public:
    static int& get_lenght(){
        return lenght;
    };

    static int& get_width(){
        return width;
    };

    static int& get_height(){
        return height;
    };

    static uint8_t*& get_arr(){
        return arr;
    };

private:
    static inline uint8_t* arr = nullptr;
    static inline int lenght = 0;
    static inline int width = 0;
    static inline int height = 0;
};

class Array_3D;
class Array_1D;
class Array_2D;

struct Wrapper{
public:
    Wrapper& operator= (const int input);
    Wrapper& operator= (const Wrapper& input);
    friend std::ostream& operator<< (std::ostream& stream, const Wrapper& value);

    friend std::istream& operator>> (std::istream& in, Wrapper value);

    int& get_number(){
        return number;
    };

    int& get_pos_number(){
        return pos_number;
    };

    uint8_t*& get_arr(){
        return arr;
    };

private:
    uint8_t* arr;
    int number = 0;
    int pos_number;
};

struct Array_1D: public Base{
    Wrapper operator[] (const int height_in);
    int& get_y(){
        return y_size;
    }
    int& get_z(){
        return z_size;
    }
private:
    int y_size;
    int z_size;
};

struct Array_2D: public Base{
    Array_1D operator[] (const int width_in);
    int& get_y(){
        return y_size;
    }
    int& get_z(){
        return z_size;
    }
private:
    int y_size;
    int z_size;
};

class Array_3D: public Base{
public:
    Array_3D(){
        get_arr() = nullptr;
    };

    ~Array_3D(){
        //delete get_arr();
    };

    Array_3D& operator=(Array_3D arr);

    static Array_3D MakeArray(size_t lenght_input, size_t width_input, size_t height_input);

    Array_2D operator[](const int lenght_in);

    friend std::ostream& operator<< (std::ostream& stream, Array_3D& value);

    friend std::istream& operator>> (std::istream& in, Array_3D& value);
};

int operator* (Wrapper arr, int num);

int operator+ (Wrapper lhs, Wrapper rhs);

int operator- (Wrapper lhs, Wrapper rhs);

Array_3D operator+ (Array_3D& lhs, Array_3D& rhs);

Array_3D operator- (Array_3D& lhs, Array_3D& rhs);

Array_3D operator* (Array_3D& arr, int num);