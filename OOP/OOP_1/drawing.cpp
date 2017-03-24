#include "drawing.h"
#include "QDebug"


void DrawLine(drawing_act act, MyPoint S, MyPoint F) {

    QGraphicsScene *model_scene = GetScene(act);
    model_scene->addLine(PointByName(S, X_NAME), PointByName(S, Y_NAME),
                         PointByName(F, X_NAME), PointByName(F, Y_NAME));
}

void DrawEdge(drawing_act act,Edge edge,Points points)
{
    DrawLine(act, PointByIndex(points, edge.FromPointId - 1),
                  PointByIndex(points, edge.ToPointId - 1));
}

error_code DrawModel(const MyData &model, drawing_act act) {

    if (PointsN(model.Point_array) == 0) {
        return ERROR_ZERO;
    }

    if (EdgesN(model.Edge_array) == 0) {
        return ERROR_ZERO;
    }

    Clear_scene(act);


    for (int i = 0; i < EdgesN(model.Edge_array); i++) {
        DrawEdge(act,EdgeByIndex(model.Edge_array, i),model.Point_array);
    }

    return ERROR_NO;
}

void Clear_scene(drawing_act act) {
    GetScene(act)->clear();
}


QGraphicsScene *GetScene(drawing_act act) {
    return act.scene;
}



