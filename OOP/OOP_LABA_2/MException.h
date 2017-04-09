//
// Created by alexey on 09.04.17.
//

#ifndef OOP_LABA_2_MEXCEPTION_H
#define OOP_LABA_2_MEXCEPTION_H
#include <exception>

class MExceptionBase : public std::exception
{
public:
    virtual const char* what() const throw()
    {
        return "Base exception";
    }
};

class MExcOutOfRange : public MExceptionBase
{
    //Iterator value
public:
    virtual const char* what() const throw()
    {
        return "Out of range";
    }
};
class MExcObjectPointer : public MExceptionBase
{
    //iterator equal
public:
    virtual const char* what() const throw()
    {
        return "Pointers to different objects";
    }
};

class MExcMemoryAlloc : public MExceptionBase
{
public:
    virtual const char* what() const throw()
    {
        return "Memory allocation error";
    }
};

#endif //OOP_LABA_2_MEXCEPTION_H
