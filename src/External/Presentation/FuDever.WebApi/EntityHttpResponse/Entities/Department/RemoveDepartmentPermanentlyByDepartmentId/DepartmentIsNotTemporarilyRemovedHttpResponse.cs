﻿using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by department
///     Id response status code - department id not
///     found http response.
/// </summary>
internal sealed class DepartmentIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentPermanentlyByDepartmentIdHttpResponse
{
    internal DepartmentIsNotTemporarilyRemovedHttpResponse(
        RemoveDepartmentPermanentlyByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Department with Id = {request.DepartmentId} is not found in temporarily removed storage."
        ];
    }
}