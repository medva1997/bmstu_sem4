#include "Model.h"

void SetModelPoints(MyData &Datapack, Points points) {

    Datapack.Point_array = points;
}

void SetModelEdges(MyData &Datapack, Edges edges) {
    Datapack.Edge_array = edges;
}

void SetModel(MyData &Datapack, Points points, Edges edges) {
    SetModelPoints(Datapack, points);
    SetModelEdges(Datapack, edges);
}

void ClearModel(MyData &model) {
    ClearEdges(model.Edge_array);
    ClearPoints(model.Point_array);
}

error_code LoadModelStream(MyData &datapack, STREAM *file_stream) {
    Points points;
    Edges edges;
    error_code error_flag = ERROR_NO;

    int n_points = 0;
    int n_edges = 0;
    if ((error_flag = GetCounts(n_points, n_edges, file_stream)) != ERROR_NO) {
        return error_flag;
    }

    if ((error_flag = LoadPoints(points, n_points, file_stream)) != ERROR_NO) {
        return error_flag;
    }

    if ((error_flag = LoadEdges(edges, n_edges, file_stream)) != ERROR_NO) {
        ClearPoints(points);
        return error_flag;
    }

    SetModel(datapack, points, edges);

    return error_flag;
}


error_code LoadModel(MyData &datapack, char *filename) {
    STREAM *file_stream = OpenFile(filename);

    if (!(file_stream)) {
        return ERROR_FILE;
    }

    error_code error_flag = ERROR_NO;
    MyData temp_model;

    if ((error_flag = LoadModelStream(temp_model, file_stream)) != ERROR_NO) {
        CloseFile(file_stream);
        return error_flag;
    }

    CloseFile(file_stream);
    ClearModel(datapack);
    datapack = temp_model;

    return error_flag;

}

MyData InitModel() {
    MyData model;
    model.Point_array = InitPoints();
    model.Edge_array = InitEdges();
    return model;
}
