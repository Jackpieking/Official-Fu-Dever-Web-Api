using WebApi.ApiReturnCodes.Base;

namespace WebApi.ApiReturnCodes;

/// <summary>
///     Represent hobby entity api return code.
/// </summary>
internal abstract class HobbyApiReturnCode : BaseApiReturnCode
{
    internal const int HOBBY_IS_NOT_FOUND = 3;

    internal const int HOBBY_ALREADY_EXISTS = 4;

    internal const int HOBBY_IS_ALREADY_SOFT_REMOVED = 5;

    internal const int HOBBY_IS_NOT_SOFT_REMOVED = 6;
}