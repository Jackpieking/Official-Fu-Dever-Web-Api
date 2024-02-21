namespace Application.Features.Position.RemovePositionTemporarilyByPositionId;

/// <summary>
///     Remove position temporarily by position id response status code.
/// </summary>
public enum RemovePositionTemporarilyByPositionIdStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
    POSITION_IS_NOT_FOUND
}
