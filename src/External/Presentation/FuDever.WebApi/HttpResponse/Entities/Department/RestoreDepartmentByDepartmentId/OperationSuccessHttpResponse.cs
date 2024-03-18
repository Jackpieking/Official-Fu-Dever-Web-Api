using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponse.Base;
using FuDever.WebApi.HttpResponse.Entities.Department.RestoreDepartmentByDepartmentId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department
///     Id response status code - operation success
///     http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IRestoreDepartmentByDepartmentIdHttpResponse
{
    internal OperationSuccessHttpResponse()
    {
        HttpCode = StatusCodes.Status201Created;
        AppCode = BaseAppCode.SUCCESS;
    }
}
