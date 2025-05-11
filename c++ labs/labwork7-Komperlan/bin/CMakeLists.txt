add_executable(${PROJECT_NAME} main.cpp)

target_link_libraries(${PROJECT_NAME} PRIVATE parse)
target_link_libraries(${PROJECT_NAME} PRIVATE read_config)
target_link_libraries(${PROJECT_NAME} PRIVATE pictures)

target_include_directories(${PROJECT_NAME} PUBLIC ${PROJECT_SOURCE_DIR})


set(FETCHCONTENT_UPDATES_DISCONNECTED TRUE)
include(FetchContent)
FetchContent_Declare(cpr GIT_REPOSITORY https://github.com/libcpr/cpr.git
        GIT_TAG 1ad1cd2daf138aefdcdb1c35d99608387d230ec1)
FetchContent_MakeAvailable(cpr)