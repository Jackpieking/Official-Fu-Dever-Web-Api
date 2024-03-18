using FuDever.Application.Features.Department.GetAllDepartments;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.GetAllDepartments.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.GetAllDepartments;

/// <summary>
///     Get all departments response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllDepartmentHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllDepartmentsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundDepartments;
    }
}
