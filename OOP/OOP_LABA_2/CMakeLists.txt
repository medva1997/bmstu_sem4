cmake_minimum_required(VERSION 3.7)
project(OOP_LABA_2)

set(CMAKE_CXX_STANDARD 11)

set(SOURCE_FILES main.cpp BaseForList.cpp BaseForList.h MyList.cpp MyList.h MyIterator.cpp MyIterator.h MException.h)
add_executable(OOP_LABA_2 ${SOURCE_FILES})