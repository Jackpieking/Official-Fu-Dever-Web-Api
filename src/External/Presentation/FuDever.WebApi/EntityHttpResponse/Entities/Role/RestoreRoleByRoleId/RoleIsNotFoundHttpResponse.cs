using FuDever.Application.Features.Role.RestoreRoleByRoleId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.RestoreRoleByRoleId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.RestoreRoleByRoleId;

/// <summary>
///     Restore role by role
///     Id response status code - role is not
///     found http response.
/// </summary>
internal sealed class RoleIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRestoreRoleByRoleIdHttpResponse
{
    internal RoleIsNotFoundHttpResponse(RestoreRoleByRoleIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = RoleAppCode.ROLE_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Role with Id = {request.RoleId} is not found."
        ];
    }
}
