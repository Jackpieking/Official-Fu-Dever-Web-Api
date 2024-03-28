using FuDever.Application.Features.Position.CreatePosition;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.CreatePosition.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.CreatePosition;

/// <summary>
///     Create position response status code
///     - position already exists http response.
/// </summary>
internal sealed class PositionAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreatePositionHttpResponse
{
    internal PositionAlreadyExistsHttpResponse(CreatePositionRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = PositionAppCode.POSITION_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Position with name = {request.NewPositionName} already exists."
        ];
    }
}
