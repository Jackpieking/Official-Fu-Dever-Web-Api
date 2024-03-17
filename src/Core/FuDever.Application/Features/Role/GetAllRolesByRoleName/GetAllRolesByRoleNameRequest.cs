using FuDever.Application.Features.Role.GetAllRolesByRoleName.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Role.GetAllRolesByRoleName;

/// <summary>
///     Get all roles by role name request.
/// </summary>
public sealed class GetAllRolesByRoleNameRequest :
    IRequest<GetAllRolesByRoleNameResponse>,
    IGetAllRolesByRoleNameMiddleware
{
    public string RoleName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}
