namespace FuDever.Application.Features.Role.CreateRole;

/// <summary>
///     Create role response status code.
/// </summary>
public enum CreateRoleResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
    ROLE_ALREADY_EXISTS
}
