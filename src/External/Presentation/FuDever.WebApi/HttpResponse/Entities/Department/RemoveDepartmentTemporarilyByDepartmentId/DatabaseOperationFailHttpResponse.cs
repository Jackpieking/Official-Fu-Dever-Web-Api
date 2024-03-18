﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department
///     Id response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentTemporarilyByDepartmentIdHttpResponse
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
