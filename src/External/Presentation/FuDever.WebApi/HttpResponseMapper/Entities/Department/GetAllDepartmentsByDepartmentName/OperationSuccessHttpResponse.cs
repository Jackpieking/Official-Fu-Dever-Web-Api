﻿using FuDever.Application.Features.Department.GetAllDepartmentsByDepartmentName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Department.GetAllDepartmentsByDepartmentName.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.GetAllDepartmentsByDepartmentName;

/// <summary>
///     Get all departments by department name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllDepartmentsByDepartmentNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllDepartmentsByDepartmentNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundDepartments;
    }
}
