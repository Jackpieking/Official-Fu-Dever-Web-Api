﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department
///     Id response status code - operation success
///     http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentTemporarilyByDepartmentIdHttpResponse
{
    internal OperationSuccessHttpResponse()
    {
        HttpCode = StatusCodes.Status201Created;
        AppCode = BaseAppCode.SUCCESS;
    }
}
