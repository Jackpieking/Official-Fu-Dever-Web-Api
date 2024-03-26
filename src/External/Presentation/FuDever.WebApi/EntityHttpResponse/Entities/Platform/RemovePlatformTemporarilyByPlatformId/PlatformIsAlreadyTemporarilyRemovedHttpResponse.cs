using FuDever.Application.Features.Platform.RemovePlatformTemporarilyByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformTemporarilyByPlatformId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformTemporarilyByPlatformId;

/// <summary>
///     Remove platform temporarily by platform
///     Id response status code - platform is already
///     temporarily removed http response.
/// </summary>
internal sealed class PlatformIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemovePlatformTemporarilyByPlatformIdHttpResponse
{
    internal PlatformIsAlreadyTemporarilyRemovedHttpResponse(RemovePlatformTemporarilyByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PlatformAppCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found platform with Id = {request.PlatformId} in temporarily removed storage."
        ];
    }
}
