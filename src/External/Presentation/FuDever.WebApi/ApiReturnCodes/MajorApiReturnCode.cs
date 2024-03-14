using FuDever.WebApi.ApiReturnCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent major entity api return code.
/// </summary>
internal abstract class MajorApiReturnCode : BaseApiReturnCode
{
    internal const int MAJOR_IS_NOT_FOUND = 3;

    internal const int MAJOR_ALREADY_EXISTS = 4;

    internal const int MAJOR_IS_ALREADY_SOFT_REMOVED = 5;

    internal const int MAJOR_IS_NOT_SOFT_REMOVED = 6;
}