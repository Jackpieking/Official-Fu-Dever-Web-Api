using FuDever.Application.Features.Role.CreateRole;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.CreateRole.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.CreateRole;

/// <summary>
///     Create role response status code
///     - role already exists http response.
/// </summary>
internal sealed class RoleAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreateRoleHttpResponse
{
    internal RoleAlreadyExistsHttpResponse(CreateRoleRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = RoleAppCode.ROLE_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Role with name = {request.NewRoleName} already exists."
        ];
    }
}
