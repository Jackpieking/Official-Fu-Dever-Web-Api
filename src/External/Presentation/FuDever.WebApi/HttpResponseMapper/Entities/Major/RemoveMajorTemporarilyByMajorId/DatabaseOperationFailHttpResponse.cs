﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.RemoveMajorTemporarilyByMajorId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.RemoveMajorTemporarilyByMajorId;

/// <summary>
///     Remove major temporarily by major
///     Id response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse :
    BaseHttpResponse,
    IRemoveMajorTemporarilyByMajorIdHttpResponse
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
