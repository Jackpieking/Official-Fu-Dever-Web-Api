using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.UpdatePosition;

/// <summary>
///     Update position by position id request handler.
/// </summary>
internal sealed class UpdatePositionByPositionIdRequestHandler : IRequestHandler<
    UpdatePositionByPositionIdRequest,
    UpdatePositionByPositionIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdatePositionByPositionIdRequestHandler(
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

    public async Task<UpdatePositionByPositionIdResponse> Handle(
        UpdatePositionByPositionIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is position found by position id.
        var isPositionFoundByPositionId = await IsPositionFoundByPositionIdQueryAsync(
            positionId: request.PositionId,
            cancellationToken: cancellationToken);

        // position is not found by position id.
        if (!isPositionFoundByPositionId)
        {
            return new()
            {
                StatusCode = UpdatePositionByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND
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
                StatusCode = UpdatePositionByPositionIdResponseStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is position with the same position name found.
        var isPositionWithTheSameNameFound = await IsPositionWithTheSameNameFoundByPositionNameQueryAsync(
            newPositionName: request.NewPositionName,
            cancellationToken: cancellationToken);

        // Position with the same position name is found.
        if (isPositionWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdatePositionByPositionIdResponseStatusCode.POSITION_ALREADY_EXISTS
            };
        }

        // Update position by position id.
        var result = await UpdatePositionByPositionIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdatePositionByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdatePositionByPositionIdResponseStatusCode.OPERATION_SUCCESS
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

    /// <summary>
    ///     Is position found by position name in
    ///     the in-sensitive way.
    /// </summary>
    /// <param name="newPositionName">
    ///     New position name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if position already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsPositionWithTheSameNameFoundByPositionNameQueryAsync(
        string newPositionName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionByNameSpecification(
                    positionName: newPositionName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update position with new name by position id.
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
    ///     True if updating position operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdatePositionByPositionIdCommandAsync(
        UpdatePositionByPositionIdRequest request,
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
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUser.Id,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.PositionUpdatedBy,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.PositionRepository.BulkUpdateByPositionIdVer2Async(
                        positionId: request.PositionId,
                        positionName: request.NewPositionName,
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
