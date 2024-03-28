using FuDever.Application.Features.Role.UpdateRoleByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Role.UpdateRoleByRoleId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role
///     Id response status code - role is already
///     temporarily removed http response.
/// </summary>
internal sealed class RoleIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdateRoleByRoleIdHttpResponse
{
    internal RoleIsAlreadyTemporarilyRemovedHttpResponse(UpdateRoleByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = RoleAppCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found role with Id = {request.RoleId} in temporarily removed storage."
        ];
    }
}
