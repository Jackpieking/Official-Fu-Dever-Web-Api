using FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Platform.UpdatePlatformByPlatformId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.UpdatePlatformByPlatformId;

/// <summary>
///     Update platform by platform
///     Id response status code - platform is already
///     temporarily removed http response.
/// </summary>
internal sealed class PlatformIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdatePlatformByPlatformIdHttpResponse
{
    internal PlatformIsAlreadyTemporarilyRemovedHttpResponse(UpdatePlatformByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PlatformAppCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found platform with Id = {request.PlatformId} in temporarily removed storage."
        ];
    }
}
