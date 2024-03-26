using FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRoleTemporarilyByRoleId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRoleTemporarilyByRoleId;

/// <summary>
///     Remove role temporarily by role
///     Id response status code - role is already
///     temporarily removed http response.
/// </summary>
internal sealed class RoleIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveRoleTemporarilyByRoleIdHttpResponse
{
    internal RoleIsAlreadyTemporarilyRemovedHttpResponse(RemoveRoleTemporarilyByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = RoleAppCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found role with Id = {request.RoleId} in temporarily removed storage."
        ];
    }
}
