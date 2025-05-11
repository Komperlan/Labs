cmake_minimum_required(VERSION 3.20)

project(
        labwork7
)

set(CMAKE_CXX_STANDARD 20)

if (WIN32) # Install dlls in the same directory as the executable on Windows
    set(CMAKE_LIBRARY OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR})
endif()

add_subdirectory(bin)
add_subdirectory(lib)

target_link_libraries(parse PRIVATE cpr::cpr)

include(FetchContent)
set(FETCHCONTENT_UPDATES_DISCONNECTED TRUE)
FetchContent_Declare(ftxui
        GIT_REPOSITORY https://github.com/ArthurSonzogni/ftxui
        GIT_TAG main # Important: Specify a version or a commit hash here.
)
FetchContent_MakeAvailable(ftxui)

target_link_libraries(labwork7
        PRIVATE ftxui::screen
        PRIVATE ftxui::dom
        PRIVATE ftxui::component # Not needed for this example.
)