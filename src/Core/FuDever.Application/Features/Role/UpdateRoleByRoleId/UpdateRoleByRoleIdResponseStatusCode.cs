namespace FuDever.Application.Features.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role id response status code.
/// </summary>
public enum UpdateRoleByRoleIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    DEPARTMENT_IS_NOT_FOUND,
    DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
    DEPARTMENT_ALREADY_EXISTS
}
