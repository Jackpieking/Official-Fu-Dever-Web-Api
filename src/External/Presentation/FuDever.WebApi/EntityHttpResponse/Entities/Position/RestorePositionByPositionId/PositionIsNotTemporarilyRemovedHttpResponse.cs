using FuDever.Application.Features.Position.RestorePositionByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.RestorePositionByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.RestorePositionByPositionId;

/// <summary>
///     Restore position by position
///     Id response status code - position id not
///     found http response.
/// </summary>
internal sealed class PositionIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestorePositionByPositionIdHttpResponse
{
    internal PositionIsNotTemporarilyRemovedHttpResponse(RestorePositionByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PositionAppCode.POSITION_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Position with Id = {request.PositionId} is not found in temporarily removed storage."
        ];
    }
}
