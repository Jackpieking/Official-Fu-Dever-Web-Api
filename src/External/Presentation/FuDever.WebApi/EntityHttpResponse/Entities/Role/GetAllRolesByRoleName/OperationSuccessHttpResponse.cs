using FuDever.Application.Features.Role.GetAllRolesByRoleName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllRolesByRoleName.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllRolesByRoleName;

/// <summary>
///     Get all roles by role name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllRolesByRoleNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllRolesByRoleNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundRoles;
    }
}
