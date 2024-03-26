﻿using FuDever.Application.Features.Platform.CreatePlatform;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.CreatePlatform.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.CreatePlatform;

/// <summary>
///     Create platform response status code
///     - platform already exists http response.
/// </summary>
internal sealed class PlatformAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreatePlatformHttpResponse
{
    internal PlatformAlreadyExistsHttpResponse(CreatePlatformRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = PlatformAppCode.PLATFORM_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Platform with name = {request.NewPlatformName} already exists."
        ];
    }
}
