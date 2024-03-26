using FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllTemporarilyRemovedPlatforms.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllTemporarilyRemovedPlatforms;

/// <summary>
///     Get all temporarily removed platforms response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedPlatformsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedPlatformsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedPlatforms;
    }
}
