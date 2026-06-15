namespace DGII.API.Models;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    ApiError? Error
);

public record ApiError(string Message);
