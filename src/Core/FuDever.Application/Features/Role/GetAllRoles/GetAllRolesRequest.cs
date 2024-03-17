using FuDever.Application.Features.Role.GetAllRoles.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Role.GetAllRoles;

/// <summary>
///     Get all role request.
/// </summary>
public sealed class GetAllRolesRequest :
    IRequest<GetAllRolesResponse>,
    IGetAllRolesMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}
