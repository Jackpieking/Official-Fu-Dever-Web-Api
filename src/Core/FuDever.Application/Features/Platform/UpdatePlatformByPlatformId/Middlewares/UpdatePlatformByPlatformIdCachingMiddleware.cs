using FuDever.Application.Features.Platform.GetAllPlatforms;
using FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName;
using FuDever.Application.Interfaces.Caching;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.UpdatePlatformByPlatformId.Middlewares;

/// <summary>
///     Update platform by platform id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class UpdatePlatformByPlatformIdCachingMiddleware :
    IPipelineBehavior<
        UpdatePlatformByPlatformIdRequest,
        UpdatePlatformByPlatformIdResponse>,
    IUpdatePlatformByPlatformIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdatePlatformByPlatformIdCachingMiddleware(
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
    public async Task<UpdatePlatformByPlatformIdResponse> Handle(
        UpdatePlatformByPlatformIdRequest request,
        RequestHandlerDelegate<UpdatePlatformByPlatformIdResponse> next,
        CancellationToken cancellationToken)
    {
        // Finding current platform by platform id.
        var foundPlatform = await _unitOfWork.PlatformRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformByIdSpecification(platformId: request.PlatformId),
                _superSpecificationManager.Platform.SelectFieldsFromPlatformSpecification.Ver3()
            ],
            cancellationToken: cancellationToken);

        // Platform is found by platform id.
        if (!Equals(objA: foundPlatform, objB: default))
        {
            await _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllPlatformsByPlatformNameRequest)}_param_{foundPlatform.Name.ToLower()}",
                cancellationToken: cancellationToken);
        }

        var response = await next();

        if (response.StatusCode == UpdatePlatformByPlatformIdResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllPlatformsByPlatformNameRequest)}_param_{request.NewPlatformName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllPlatformsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
