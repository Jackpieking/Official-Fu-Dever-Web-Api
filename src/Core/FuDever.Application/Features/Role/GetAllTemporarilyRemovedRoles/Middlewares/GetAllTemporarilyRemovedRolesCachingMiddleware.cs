using FuDever.Application.Interfaces.Caching;
using FuDever.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles.Middlewares;

/// <summary>
///     Get all temporarily removed roles
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
public class GetAllTemporarilyRemovedRolesCachingMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedRolesRequest,
        GetAllTemporarilyRemovedRolesResponse>,
    IGetAllTemporarilyRemovedRolesMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllTemporarilyRemovedRolesCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    ///     Entry to middleware handler.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
    public async Task<GetAllTemporarilyRemovedRolesResponse> Handle(
        GetAllTemporarilyRemovedRolesRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedRolesResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        const string cachedKey = nameof(GetAllTemporarilyRemovedRolesRequest);

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllTemporarilyRemovedRolesResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllTemporarilyRemovedRolesResponse>.NotFound))
        {
            return cacheModel.Value;
        }

        var response = await next();

        // Caching the return value.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: response,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                    seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return response;
    }
}
