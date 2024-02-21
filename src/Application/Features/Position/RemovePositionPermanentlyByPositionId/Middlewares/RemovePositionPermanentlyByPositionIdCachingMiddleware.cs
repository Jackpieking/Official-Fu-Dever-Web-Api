using Application.Features.Position.GetAllTemporarilyRemovedPositions;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.RemovePositionPermanentlyByPositionId.Middlewares;

/// <summary>
///     Remove permanently position by
///     position id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemovePositionPermanentlyByPositionIdCachingMiddleware :
    IPipelineBehavior<
        RemovePositionPermanentlyByPositionIdRequest,
        RemovePositionPermanentlyByPositionIdResponse>,
    IRemovePositionPermanentlyByPositionIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public RemovePositionPermanentlyByPositionIdCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<RemovePositionPermanentlyByPositionIdResponse> Handle(
        RemovePositionPermanentlyByPositionIdRequest request,
        RequestHandlerDelegate<RemovePositionPermanentlyByPositionIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemovePositionPermanentlyByPositionIdStatusCode.OPERATION_SUCCESS)
        {
            await _cacheHandler.RemoveAsync(
                key: nameof(GetAllTemporarilyRemovedPositionsRequest),
                cancellationToken: cancellationToken);
        }

        return response;
    }
}
