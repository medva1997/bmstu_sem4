#ifndef DRAWING_H
#define DRAWING_H

#include "geometry.h"

#include "Error.h"
#include <QGraphicsScene>
#include "Model.h"
#include "geometry.h"

typedef struct {
    QGraphicsScene *scene;
    int width;
    int height;
} drawing_act;

void DrawEdge(Edge edge,Points points);

void DrawLine(drawing_act act, MyPoint S, MyPoint F);

error_code DrawModel(const MyData &model, drawing_act act);

QGraphicsScene *GetScene(drawing_act act);

void Clear_scene(drawing_act act);

#endif // DRAWING_H
