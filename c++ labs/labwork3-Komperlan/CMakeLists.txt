cmake_minimum_required(VERSION 3.00)
project(labwork3-Komperlan VERSION 0.1.0 LANGUAGES C CXX)

include_directories(src)

add_executable(labwork3-Komperlan bin/main.cpp
        bin/Sandbox.cpp
        src/main.h
        bin/bmp.cpp
        src/Sanbox.h
        src/bmp.h
        src/qeueue.h)
