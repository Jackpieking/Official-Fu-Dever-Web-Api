using FuDever.Application.Features.Position.GetAllPositions;
using FuDever.Application.Features.Position.GetAllPositionsByPositionName;
using FuDever.Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.CreatePosition.Middlewares;
/// <summary>
///     Create position request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreatePositionCachingMiddleware :
    IPipelineBehavior<
        CreatePositionRequest,
        CreatePositionResponse>,
    ICreatePositionMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public CreatePositionCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<CreatePositionResponse> Handle(
        CreatePositionRequest request,
        RequestHandlerDelegate<CreatePositionResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == CreatePositionResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllPositionsByPositionNameRequest)}_param_{request.NewPositionName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllPositionsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}

