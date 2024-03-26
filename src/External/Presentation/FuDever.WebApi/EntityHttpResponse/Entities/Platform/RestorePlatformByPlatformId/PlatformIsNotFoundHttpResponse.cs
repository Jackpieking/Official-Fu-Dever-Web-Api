using FuDever.Application.Features.Platform.RestorePlatformByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.RestorePlatformByPlatformId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.RestorePlatformByPlatformId;

/// <summary>
///     Restore platform by platform
///     Id response status code - platform is not
///     found http response.
/// </summary>
internal sealed class PlatformIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRestorePlatformByPlatformIdHttpResponse
{
    internal PlatformIsNotFoundHttpResponse(RestorePlatformByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = PlatformAppCode.PLATFORM_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Platform with Id = {request.PlatformId} is not found."
        ];
    }
}
