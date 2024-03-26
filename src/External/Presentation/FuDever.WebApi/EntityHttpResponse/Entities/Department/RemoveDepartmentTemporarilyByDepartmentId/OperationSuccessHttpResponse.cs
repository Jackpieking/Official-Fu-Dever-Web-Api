﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId;

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