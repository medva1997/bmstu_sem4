#include "geometry.h"
#include "Error.h"
#include <QTextStream>

double Max(const Points &points, int coord_name) {
    double max = PointByName(PointByIndex(points, 0), coord_name);

    for (int i = 1; i < PointsN(points); ++i) {
        double temp = PointByName(PointByIndex(points, i), coord_name);
        if (max < temp) {
            max = temp;
        }
    }
    return max;
}

double Min(const Points &points, int coord_name) {
    double min = PointByName(PointByIndex(points, 0), coord_name);

    for (int i = 1; i < PointsN(points); ++i) {
        double temp = PointByName(PointByIndex(points, i), coord_name);
        if (min > temp) {
            min = temp;
        }
    }
    return min;
}

MyPoint GetCentre(const Points &points) {
    double X_max = Max(points, X_NAME);
    double X_min = Min(points, X_NAME);

    double Y_max = Max(points, Y_NAME);
    double Y_min = Min(points, Y_NAME);

    double Z_max = Max(points, Z_NAME);
    double Z_min = Min(points, Z_NAME);

    MyPoint buff_centre;
    SetPoint(buff_centre, (X_max + X_min) / 2, (Y_max + Y_min) / 2, (Z_max + Z_min) / 2);
    return buff_centre;
}

error_code ChangePoints(Points &points, MyPoint &centre, action_values values) {
    if (PointsN(points) == 0) {
        return ERROR_ZERO;
    }

    CalculateCentre(centre, values);
    CalculateAllPoints(points, centre, values);
    CleanAction(values);

    return ERROR_NO;
}

void CalculateAllPoints(Points &points, MyPoint centre, action_values values) {
    for (int i = 0; i < PointsN(points); i++) {
        MyPoint point = PointByIndex(points, i);
        CalculatePoint(point, centre, values);

        SetPointByIndex(points, point, i);
        QTextStream(stdout) << point.x << " ; " << point.y << " ; " << point.z << endl;

    }
}
