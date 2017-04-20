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
        return message.c_str();;
    }
    explicit MExceptionBase() = default;

    explicit MExceptionBase(const char* message)
            : message(message) {}

    explicit MExceptionBase(const std::string& message)
            : message(message) {}

    explicit MExceptionBase(const std::string& message, const std::string& add_message)
            : message(message), additional_message(add_message) {}

    explicit MExceptionBase(const char* message, const char* add_message)
            : message(message), additional_message(add_message) {}

protected:
    std::string message;
    std::string additional_message;
};

class MExcOutOfRange : public MExceptionBase
{
    //Iterator value
public:

    explicit MExcOutOfRange()
            : MExceptionBase("Out of range"){}

    explicit MExcOutOfRange(const std::string& add_message)
            : MExceptionBase("Out of range", add_message){}

};


class MExcMemoryAlloc : public MExceptionBase
{
public:
    explicit MExcMemoryAlloc()
            : MExceptionBase("Bad memory allocation"){}

    explicit MExcMemoryAlloc(const std::string& add_message)
            : MExceptionBase("Bad memory allocation", add_message){}
};

#endif //OOP_LABA_2_MEXCEPTION_H
