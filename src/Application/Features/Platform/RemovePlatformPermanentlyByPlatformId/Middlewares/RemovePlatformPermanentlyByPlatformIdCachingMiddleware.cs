using Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Platform.RemovePlatformPermanentlyByPlatformId.Middlewares;

/// <summary>
///     Remove platform permanently by platform id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemovePlatformPermanentlyByPlatformIdCachingMiddleware :
    IPipelineBehavior<
        RemovePlatformPermanentlyByPlatformIdRequest,
        RemovePlatformPermanentlyByPlatformIdResponse>,
    IRemovePlatformPermanentlyByPlatformIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public RemovePlatformPermanentlyByPlatformIdCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<RemovePlatformPermanentlyByPlatformIdResponse> Handle(
        RemovePlatformPermanentlyByPlatformIdRequest request,
        RequestHandlerDelegate<RemovePlatformPermanentlyByPlatformIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemovePlatformPermanentlyByPlatformIdResponseStatusCode.OPERATION_SUCCESS)
        {
            await _cacheHandler.RemoveAsync(
                key: nameof(GetAllTemporarilyRemovedPlatformsRequest),
                cancellationToken: cancellationToken);
        }

        return response;
    }
}
