﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.UpdatePositionByPositionId;

/// <summary>
///     Update position by position
///     Id response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse :
    BaseHttpResponse,
    IUpdatePositionByPositionIdHttpResponse
{
    internal DatabaseOperationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = BaseAppCode.SERVER_ERROR;
        ErrorMessages =
        [
            "Database operations failed."
        ];
    }
}
