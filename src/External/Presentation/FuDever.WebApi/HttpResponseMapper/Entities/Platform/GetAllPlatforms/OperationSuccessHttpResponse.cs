using FuDever.Application.Features.Platform.GetAllPlatforms;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Platform.GetAllPlatforms.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.GetAllPlatforms;

/// <summary>
///     Get all platforms response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllPlatformsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllPlatformsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundPlatforms;
    }
}
