using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department id response
///     status code - database operation
///     fail http response.
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
