#ifndef POINTS
#define POINTS

#include "Point.h"
#include "memory.h"

struct Points {
    int n = 0;
    MyPoint *points_array = NULL;
};

void SetPointsN(Points &points, int n);

void SetPointsArray(Points &points, MyPoint *point_array);

void SetPoints(Points &points, MyPoint *point_array, int n);

void SetPointByIndex(Points &points,const MyPoint &point, int index);

error_code LoadPoints(Points &points, int n, STREAM *file_stream);

MyPoint PointByIndex(const Points &points, int index);

int PointsN(const Points &points);

void ClearPoints(Points &points);

Points InitPoints();

#endif
