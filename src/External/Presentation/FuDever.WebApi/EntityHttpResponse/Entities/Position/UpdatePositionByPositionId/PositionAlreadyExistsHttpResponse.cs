using FuDever.Application.Features.Position.UpdatePositionByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId;

/// <summary>
///     Update position by position id response
///     status code - position already exists
///     http response.
/// </summary>
internal sealed class PositionAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdatePositionByPositionIdHttpResponse
{
    internal PositionAlreadyExistsHttpResponse(UpdatePositionByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = PositionAppCode.POSITION_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Position with name = {request.NewPositionName} already exists."
        ];
    }
}
