﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatforms.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatforms;

/// <summary>
///     Get all platforms response status code
///     - input validation fail http response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    IGetAllPlatformsHttpResponse
{
    internal InputValidationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = BaseAppCode.SERVER_ERROR;
        ErrorMessages =
        [
            "Server error. Please try again later."
        ];
    }
}
