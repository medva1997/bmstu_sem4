#include "actiongeometry.h"


double GetActionValue(actions action, action_values values) {
    switch (action) {
        case moveOX:
            return values.moveOX;

        case moveOY:
            return values.moveOY;

        case moveOZ:
            return values.moveOZ;

        case scale:
            return values.scale;

        case angleOX:
            return values.angleOX;

        case angleOY:
            return values.angleOY;

        case angleOZ:
            return values.angleOZ;

        default:
            return NULL;
    }
}

void SetAction(action_values &values, actions action, double value) {
    switch (action) {

        case moveOX:
            values.moveOX = value;
            break;

        case moveOY:
            values.moveOY = value;
            break;

        case moveOZ:
            values.moveOZ = value;
            break;

        case angleOX:
            values.angleOX = value;
            break;

        case angleOY:
            values.angleOY = value;
            break;

        case angleOZ:
            values.angleOZ = value;
            break;

        case scale:
            values.scale = value;
            break;

        default:
            break;
    }
}

void CleanAction(action_values &values) {
    SetAction(values, angleOX, 0);
    SetAction(values, angleOY, 0);
    SetAction(values, angleOZ, 0);
    SetAction(values, moveOX, 0);
    SetAction(values, moveOY, 0);
    SetAction(values, moveOZ, 0);
    SetAction(values, scale, 1);
}

void CalculatePoint(MyPoint &point, MyPoint centre, action_values values) {
    double value = 0;

    if ((value = GetActionValue(moveOX, values)) != 0) {
        MoveOX(point, value);
    }

    if ((value = GetActionValue(moveOY, values)) != 0) {
        MoveOY(point, value);
    }
    if ((value = GetActionValue(moveOZ, values)) != 0) {
        MoveOZ(point, value);
    }

    if ((value = GetActionValue(angleOX, values)) != 0) {
        RotateX(point, value, centre);
    }

    if ((value = GetActionValue(angleOY, values)) != 0) {
        RotateY(point, value, centre);
    }

    if ((value = GetActionValue(angleOZ, values)) != 0) {
        RotateZ(point, value, centre);
    }

    if ((value = GetActionValue(scale, values)) != 1) {
        Scale(&point, value, centre);
    }
}

void CalculateCentre(MyPoint &point, action_values values) {
    double value = 0;

    if ((value = GetActionValue(moveOX, values)) != 0) {
        MoveOX(point, value);
    }

    if ((value = GetActionValue(moveOY, values)) != 0) {
        MoveOY(point, value);
    }
    if ((value = GetActionValue(moveOZ, values)) != 0) {
        MoveOZ(point, value);
    }
}
