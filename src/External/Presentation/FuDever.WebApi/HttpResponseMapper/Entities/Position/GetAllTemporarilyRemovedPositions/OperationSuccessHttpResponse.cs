using FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.GetAllTemporarilyRemovedPositions.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.GetAllTemporarilyRemovedPositions;

/// <summary>
///     Get all temporarily removed positions response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedPositionsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedPositionsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedPositions;
    }
}
