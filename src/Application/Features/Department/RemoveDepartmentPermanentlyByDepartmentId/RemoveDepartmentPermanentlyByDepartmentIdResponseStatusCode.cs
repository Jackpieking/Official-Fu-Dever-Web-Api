namespace Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by
///     department id response status code.
/// </summary>
public enum RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    DEPARTMENT_IS_NOT_FOUND,
    DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED
}
