# module of interpolation

"""
This function counts the different difference
@ x - table of x values
@ y - table of y values
"""
def difdif(x,y):
    if (len(y) != len(x)):
        print('Unappropriate x and y size in difdif.')
        return 0
    
    if len(x) < 2:
        print('difdif error')
        return 0
    
    if len(x) == 2:
        res = (y[0]-y[1])/(x[0]-x[1])
        return res
    else:
        return (difdif(x[:-1],y[:-1]) - difdif(x[1:],y[1:]))/(x[0]-x[len(x)-1])


"""
Finds the position for interpolation in list of x values
@ x - value
@ xt - list of values
"""
def find_pos(x, xt):
    i = 0
    while (x > xt[i] and i < len(xt)-1):
        i += 1
    return i


"""
Make choice for interpolation
@ pos - start position
@ x - list of x values
@ y - list of y values
@ n - polynom degree
"""
def make_choice(pos, x, n):
    if n > len(x):
        print("Make_choice error")
        return 0,0
    
    left, right = pos, pos
    r, l = n // 2, n // 2
    j = n % 2
    
    if left - l < 0:
        r = r - left + l
        l = 0

    if right + r >= len(x):
        l = l + right + r - len(x) + 1
        r = len(x) - 1

    left -= l
    right += r
    
    if j == 1:
        if left - 1 >= 0:
            left = left - 1
        else:
            right += 1
    
    return (int(left),int(right+1))


"""
The function makes interpolation on y(x)
@ x - value
@ xt - table of x values
@ yt - table of y values
@ n - polynom degree
"""
def interpolate(x,xt,yt,n,flag = 0):
    if len(xt) != len(yt):
        print("Incorrect input values.")
        return 0
    
    pos = find_pos(x,xt)
    l, r = make_choice(pos, xt, n)
    work_x, work_y = xt[l:r], yt[l:r]
    
    if flag:
        print('|','{:^10}'.format('x'),'|','{:^10}'.format('y'),'|')
        for i in range(len(work_x)):
            print('|','{:^10.4g}'.format(work_x[i]),
                  '|','{:^10.4g}'.format(work_y[i]),'|')
        
    cur = 0
    tmp = 1
    res = work_y[0]
    
    while cur < n:
        cur += 1
        tmp *= x - work_x[cur-1]
        res += tmp*difdif(work_x[:cur+1],work_y[:cur+1])

    return res


"""
The function makes interpolation on z(x,y)
@ x - value
@ y - value
@ xt - table of x values
@ yt - table of y values
@ zt - matrix of z values
@ nx, ny - polynom degrees
"""
def double_interpolate(x,y,xt,yt,zt,nx,ny):
    if nx > len(xt)-1:
        nx = len(xt)-1
    if ny > len(yt)-1:
        ny = len(yt)-1
        
    xpos, ypos = find_pos(x,xt), find_pos(y,yt)

    lx,rx = make_choice(xpos,xt,nx)
    ly,ry = make_choice(ypos,yt,ny)

    work_x, work_y = xt[lx:rx], yt[ly:ry]
    work_z = []
    
    for i in range(len(work_x)):
        w_z = zt[lx+i][ly:ry]
        
        cur_z = interpolate(y,work_y,w_z,ny,0)
        work_z.append(cur_z)
    
    res = interpolate(x, work_x, work_z, nx, 0)
    return res


# For spline interpolation
def get_coefs(xt,yt):
    n = len(xt)
    spl = [[yt[i],0.0,0.0,0.0,xt[i]] for i in range(n)] # a, b, c, d, x
    
    alpha = [0.0 for i in range(n)] # ksi
    beta = [0.0 for i in range(n)] # eta

    # Progon method
    for i in range(1,n - 1):
        hi_cur = xt[i] - xt[i - 1]                                               # A
        hi_next = xt[i + 1] - xt[i]                                              # D
        C = 2.0 * (hi_cur + hi_next)                                             # B

        F = 6.0 * ((yt[i + 1] - yt[i]) / hi_next - (yt[i] - yt[i - 1]) / hi_cur)
        Z = (hi_cur * alpha[i - 1] + C)
        alpha[i] = -hi_next / Z
        beta[i] = (F - hi_cur * beta[i - 1]) / Z 

        if (i == n - 1):
            spl[n - 1][2] = ((F - hi_cur * beta[n - 2]) /
                             (C + hi_cur * alpha[n - 2]))
    
    # Going back in progon method
    for i in range(n - 2, 0, -1):
        spl[i][2] = alpha[i] * spl[i + 1][2] + beta[i]

    for i in range(n - 1, 0, -1):
        hi_cur = xt[i] - xt[i - 1]
        spl[i][3] = (spl[i][2] - spl[i - 1][2]) / hi_cur
        spl[i][1] = (hi_cur * (2. * spl[i][2] + spl[i - 1][2]) / 6.
                     + (yt[i] - yt[i - 1]) / hi_cur)
    return spl


# Spline interpolation
def spline_interpolate(x,coefs):
    n = len(coefs)
    
    if (n == 0):
        return 0
    elem = 0

    if (x <= coefs[0][4]):
        elem = coefs[0]
    elif (x >= coefs[n - 1][4]):
        elem = coefs[n - 1]
    else:
        # Bin search
        j = n - 1
        i = 0
        while (i + 1 < j):
            k = int((i + j) / 2)
            if (x <= coefs[k][4]):
                j = k
            else:
                i = k
        
        elem = coefs[j]

    dx = (x - elem[4])
    return elem[0] + (elem[1] + (elem[2] / 2 + elem[3] * dx / 6.0) * dx ) * dx


# Tests (TODO)
def test_difdif():
    ACC = 0.001
    x = [1, 2, 3]
    y = [0.5, 0.866, 1]

    print("Test of difdif:")
    
    if abs(difdif(x,y)+0.116) < ACC: print("OK")
    else: print("FALSE")


def test_interpolate():
    ACC = 0.001
    x = [-1, 0, 1, 2, 3]
    y = [-0.5, 0, 0.5, 0.866, 1]
    
    print("Test of interpolate:")
    
    if abs(interpolate(1.5, x, y, 2,0) - 0.712) < ACC: print("OK")
    else: print("FALSE")

    if abs(interpolate(1.5, x, y, 3,0) - 0.706) < ACC: print("OK")
    else: print("FALSE")


def test_double_int():      # TODO
    print("OK")


if __name__ == "__main__":
    test_difdif()
    test_interpolate()
    test_double_int()
