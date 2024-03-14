namespace FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;

/// <summary>
///     Remove hobby permanently by hobby id response status code.
/// </summary>
public enum RemoveHobbyPermanentlyByHobbyIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    HOBBY_IS_NOT_FOUND,
    HOBBY_IS_NOT_TEMPORARILY_REMOVED
}
