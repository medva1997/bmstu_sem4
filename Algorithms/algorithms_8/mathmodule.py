from math import *
from interpolation import *
from module_count_integral import *
import numpy as np

# Defining the root by half division method
def halfs(a,b,E,f,flag=0):
    eps = abs(a-b)*E
    x = (a+b)/2
    while abs(a-b) > eps:
        if flag:
            print(a, b)
        x = (a+b) / 2
        if f(x) < 0:
            a = x
        else:
            b = x
    return x


# Making y(x) from f(x,y) = 0
def upper(x,f,min_v, max_v):
    def ff(y):
        return f(x,y)
    cur = 0

    cur = halfs(min_v, max_v, 0.000001, ff)
    return cur


# Sorting x and y by y
def sort_y(x,y):
    st = [0, len(y)-1]
    while st != []:
        q = y[(st[0] + st[1]) // 2]       # Задаем опорный элемент
        i = st[0]
        j = st[1]
        # Заносим влево элементы меньше опорного, а вправо - больше
        while i <= j:
            while y[i] < q:
                i += 1
            while y[j] > q:
                j -= 1
            if i <= j:
                    y[i], y[j] = y[j], y[i]
                    x[i], x[j] = x[j], x[i]
                    i += 1
                    j -= 1
        # Запоминаем в стеке новые индексы границ,
        # Аналогично рекурсивному вызову функции
        if st[0] < j:
            st += [st[0],j]
        if i < st[1]:
            st += [i,st[1]]
        # Удаляем текущие границы, т.к. они уже не нужны
        st = st[2:]


# Finding borders for halfs, f1(x,y)
def find_borders(f1):
    min_v = 0
    max_v = 0
    if f1(1,0) < 0:
        min_v = 0
        if (f1(1,10)-f1(1,0)) > 0:
            v = 0
            while f1(1,v) < 0:
                v += 10
                max_v = v
        else:
            v = 0
            while f1(1,v) < 0:
                v -= 10
                max_v = v
    else:
        max_v = 0
        if (f1(1,10)-f1(1,0)) > 0:
            v = 0
            while f1(1,v) > 0:
                v -= 10
                min_v = v
        else:
            v = 0
            while f1(1,v) > 0:
                v += 10
                min_v = v
    return min_v, max_v


# Finding borders for halfs, f1(x)
def find_borders_gamma(f1):
    min_v = 0
    max_v = 0
    if f1(0) < 0:
        min_v = 0
        if (f1(1)-f1(0)) > 0:
            v = 0
            while f1(v) < 0:
                v += 1
                max_v = v
        else:
            v = 0
            while f1(v) < 0:
                v -= 1
                max_v = v
    else:
        max_v = 0
        if (f1(1)-f1(0)) > 0:
            v = 0
            while f1(v) > 0:
                v -= 1
                min_v = v
        else:
            v = 0
            while f1(v) > 0:
                v += 1
                min_v = v
    return min_v, max_v


# Solving the system of f1 = 0; f2 = 0
def find_solution(f1,f2):
    min1,max1 = find_borders(f1)
    def ff1(x):
        return upper(x,f1,min1,max1)
    min2,max2 = find_borders(f2)
    def ff2(x):
        return upper(x,f2,min2,max2)

    x = [i/10 for i in range(10, 20, 1)]
    y1 = [ff1(i) for i in x]

    y2 = [ff2(i) for i in x]
    
    y = [y1[i]-y2[i] for i in range(len(x))]

    sort_y(x,y)
    #res = interpolate(0,y,x,4,0)
    
    import numpy as np
    res = np.interp(x=[0],xp=y,fp=x)
    res = res[0]
    
    res_y = ff2(res)
    return res, res_y
