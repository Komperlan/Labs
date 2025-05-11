#include "parse.h"
#include "cpr/cpr.h"
#include "json.hpp"


Coordinates GiveCoordinates(const std::string& city) {
    Coordinates position;
//    std::string API_key = "";
//
//    char kLongitudeSize = 12;
//
//    cpr::Response input = cpr::Get(cpr::Url("https://api.api-ninjas.com/v1/city"),cpr::Parameters{{"name", city}, {"X-Api-Key", API_key}} );
//
//    position.longitude = input.text.substr(input.text.find("longitude") + kLongitudeSize, 5);
//
//    position.latitude = input.text.substr(input.text.find("latitude") + kLongitudeSize - 1, 5);
//    std::cout<<input.text<<'\n';

// Я про*** все запросы апишки, поэтому эмулирую их

    if(city == "Petrozavodsk"){
        position.latitude = "61.7833";
        position.longitude = "34.35";
    }else{
        position.latitude = "55.7558";
        position.longitude = "37.6178";
    }

    return position;
}

void GiveForecast(Coordinates position, std::vector<Forecast>& info, int days){
    cpr::Response input = cpr::Get(cpr::Url("https://api.open-meteo.com/v1/forecast"),cpr::Parameters{ {"forecast_days", std::to_string(days)}, {"hourly", "temperature_2m"},
                                                                                                       {"hourly" ,"relative_humidity_2m"}, {"hourly" ,"visibility"} ,
                                                                                                       {"latitude", position.latitude}, {"longitude", position.longitude},
                                                                                                       {"hourly", "weather_code"}, {"hourly", "wind_direction_10m"}, {"hourly",
                                                                                                                                                                      "wind_speed_10m"}});

    using json = nlohmann::json;

    json file = json::parse(input.text);

    info.clear();

    info.resize(days * 4);

    std::vector<std::string> options = {"wind_speed_10m", "wind_direction_10m", "temperature_2m", "weather_code", "time"};


    for(int i = 0; i < info.size() * 6; i += 6) {
        for(auto s: options) {
            if(s == "wind_speed_10m") {
                info[i / 6].wind_speed = file["hourly"][s][i];
            } else if(s == "wind_direction_10m") {
                info[i / 6].wind_direction = file["hourly"][s][i];
            } else if(s == "temperature_2m") {
                info[i / 6].temperature = file["hourly"][s][i];
            } else if(s == "weather_code") {
                info[i / 6].weather_code = file["hourly"][s][i];
            } else if(s == "time") {
                info[i / 6].time = file["hourly"][s][i];
            }
        }
    }
}