using FuDever.Application.Features.Platform.RestorePlatformByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Platform.RestorePlatformByPlatformId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.RestorePlatformByPlatformId;

/// <summary>
///     Restore platform by platform
///     Id response status code - platform id not
///     found http response.
/// </summary>
internal sealed class PlatformIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestorePlatformByPlatformIdHttpResponse
{
    internal PlatformIsNotTemporarilyRemovedHttpResponse(RestorePlatformByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PlatformAppCode.PLATFORM_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Platform with Id = {request.PlatformId} is not found in temporarily removed storage."
        ];
    }
}
