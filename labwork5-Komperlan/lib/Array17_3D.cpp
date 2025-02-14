#include "Array17_3D.h"


static const int8_t kByteSize = 8;
static const int8_t kNumberSize = 17;

int My_pow(int lhs, int rhs){
    int output = 1;
    for(int i = 0; i < rhs; ++i){
        output *= lhs;
    }

    return output;
}

Array_2D Array_3D::operator[](const int lenght_in){
    Array_2D array2d;
    array2d.get_y() = get_width();
    array2d.get_z() = get_height();
    array2d.get_lenght() = lenght_in;
    array2d.get_arr() = get_arr();
    return array2d;
}

Array_1D Array_2D::operator[](const int width_in){
    Array_1D array1d;
    array1d.get_arr() = get_arr();
    array1d.get_lenght() = get_lenght();
    array1d.get_width() = width_in;
    array1d.get_y() = get_y();
    array1d.get_z() = get_z();
    return array1d;
}

std::istream& operator>> (std::istream& in, Wrapper value){
    in >> value.number;

    return in;
}

int operator*(Wrapper arr, int num){
    return arr.get_number() * num;
}

Wrapper& Wrapper::operator= (const Wrapper& input){
    number = input.number;
    pos_number = input.pos_number;
    arr = input.arr;

    return *this;
}

Wrapper Array_1D::operator[](const int height_in){
    Wrapper output;
    int start_byte = ((height_in + get_width() * z_size + z_size * y_size * get_lenght()) * kNumberSize) / kByteSize;
    int start_bit_by_byte = ((height_in + get_width() * z_size + z_size * y_size * get_lenght()) * kNumberSize) % kByteSize;
    output.get_number() = ((get_arr()[start_byte + 2] & (My_pow(2, start_bit_by_byte + 1) - 1)));
    output.get_number() = output.get_number() << kByteSize;
    output.get_number() |= get_arr()[start_byte + 1];
    output.get_number() = output.get_number() << (kByteSize - start_bit_by_byte);
    output.get_number() |=  get_arr()[start_byte] >> start_bit_by_byte;
    output.get_pos_number() = (height_in + get_width() * z_size + z_size * y_size * get_lenght());
    output.get_arr() = get_arr();

    return output;
}

Wrapper& Wrapper::operator= (const int input){
    number = input;
    int start_bit_by_byte = (pos_number * kNumberSize) % kByteSize;
    int start_byte = (pos_number * kNumberSize) / kByteSize;
    arr[start_byte] = (arr[start_byte] & (My_pow(2, start_bit_by_byte) - 1))
            | ((number & (My_pow(2, kByteSize - start_bit_by_byte) - 1)) << start_bit_by_byte);
    arr[start_byte + 1] = ((number>>(kByteSize - start_bit_by_byte)) & (My_pow(2, kByteSize) - 1));
    arr[start_byte + 2] = ((arr[start_byte + 2] >> (1 + start_bit_by_byte))<< (1 + start_bit_by_byte) )  |
            (number >> (2 * kByteSize - start_bit_by_byte));

    return *this;
}

std::ostream& operator<< (std::ostream& stream, const Wrapper& value){
    stream<<value.number;

    return stream;
}

int operator+ (Wrapper lhs, Wrapper rhs){
    return lhs.get_number() + rhs.get_number();
}

int operator- (Wrapper lhs, Wrapper rhs){
    return lhs.get_number() - rhs.get_number();
}

std::ostream& operator<< (std::ostream& stream, Array_3D& value){
    for(int x = 0; x < value.get_lenght(); ++x){
        for(int y = 0; y < value.get_width(); ++y){
            for(int z = 0; z < value.get_height(); ++z){
                stream<<value[x][y][z]<<" ";
            }
            stream<<'\n';
        }
        stream<<'\n';
    }

    return stream;
}

std::istream& operator>> (std::istream& in, Array_3D& value){
    int num;
    for(int x = 0; x < value.get_lenght(); ++x){
        for(int y = 0; y < value.get_width(); ++y){
            for(int z = 0; z < value.get_height(); ++z){
                in >> num;
                value[x][y][z] = num;
            }
        }
    }

    return in;
}

Array_3D& Array_3D:: operator=(Array_3D arr){
    this->get_lenght() = arr.get_lenght();
    this->get_width() = arr.get_width();
    this->get_height() = arr.get_height();
    for(int x = 0; x < get_lenght(); ++x){
        for(int y = 0; y < get_width(); ++y){
            for(int z = 0; z < get_height(); ++z){
                (*this)[x][y][z] = arr[x][y][z] * 1;
            }
        }
    }

    return *this;
}

Array_3D operator- (Array_3D& lhs, Array_3D& rhs){
    Array_3D arr;
    arr.MakeArray(lhs.get_lenght(), lhs.get_width(), lhs.get_height());
    for(int x = 0; x < lhs.get_lenght(); ++x){
        for(int y = 0; y < lhs.get_width(); ++y){
            for(int z = 0; z < lhs.get_height(); ++z){
                arr[x][y][z] = lhs[x][y][z] - rhs[x][y][z];
            }
        }
    }

    return arr;
}

Array_3D operator+ (Array_3D& lhs, Array_3D& rhs){
    Array_3D arr;
    arr.MakeArray(lhs.get_lenght(), lhs.get_width(), lhs.get_height());
    for(int x = 0; x < lhs.get_lenght(); ++x){
        for(int y = 0; y < lhs.get_width(); ++y){
            for(int z = 0; z < lhs.get_height(); ++z){
                arr[x][y][z] = lhs[x][y][z] + rhs[x][y][z];
            }
        }
    }

    return arr;
}

Array_3D operator* (Array_3D& lhs, int rhs){
    Array_3D arr;
    arr.MakeArray(lhs.get_lenght(), lhs.get_width(), lhs.get_height());
    for(int x = 0; x < lhs.get_lenght(); ++x){
        for(int y = 0; y < lhs.get_width(); ++y){
            for(int z = 0; z < lhs.get_height(); ++z){
                arr[x][y][z] = lhs[x][y][z] * rhs;
            }
        }
    }

    return arr;
}

Array_3D Array_3D::MakeArray (size_t lenght_input, size_t width_input, size_t height_input){
    if(lenght_input <= 0 || width_input <= 0 || height_input <= 0){
        std::cerr<<"Incorrecteble input";
        exit(EXIT_FAILURE);
    }

    Array_3D arr;

    arr.get_lenght() = static_cast<int>(lenght_input);
    arr.get_width() = static_cast<int>(width_input);
    arr.get_height() = static_cast<int>(height_input);

    arr.get_arr() = new uint8_t[(lenght_input * width_input * height_input * kNumberSize) /
                               kByteSize + static_cast<int>((lenght_input * width_input * height_input * kNumberSize) % kByteSize)];

    return arr;
}
