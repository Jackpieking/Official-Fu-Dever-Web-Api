﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major
///     Id response status code - operation success
///     http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IUpdateMajorByMajorIdHttpResponse
{
    internal OperationSuccessHttpResponse()
    {
        HttpCode = StatusCodes.Status201Created;
        AppCode = BaseAppCode.SUCCESS;
    }
}
