namespace Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department id response status code.
/// </summary>
public enum RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
    DEPARTMENT_IS_NOT_FOUND
}
