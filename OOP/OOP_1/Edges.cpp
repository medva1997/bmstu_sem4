#include "Edges.h"
void SetEdgesN(Edges& edges,int n)
{	
    edges.n=n;
}

void SetEdgesArray(Edges& edges,Edge * edge_array)
{	
    edges.edges_array=edge_array;
}

void SetEdges(Edges& edges,Edge * edge_array,int n)
{
	SetEdgesN(edges,n);
	SetEdgesArray(edges,edge_array);
}

int EdgesN(const Edges& edges)
{
    return edges.n;
}
Edge EdgeByIndex(const Edges& edge,int index)
{
    return edge.edges_array[index];
}


error_code LoadEdges(Edges& edges,int n, STREAM *file_stream)
{	
    Edge* ptr = NULL;

    if (!(ptr = (Edge*) AllocMemory(sizeof(Edge), n))) {
		ptr = NULL;		
		return ERROR_WITH_MEMORY;
	}
    error_code error_flag=LoadEdges(ptr, n, file_stream);
	if(error_flag!=ERROR_NO)
	{
		ClearMemory(ptr);
		return error_flag;
	}
    SetEdges(edges, ptr,n);

	return error_flag;
}

void ClearEdges(Edges &edges)
{
    ClearMemory(edges.edges_array);
	SetEdges(edges,NULL,0);
}

Edges InitEdges()
{
    Edges edges;
    edges.edges_array = NULL;
    edges.n = 0;
    return edges;
}
