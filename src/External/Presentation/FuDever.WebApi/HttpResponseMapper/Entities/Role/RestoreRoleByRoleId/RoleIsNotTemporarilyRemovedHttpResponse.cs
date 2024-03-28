using FuDever.Application.Features.Role.RestoreRoleByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Role.RestoreRoleByRoleId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.RestoreRoleByRoleId;

/// <summary>
///     Restore role by role
///     Id response status code - role id not
///     found http response.
/// </summary>
internal sealed class RoleIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestoreRoleByRoleIdHttpResponse
{
    internal RoleIsNotTemporarilyRemovedHttpResponse(RestoreRoleByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = RoleAppCode.ROLE_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Role with Id = {request.RoleId} is not found in temporarily removed storage."
        ];
    }
}
