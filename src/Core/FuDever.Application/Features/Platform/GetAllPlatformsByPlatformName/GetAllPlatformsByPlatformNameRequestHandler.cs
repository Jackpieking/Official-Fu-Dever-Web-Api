using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName;

/// <summary>
///     Get all platforms by name request handler.
/// </summary>
internal sealed class GetAllPlatformsByPlatformNameRequestHandler : IRequestHandler<
    GetAllPlatformsByPlatformNameRequest,
    GetAllPlatformsByPlatformNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllPlatformsByPlatformNameRequestHandler(
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
    /// </param>
    /// <returns>
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<GetAllPlatformsByPlatformNameResponse> Handle(
        GetAllPlatformsByPlatformNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all platforms by name.
        var foundPlatforms = await GetAllPlatformsByPlatformNameQueryAsync(
            platformName: request.PlatformName,
            cancellationToken: cancellationToken);

        return new()
        {
            StatusCode = GetAllPlatformsByPlatformNameResponseStatusCode.OPERATION_SUCCESS,
            FoundPlatforms = foundPlatforms.Select(selector: foundPlatform =>
            {
                return new GetAllPlatformsByPlatformNameResponse.Platform()
                {
                    PlatformId = foundPlatform.Id,
                    PlatformName = foundPlatform.Name
                };
            }),
        };
    }

    #region Queries
    /// <summary>
    ///     Get all platform by platform name
    /// </summary>
    /// <param name="platformName">
    ///     Platform name to find.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found platforms.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Platform>> GetAllPlatformsByPlatformNameQueryAsync(
        string platformName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformAsNoTrackingSpecification,
                _superSpecificationManager.Platform.PlatformByNameSpecification(
                        platformName: platformName,
                        isCaseSensitive: false),
                _superSpecificationManager.Platform.PlatformNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Platform.SelectFieldsFromPlatformSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
