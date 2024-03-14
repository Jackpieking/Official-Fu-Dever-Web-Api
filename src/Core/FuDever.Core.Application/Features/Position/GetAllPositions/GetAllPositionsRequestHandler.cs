using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.GetAllPositions;

/// <summary>
///     Get all positions request handler.
/// </summary>
internal sealed class GetAllPositionsRequestHandler : IRequestHandler<
    GetAllPositionsRequest,
    GetAllPositionsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllPositionsRequestHandler(
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
    public async Task<GetAllPositionsResponse> Handle(
        GetAllPositionsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all non temporarily removed positions.
        var foundDepartments = await GetAllNonTemporarilyRemovedPositionsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllPositionsResponseStatusCode.OPERATION_SUCCESS,
            FoundPositions = foundDepartments.Select(selector: foundPosition =>
            {
                return new GetAllPositionsResponse.Position()
                {
                    PositionId = foundPosition.Id,
                    PositionName = foundPosition.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all positions which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found positions.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Position>> GetAllNonTemporarilyRemovedPositionsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionAsNoTrackingSpecification,
                _superSpecificationManager.Position.PositionNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Position.SelectFieldsFromPositionSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
