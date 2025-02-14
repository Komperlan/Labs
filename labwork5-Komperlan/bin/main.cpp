#include "iostream"
#include "lib/Array17_3D.h"
#include "string"
#include <sstream>

int main(){
    Array_3D arg;//Minecraft, bitch, is my life! Minecraaaaaaft!
    Array_3D arg2;

    arg = Array_3D::MakeArray(1, 5, 2);
    arg2 = Array_3D::MakeArray(1, 5, 4);
    arg[0][4][1] = 2;
    arg2[0][4][3] = 4;

    std::cout<<" "<<arg[0][4][1]<<" "<<arg2[0][4][3];
}