using FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.UpdatePlatformByPlatformId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.UpdatePlatformByPlatformId;

/// <summary>
///     Update platform by platform id response
///     status code - platform already exists
///     http response.
/// </summary>
internal sealed class PlatformAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdatePlatformByPlatformIdHttpResponse
{
    internal PlatformAlreadyExistsHttpResponse(UpdatePlatformByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = PlatformAppCode.PLATFORM_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Platform with name = {request.NewPlatformName} already exists."
        ];
    }
}
