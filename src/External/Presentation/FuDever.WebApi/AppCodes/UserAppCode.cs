using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent user entity api return code.
/// </summary>
internal abstract class UserAppCode : BaseAppCode
{
    internal const int USER_EMAIL_IS_NOT_FOUND = 3;

    internal const int USER_ACCOUNT_IS_NOT_APPROVED = 4;

    internal const int USER_IS_NOT_FOUND = 5;

    internal const int USER_WITH_EMAIL_ALREADY_EXISTS = 6;

    internal const int USER_WITH_PHONE_NUMBER_ALREADY_EXISTS = 7;

    internal const int DEPARTMENT_IS_NOT_FOUND = 8;

    internal const int MAJOR_IS_NOT_FOUND = 9;

    internal const int POSITION_IS_NOT_FOUND = 10;

    internal const int PLATFORM_IS_NOT_FOUND = 11;

    internal const int USER_PLATFORM_IS_NOT_FOUND = 12;

    internal const int UPDATE_USER_IS_FORBIDDEN = 13;

    internal const int USER_IS_ALREADY_SOFT_REMOVED = 14;

    internal const int USER_IS_NOT_SOFT_REMOVED = 15;

    internal const int PROJECT_IS_NOT_FOUND = 16;

    internal const int PROJECT_ALREADY_EXISTS = 17;
}