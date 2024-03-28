using FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Role.RemoveRolePermanentlyByRoleId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.RemoveRolePermanentlyByRoleId;

/// <summary>
///     Remove role permanently by role
///     Id response status code - role id not
///     found http response.
/// </summary>
internal sealed class RoleIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveRolePermanentlyByRoleIdHttpResponse
{
    internal RoleIsNotTemporarilyRemovedHttpResponse(RemoveRolePermanentlyByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = RoleAppCode.ROLE_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Role with Id = {request.RoleId} is not found in temporarily removed storage."
        ];
    }
}
