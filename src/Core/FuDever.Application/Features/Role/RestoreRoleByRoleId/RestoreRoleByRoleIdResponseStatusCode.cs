namespace FuDever.Application.Features.Role.RestoreRoleByRoleId;

/// <summary>
///     Restore role by role
///     id response status code.
/// </summary>
public enum RestoreRoleByRoleIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_TEMPORARILY_REMOVED,
    ROLE_IS_NOT_FOUND
}
