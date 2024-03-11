namespace Application.Features.Platform.RemovePlatformTemporarilyByPlatformId;

/// <summary>
///     Remove platform temporarily by platform id response status code.
/// </summary>
public enum RemovePlatformTemporarilyByPlatformIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED,
    PLATFORM_IS_NOT_FOUND
}
