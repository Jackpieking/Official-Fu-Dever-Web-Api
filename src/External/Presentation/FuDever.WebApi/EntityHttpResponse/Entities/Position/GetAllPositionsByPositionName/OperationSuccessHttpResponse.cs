using FuDever.Application.Features.Position.GetAllPositionsByPositionName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.GetAllPositionsByPositionName.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.GetAllPositionsByPositionName;

/// <summary>
///     Get all positions by position name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllPositionsByPositionNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllPositionsByPositionNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundPositions;
    }
}
