#ifndef POINT
#define POINT

#include "FileWorker.h"
#include "Error.h"


struct MyPoint {
    double x = 0;
    double y = 0;
    double z = 0;
};
enum coord_names {
    X_NAME, Y_NAME, Z_NAME
};

void SetPoint(MyPoint &point, double x, double y, double z);

error_code ReadPoint(MyPoint &point, STREAM *file_stream);

error_code ReadPoints(MyPoint *point_array, int n, STREAM *file_stream);

double PointByName(const MyPoint point, int i);

void SetPointByName(MyPoint &point, int i, double value);

#endif
