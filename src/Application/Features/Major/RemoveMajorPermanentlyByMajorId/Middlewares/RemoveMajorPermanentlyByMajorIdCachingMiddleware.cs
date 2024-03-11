using Application.Features.Major.GetAllTemporarilyRemovedMajors;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Major.RemoveMajorPermanentlyByMajorId.Middlewares;

/// <summary>
///     Remove permanently major by
///     major id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveMajorPermanentlyByMajorIdCachingMiddleware :
    IPipelineBehavior<
        RemoveMajorPermanentlyByMajorIdRequest,
        RemoveMajorPermanentlyByMajorIdResponse>,
    IRemoveMajorPermanentlyByMajorIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public RemoveMajorPermanentlyByMajorIdCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<RemoveMajorPermanentlyByMajorIdResponse> Handle(
        RemoveMajorPermanentlyByMajorIdRequest request,
        RequestHandlerDelegate<RemoveMajorPermanentlyByMajorIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveMajorPermanentlyByMajorIdResponseStatusCode.OPERATION_SUCCESS)
        {
            await _cacheHandler.RemoveAsync(
                key: nameof(GetAllTemporarilyRemovedMajorsRequest),
                cancellationToken: cancellationToken);
        }

        return response;
    }
}
