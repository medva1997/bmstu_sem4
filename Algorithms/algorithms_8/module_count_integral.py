def integral_boole(a,b,E,f):
    Iold = boole(a,b,abs(a-b),f)
    Inew = boole(a,b,abs(a-b)/2,f)
    n = 2
    while abs(Inew - Iold) >= E:
        print(Inew - Iold)
        Iold = Inew
        n = 2*n
        h = abs(a-b)/n
        Inew = boole(a,b,h,f)
    return Inew


def integral_lrects(a,b,E,f):
    Iold = left(a,b,abs(a-b),f)
    Inew = left(a,b,abs(a-b)/2,f)
    n = 2
    while abs(Inew - Iold) >= E:
        print(Inew - Iold)
        Iold = Inew
        n = 2*n
        h = abs(a-b)/n
        Inew = left(a,b,h,f)
    return Inew


# left rects method
def left(a,b,h,f):
    s = 0
    i = a
    while i < (b-h/2):
        s += f(i)
        i += h
    return s*h


# Boole's rule
def boole(a,b,h,f):
    i = a; I = 0
    while i < (b-h/2):
        k = (h)/4
        I += (2*k/45*(7*f(i)+32*f(i+k)+12*f(i+2*k)+
                     32*f(i+3*k)+7*f(i+4*k)))
        i += h
    return I
