using FuDever.Application.Features.Platform.CreatePlatform;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.CreatePlatform.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.CreatePlatform;

/// <summary>
///     Create platform response status code
///     - platform is already temporarily removed
///     http response.
/// </summary>
internal sealed class PlatformIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreatePlatformHttpResponse
{
    internal PlatformIsAlreadyTemporarilyRemovedHttpResponse(CreatePlatformRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PlatformAppCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found platform with name = {request.NewPlatformName} in temporarily removed storage."
        ];
    }
}
