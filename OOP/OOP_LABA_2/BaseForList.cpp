//
// Created by alexey on 01.04.17.
//

#include "BaseForList.h"

using namespace laba::laba_core;

BaseForList::BaseForList()
        : element_count(0) {}

BaseForList::~BaseForList()
{
    this->element_count = 0;
}

bool BaseForList::is_empty() const
{
    return element_count == 0;
}

size_t BaseForList::size() const
{
    return element_count;
}