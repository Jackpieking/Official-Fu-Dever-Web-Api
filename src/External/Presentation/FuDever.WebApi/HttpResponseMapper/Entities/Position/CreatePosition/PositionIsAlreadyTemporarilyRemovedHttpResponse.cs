using FuDever.Application.Features.Position.CreatePosition;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.CreatePosition.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.CreatePosition;

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
