﻿using FuDever.Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Platform.RemovePlatformPermanentlyByPlatformId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.RemovePlatformPermanentlyByPlatformId;

/// <summary>
///     Remove platform permanently by platform
///     Id response status code - platform id not
///     found http response.
/// </summary>
internal sealed class PlatformIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemovePlatformPermanentlyByPlatformIdHttpResponse
{
    internal PlatformIsNotTemporarilyRemovedHttpResponse(RemovePlatformPermanentlyByPlatformIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PlatformAppCode.PLATFORM_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Platform with Id = {request.PlatformId} is not found in temporarily removed storage."
        ];
    }
}
