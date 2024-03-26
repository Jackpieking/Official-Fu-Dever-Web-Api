using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent platform entity api return code.
/// </summary>
internal abstract class PlatformAppCode : BaseAppCode
{
    internal const int PLATFORM_IS_NOT_FOUND = 3;

    internal const int PLATFORM_ALREADY_EXISTS = 4;

    internal const int PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED = 5;

    internal const int PLATFORM_IS_NOT_TEMPORARILY_REMOVED = 6;
}