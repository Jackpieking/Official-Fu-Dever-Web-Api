using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department
///     Id response status code - department is already
///     temporarily removed http response.
/// </summary>
internal sealed class DepartmentIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdateDepartmentByDepartmentIdHttpResponse
{
    internal DepartmentIsAlreadyTemporarilyRemovedHttpResponse(
        UpdateDepartmentByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found department with Id = {request.DepartmentId} in temporarily removed storage."
        ];
    }
}
