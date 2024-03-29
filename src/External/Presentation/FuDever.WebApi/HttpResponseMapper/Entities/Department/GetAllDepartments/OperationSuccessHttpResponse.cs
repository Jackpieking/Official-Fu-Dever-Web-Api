﻿using FuDever.Application.Features.Department.GetAllDepartments;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Department.GetAllDepartments.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.GetAllDepartments;

/// <summary>
///     Get all departments response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllDepartmentsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllDepartmentsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundDepartments;
    }
}
