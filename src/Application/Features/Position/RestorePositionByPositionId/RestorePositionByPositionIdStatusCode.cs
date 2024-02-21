namespace Application.Features.Position.RestorePositionByPositionId;

/// <summary>
///     Restore position by position id response status code.
/// </summary>
public enum RestorePositionByPositionIdStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    POSITION_IS_NOT_TEMPORARILY_REMOVED,
    POSITION_IS_NOT_FOUND
}
