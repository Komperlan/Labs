#include "lib/read_config.h"
#include <lib/parse.h>
#include "lib/pictures.h"
#include "ftxui/component/component.hpp"
#include "ftxui/component/event.hpp"
#include "ftxui/component/screen_interactive.hpp"
#include "ftxui/dom/elements.hpp"

void HandleEvent(ftxui::Event event, Config& config) {
    if(event.input() == "w"){
        config.Days++;
    }else if(event.input() == "s") {
        config.Days--;
    }else if(event.input() == "a") {
        config.Cities.push_back(config.Cities.front());
        config.Cities.pop_front();
    }else if(event.input() == "d") {
        config.Cities.push_front(config.Cities.back());
        config.Cities.pop_back();
    }else if(event == ftxui::Event::Escape){
        exit(EXIT_SUCCESS);
    }
}

int main() {
    Config config = ReadConfig();

    std::vector<Forecast> info;

    using namespace ftxui;

    Event key;

    auto component = Renderer([&] {
        Elements horizontal;
        Elements vertical;

        HandleEvent(key, config);

        if(config.Days <= 1) {
            config.Days = 2;
        }
        if(config.Days > 5) {
            config.Days = 5;
        }

        Coordinates position = GiveCoordinates(config.Cities.front());

        GiveForecast(position, info, config.Days);

        vertical.push_back(flexbox( {
                                            bold(text(config.Cities.front())) | border | center
                                    } ));

        for(int i = 0; i < info.size(); i ++) {
            if(i % 4 == 0){;
                vertical.push_back(hbox(horizontal) | center);
                horizontal.clear();
            }
            horizontal.push_back(
                    vbox( {
                                  text(info[i].time) | border | center,
                                  hflow(paragraph(std::to_string(info[i].temperature) + " °C"))| center,
                                  hflow(paragraph(GiveWeatherType(info[i].weather_code)))| center | size(WIDTH, EQUAL, 25),
                                  hflow(paragraph(std::to_string(info[i].wind_speed) + " км/ч")) | center,
                                  hflow(paragraph(std::to_string(info[i].wind_direction) + " по азимуту")) | center,
                          } ) | border | center

            );
        }
        return vbox(vertical | center);
    });

    component |= CatchEvent([&](Event event) {
        key = event;

        return true;
    });

    auto screen = ScreenInteractive::TerminalOutput();
    screen.Loop(component);
}