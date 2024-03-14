namespace FuDever.Application.Features.Major.RestoreMajorByMajorId;

/// <summary>
///     Restore major by major id response status code.
/// </summary>
public enum RestoreMajorByMajorIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MAJOR_IS_NOT_TEMPORARILY_REMOVED,
    MAJOR_IS_NOT_FOUND
}
