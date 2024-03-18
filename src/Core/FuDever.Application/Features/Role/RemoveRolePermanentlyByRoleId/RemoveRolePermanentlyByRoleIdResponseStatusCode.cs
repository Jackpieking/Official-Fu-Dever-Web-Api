namespace FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;

/// <summary>
///     Remove role permanently by
///     role id response status code.
/// </summary>
public enum RemoveRolePermanentlyByRoleIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_FOUND,
    ROLE_IS_NOT_TEMPORARILY_REMOVED
}
