﻿using FuDever.Application.Features.Department.RestoreDepartmentByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Department.RestoreDepartmentByDepartmentId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department
///     Id response status code - department is not
///     found http response.
/// </summary>
internal sealed class DepartmentIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRestoreDepartmentByDepartmentIdHttpResponse
{
    internal DepartmentIsNotFoundHttpResponse(RestoreDepartmentByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Department with Id = {request.DepartmentId} is not found."
        ];
    }
}
