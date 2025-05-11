#include "read_config.h"

config ReadConfig() {
    std::fstream file("config.txt");

    config info;

    char byte;

    std::string city;

    file.seekg(8, std::ios_base::beg);

    file.read(&byte, 1);

    while(byte != '\n') {
        if(byte == ' ') {
            info.Cities.push_back(city);
            city.resize(0);
        }
        city += byte;
        file.read(&byte, 1);
    }
    info.Cities.push_back(city);
    city.resize(0);


    while(!file.eof()) {
        file.read(&byte, 1);
        city += byte;
    }

    city.pop_back();

    city.erase(0, 6);

    info.Days = static_cast<int>(std::stoi(city));

    return info;
}