namespace FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;

/// <summary>
///     Update hobby by hobby id response status code.
/// </summary>
public enum UpdateHobbyByHobbyIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    HOBBY_IS_NOT_FOUND,
    HOBBY_IS_ALREADY_TEMPORARILY_REMOVED,
    HOBBY_ALREADY_EXISTS
}
