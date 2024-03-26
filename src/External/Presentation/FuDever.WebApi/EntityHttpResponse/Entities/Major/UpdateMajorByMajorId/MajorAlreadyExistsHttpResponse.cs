﻿using FuDever.Application.Features.Major.UpdateMajorByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major id response
///     status code - major already exists
///     http response.
/// </summary>
internal sealed class MajorAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdateMajorByMajorIdHttpResponse
{
    internal MajorAlreadyExistsHttpResponse(UpdateMajorByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = MajorAppCode.MAJOR_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Major with name = {request.NewMajorName} already exists."
        ];
    }
}
