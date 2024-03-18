using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department
///     Id response status code - department is already
///     temporarily removed http response.
/// </summary>
internal sealed class DepartmentIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentTemporarilyByDepartmentIdHttpResponse
{
    internal DepartmentIsAlreadyTemporarilyRemovedHttpResponse(
        RemoveDepartmentTemporarilyByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found department with Id = {request.DepartmentId} in temporarily removed storage."
        ];
    }
}
