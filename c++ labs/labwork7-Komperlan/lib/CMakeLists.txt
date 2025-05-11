add_library(parse parse.cpp parse.h
        json.hpp)
add_library(read_config read_config.cpp read_config.h)
add_library(pictures pictures.cpp pictures.h)

FetchContent_Declare(ftxui
        GIT_REPOSITORY https://github.com/ArthurSonzogni/ftxui
        GIT_TAG f495ce029cb3a9b4c8309e4456e494f8b6b09967
)


