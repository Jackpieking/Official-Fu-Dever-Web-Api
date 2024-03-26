using FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatformsByPlatformName.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatformsByPlatformName;

/// <summary>
///     Get all platforms by platform name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllPlatformsByPlatformNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllPlatformsByPlatformNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundPlatforms;
    }
}
