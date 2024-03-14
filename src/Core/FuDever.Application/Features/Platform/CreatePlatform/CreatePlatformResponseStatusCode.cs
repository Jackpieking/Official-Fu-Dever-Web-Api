namespace FuDever.Application.Features.Platform.CreatePlatform;

/// <summary>
///     Create platform response status code.
/// </summary>
public enum CreatePlatformResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED,
    PLATFORM_ALREADY_EXISTS
}
