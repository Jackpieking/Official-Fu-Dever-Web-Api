using FuDever.Application.Features.Major.GetAllMajors;
using FuDever.Application.Features.Major.GetAllMajorsByMajorName;
using FuDever.Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.CreateMajor.Middlewares;

/// <summary>
///     Create major request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreateMajorCachingMiddleware :
    IPipelineBehavior<
        CreateMajorRequest,
        CreateMajorResponse>,
    ICreateMajorMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public CreateMajorCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<CreateMajorResponse> Handle(
        CreateMajorRequest request,
        RequestHandlerDelegate<CreateMajorResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == CreateMajorResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllMajorsByMajorNameRequest)}_param_{request.NewMajorName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllMajorsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
