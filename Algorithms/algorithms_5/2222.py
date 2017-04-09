"""
наилучшее среднеквадратичное приближениеи
"""
from math import sin, pi, factorial, cos, exp, log
import Gauss


import ggg

from collections import namedtuple
Table = namedtuple('Val', ['x','y', 'w']) # w = вес функции

eps_const = 0.00001
eps_otn = 0.0001
def F(x, k):
    return x ** k

# Загрузка таблицы координат точек и их весов из файла
def get_table(filename):
    infile = open(filename, 'r')
    data = []
    for line in infile:
        if line:
            a, b, c = map(float, line.split())
            data.append(Table(a, b, c))
        print(data[-1])
    infile.close()
    return data

# Вывод графика аппроксимирующей функции и исходных точек
def print_result(table, A, n):
    import numpy as np
    import matplotlib.pyplot as plt
    #шаг для точек графика апроксимации
    dx = 10
    if len(table) > 1:
        dx = (table[1].x - table[0].x)

    # построение аппроксимирующей функции
    x = np.linspace(table[0].x - dx, table[-1].x + dx, 100)
    y = []
    for i in x:
        tmp = 0;
        for j in range(0, n + 1):
            tmp += F(i, j) * A[j]
        y.append(tmp)

    plt.plot(x, y)

    #Вывод точек из файла
    x1 = [a.x for a in table]
    y1 = [a.y for a in table]
    plt.plot(x1, y1, 'kD', color = 'black', label = '$исходные значения$')
    plt.grid(True)
    plt.legend(loc = 'best')
    miny = min(min(y), min(y1))
    maxy = max(max(y), max(y1))
    dy = (maxy - miny) * 0.03
    plt.axis([table[0].x - dx, table[-1].x + dx, miny - dy, maxy + dy])

    plt.show()
    return



# получение СЛАУ по исходным данным для заданной степени
# возвращает матрицу коэф. и столбец свободных членов
def get_slau_matrix(table, n):
    N = len(table)
    #Пустая матрица
    matrix = [[0 for i in range(0, n + 1)] for j in range (0, n + 1)]
    # Пустая колонка
    col = [0 for i in range(0, n + 1)]
    for m in range(0, n + 1):
        for i in range(0, N):
            tmp = table[i].w * F(table[i].x, m)
            for k in range(0, n + 1):
                matrix[m][k] += tmp * F(table[i].x, k)
            col[m] += tmp * table[i].y
    return matrix, col

# умножение столбца на матрицу
def mult(col, b):
    n = len(col)
    c = [0 for j in range(0, n)]
    for j in range(0, n):
        for k in range(0, n):
            c[j] += col[k] * b[j][k]
    return c

def get_inverse_matrix1(a):
    from numpy.linalg import inv
    return  inv(a)

# получение коэф. аппроксимирующей функции
def get_approx_coef(table, n):
    m, z = get_slau_matrix(table, n)
    inv1 = get_inverse_matrix1(m)
    a = mult(z, inv1)
    return a


def FillSlauMatrix(points, n):
    matrix = []
    for i in range(0, n):
        temparr = []
        for j in range(0, n+1):
            temparr.append(0)
        matrix.append(temparr)
    for i in range(0, n):
        for j in range(0, n):
            sumA = 0
            sumB = 0
            for k in range(0, len(points)):
                sumA += points[k].w * F(points[k].x, i) * F(points[k].x, j)
                sumB += points[k].w * points[k].y * F(points[k].x, j)
            matrix[i][j] = sumA
            matrix[j][n] = sumB
    return matrix




table = get_table("table.txt")
print("Считано точек: "+ str(len(table)))
n = int(input("Cтепень полинома n = "))
if(n<0):
    exit(0)
matrix,col=get_slau_matrix(table, n)
if(n>=len(table)):
    #print("Может выйти что-то странное")
    A = ggg.bigsolve(matrix,col)
else:
    A = ggg.solve(matrix, col)




#A =Gauss.gaus2(matrix,col)
#A = Gauss.Gauss(matrix,n+1,n+2)
#A=get_approx_coef(table, n)
print(A)
print_result(table, A, n)


