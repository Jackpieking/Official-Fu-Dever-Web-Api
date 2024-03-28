using System;
using System.Threading;
using System.Threading.Tasks;
using FuDever.Application.Interfaces.Caching;
using FuDever.Application.Models;
using MediatR;

namespace FuDever.Application.Features.Auth.Login.Middlewares;

/// <summary>
///     Login request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class LoginCachingMiddleware :
    IPipelineBehavior<
        LoginRequest,
        LoginResponse>,
    ILoginMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public LoginCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<LoginResponse> Handle(
        LoginRequest request,
        RequestHandlerDelegate<LoginResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = $"{nameof(LoginRequest)}_param_username__{request.Username}_password__{request.Password}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<LoginResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<LoginResponse>.NotFound))
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
