using FuDever.Application.Features.Department.CreateDepartment;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Department.CreateDepartment.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.CreateDepartment;

/// <summary>
///     Create department response status code
///     - department is already temporarily removed
///     http response.
/// </summary>
internal sealed class DepartmentIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreateDepartmentHttpResponse
{
    internal DepartmentIsAlreadyTemporarilyRemovedHttpResponse(CreateDepartmentRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found department with name = {request.NewDepartmentName} in temporarily removed storage."
        ];
    }
}
