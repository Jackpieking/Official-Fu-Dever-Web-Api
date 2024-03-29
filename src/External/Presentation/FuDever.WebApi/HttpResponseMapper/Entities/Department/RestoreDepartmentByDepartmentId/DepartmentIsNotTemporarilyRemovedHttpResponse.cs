﻿using FuDever.Application.Features.Department.RestoreDepartmentByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Department.RestoreDepartmentByDepartmentId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department
///     Id response status code - department id not
///     found http response.
/// </summary>
internal sealed class DepartmentIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestoreDepartmentByDepartmentIdHttpResponse
{
    internal DepartmentIsNotTemporarilyRemovedHttpResponse(
        RestoreDepartmentByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Department with Id = {request.DepartmentId} is not found in temporarily removed storage."
        ];
    }
}
