namespace FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;

/// <summary>
///     Update platform response status code.
/// </summary>
public enum UpdatePlatformByPlatformIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PLATFORM_IS_NOT_FOUND,
    PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED,
    PLATFORM_ALREADY_EXISTS
}
