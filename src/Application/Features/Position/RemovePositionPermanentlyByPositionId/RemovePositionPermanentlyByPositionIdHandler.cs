using Application.Commons;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.RemovePositionPermanentlyByPositionId;

/// <summary>
///     Remove position permanently by position id request handler.
/// </summary>
internal sealed class RemovePositionPermanentlyByPositionIdHandler : IRequestHandler<
    RemovePositionPermanentlyByPositionIdRequest,
    RemovePositionPermanentlyByPositionIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemovePositionPermanentlyByPositionIdHandler(
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
                StatusCode = RemovePositionPermanentlyByPositionIdStatusCode.POSITION_IS_NOT_FOUND
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
                StatusCode = RemovePositionPermanentlyByPositionIdStatusCode.POSITION_IS_NOT_TEMPORARILY_REMOVED
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
                StatusCode = RemovePositionPermanentlyByPositionIdStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemovePositionPermanentlyByPositionIdStatusCode.OPERATION_SUCCESS
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

                    var foundUsers = await _unitOfWork.UserRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.User.UserAsNoTrackingSpecification,
                            _superSpecificationManager.User.UserByPositionIdSpecification(
                                positionId: request.PositionId),
                            _superSpecificationManager.User.SelectFieldsFromUserSpecification.Ver5(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUser in foundUsers)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer2Async(
                            userId: foundUser.Id,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.PositionRemovedBy,
                            positionId: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.PositionRepository.BulkRemoveByPositionIdAsync(
                        positionId: request.PositionId,
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
