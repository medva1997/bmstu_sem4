#ifndef GEOMETRY_H
#define GEOMETRY_H

#include "actiongeometry.h"
#include "Points.h"

double Max(const Points &points, int coord_name);

double Min(const Points &points, int coord_name);

MyPoint GetCentre(const Points &points);

error_code ChangePoints(Points &points, MyPoint &centre, action_values values);

void CalculateAllPoints(Points &points, MyPoint centre, action_values values);

#endif // GEOMETRY_H
