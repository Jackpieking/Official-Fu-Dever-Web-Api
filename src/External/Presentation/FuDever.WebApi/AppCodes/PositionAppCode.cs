using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent position entity api return code.
/// </summary>
internal abstract class PositionAppCode : BaseAppCode
{
    internal const int POSITION_IS_NOT_FOUND = 3;

    internal const int POSITION_ALREADY_EXISTS = 4;

    internal const int POSITION_IS_ALREADY_TEMPORARILY_REMOVED = 5;

    internal const int POSITION_IS_NOT_TEMPORARILY_REMOVED = 6;
}