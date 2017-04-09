def Gauss( matrix,rowCount, colCount):
    mask =[i for i in range(0, colCount -1)]
    flag,matrix,mask=GaussDirectPass(matrix,mask,colCount,rowCount)
    if(flag==True):
        answer=GaussReversePass(matrix, mask, colCount, rowCount)
        return answer
    else:
        return None

def GaussDirectPass(matrix,mask,colCount,rowCount):

    for i in range(0,rowCount):
        maxId=i
        maxVal = matrix[i][i]
        for j in range(i+1,colCount-1):
            if(abs(maxVal)<abs(matrix[i][j])):
                maxVal = matrix[i][ j]
                maxId = j
        if (maxVal == 0):
            return False,matrix,mask
        if (i != maxId):
            for j in range(0,rowCount):
                matrix[j][ i], matrix[j][ maxId] = matrix[j][ maxId],matrix[j][ i]
            mask[i], mask[maxId] = mask[maxId],mask[i]
        for j in range(0, colCount):
            matrix[i][j] /= maxVal
        for j in range(0, rowCount):
            tempMn = matrix[j][ i]
            for k in range(0, colCount):
                matrix[j][ k] -= matrix[i][ k] * tempMn;
    return True,matrix,mask

def GaussReversePass(matrix, mask,colCount,rowCount):
    for i in range (rowCount-1,-1,-1):
        for j in range(i-1,-1,-1):
            tempMn = matrix[j][ i]
            for k in range(0,colCount):
                matrix[j][ k] -= matrix[i][ k] * tempMn;


    answer = [0 for i in range(0, rowCount)]
    for i in range(0, rowCount):
        answer[mask[i]] = matrix[i][colCount - 1]
    return answer



def gaus2(A,b):
    import numpy as np
    ITERATION_LIMIT = 1000
    x = np.zeros_like(b)
    for it_count in range(ITERATION_LIMIT):
        x_new = np.zeros_like(x)
        for i in range(A.shape[0]):
            s1 = np.dot(A[i, :i], x_new[:i])
            s2 = np.dot(A[i, i + 1:], x[i + 1:])
            x_new[i] = (b[i] - s1 - s2) / A[i, i]
        if np.allclose(x, x_new, rtol=1e-8):
            break
        x = x_new
    return  x