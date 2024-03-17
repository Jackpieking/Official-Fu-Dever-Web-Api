using FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;

/// <summary>
///     Get all temporarily removed roles request.
/// </summary>
public sealed class GetAllTemporarilyRemovedRolesRequest :
    IRequest<GetAllTemporarilyRemovedRolesResponse>,
    IGetAllTemporarilyRemovedRolesMiddleware
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
