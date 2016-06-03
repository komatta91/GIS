cmake_minimum_required(VERSION 3.3)
project(GraphGenerator)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11 -static-libgcc -static-libstdc++")


set(CORE_MAJOR_VERSION 1)
set(CORE_MINOR_VERSION 0)
set(CORE_PATCH_VERSION 0)
set(CORE_VERSION
        ${CORE_MAJOR_VERSION}.${CORE_MINOR_VERSION}.${CORE_PATCH_VERSION})

configure_file(include/Core.h.in "${CMAKE_CURRENT_SOURCE_DIR}/include/Core.h")


SET(HEADER_FILES
        include/GraphGenerator.h
        include/Graph.h)

set(SOURCE_FILES
        src/Main.cpp
        src/GraphGenerator.cpp
        src/Graph.cpp src/Vertex.cpp include/Vertex.h)


set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${PROJECT_SOURCE_DIR}/bin)


set(BOOST_ROOT "E:/Programowanie/CLion/Middleware/boost_1_60_0")

set (Boost_NO_SYSTEM_PATHS ON)
set (Boost_USE_MULTITHREADED ON)
set (Boost_USE_STATIC_LIBS ON)
set (Boost_USE_STATIC_RUNTIME OFF)
set (BOOST_ALL_DYN_LINK OFF)

find_package(Boost REQUIRED COMPONENTS program_options filesystem system)
if(Boost_FOUND)
    include_directories (SYSTEM ${Boost_INCLUDE_DIR})
endif()

include_directories(include)

add_executable(GraphGenerator ${SOURCE_FILES})
target_link_libraries(GraphGenerator ${Boost_LIBRARIES})




