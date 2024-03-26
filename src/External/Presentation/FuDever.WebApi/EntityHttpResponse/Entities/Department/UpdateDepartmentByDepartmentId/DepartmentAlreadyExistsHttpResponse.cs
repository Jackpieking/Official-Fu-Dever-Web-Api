using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department id response
///     status code - department already exists
///     http response.
/// </summary>
internal sealed class DepartmentAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdateDepartmentByDepartmentIdHttpResponse
{
    internal DepartmentAlreadyExistsHttpResponse(UpdateDepartmentByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = DepartmentAppCode.DEPARTMENT_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Department with name = {request.NewDepartmentName} already exists."
        ];
    }
}
