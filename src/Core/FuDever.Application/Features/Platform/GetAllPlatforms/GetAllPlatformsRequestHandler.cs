using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllPlatforms;

/// <summary>
///     Get all platforms request handler.
/// </summary>
internal sealed class GetAllPlatformsRequestHandler : IRequestHandler<
    GetAllPlatformsRequest,
    GetAllPlatformsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllPlatformsRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<GetAllPlatformsResponse> Handle(
        GetAllPlatformsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all non temporarily removed platforms.
        var foundPlatforms = await GetAllNonTemporarilyRemovedPlatformsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllPlatformsResponseStatusCode.OPERATION_SUCCESS,
            FoundPlatforms = foundPlatforms.Select(selector: foundPlatform =>
            {
                return new GetAllPlatformsResponse.Platform()
                {
                    PlatformId = foundPlatform.Id,
                    PlatformName = foundPlatform.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all platforms which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found platforms.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Platform>> GetAllNonTemporarilyRemovedPlatformsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformAsNoTrackingSpecification,
                _superSpecificationManager.Platform.PlatformNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Platform.SelectFieldsFromPlatformSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}