from copy import deepcopy
import sys




def new_empty(n):
    """
    Создание новой матрицы
    """
    return [x[:] for x in [[0.0] * n] * n]


def pivotize(mat_a, x):
    """
    Путем обмена строк расположить наибольшие элементы на диагонали
    """
    mat_a = deepcopy(mat_a)
    size = len(mat_a)
    row = max(range(x, size), key=lambda i: abs(mat_a[i][x]))
    if x != row:
        mat_a[x], mat_a[row] = mat_a[row], mat_a[x]
    return mat_a


def invert(mat_a):
    """
    Обращение матрицы методом Гаусса-Жордана
    """
    mat_a = deepcopy(mat_a)
    n = len(mat_a)

    # Дополнить матрицу справа единичной матрицей
    for i in range(n):
        mat_a[i] += [int(i == j) for j in range(n)]

    # Прямой ход
    for x in range(n):
        mat_a = pivotize(mat_a, x)
        for i in range(x + 1, n):
            coefficient = mat_a[i][x] / mat_a[x][x]
            for j in range(x, n * 2):
                mat_a[i][j] -= coefficient * mat_a[x][j]

    # Обратный ход
    for x in reversed(range(n)):
        for i in reversed(range(x)):
            coefficient = mat_a[i][x] / mat_a[x][x]
            for j in reversed(range(n * 2)):
                mat_a[i][j] -= coefficient * mat_a[x][j]

    # Разделить строки на ведущие элементы
    for i in range(n):
        denominator = mat_a[i][i]
        for j in range(n * 2):
            mat_a[i][j] /= denominator

    # Оставить только правую часть матрицы
    for i in range(n):
        mat_a[i] = mat_a[i][n:]

    return mat_a

# умножение столбца на матрицу
def mult(col, b):
    n = len(col)
    c = [0 for j in range(0, n)]
    for j in range(0, n):
        for k in range(0, n):
            c[j] += col[k] * b[j][k]
    return c

def solve(mat_a, z):
    mat_a=invert(mat_a)
    c=mult(z, mat_a)
    print(c)
    return c

def bigsolve(a,b):
    from numpy.linalg import solve
    return solve(a,b)