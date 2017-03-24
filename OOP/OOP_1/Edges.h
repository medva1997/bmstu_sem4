#ifndef EDGES
#define EDGES

#include "Edge.h"
#include "memory.h"

struct Edges {
    int n = 0;
    Edge *edges_array = NULL;
};

void SetEdgesN(Edges &edges, int n);

int EdgesN(const Edges &edges);

void SetEdgesArray(Edges &edges, Edge *edge_array);

void SetEdges(Edges &edges, Edge *edge_array, int n);

error_code LoadEdges(Edges &edges, int n, STREAM *file_stream);

void ClearEdges(Edges &edges);

Edge EdgeByIndex(const Edges &edge, int index);

Edges InitEdges();

#endif
