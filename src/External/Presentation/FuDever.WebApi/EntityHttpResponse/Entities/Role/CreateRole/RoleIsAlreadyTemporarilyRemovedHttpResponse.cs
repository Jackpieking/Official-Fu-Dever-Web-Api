using FuDever.Application.Features.Role.CreateRole;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.CreateRole.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.CreateRole;

/// <summary>
///     Create role response status code
///     - role is already temporarily removed
///     http response.
/// </summary>
internal sealed class RoleIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreateRoleHttpResponse
{
    internal RoleIsAlreadyTemporarilyRemovedHttpResponse(CreateRoleRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = RoleAppCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found role with name = {request.NewRoleName} in temporarily removed storage."
        ];
    }
}
