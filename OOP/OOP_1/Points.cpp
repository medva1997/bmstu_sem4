#include "Points.h"
#include "QDebug"

void SetPointsN(Points &points, int n) {
    points.n = n;
}

int PointsN(const Points &points) {
    return points.n;
}

MyPoint PointByIndex(const Points &points, int index) {
    return points.points_array[index];
}

void SetPointByIndex(Points &points, const MyPoint &point, int index) {

    points.points_array[index].x = point.x;
    points.points_array[index].y = point.y;
    points.points_array[index].z = point.z;
}


void SetPointsArray(Points &points, MyPoint *point_array) {
    points.points_array = point_array;
}

void SetPoints(Points &points, MyPoint *point_array, int n) {
    SetPointsN(points, n);
    SetPointsArray(points, point_array);
}

//Load points
error_code LoadPoints(Points &points, int n, STREAM *file_stream ) {
    MyPoint *ptr = NULL;

    if (!(ptr = (MyPoint *) AllocMemory(sizeof(MyPoint), n))) {
        ptr = NULL;
        return ERROR_WITH_MEMORY;
    }
    error_code error_flag = ReadPoints(ptr, n, file_stream);
    if (error_flag != ERROR_NO) {
        ClearMemory(ptr);
        return error_flag;
    }
    SetPoints(points, ptr, n);
    return error_flag;
}

void ClearPoints(Points &points) {
    ClearMemory(points.points_array);
    SetPoints(points, NULL, 0);
}

Points InitPoints()
{
    Points points;
    points.points_array= NULL;
    points.n = 0;
    return points;
}
