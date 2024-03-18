namespace FuDever.Application.Features.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role id response status code.
/// </summary>
public enum UpdateRoleByRoleIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_FOUND,
    ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
    ROLE_ALREADY_EXISTS
}
