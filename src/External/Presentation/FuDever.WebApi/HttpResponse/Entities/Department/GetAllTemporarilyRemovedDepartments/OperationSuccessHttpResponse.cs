using FuDever.Application.Features.Department.GetAllTemporarilyRemovedDepartments;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.GetAllTemporarilyRemovedDepartments.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.GetAllTemporarilyRemovedDepartments;

/// <summary>
///     Get all temporarily removed departments response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedDepartmentsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedDepartmentsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedDepartments;
    }
}
