using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by department
///     Id response status code - department is not
///     found http response.
/// </summary>
internal sealed class DepartmentIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveDepartmentPermanentlyByDepartmentIdHttpResponse
{
    internal DepartmentIsNotFoundHttpResponse(RemoveDepartmentPermanentlyByDepartmentIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = DepartmentAppCode.DEPARTMENT_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Department with Id = {request.DepartmentId} is not found."
        ];
    }
}
