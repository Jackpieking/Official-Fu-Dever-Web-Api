using FuDever.Application.Features.Position.UpdatePositionByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.UpdatePositionByPositionId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.UpdatePositionByPositionId;

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
