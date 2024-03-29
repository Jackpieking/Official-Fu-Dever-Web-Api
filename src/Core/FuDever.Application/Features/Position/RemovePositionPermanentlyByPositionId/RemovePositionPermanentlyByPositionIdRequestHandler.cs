using FuDever.Application.Commons;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;

/// <summary>
///     Remove position permanently by position id request handler.
/// </summary>
internal sealed class RemovePositionPermanentlyByPositionIdRequestHandler : IRequestHandler<
    RemovePositionPermanentlyByPositionIdRequest,
    RemovePositionPermanentlyByPositionIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemovePositionPermanentlyByPositionIdRequestHandler(
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
    public async Task<RemovePositionPermanentlyByPositionIdResponse> Handle(
        RemovePositionPermanentlyByPositionIdRequest request,
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
                StatusCode = RemovePositionPermanentlyByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND
            };
        }

        // Is position temporarily removed by position id.
        var isPositionTemporarilyRemoved = await IsPositionTemporarilyRemovedByPositionIdQueryAsync(
            positionId: request.PositionId,
            cancellationToken: cancellationToken);

        // Position is not temporarily removed by position id.
        if (!isPositionTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RemovePositionPermanentlyByPositionIdResponseStatusCode.POSITION_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove position permanently by position id.
        var result = await RemovePositionPermanentlyByPositionIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemovePositionPermanentlyByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemovePositionPermanentlyByPositionIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to remove this position permanently.
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
    private async Task<bool> RemovePositionPermanentlyByPositionIdCommandAsync(
        RemovePositionPermanentlyByPositionIdRequest request,
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

                    await _unitOfWork.UserRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.User.UserByPositionIdSpecification(
                                positionId: request.PositionId),
                            _superSpecificationManager.User.UpdateFieldOfUserSpecification.Ver4(
                                userUpdatedAt: DateTime.UtcNow,
                                userUpdatedBy: request.PositionRemovedBy,
                                userPositionId: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID),
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.PositionRepository.BulkDeleteAsync(
                        specifications:
                        [
                            _superSpecificationManager.Position.PositionByIdSpecification(
                                positionId: request.PositionId),
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
