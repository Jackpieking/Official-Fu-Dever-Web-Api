using FuDever.Application.Features.Position.CreatePosition;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.CreatePosition.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.CreatePosition;

/// <summary>
///     Create position response status code
///     - position is already temporarily removed
///     http response.
/// </summary>
internal sealed class PositionIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreatePositionHttpResponse
{
    internal PositionIsAlreadyTemporarilyRemovedHttpResponse(CreatePositionRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PositionAppCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found position with name = {request.NewPositionName} in temporarily removed storage."
        ];
    }
}
