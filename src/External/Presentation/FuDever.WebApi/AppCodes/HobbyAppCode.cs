using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent hobby entity api return code.
/// </summary>
internal abstract class HobbyAppCode : BaseAppCode
{
    internal const int HOBBY_IS_NOT_FOUND = 3;

    internal const int HOBBY_ALREADY_EXISTS = 4;

    internal const int HOBBY_IS_ALREADY_TEMPORARILY_REMOVED = 5;

    internal const int HOBBY_IS_NOT_TEMPORARILY_REMOVED = 6;
}