using FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Platform.GetAllPlatformsByPlatformName.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.GetAllPlatformsByPlatformName;

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
