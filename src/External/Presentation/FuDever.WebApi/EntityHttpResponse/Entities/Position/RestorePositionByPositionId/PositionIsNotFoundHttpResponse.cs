using FuDever.Application.Features.Position.RestorePositionByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.RestorePositionByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.RestorePositionByPositionId;

/// <summary>
///     Restore position by position
///     Id response status code - position is not
///     found http response.
/// </summary>
internal sealed class PositionIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRestorePositionByPositionIdHttpResponse
{
    internal PositionIsNotFoundHttpResponse(RestorePositionByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = PositionAppCode.POSITION_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Position with Id = {request.PositionId} is not found."
        ];
    }
}
