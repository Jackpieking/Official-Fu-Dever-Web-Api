namespace FuDever.Application.Features.Major.RemoveMajorPermanentlyByMajorId;

/// <summary>
///     Remove major permanently by major Id response status code.
/// </summary>
public enum RemoveMajorPermanentlyByMajorIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MAJOR_IS_NOT_FOUND,
    MAJOR_IS_NOT_TEMPORARILY_REMOVED
}
