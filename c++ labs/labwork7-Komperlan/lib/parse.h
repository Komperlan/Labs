#pragma once
#include "string"
#include "list"
#include "vector"

struct Forecast {
    std::string city;
    int temperature;
    int wind_direction;
    float wind_speed;
    int weather_code;
    std::string time;
    std::string visibility;
};

struct Coordinates {
    std::string latitude;
    std::string longitude;
};

Coordinates GiveCoordinates(const std::string& city);

void GiveForecast(Coordinates position, std::vector<Forecast>& info, int days);