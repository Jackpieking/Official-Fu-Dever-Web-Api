namespace Application.Features.Hobby.RestoreHobbyByHobbyId;

/// <summary>
///     Restore hobby by hobby id response status code.
/// </summary>
public enum RestoreHobbyByHobbyIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    HOBBY_IS_NOT_TEMPORARILY_REMOVED,
    HOBBY_IS_NOT_FOUND
}
