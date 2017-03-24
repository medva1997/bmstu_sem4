#ifndef ACTIONGEOMETRY_H
#define ACTIONGEOMETRY_H

#include "mypointmath.h"

typedef struct {
    double moveOX;
    double moveOY;
    double moveOZ;
    double scale;
    double angleOX;
    double angleOY;
    double angleOZ;
} action_values;

enum actions {
    moveOX, moveOY, moveOZ, scale, angleOX, angleOY, angleOZ
};

void CalculatePoint(MyPoint &point, MyPoint centre, action_values values);

void CleanAction(action_values &values);

void SetAction(action_values &values, actions action, double value);

double GetActionValue(actions action, action_values values);

void CalculateCentre(MyPoint &point, action_values values);

#endif // ACTIONGEOMETRY_H
