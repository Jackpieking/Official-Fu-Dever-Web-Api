using FuDever.Application.Features.Role.UpdateRoleByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.UpdateRoleByRoleId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role id response
///     status code - role already exists
///     http response.
/// </summary>
internal sealed class RoleAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdateRoleByRoleIdHttpResponse
{
    internal RoleAlreadyExistsHttpResponse(UpdateRoleByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = RoleAppCode.ROLE_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Role with name = {request.NewRoleName} already exists."
        ];
    }
}
