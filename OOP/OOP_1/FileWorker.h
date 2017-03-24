#ifndef FILE_WORKER
#define FILE_WORKER

#include <stdio.h>
#include "Error.h"

#define STREAM FILE


STREAM *OpenFile(char *filename);

void CloseFile(STREAM *file_stream);

int ReadEdge(int *from, int *to, STREAM *file_sream);

int ReadPoint(double *x, double *y, double *z, STREAM *file_sream);

int ReadCounts(int *n_points, int *n_edges, STREAM *file_stream);

#endif
