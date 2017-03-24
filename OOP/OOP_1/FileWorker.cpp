#include "FileWorker.h"

STREAM *OpenFile(char *filename) {
    return fopen(filename, "r");;
}

void CloseFile(STREAM *file_stream) {
    fclose(file_stream);
}

int ReadEdge(int *from, int *to, STREAM *file_sream) {
    return fscanf(file_sream, "%d-%d", from, to);
}

int ReadPoint(double *x, double *y, double *z, STREAM *file_stream) {
    return fscanf(file_stream, "[%lf;%lf;%lf]\n", x, y, z);
}

int ReadCounts(int *n_points, int *n_edges, STREAM *file_stream) {
    return fscanf(file_stream, "%d %d\n", n_points, n_edges);
}
