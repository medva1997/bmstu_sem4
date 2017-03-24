#include "Counts.h"

error_code GetCounts(int &n_points, int &n_edge, STREAM *file_stream) {
    if (ReadCounts(&n_points, &n_edge, file_stream) != 2) {
        return ERROR_FILE;
    }
    return ERROR_NO;
}
