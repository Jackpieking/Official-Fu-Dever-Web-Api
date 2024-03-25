using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId;

/// <summary>
///     Remove position temporarily by position id request handler.
/// </summary>
internal sealed class RemovePositionTemporarilyByPositionIdRequestHandler : IRequestHandler<
    RemovePositionTemporarilyByPositionIdRequest,
    RemovePositionTemporarilyByPositionIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemovePositionTemporarilyByPositionIdRequestHandler(
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
    public async Task<RemovePositionTemporarilyByPositionIdResponse> Handle(
        RemovePositionTemporarilyByPositionIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is position found by position id.
        var isPositionFound = await IsPositionFoundByPositionIdQueryAsync(
            positionId: request.PositionId,
            cancellationToken: cancellationToken);

        // Position is not found by position id.
        if (!isPositionFound)
        {
            return new()
            {
                StatusCode = RemovePositionTemporarilyByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND
            };
        }

        // Is position temporarily removed by position id.
        var isPositionTemporarilyRemoved = await IsPositionTemporarilyRemovedByPositionIdQueryAsync(
            positionId: request.PositionId,
            cancellationToken: cancellationToken);

        // Position is already temporarily removed by position id.
        if (isPositionTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RemovePositionTemporarilyByPositionIdResponseStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Remove position temporarily by position id.
        var result = await RemovePositionTemporarilyByPositionIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemovePositionTemporarilyByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemovePositionTemporarilyByPositionIdResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is position found by position id.
    /// </summary>
    /// <param name="positionId">
    ///     Position id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if position is found by position
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsPositionFoundByPositionIdQueryAsync(
        Guid positionId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionByIdSpecification(positionId: positionId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is position temporarily removed by position id.
    /// </summary>
    /// <param name="positionId">
    ///     Position id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if position is temporarily removed by position id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsPositionTemporarilyRemovedByPositionIdQueryAsync(
        Guid positionId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionByIdSpecification(positionId: positionId),
                _superSpecificationManager.Position.PositionTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to remove this position temporarily.
    /// </summary>
    /// <param name="request">
    ///     Containing position information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if removed successfully. Otherwise, false.
    /// </returns>
    private async Task<bool> RemovePositionTemporarilyByPositionIdCommandAsync(
        RemovePositionTemporarilyByPositionIdRequest request,
        CancellationToken cancellationToken)
    {
        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.PositionRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Position.PositionByIdSpecification(
                                positionId: request.PositionId),
                            _superSpecificationManager.Position.UpdateFieldOfPositionSpecification.Ver1(
                                positionRemovedAt: DateTime.UtcNow,
                                positionRemovedBy: request.PositionRemovedBy)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken: cancellationToken);
                }
                finally
                {
                    await _unitOfWork.DisposeTransactionAsync();
                }
            });

        return executedTransactionResult;
    }
    #endregion
}
