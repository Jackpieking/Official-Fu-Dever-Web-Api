using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department
///     Id response status code - department is not
///     found http response.
/// </summary>
internal sealed class DepartmentIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentTemporarilyByDepartmentIdHttpResponse
{
    internal DepartmentIsNotFoundHttpResponse(RemoveDepartmentTemporarilyByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Department with Id = {request.DepartmentId} is not found."
        ];
    }
}
