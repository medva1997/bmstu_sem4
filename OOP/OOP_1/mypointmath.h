#ifndef MYPOINTMATH_H
#define MYPOINTMATH_H

#include "Point.h"


void RotateX(MyPoint &point, double Alpha, MyPoint centre);

void RotateY(MyPoint &point, double Alpha, MyPoint centre);

void RotateZ(MyPoint &point, double Alpha, MyPoint centre);

void MoveOX(MyPoint &point, double Move);

void MoveOY(MyPoint &point, double Move);

void MoveOZ(MyPoint &point, double Move);

void Scale(MyPoint *point, double Scale, MyPoint centre);


#endif // MYPOINTMATH_H
