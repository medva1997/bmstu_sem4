#ifndef ACTION_H
#define ACTION_H

#include "Model.h"
#include "geometry.h"
#include "drawing.h"

enum action {
    LOAD,
    CHANGE,
    DRAW,
    CLEAR
};

typedef union {
    char *load_act;
    action_values modify_act;
    drawing_act draw_act;
} argument;


action_values *ModifyAct(argument *arg);

char *LoadAct(argument arg);

error_code DrawProsessor(MyData &model, argument arg);

error_code ChangePointsProsessor(MyData &model, argument arg);

error_code LoadModelProsessor(MyData &model, argument arg);

error_code ClearALLProsessor(MyData &model);

#endif // ACTION_H
