namespace FuDever.Application.Features.Hobby.CreateHobby;

/// <summary>
///     Status codes for create hobby response.
/// </summary>
public enum CreateHobbyResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    HOBBY_IS_ALREADY_TEMPORARILY_REMOVED,
    HOBBY_ALREADY_EXISTS
}
