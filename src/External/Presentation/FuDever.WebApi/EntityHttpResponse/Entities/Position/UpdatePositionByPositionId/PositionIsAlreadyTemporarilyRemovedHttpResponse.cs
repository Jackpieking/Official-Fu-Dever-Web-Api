using FuDever.Application.Features.Position.UpdatePositionByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId;

/// <summary>
///     Update position by position
///     Id response status code - position is already
///     temporarily removed http response.
/// </summary>
internal sealed class PositionIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdatePositionByPositionIdHttpResponse
{
    internal PositionIsAlreadyTemporarilyRemovedHttpResponse(UpdatePositionByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PositionAppCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found position with Id = {request.PositionId} in temporarily removed storage."
        ];
    }
}
