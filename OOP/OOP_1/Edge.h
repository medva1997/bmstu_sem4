#ifndef EDGE
#define EDGE

#include "FileWorker.h"

struct Edge {
    int FromPointId = 0;
    int ToPointId = 0;
};

void SetEdge(Edge &edge, int from, int to);

error_code LoadEdge( Edge &edge, STREAM *file_stream);

error_code LoadEdges(Edge *edge_array, int n, STREAM *file_stream);

#endif
