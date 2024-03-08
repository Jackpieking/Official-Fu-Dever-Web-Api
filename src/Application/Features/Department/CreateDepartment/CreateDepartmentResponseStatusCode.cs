namespace Application.Features.Department.CreateDepartment;

/// <summary>
///     Create department response status code.
/// </summary>
public enum CreateDepartmentResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
    DEPARTMENT_ALREADY_EXISTS
}
