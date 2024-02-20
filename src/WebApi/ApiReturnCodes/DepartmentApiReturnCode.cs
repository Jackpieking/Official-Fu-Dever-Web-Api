using WebApi.ApiReturnCodes.Base;

namespace WebApi.ApiReturnCodes;

/// <summary>
///     Represent department entity api return code.
/// </summary>
internal abstract class DepartmentApiReturnCode : BaseApiReturnCode
{
    internal const int DEPARTMENT_IS_NOT_FOUND = 3;

    internal const int DEPARTMENT_ALREADY_EXISTS = 4;

    internal const int DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED = 5;

    internal const int DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED = 6;
}