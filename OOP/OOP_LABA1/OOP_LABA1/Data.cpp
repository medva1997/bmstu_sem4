#include "Data.h"

using namespace System::Drawing;
using namespace std;

//Чтение точек из файла
int ReadPoints(FILE *file,MyPoint * point_array,int n)
{
	for (int i = 0; i < n; ++i)
	{
		if(fscanf(file,"[%lf,%lf,%lf]\n", &point_array[i].x,&point_array[i].y,&point_array[i].z)!=3)
		{
			return FAIL_READ;
		}
	}
	return OK;

}

//Чтение ребер из файла
int ReadEdges(FILE *file,Edge * edge_array,int n)
{
	for (int i = 0; i < n; ++i)
	{
		if(fscanf(file,"%d-%d", &edge_array[i].FromPointId,&edge_array[i].ToPointId)!=2)
		{
			return FAIL_READ;
		}
	}
	return OK;
}

//Загрузка файла
int LoadFile(char *filename,Data *datapack)
{
	int code_error=OK;

	FILE * file_in = fopen(filename, "r");
	if (file_in == 0)
			code_error=ERROR_OPEN_OUTPUT_FILE;

	if (!datapack)
		code_error=ERROR_MALLOC;

	//Чтение количества точек и вершин
	if(code_error==OK)
	{
		if((fscanf(file_in,"%d %d\n", &datapack->n_points,&datapack->n_edges)!=2))
			code_error=FAIL_READ;
	}

	if(code_error==OK)
	{
		datapack->Point_array=(MyPoint*) malloc(sizeof(MyPoint)*datapack->n_points);
		datapack->Edge_array=(Edge*) malloc(sizeof(Edge)*datapack->n_edges);
	}


	if (!datapack->Point_array)
		code_error=ERROR_MALLOC;
	if (!datapack->Edge_array)
		code_error=ERROR_MALLOC;

	//Чтение данных
	if(code_error==OK)
		code_error=ReadPoints(file_in,datapack->Point_array,datapack->n_points);

	if(code_error==OK)
		code_error=ReadEdges(file_in,datapack->Edge_array,datapack->n_edges);

	if (file_in)
	{
		fclose(file_in);
	}
	//Поск центра фигуры
	SearchCentre(datapack);

	return code_error;
}

//Освобождение ресурсов
void freedata(Data *datapack)
{
	if(datapack)
    {
        if(datapack->Point_array)
        {
            free(datapack->Point_array);
            datapack->Point_array=NULL;
        }

        if(datapack->Edge_array)
        {
            free(datapack->Edge_array);
            datapack->Edge_array=NULL;
        }
		if(datapack->DrawCentre)
		{
			delete datapack->DrawCentre;
			datapack->DrawCentre=NULL;
		}
    }

}

//выделение памяти и забивка нулями матрицы
double** allocate_matrix_solid(int n, int m)
{
    double **data =(double **)malloc(n * sizeof(double*) + n * m * sizeof(double));
    if (!data)
        return NULL;

    for (int i = 0; i < n; i++)
        data[i] = (double*)((char*) data +  n * sizeof(double*) + i * m * sizeof(double));


    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
            data[i][j]=0;
    }

    return data;
}

//Удаление матриц
struct Matrix * erase(struct Matrix *matrA)
{
    if(matrA)
    {
		if(matrA->matrix)
            free(matrA->matrix);
        matrA->matrix=NULL;
        free(matrA);
    }
    return NULL;
}

//Конвертация из точки в матрицу
struct Matrix* ConvertFromPoint(struct MyPoint* point)
{
    struct Matrix* matrC=(struct Matrix*) malloc(sizeof(struct Matrix));
    if(!matrC)
        return NULL;
    //TODO Вынести конствнты
    matrC->n=1;
    matrC->m=3;
    matrC->matrix=allocate_matrix_solid(matrC->n, matrC->m);
    matrC->matrix[0][0]=point->x;
    matrC->matrix[0][1]=point->y;
    matrC->matrix[0][2]=point->z;
    return matrC;
}

//Конвертация из матрицы в точку
struct MyPoint ConvertFromMatrix(struct Matrix* matr)
{
    struct MyPoint point;
    point.x=matr->matrix[0][0];
    point.y=matr->matrix[0][1];
    point.z=matr->matrix[0][2];
    return point;
}

//Умножение
struct Matrix* multiplication(const struct Matrix *matrA,const struct Matrix *matrB,int *codeerror)
{
    *codeerror=OK;

    struct Matrix* matrC= (struct Matrix*) malloc(sizeof(struct Matrix));
    if(!matrC)
        return NULL;

    if(matrA->m!=matrB->n)
    {
        *codeerror=ERROR_SIZE;
        //printf("Неверная размерность\n");
        return NULL;
    }

    matrC->n=matrA->n;
    matrC->m=matrB->m;
    matrC->matrix=allocate_matrix_solid(matrC->n, matrC->m);
    if(!matrC->matrix)
        return NULL;

    for (int i = 0; i < matrA->n; ++i)
    {
        for (int j = 0; j < matrB->m; ++j)
        {
            for (int l = 0; l < matrA->m; l++)
            {
                matrC->matrix[i][j]+=matrA->matrix[i][l]*matrB->matrix[l][j];
            }
        }
    }
    return  matrC;
}

//Поворот вокруг оси X
void TurnByX (double angle, struct Data *datapack)
{
    struct Matrix* matr=(struct Matrix*) malloc(sizeof(struct Matrix)); 

    //TODO Вынести конствнты
    matr->n=3;
    matr->m=3;
    matr->matrix=allocate_matrix_solid(matr->n, matr->m);

    matr->matrix[0][0]=1;
    matr->matrix[1][1]=cos(angle);
    matr->matrix[1][2]=-sin(angle);

    matr->matrix[2][1]=sin(angle);
    matr->matrix[2][2]=cos(angle);

    int codeerror=OK;
    for (int i = 0; i <datapack->n_points ; ++i) {
        struct Matrix *tempmatr=ConvertFromPoint(&datapack->Point_array[i]);
        struct Matrix *rez=multiplication(tempmatr,matr,&codeerror);
        datapack->Point_array[i]=ConvertFromMatrix(rez);
        rez=erase(rez);
        tempmatr=erase(tempmatr);
    }

    matr=erase(matr);
}

//Поворот вокруг оси Y
void TurnByY (double angle, struct Data *datapack)
{
    struct Matrix* matr=(struct Matrix*) malloc(sizeof(struct Matrix));
   
    matr->n=3;
    matr->m=3;
    matr->matrix=allocate_matrix_solid(matr->n, matr->m);

    matr->matrix[0][0]=cos(angle);
    matr->matrix[0][2]=sin(angle);
    matr->matrix[1][1]=1;
    matr->matrix[2][0]=-sin(angle);
    matr->matrix[2][2]=cos(angle);

    int codeerror=OK;
    for (int i = 0; i <datapack->n_points ; ++i) {
        struct Matrix *tempmatr=ConvertFromPoint(&datapack->Point_array[i]);
        struct Matrix *rez=multiplication(tempmatr,matr,&codeerror);
        datapack->Point_array[i]=ConvertFromMatrix(rez);
        rez=erase(rez);
        tempmatr=erase(tempmatr);
    }

    matr=erase(matr);
}

//Поворот вокруг оси Z
void TurnByZ (double angle, struct Data *datapack)
{
    struct Matrix* matr=(struct Matrix*) malloc(sizeof(struct Matrix));
   
    //TODO Вынести конствнты
    matr->n=3;
    matr->m=3;
    matr->matrix=allocate_matrix_solid(matr->n, matr->m);

    matr->matrix[0][0]=cos(angle);
    matr->matrix[0][1]=-sin(angle);
    matr->matrix[1][0]=sin(angle);
    matr->matrix[1][2]=cos(angle);
    matr->matrix[2][2]=1;


    int codeerror=OK;
    for (int i = 0; i <datapack->n_points ; ++i) {
        struct Matrix *tempmatr=ConvertFromPoint(&datapack->Point_array[i]);
        struct Matrix *rez=multiplication(tempmatr,matr,&codeerror);
        datapack->Point_array[i]=ConvertFromMatrix(rez);
        rez=erase(rez);
        tempmatr=erase(tempmatr);
    }

    matr=erase(matr);
}

//Отрисовка
void Draw( System::Drawing::Graphics^ g, Pen^ pen, Data *datapack)
{
	float dx=datapack->DrawCentre->X;
	float dy=datapack->DrawCentre->Y;
	for(int i=0; i<datapack->n_edges; i++)
	{
		
		int from=datapack->Edge_array[i].FromPointId-1;
		int to=datapack->Edge_array[i].ToPointId-1;
		PointF p1= PointF((float)(datapack->Point_array[from].x+dx),(float)(datapack->Point_array[from].y+dy));
		PointF p2=PointF((float)(datapack->Point_array[to].x+dx),(float)(datapack->Point_array[to].y+dy));
		g->DrawLine(pen,p1,p2);

	}
}

//Поиск центра
void SearchCentre(struct Data *datapack)
{
	MyPoint max=datapack->Point_array[0];
	MyPoint min=datapack->Point_array[0];

	for (int i = 0; i < datapack->n_points; i++)
	{
		min.x=std::min(datapack->Point_array[i].x,min.x);
		min.y=std::min(datapack->Point_array[i].y,min.y);
		min.z=std::min(datapack->Point_array[i].z,min.z);
		max.x=std::max(datapack->Point_array[i].x,max.x);
		max.y=std::max(datapack->Point_array[i].y,max.y);
		max.z=std::max(datapack->Point_array[i].z,max.z);
	}

	datapack->centre.x=(min.x+max.x)/2;
	datapack->centre.y=(min.y+max.y)/2;
	datapack->centre.z=(min.z+max.z)/2;

	for (int i = 0; i < datapack->n_points; i++)
	{
		datapack->Point_array[i].x=datapack->Point_array[i].x-datapack->centre.x;
		datapack->Point_array[i].y=datapack->Point_array[i].y-datapack->centre.y;
		datapack->Point_array[i].z=datapack->Point_array[i].z-datapack->centre.z;
	}

	
}