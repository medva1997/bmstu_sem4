#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define PI 3.141592
#define EPS 0.00001


float F(float x) {
    return x * x;
}

float **allocate_memory(int n, int m) {

   
    float **data = (float **) calloc(n * sizeof(float *) + n * m * sizeof(float), 1);
    if (!data)
        return NULL;

    for (int i = 0; i < n; i++)
        data[i] = (float *) ((char *) data + n * sizeof(float *) + i * m * sizeof(float));

    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            data[i][j] = 0;
   
    return data;
}

void print_matrix(int n, int m, float **matrix) {

    for (int i = 0; i < n; i++) {

        for (int j = 0; j < m; j++) {
            if (matrix[i][j] - (int) matrix[i][j] == 0)
                printf("%8d", (int) matrix[i][j]);
            else
                printf(" %7.2f", matrix[i][j]);
        }

        printf("\n");
    }
}

//Source Matrix of x and y
float **generate_table(float begin, float end, float step, float(*F)(float)) {
    int n = (end - begin) / step;
    float **table = allocate_memory(n + 1, 2);

    float x = begin;
    int i = 0;
    while (x < end + step) {
        table[i][0] = x;
        table[i][1] = F(x);
        x += step;
        i++;
    }
    return table;
}

//Tmp table with dx dy
float **temporary_coef(float **table, int n) {
    float **Coef = allocate_memory(n + 1, 2);
    Coef[0][0] = 0;
    Coef[0][1] = 0;

    for (int i = 1; i < n + 1; i++) {
        float dx = table[i][0] - table[i - 1][0];
        float dy = (table[i][1] - table[i - 1][1]) / dx;

        Coef[i][0] = dx;
        Coef[i][1] = dy;
    }

    printf("\nTmp table with dx dy\n");
    print_matrix(n + 1, 2, Coef);

    return Coef;
}

void t(float **tab, int ii, float t1, float t2, float t3, float t4) {

    tab[ii][0] = t1;
    tab[ii][1] = t2;
    tab[ii][2] = t3;
    tab[ii][3] = t4;

}

float **slau_coef(float **table, int n) {
    float **Slau = allocate_memory(n + 1, 4);
    t(Slau, 0, 0, 0, 0, 0);
    t(Slau, 1, 0, 0, 0, 0);

    for (int i = 2; i < n + 1; i++) {
        float a = table[i - 1][0];
        float b = -2 * (table[i - 1][0] + table[i][0]);
        float c = table[i][0];
        float d = -3 * (table[i][1] - table[i - 1][1]);

        t(Slau, i, a, b, c, d);
    }

    printf("SLAU\n");
    print_matrix(n + 1, 4, Slau);
    return Slau;
}

float **progon_coef(float **table, int n) {
    float **Progon = allocate_memory(n + 1, 2);

    Progon[0][0] = 0;//A
    Progon[0][1] = 0;//B
    Progon[1][0] = 0;//D
    Progon[1][1] = 0;//F

    float eta = 0;
    float ksi = 0;

    if(n==1)
    {
        printf("PROGON\n");
        print_matrix(n + 1, 2, Progon);
        return Progon;
    }

    Progon[2][0] = eta;
    Progon[2][1] = ksi;
 
    for (int i = 2; i < n; i++) {
        //Formala 6, list2
        float znam = table[i][1] - table[i][0] * ksi;
        eta = (table[i][0] * eta + table[i][3]) / znam;
        ksi = table[i][2] / znam;

        Progon[i + 1][0] = eta;
        Progon[i + 1][1] = ksi;
    }

    printf("PROGON\n");
    print_matrix(n + 1, 2, Progon);

    return Progon;
}

//search a b c d by c
float **interp_coef(float **points, float **tmp, float *c, int n) {

    float **Interp = allocate_memory(n + 1, 4);
    t(Interp, 0, 0, 0, 0, 0);

    for (int i = 1; i < n + 1; i++) {

        float a = points[i - 1][1];    //formula a list 1

        float b = tmp[i][1];            //formula b list 1
        b -= tmp[i][0] * (c[i + 1] + 2 * c[i]) / 3;

        float d = (c[i + 1] - c[i]) / (3 * tmp[i][0]); //formula c list 1

        t(Interp, i, a, b, c[i], d);//save
    }
    printf("\nINTERP a b c d\n");
    print_matrix(n + 1, 4, Interp);
    return Interp;
}

float **interpolation(float **points, int n) {   

    float **tmp = temporary_coef(points, n);
    float **slau = slau_coef(tmp, n);
    float **progon = progon_coef(slau, n);

    float c[n + 2];
    //c[0] = 0;
    c[n + 1] = 0;
    if (n >= 2)
        c[n] = (-slau[n][3] - slau[n][0] * progon[n][0]) / (-slau[n][1] + slau[n][0] * progon[n][1]);
        
    else
        c[n] = 0;
   
    int ii = n - 1;
    
    for (int i = n; i > 0; i--) {
       
        c[ii] = progon[i][1] * c[ii + 1] + progon[i][0];
       
        ii--;
    }
    float **Interp = interp_coef(points, tmp, c, n);
    return Interp;
}

int binsearch(float x, float **points, int n) {
    int a = 0;
    int b = n;
    while (b - a > 1) {
        int m = (a + b) / 2;
        if (points[m][0] > x)
            b = m;
        else if (points[m][0] == x)
            return m;
        else
            a = m;
    }
    return a;
}

float getY(float x, float **points, float **interp, int n) {
    int i = binsearch(x, points, n) + 1;
    float dx = x - points[i - 1][0];
    float y = interp[i][0] + dx * (interp[i][1] + dx * (interp[i][2] + dx * interp[i][3]));// formaula 1 list 1
    return y;
}

int main(void) {
    float begin = -8.0;
    float end = -7.0;
    float step = 1;

    float **Points = generate_table(begin, end, step, F);
    int n = (end - begin) / step;
    printf("Source Matrix of x and y:\n");
    print_matrix(n + 1, 2, Points);

    float **Interp = interpolation(Points, n);
    float x;
    printf("Input x: ");
    scanf("%f", &x);
    float Y = getY(x, Points, Interp, n);
    printf("\nP(%.3f) = %f\n", x, Y);
    printf("F(%.3f) = %f\n", x, F(x));
    printf("|P-F| = %f\n", fabs(Y - F(x)));
}