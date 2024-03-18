﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department
///     Id response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse :
    BaseHttpResponse,
    IUpdateDepartmentByDepartmentIdHttpResponse
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
