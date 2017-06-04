from math import *

def mul_col_matr(col, matr):
    n = len(col)
    res = [0 for i in range(n)]
    for i in range(n):
        for j in range(n):
            res[i] += col[j]*matr[i][j]
    return res


def mul_matr(first,other):
    if len(first[0]) != len(other):
        return None

    res = [[0 for i in range(len(other[0]))] for j in range(len(first))]

    for i in range(len(first)):
        for j in range(len(other[0])):
            s = 0.0
            for k in range(len(first[0])):
                s += first[i][k]*other[k][j]
                res[i][j] = s
    return res


def add_matr(first, other):
    if len(first) != len(other) or len(first[0]) != len(other[0]):
        return None

    res = [[0 for i in j] for j in other]
    for i in range(len(first)):
        for j in range(len(first[i])):
            res[i][j] = first[i][j]+other[i][j]

    return res


def minor(arr2,arr2_size,cur_i,cur_j):
    arr = [[0 for i in range(arr2_size-1)] for j in range(arr2_size-1)]

    i2 = 0
    for i in range(arr2_size):
        j2 = 0
        if i == cur_i:
            continue
        for j in range(arr2_size):
            if j == cur_j:
                continue
            arr[i2][j2] = arr2[i][j]
            j2 += 1
        i2 += 1

    return arr


def det(arr, n):
    A = 0
    k = 1
    if (n == 1):
        return arr[0][0]
    elif n == 2:
        return arr[0][0] * arr[1][1] - arr[0][1] * arr[1][0];
    else:
        for i in range(n):
            cur_minor = minor(arr, n, i,0)
            opr = det(cur_minor, n - 1)
            A += k * arr[i][0] * opr
            k = -k
        return A


def reverse(A, N):
    temp = 0.0

    E = [[0 for i in range(N)] for j in range(N)]

    for i in range(N):
        E[i][i] = 1.0

    for i in range(N):
        if A[i][i] == 0:
            for j in range(N):
                if A[j][i] != 0:
                    for k in range(N):
                        A[i][k] += A[j][k]
                        E[i][k] += E[j][k]
                    break
    
    for k in range(N):
        temp = A[k][k]
        for j in range(N):
            A[k][j] /= temp
            E[k][j] /= temp
        for i in range(k + 1, N):
            temp = A[i][k]
            for j in range(N):
                A[i][j] -= A[k][j] * temp
                E[i][j] -= E[k][j] * temp


    for k in range(N-1, 0, -1):
        for i in range(k-1, -1, -1):
            temp = A[i][k]

            for j in range(N):
                A[i][j] -= A[k][j] * temp
                E[i][j] -= E[k][j] * temp

    for i in range(N):
        for j in range(N):
            A[i][j] = E[i][j]


def invert(A):
    k = det(A, len(A))
    if abs(k) < 0.0001:
        print("Zero det")
        return None
    if len(A) == len(A[0]):
        C = [[i for i in j] for j in A]
        reverse(C, len(C))
        return C
    else:
        return None


def solve_slau(matr,free_coef):
    rev = invert(matr)
    solution = mul_col_matr(free_coef,rev)
    return solution
