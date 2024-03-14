namespace FuDever.Application.Features.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major id response status code.
/// </summary>
public enum UpdateMajorByMajorIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MAJOR_IS_NOT_FOUND,
    MAJOR_IS_ALREADY_TEMPORARILY_REMOVED,
    MAJOR_ALREADY_EXISTS
}
