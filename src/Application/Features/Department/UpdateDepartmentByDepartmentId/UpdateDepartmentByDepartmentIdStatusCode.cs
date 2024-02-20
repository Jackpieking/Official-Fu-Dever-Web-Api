namespace Application.Features.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department id response status code.
/// </summary>
public enum UpdateDepartmentByDepartmentIdStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    DEPARTMENT_IS_NOT_FOUND,
    DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
    DEPARTMENT_ALREADY_EXISTS
}
