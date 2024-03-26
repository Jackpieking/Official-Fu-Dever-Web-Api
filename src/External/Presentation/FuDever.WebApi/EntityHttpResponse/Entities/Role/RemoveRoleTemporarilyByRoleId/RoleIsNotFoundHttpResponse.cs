using FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRoleTemporarilyByRoleId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRoleTemporarilyByRoleId;

/// <summary>
///     Remove role temporarily by role
///     Id response status code - role is not
///     found http response.
/// </summary>
internal sealed class RoleIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveRoleTemporarilyByRoleIdHttpResponse
{
    internal RoleIsNotFoundHttpResponse(RemoveRoleTemporarilyByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = RoleAppCode.ROLE_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Role with Id = {request.RoleId} is not found."
        ];
    }
}
