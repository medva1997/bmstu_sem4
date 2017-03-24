#include "Point.h"
#include <QDebug>

//Set Point by x,y,z
void SetPoint(MyPoint &point, double x, double y, double z) {
    point.x = x;
    point.y = y;
    point.z = z;
}

//Get only one point
error_code ReadPoint(MyPoint &point, STREAM *file_stream) {
    double x, y, z;
    if (ReadPoint(&x, &y, &z, file_stream) != 3) {
        qDebug() << "Error read point. Please see point.cpp";
        return ERROR_FILE;
    }
    SetPoint(point, x, y, z);

    return ERROR_NO;
}

//Get points array
error_code ReadPoints(MyPoint *point_array, int n, STREAM *file_stream) {
    error_code error_flag = ERROR_NO;
    int i = 0;
    if (point_array == NULL)
        return ERROR_WITH_MEMORY;

    while (i < n && error_flag == ERROR_NO) {
        error_flag = ReadPoint(point_array[i],  file_stream);
        i++;
    }
    return error_flag;
}

double PointByName(const MyPoint point, int i) {
    switch (i) {

        case X_NAME:
            return point.x;

        case Y_NAME:
            return point.y;

        case Z_NAME:
            return point.z;

        default:
            return 0;
    }
}

void SetPointByName(MyPoint &point, int i, double value) {
    switch (i) {

        case X_NAME:
            point.x = value;
            break;

        case Y_NAME:
            point.y = value;
            break;

        case Z_NAME:
            point.z = value;
            break;

        default:
            break;
    }
}

