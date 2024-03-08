using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.GetAllPositionsByPositionName;

/// <summary>
///     Get all position by position name request handler.
/// </summary>
internal sealed class GetAllPositionsByPositionNameRequestHandler : IRequestHandler<
    GetAllPositionsByPositionNameRequest,
    GetAllPositionsByPositionNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllPositionsByPositionNameRequestHandler(
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

    public async Task<GetAllPositionsByPositionNameResponse> Handle(
        GetAllPositionsByPositionNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all positions by name.
        var foundPositions = await GetAllPositionsByPositionNameQueryAsync(
            positionName: request.PositionName,
            cancellationToken: cancellationToken);

        return new()
        {
            StatusCode = GetAllPositionsByPositionNameResponseStatusCode.OPERATION_SUCCESS,
            FoundPositions = foundPositions.Select(selector: foundPosition =>
            {
                return new GetAllPositionsByPositionNameResponse.Position()
                {
                    PositionId = foundPosition.Id,
                    PositionName = foundPosition.Name
                };
            }),
        };
    }

    #region Queries
    /// <summary>
    ///     Get all position by position name
    /// </summary>
    /// <param name="positionName">
    ///     position name to find.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found positions.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Position>> GetAllPositionsByPositionNameQueryAsync(
        string positionName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionAsNoTrackingSpecification,
                _superSpecificationManager.Position.PositionByNameSpecification(
                    positionName: positionName,
                    isCaseSensitive: false),
                _superSpecificationManager.Position.PositionNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Position.SelectFieldsFromPositionSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
