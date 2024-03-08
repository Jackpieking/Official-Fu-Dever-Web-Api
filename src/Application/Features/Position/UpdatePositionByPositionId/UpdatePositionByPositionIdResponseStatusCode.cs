namespace Application.Features.Position.UpdatePosition;

/// <summary>
///     Update position response status code.
/// </summary>
public enum UpdatePositionByPositionIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    POSITION_IS_NOT_FOUND,
    POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
    POSITION_ALREADY_EXISTS
}
