#include "myprocessor.h"

#include "Model.h"
#include "QDebug"


error_code processor(const argument arg, const action act) {
    static MyData mod = InitModel();
    error_code err = ERROR_NO;

    switch (act) {

        case LOAD:
            err = LoadModelProsessor(mod, arg);
            break;

        case CHANGE:
            err = ChangePointsProsessor(mod, arg);
            break;

        case DRAW:
            err = DrawProsessor(mod, arg);
            break;

        case CLEAR:
            err = ClearALLProsessor(mod);
            break;
    }
    return err;
}

