#include "mypointmath.h"
#include "math.h"

void RotateX(MyPoint &point, double Alpha, MyPoint centre) {
    Alpha *= M_PI / 180;
    MyPoint buff = point;
    SetPointByName(point, X_NAME, centre.x + (buff.x - centre.x));
    SetPointByName(point, Y_NAME, centre.y + (buff.y - centre.y) * cos(Alpha) + (buff.z - centre.z) * sin(Alpha));
    SetPointByName(point, Z_NAME, centre.z + (buff.y - centre.y) * sin(-Alpha) + (buff.z - centre.z) * cos(Alpha));
}

void RotateY(MyPoint &point, double Alpha, MyPoint centre) {
    Alpha *= M_PI / 180;
    MyPoint buff = point;
    SetPointByName(point, X_NAME, centre.x + (buff.x - centre.x) * cos(Alpha) + (buff.z - centre.z) * sin(Alpha));
    SetPointByName(point, Y_NAME, centre.y + (buff.y - centre.y));
    SetPointByName(point, Z_NAME, centre.z + (buff.x - centre.x) * sin(-Alpha) + (buff.z - centre.z) * cos(Alpha));
}

void RotateZ(MyPoint &point, double Alpha, MyPoint centre) {
    Alpha *= M_PI / 180;
    MyPoint buff = point;
    SetPointByName(point, X_NAME, centre.x + (buff.x - centre.x) * cos(Alpha) + (buff.y - centre.y) * sin(-Alpha));
    SetPointByName(point, Y_NAME, centre.y + (buff.x - centre.x) * sin(Alpha) + (buff.y - centre.y) * cos(Alpha));
    SetPointByName(point, Z_NAME, centre.z + (buff.z - centre.z));
}

void MoveOX(MyPoint &point, double Move) {
    double X = PointByName(point, X_NAME);
    X += Move;
    SetPointByName(point, X_NAME, X);
}

void MoveOY(MyPoint &point, double Move) {
    double Y = PointByName(point, Y_NAME);
    Y += Move;
    SetPointByName(point, Y_NAME, Y);
}

void MoveOZ(MyPoint &point, double Move) {
    double Z = PointByName(point, Z_NAME);
    Z += Move;
    SetPointByName(point, Z_NAME, Z);
}

void Scale(MyPoint *point, double Scale, MyPoint centre) {
    SetPoint(*point, centre.x + (point->x - centre.x) * Scale,
             centre.y + (point->y - centre.y) * Scale,
             centre.z + (point->z - centre.z) * Scale);
}

