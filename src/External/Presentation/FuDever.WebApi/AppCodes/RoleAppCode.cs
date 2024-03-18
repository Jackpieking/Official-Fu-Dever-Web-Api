using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent role entity api return code.
/// </summary>
internal abstract class RoleAppCode : BaseAppCode
{
    internal const int ROLE_IS_NOT_FOUND = 3;

    internal const int ROLE_ALREADY_EXISTS = 4;

    internal const int ROLE_IS_ALREADY_SOFT_REMOVED = 5;

    internal const int ROLE_IS_NOT_SOFT_REMOVED = 6;
}