using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using Application.Features.Platform.GetAllPlatforms;
using Application.Features.Platform.GetAllPlatformsByPlatformName;
using Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;

namespace Application.Features.Platform.RestorePlatformByPlatformId.Middlewares;

/// <summary>
///     Restore platform by platform
///     id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RestorePlatformByPlatformIdCachingMiddleware :
    IPipelineBehavior<
        RestorePlatformByPlatformIdRequest,
        RestorePlatformByPlatformIdResponse>,
    IRestorePlatformByPlatformIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RestorePlatformByPlatformIdCachingMiddleware(
        ICacheHandler cacheHandler,
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _cacheHandler = cacheHandler;
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
    public async Task<RestorePlatformByPlatformIdResponse> Handle(
        RestorePlatformByPlatformIdRequest request,
        RequestHandlerDelegate<RestorePlatformByPlatformIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RestorePlatformByPlatformIdResponseStatusCode.OPERATION_SUCCESS)
        {
            var foundPlatform = await _unitOfWork.PlatformRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Platform.PlatformByIdSpecification(platformId: request.PlatformId),
                    _superSpecificationManager.Platform.SelectFieldsFromPlatformSpecification.Ver3()
                ],
                cancellationToken: cancellationToken);

            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllPlatformsByPlatformNameRequest)}_param_{foundPlatform.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllPlatformsRequest),
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllTemporarilyRemovedPlatformsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
