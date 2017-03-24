#include "action.h"

action_values ModifyAct(argument &arg) {
    return arg.modify_act;
}

char *LoadAct(argument arg) {
    return arg.load_act;
}

drawing_act DrawAct(argument arg) {
    return arg.draw_act;
}

error_code DrawProsessor(MyData &model, argument arg) {
    return DrawModel(model, DrawAct(arg));
}

error_code ChangePointsProsessor(MyData &model, argument arg) {

    model.centre = GetCentre(model.Point_array);
    return ChangePoints(model.Point_array, model.centre, ModifyAct(arg));
}

error_code LoadModelProsessor(MyData &model, argument arg) {
    return LoadModel(model, LoadAct(arg));
}

error_code ClearALLProsessor(MyData &model) {
    error_code err = ERROR_NO;
    ClearModel(model);
    return err;
}




