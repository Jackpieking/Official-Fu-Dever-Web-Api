using FuDever.Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformPermanentlyByPlatformId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformPermanentlyByPlatformId;

/// <summary>
///     Remove platform permanently by platform
///     Id response status code - platform is not
///     found http response.
/// </summary>
internal sealed class PlatformIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemovePlatformPermanentlyByPlatformIdHttpResponse
{
    internal PlatformIsNotFoundHttpResponse(RemovePlatformPermanentlyByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = PlatformAppCode.PLATFORM_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Platform with Id = {request.PlatformId} is not found."
        ];
    }
}
