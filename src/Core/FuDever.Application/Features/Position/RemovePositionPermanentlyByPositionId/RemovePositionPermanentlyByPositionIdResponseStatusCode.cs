namespace FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;

/// <summary>
///     Remove position permanently by position id
///     response status code.
/// </summary>
public enum RemovePositionPermanentlyByPositionIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    POSITION_IS_NOT_FOUND,
    POSITION_IS_NOT_TEMPORARILY_REMOVED
}
