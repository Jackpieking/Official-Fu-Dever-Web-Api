﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department
///     Id response status code - input validation
///     fail http response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    IUpdateDepartmentByDepartmentIdHttpResponse
{
    internal InputValidationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = BaseAppCode.INVALID_INPUTS;
        ErrorMessages =
        [
            "Input validation fail. Please check your inputs and try again."
        ];
    }
}