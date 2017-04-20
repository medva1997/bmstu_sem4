cmake_minimum_required(VERSION 3.7)
project(OOP_LABA_2)

set(CMAKE_CXX_STANDARD 11)

set(SOURCE_FILES main.cpp BaseForList.cpp BaseForList.h MyList_code.h MyList.h MException.h base_iterator.h Iterators.h base_iterator_code.h iterators_code.h st.h Node.cpp Node.h)
add_executable(OOP_LABA_2 ${SOURCE_FILES})