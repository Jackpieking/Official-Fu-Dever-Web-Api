namespace FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId;

/// <summary>
///     Remove major temporarily by major id response status code.
/// </summary>
public enum RemoveMajorTemporarilyByMajorIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MAJOR_IS_ALREADY_TEMPORARILY_REMOVED,
    MAJOR_IS_NOT_FOUND
}
