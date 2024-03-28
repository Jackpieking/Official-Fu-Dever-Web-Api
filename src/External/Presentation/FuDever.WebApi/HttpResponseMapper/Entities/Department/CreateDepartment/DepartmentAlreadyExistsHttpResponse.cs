using FuDever.Application.Features.Department.CreateDepartment;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Department.CreateDepartment.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.CreateDepartment;

/// <summary>
///     Create department response status code
///     - department already exists http response.
/// </summary>
internal sealed class DepartmentAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreateDepartmentHttpResponse
{
    internal DepartmentAlreadyExistsHttpResponse(CreateDepartmentRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = DepartmentAppCode.DEPARTMENT_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Department with name = {request.NewDepartmentName} already exists."
        ];
    }
}
