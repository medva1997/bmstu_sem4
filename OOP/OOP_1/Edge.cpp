#include "Edge.h"

void SetEdge(Edge &edge, int from, int to) {
    edge.FromPointId = from;
    edge.ToPointId = to;
}

error_code LoadEdge(Edge &edge, STREAM *file_stream) {
    int from, to;
    if (ReadEdge(&from, &to, file_stream) != 2) {
        return ERROR_FILE;
    }
    SetEdge(edge, from, to);
    return ERROR_NO;
}

error_code LoadEdges(Edge *edge_array, int n, STREAM *file_stream) {
    error_code error_flag = ERROR_NO;
    int i = 0;
    if (edge_array == NULL)
        return ERROR_WITH_MEMORY;

    while (i < n && error_flag == ERROR_NO) {
        error_flag = LoadEdge(edge_array[i], file_stream);
        i++;
    }
    return error_flag;
}


