from interpolation import get_coefs, spline_interpolate
from mathmodule import *
from module_count_integral import integral_boole
from slau import solve_slau
from math import *
import numpy as np
import scipy.integrate as integrate

Bolcman = 8.613*10**(-5)

#45 низ
def count_k(T, E, dE, q):
    K = [0,0,0,0]
    for i in range(4):
        K[i] = (4.830*(10**(-3))*(q[i+1](T)/q[i](T))*(T**(1.5))*
                exp(-(E[i]-dE[i])*11603/T))
    return K

#Катя 44
def gamma_f(x,xe,T):
    z = [0,1,2,3,4]
    
    def gamma(root):
        s = 0
        for i in range(1,4,1):
            temp = exp(x[i])*z[i]*z[i]/(1 + z[i]*z[i]*root/2)
            s += temp
        return 5.87*(10**10)*1/(T**3)*(exp(xe)/(1+root/2) + s) - root*root
    
    min_b, max_b = find_borders_gamma(gamma)
    return halfs(min_b, max_b, 0.0001, gamma)

#Катя 46 самый низ
def find_dE(gamma,T):
    global Bolcman
    dE = [0,0,0,0]
    z = [0,1,2,3,4]
    half = gamma/2
    
    for i in range(4):
        dE[i] = Bolcman*T*log((1+z[i+1]*z[i+1]*half) *
                              (1 + half) /
                              (1+z[i]*z[i]*half))
    return dE

#Катя 45 самый низ
def find_alpha(gamma,T):
    return 0.285 * (10**(-11)) * ((gamma*T)**3)


def count_abs_for_lenear(res,old):
    answer = []
    for i in range(len(res)):
        answer.append(abs(res[i]/old[i]))

    return max(answer)


def solve_system(p, T, q, E):
    gamma = 0
    alpha = 0
    dE = [0,0,0,0]
    K = count_k(T,E,dE,q)

    # Part 1
    # x1 = 3*xe + x4 - ln(k3) - ln(k2) - ln(k1)
    # x2 = 2*xe + x4 - ln(k3) - ln(k2)
    # x3 = xe + x4 - ln(k3)

    def f1(xe, x5):
        x4 = xe + x5 - log(K[3])
        x3 = xe + x4 - log(K[2])
        x2 = xe + x3 - log(K[1])
        x1 = xe + x2 - log(K[0])
        return exp(x2) + 2*exp(x3) + 3*exp(x4) + 4*exp(x5) - exp(xe)


    def f2(xe, x5):
        x4 = xe + x5 - log(K[3])
        x3 = xe + x4 - log(K[2])
        x2 = xe + x3 - log(K[1])
        x1 = xe + x2 - log(K[0])
        return (exp(x1) + exp(x2) + exp(x3) +
                exp(x4) + exp(x5) + exp(xe) - 7242*p/T)

    xe,x5 = find_solution(f1,f2)
    x4 = xe + x5 - log(K[3])
    x3 = xe + x4 - log(K[2])
    x2 = xe + x3 - log(K[1])
    x1 = xe + x2 - log(K[0])

    # Part 2
    old_gamma = gamma
    EPS = 0.0001
    flag = True
    
    while abs(old_gamma - gamma) > EPS or flag:
        flag  = False
        old_gamma = gamma
        gamma = gamma_f([x1,x2,x3,x4,x5],xe,T)
        dE = find_dE(gamma,T)
        alpha = find_alpha(gamma,T)
        K = count_k(T,E,dE,q)
        # Linear process
        matr = [[-1,1,0,0,0,1],
                [0,-1,1,0,0,1],
                [0,0,-1,1,0,1],
                [0,0,0,-1,1,1],
                [0,1*exp(x2),2*exp(x3),3*exp(x4),4*exp(x5),(-1)*exp(xe)],
                [exp(x1),exp(x2),exp(x3),exp(x4),exp(x5),exp(xe)]]
        
        free_coef = [
                     -(xe + x2 - x1 - log(K[0])),
                     -(xe + x3 - x2 - log(K[1])),
                     -(xe + x4 - x3 - log(K[2])),
                     -(xe + x5 - x4 - log(K[3])),
                     -(2*exp(x2)+2*exp(x3)+3*exp(x4)+4*exp(x5)-exp(xe)),
                     -(exp(x1)+exp(x2)+exp(x3)+exp(x4)
                       +exp(x5)+exp(xe)-alpha-7242*p/T)
                     ]
        
        res = np.linalg.solve(matr,free_coef)
        
        while count_abs_for_lenear(res,[x1,x2,x3,x4,x5,xe]) > EPS:
            x1 += res[0]
            x2 += res[1]
            x3 += res[2]
            x4 += res[3]
            x5 += res[4]
            xe += res[5]
            matr = [[-1,1,0,0,0,1],
                    [0,-1,1,0,0,1],
                    [0,0,-1,1,0,1],
                    [0,0,0,-1,1,1],
                    [0,1*exp(x2),2*exp(x3),3*exp(x4),4*exp(x5),(-1)*exp(xe)],
                    [exp(x1),exp(x2),exp(x3),exp(x4),exp(x5),exp(xe)]]

            free_coef = [
                         -(xe + x2 - x1 - log(K[0])),
                         -(xe + x3 - x2 - log(K[1])),
                         -(xe + x4 - x3 - log(K[2])),
                         -(xe + x5 - x4 - log(K[3])),
                         -(2*exp(x2)+2*exp(x3)+3*exp(x4)+4*exp(x5)-exp(xe)),
                         -(exp(x1)+exp(x2)+exp(x3)+exp(x4)
                           +exp(x5)+exp(xe)-alpha-7242*p/T)
                         ]
            res = np.linalg.solve(matr,free_coef)
            
    # Getting result
    n1 = exp(x1)
    n2 = exp(x2)
    n3 = exp(x3)
    n4 = exp(x4)
    n5 = exp(x5)
    ne = exp(xe)
    
    return n1+n2+n3+n4+n5

# Вычисление температуры по формеле Катя 47
def T(z, T0, Tw, m):
    return T0 + (Tw - T0)*(z**m)


def find_pressure(p_start, T_start, q, E, T0, Tw, m):
    a = 0
    b = 1
    Eps = 0.00001
   #45 низ деленное на 2
    const = (p_start/T_start)*3621
    
    def f(z,p):
        return solve_system(p,T(z,T0,Tw,m),q,E) * z

    def f1(p):
        def ff(z):
            return f(z,p)
        return integrate.quad(ff,a,b)[0] - const
    
    a1 = 0
    b1 = 25
            
    return halfs(a1,b1,Eps,f1,0)


def main():
    Q1 = [1.0, 1.0, 1.0, 1.0001, 1.0025,
          1.0895, 1.6972, 3.6552, 7.6838]
    Q2 = [4.0, 4.0, 4.1598, 4.3006, 4.4392,
          4.6817, 4.9099, 5.2354, 5.8181]
    Q3 = [5.5, 5.5, 5.116, 5.9790, 6.4749,
          7.4145, 8.2289, 8.9509, 9.6621]
    Q4 = [11.0 for i in range(9)]
    Q5 = [15.0 for i in range(9)]
    T = [2000, 4000, 6000, 8000, 10000,
         14000, 18000, 22000, 26000]
    E = [12.13, 20.98, 31.00, 45.00]

    qc_1 = get_coefs(T, Q1)
    qc_2 = get_coefs(T, Q2)
    qc_3 = get_coefs(T, Q3)
    qc_4 = get_coefs(T, Q4)
    qc_5 = get_coefs(T, Q5)
    
    def q1(x):
        return np.interp(x=[x],xp=T,fp=Q1)[0]
        return spline_interpolate(x,qc_1)

    def q2(x):
        return np.interp(x=[x],xp=T,fp=Q2)[0]
        #return spline_interpolate(x,qc_2)

    def q3(x):
        return np.interp(x=[x],xp=T,fp=Q3)[0]
        #return spline_interpolate(x,qc_3)

    def q4(x):
        return np.interp(x=[x],xp=T,fp=Q4)[0]
        #return spline_interpolate(x,qc_4)

    def q5(x):
        return np.interp(x=[x],xp=T,fp=Q5)[0]
        #return spline_interpolate(x,qc_5)

    q = [q1,q2,q3,q4,q5]

    T0 = float(input("T0 = "))
    Tw = float(input("Tw = "))
    m = float(input("m = "))
    T_start = float(input("T начальное = "))
    p_start = float(input("p начальное = "))

    print(find_pressure(p_start, T_start, q, E, T0, Tw, m))


if __name__ == "__main__":
    main()
