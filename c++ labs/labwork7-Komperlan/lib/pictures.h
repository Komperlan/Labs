#pragma once
#include "string"
#include "vector"

struct WeatherType{
    int id;
    std::string name;
};

std::string GiveWeatherType(int id);