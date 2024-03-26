using FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.GetAllTemporarilyRemovedPositions.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.GetAllTemporarilyRemovedPositions;

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
