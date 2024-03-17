using FuDever.Application.Interfaces.Caching;
using FuDever.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllRolesByRoleName.Middlewares;

/// <summary>
///     Get all roles by role name
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllRolesByRoleNameCachingMiddleware :
    IPipelineBehavior<
        GetAllRolesByRoleNameRequest,
        GetAllRolesByRoleNameResponse>,
    IGetAllRolesByRoleNameMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllRolesByRoleNameCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllRolesByRoleNameResponse> Handle(
        GetAllRolesByRoleNameRequest request,
        RequestHandlerDelegate<GetAllRolesByRoleNameResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = $"{nameof(GetAllRolesByRoleNameRequest)}_param_{request.RoleName.ToLower()}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllRolesByRoleNameResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllRolesByRoleNameResponse>.NotFound))
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