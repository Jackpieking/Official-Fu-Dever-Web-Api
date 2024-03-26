using FuDever.Application.Features.Position.UpdatePositionByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId;

/// <summary>
///     Update position by position
///     Id response status code - position is not
///     found http response.
/// </summary>
internal sealed class PositionIsNotFoundHttpResponse :
    BaseHttpResponse,
    IUpdatePositionByPositionIdHttpResponse
{
    internal PositionIsNotFoundHttpResponse(UpdatePositionByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = PositionAppCode.POSITION_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Position with Id = {request.PositionId} is not found."
        ];
    }
}
