﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by department
///     Id response status code - operation success
///     http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentPermanentlyByDepartmentIdHttpResponse
{
    internal OperationSuccessHttpResponse()
    {
        HttpCode = StatusCodes.Status201Created;
        AppCode = BaseAppCode.SUCCESS;
    }
}
