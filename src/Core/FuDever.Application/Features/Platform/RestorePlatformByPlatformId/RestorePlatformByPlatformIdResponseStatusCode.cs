namespace FuDever.Application.Features.Platform.RestorePlatformByPlatformId;

/// <summary>
///     Restore platform by platform id response status code.
/// </summary>
public enum RestorePlatformByPlatformIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PLATFORM_IS_NOT_TEMPORARILY_REMOVED,
    PLATFORM_IS_NOT_FOUND
}
