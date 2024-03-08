using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Caching;
using Application.Models;
using System;

namespace Application.Features.Hobby.GetAllHobbiesByHobbyName.Middlewares;

/// <summary>
///     Get all hobbies by hobby name
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllHobbiesByHobbyNameCachingMiddleware :
    IPipelineBehavior<
        GetAllHobbiesByHobbyNameRequest,
        GetAllHobbiesByHobbyNameResponse>,
    IGetAllHobbiesByHobbyNameMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllHobbiesByHobbyNameCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllHobbiesByHobbyNameResponse> Handle(
        GetAllHobbiesByHobbyNameRequest request,
        RequestHandlerDelegate<GetAllHobbiesByHobbyNameResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = $"{nameof(GetAllHobbiesByHobbyNameRequest)}_param_{request.HobbyName.ToLower()}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllHobbiesByHobbyNameResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllHobbiesByHobbyNameResponse>.NotFound))
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
