using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.GetAllTemporarilyRemovedPositions;

/// <summary>
///     Get all temporarily removed positions request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedPositionsRequestHandler : IRequestHandler<
    GetAllTemporarilyRemovedPositionsRequest,
    GetAllTemporarilyRemovedPositionsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedPositionsRequestHandler(
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
    public async Task<GetAllTemporarilyRemovedPositionsResponse> Handle(
        GetAllTemporarilyRemovedPositionsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed positions.
        var foundTemporarilyRemovedPositions = await GetAllTemporarilyRemovedPositionsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedPositionsResponseStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedPositions = foundTemporarilyRemovedPositions.Select(selector: foundPosition =>
            {
                return new GetAllTemporarilyRemovedPositionsResponse.Position()
                {
                    PositionId = foundPosition.Id,
                    PositionName = foundPosition.Name,
                    PositionRemovedAt = foundPosition.RemovedAt,
                    PositionRemovedBy = foundPosition.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all positions which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found positions.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Position>> GetAllTemporarilyRemovedPositionsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionAsNoTrackingSpecification,
                _superSpecificationManager.Position.PositionTemporarilyRemovedSpecification,
                _superSpecificationManager.Position.SelectFieldsFromPositionSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
