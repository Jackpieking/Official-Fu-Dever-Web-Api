using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent cv entity api return code.
/// </summary>
internal abstract class CvAppCode : BaseAppCode
{
    internal const int CV_IS_NOT_FOUND = 3;

    internal const int CV_WITH_STUDENT_ID_IS_ALREADY_EXISTED = 4;

    internal const int CV_WITH_EMAIL_IS_ALREADY_EXISTED = 5;

    internal const int STUDENT_ID_NOT_FOUND = 6;

    internal const int CV_IS_SOFT_REMOVED = 7;

    internal const int CV_IS_NOT_SOFT_REMOVED = 8;
}
