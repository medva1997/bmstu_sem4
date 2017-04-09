"""
наилучшее среднеквадратичное приближениеи 
"""
from math import sin, pi, factorial, cos, exp, log

from collections import namedtuple
Table = namedtuple('Table', ['x','y', 'w']) # w = вес функции

eps_const = 0.00001
eps_otn = 0.0001
def fi(x, k):
    return x ** k

# Загрузка таблицы координат точек и их весов из файла
def get_table(filename):
    infile = open(filename, 'r')
    data = []
    for line in infile:
        if line:
            a, b, c = map(float, line.split())
            data.append(Table(a, b, c))
    print(data)
    infile.close()
    return data

# Вывод графика аппроксимирующей функции и исходных точек
def print_result(table, A, n):
    import numpy as np
    import matplotlib.pyplot as plt
    dx = 10
    if len(table) > 1:
        dx = (table[1].x - table[0].x)

    # построение аппроксимирующей функции    
    x = np.linspace(table[0].x - dx, table[-1].x + dx, 100)
    y = []
    for i in x:
        tmp = 0;
        for j in range(0, n + 1):
            tmp += fi(i, j) * A[j]
        y.append(tmp)

    plt.plot(x, y)

    #построение исходной таблицы
    x1 = [a.x for a in table]
    y1 = [a.y for a in table]


    plt.plot(x1, y1, 'kD', color = 'green', label = '$исходная таблица$')
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
    matrix = [[0 for i in range(0, n + 1)] for j in range (0, n + 1)]
    col = [0 for i in range(0, n + 1)]

    for m in range(0, n + 1):
        for i in range(0, N):
            tmp = table[i].w * fi(table[i].x, m)
            for k in range(0, n + 1):
                matrix[m][k] += tmp * fi(table[i].x, k)
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

# поиск столбца обратной матрицы
def find_col(a_copy, i_col):
    n = len(a_copy)
    a = [[a_copy[i][j] for j in range(0, n)] for i in range (0, n)]
    col = [0 for i in range(0, n)]
    for i in range(0, n):
        a[i].append(float(i == i_col))
    for i in range(0, n):
        if a[i][i] == 0: 
            for j in range(i + 1, n):
                if a[j][j] != 0:
                    a[i], a[j] = a[j], a[i]
        for j in range(i + 1, n):
            d = - a[j][i] / a[i][i]
            for k in range(0, n + 1):
                a[j][k] += d * a[i][k]
    for i in range(n - 1, -1, -1):
        res = 0
        for j in range(0, n):
            res += a[i][j] * col[j]
        col[i] = (a[i][n] - res) / a[i][i]
    return col

# получение обратной матрицы
def get_inverse_matrix(a):
    n = len(a)
    res = [[0 for i in range(0, n)] for j in range (0, n)]

    for i in range(0, n):
        col = find_col(a, i)
        for j in range(0, n):
            res[j][i] = col[j];
    return res;

# получение коэф. аппроксимирующей функции
def get_approx_coef(table, n):

    m, z = get_slau_matrix(table, n)

    inv = get_inverse_matrix(m)

    a = mult(z, inv)

    return a


table = get_table("table.txt")

n = int(input("Введите степень полинома n = "))

A = get_approx_coef(table, n)

print_result(table, A, n)
0
"""
наилучшее среднеквадратичное приближениеи 
"""
from math import sin, pi, factorial, cos, exp, log

from collections import namedtuple
Table = namedtuple('Table', ['x','y', 'w']) # w = вес функции

eps_const = 0.00001
eps_otn = 0.0001
def fi(x, k):
    return x ** k

# Загрузка таблицы координат точек и их весов из файла
def get_table(filename):
    infile = open(filename, 'r')
    data = []
    for line in infile:
        if line:
            a, b, c = map(float, line.split())
            data.append(Table(a, b, c))
    print(data)
    infile.close()
    return data

# Вывод графика аппроксимирующей функции и исходных точек
def print_result(table, A, n):
    import numpy as np
    import matplotlib.pyplot as plt
    dx = 10
    if len(table) > 1:
        dx = (table[1].x - table[0].x)

    # построение аппроксимирующей функции    
    x = np.linspace(table[0].x - dx, table[-1].x + dx, 100)
    y = []
    for i in x:
        tmp = 0;
        for j in range(0, n + 1):
            tmp += fi(i, j) * A[j]
        y.append(tmp)

    plt.plot(x, y)

    #построение исходной таблицы
    x1 = [a.x for a in table]
    y1 = [a.y for a in table]


    plt.plot(x1, y1, 'kD', color = 'green', label = '$исходная таблица$')
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
    matrix = [[0 for i in range(0, n + 1)] for j in range (0, n + 1)]
    col = [0 for i in range(0, n + 1)]

    for m in range(0, n + 1):
        for i in range(0, N):
            tmp = table[i].w * fi(table[i].x, m)
            for k in range(0, n + 1):
                matrix[m][k] += tmp * fi(table[i].x, k)
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

# поиск столбца обратной матрицы
def find_col(a_copy, i_col):
    n = len(a_copy)
    a = [[a_copy[i][j] for j in range(0, n)] for i in range (0, n)]
    col = [0 for i in range(0, n)]
    for i in range(0, n):
        a[i].append(float(i == i_col))
    for i in range(0, n):
        if a[i][i] == 0: 
            for j in range(i + 1, n):
                if a[j][j] != 0:
                    a[i], a[j] = a[j], a[i]
        for j in range(i + 1, n):
            d = - a[j][i] / a[i][i]
            for k in range(0, n + 1):
                a[j][k] += d * a[i][k]
    for i in range(n - 1, -1, -1):
        res = 0
        for j in range(0, n):
            res += a[i][j] * col[j]
        col[i] = (a[i][n] - res) / a[i][i]
    return col

# получение обратной матрицы
def get_inverse_matrix(a):
    n = len(a)
    res = [[0 for i in range(0, n)] for j in range (0, n)]

    for i in range(0, n):
        col = find_col(a, i)
        for j in range(0, n):
            res[j][i] = col[j];
    return res;

# получение коэф. аппроксимирующей функции
def get_approx_coef(table, n):

    m, z = get_slau_matrix(table, n)

    inv = get_inverse_matrix(m)

    a = mult(z, inv)

    return a


table = get_table("table.txt")

n = int(input("Введите степень полинома n = "))

A = get_approx_coef(table, n)

print_result(table, A, n)
