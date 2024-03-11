namespace Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;

/// <summary>
///     Response status code for remove platform permanently by platform id.
/// </summary>
public enum RemovePlatformPermanentlyByPlatformIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PLATFORM_IS_NOT_FOUND,
    PLATFORM_IS_NOT_TEMPORARILY_REMOVED
}
