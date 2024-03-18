using FuDever.Application.Features.Department.CreateDepartment;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.CreateDepartment.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.CreateDepartment;

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
