using WebApi.ApiReturnCodes.Base;

namespace WebApi.ApiReturnCodes;

/// <summary>
///     Represent authentication api return code.
/// </summary>
internal abstract class AuthApiReturnCode : BaseApiReturnCode
{
    internal const int USER_PASSWORD_IS_NOT_CORRECT = 3;

    internal const int USER_EMAIL_IS_NOT_CONFIRMED = 4;

    internal const int USER_IS_NOT_APPROVED = 5;

    internal const int USER_IS_EXISTED = 6;

    internal const int USER_IS_NOT_FOUND = 7;

    internal const int CURRENT_AND_NEW_PASSWORD_ARE_THE_SAME = 8;

    internal const int REFRESH_TOKEN_IS_NOT_FOUND = 9;

    internal const int REFRESH_TOKEN_IS_DENIED = 10;

    internal const int REFRESH_TOKEN_IS_EXPIRED = 11;

    internal const int USER_HAS_CONFIRMED_EMAIL = 12;

    internal const int ACCESS_TOKEN_IS_NOT_EXPIRED = 13;

    internal const int ASK_USER_TO_CONFIRM_EMAIL = 14;

    internal const int USER_IS_SOFT_REMOVED = 15;

    internal const int USER_IS_LOCKED_OUT = 16;

    internal const int USER_NAME_IS_NOT_A_REAL_EMAIL = 17;

    internal const int WRONG_PASSWORD_FORMAT = 18;
}