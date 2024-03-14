namespace FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;

/// <summary>
///     Remove hobby temporarily by 
///     hobby id response status code.
/// </summary>
public enum RemoveHobbyTemporarilyByHobbyIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    HOBBY_IS_ALREADY_TEMPORARILY_REMOVED,
    HOBBY_IS_NOT_FOUND
}
