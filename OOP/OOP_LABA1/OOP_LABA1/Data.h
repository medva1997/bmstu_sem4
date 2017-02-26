#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <math.h>
#include <algorithm>;

#pragma once
#define OK 0
#define FAIL_READ -1
#define ERROR_OPEN_OUTPUT_FILE -2
#define ERROR_MALLOC -3
#define ERROR_SIZE -4;
using namespace System::Drawing;

//стркутура точки
struct MyPoint
{
	double x;
	double y;
	double z;
};

//ребро
struct Edge
{
	int FromPointId; 	//откуда, индекс
	int ToPointId;		//куда, индекс
};

//’ранилище всех данных
struct  Data
{
	int n_points;
	struct MyPoint * Point_array;
	struct MyPoint min,max,centre;
	int n_edges;
	struct Edge * Edge_array;
	PointF* DrawCentre;
};

//ћатрицы дл€ умножени€
 struct Matrix
{
    int n;
    int m;
    double ** matrix;
	
};

int ReadPoints(FILE *file,MyPoint * point_array,int n);

int ReadEdges(FILE *file,Edge * edge_array,int n);

int LoadFile(char* filename,struct Data *datapack);

void freedata(struct Data *datapack);

double** allocate_matrix_solid(int n, int m);

Matrix * erase(Matrix *matrA);

Matrix* ConvertFromPoint(MyPoint* point);

MyPoint ConvertFromMatrix(Matrix* matr);

Matrix* multiplication(const Matrix *matrA,const Matrix *matrB,int *codeerror);

void TurnByX (double angle,  struct Data *datapack);

void TurnByY (double angle, struct Data *datapack);

void TurnByZ (double angle, struct Data *datapack);

void Draw( System::Drawing::Graphics^ g, Pen^ pen, struct Data *datapack);

void SearchCentre(struct Data *datapack);