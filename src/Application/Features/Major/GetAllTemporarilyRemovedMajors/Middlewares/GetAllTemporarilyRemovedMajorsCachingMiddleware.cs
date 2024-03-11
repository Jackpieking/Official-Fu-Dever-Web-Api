using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Caching;
using Application.Models;
using System;

namespace Application.Features.Major.GetAllTemporarilyRemovedMajors.Middlewares;

/// <summary>
///     Get all temporarily removed majors
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllTemporarilyRemovedMajorsCachingMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedMajorsRequest,
        GetAllTemporarilyRemovedMajorsResponse>,
    IGetAllTemporarilyRemovedMajorsMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllTemporarilyRemovedMajorsCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllTemporarilyRemovedMajorsResponse> Handle(
        GetAllTemporarilyRemovedMajorsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedMajorsResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        const string cachedKey = nameof(GetAllTemporarilyRemovedMajorsRequest);

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllTemporarilyRemovedMajorsResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllTemporarilyRemovedMajorsResponse>.NotFound))
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
