using FuDever.Application.Features.Role.CreateRole;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Role.CreateRole.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.CreateRole;

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
