namespace FuDever.Application.Features.Position.CreatePosition;

/// <summary>
///     Create position response status code.
/// </summary>
public enum CreatePositionResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
    POSITION_ALREADY_EXISTS
}
