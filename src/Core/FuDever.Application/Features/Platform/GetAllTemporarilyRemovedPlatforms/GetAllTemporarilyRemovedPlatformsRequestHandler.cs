using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;

/// <summary>
///     Get all temporarily removed platforms request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedPlatformsRequestHandler : IRequestHandler<
    GetAllTemporarilyRemovedPlatformsRequest,
    GetAllTemporarilyRemovedPlatformsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedPlatformsRequestHandler(
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
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<GetAllTemporarilyRemovedPlatformsResponse> Handle(
        GetAllTemporarilyRemovedPlatformsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed platforms.
        var foundTemporarilyRemovedPlatforms = await GetAllTemporarilyRemovedPlatformsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedPlatformsResponseStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedPlatforms = foundTemporarilyRemovedPlatforms.Select(selector: foundPlatform =>
            {
                return new GetAllTemporarilyRemovedPlatformsResponse.Platform()
                {
                    PlatformId = foundPlatform.Id,
                    PlatformName = foundPlatform.Name,
                    PlatformRemovedAt = foundPlatform.RemovedAt,
                    PlatformRemovedBy = foundPlatform.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all platforms which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found platforms.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Platform>> GetAllTemporarilyRemovedPlatformsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformAsNoTrackingSpecification,
                _superSpecificationManager.Platform.PlatformTemporarilyRemovedSpecification,
                _superSpecificationManager.Platform.PlatformNameIsNotDefaultSpecification,
                _superSpecificationManager.Platform.SelectFieldsFromPlatformSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
