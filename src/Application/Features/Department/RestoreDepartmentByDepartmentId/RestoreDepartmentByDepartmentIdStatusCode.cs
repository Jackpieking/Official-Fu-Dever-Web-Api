namespace Application.Features.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department
///     id response status code.
/// </summary>
public enum RestoreDepartmentByDepartmentIdStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED,
    DEPARTMENT_IS_NOT_FOUND
}
