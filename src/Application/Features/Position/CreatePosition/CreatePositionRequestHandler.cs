using Application.Commons;
using Application.Interfaces.Data;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.CreatePosition;

/// <summary>
///     Create position request handler.
/// </summary>
internal sealed class CreatePositionRequestHandler : IRequestHandler<
    CreatePositionRequest,
    CreatePositionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreatePositionRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IDbMinTimeHandler dbMinTimeHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
        _dbMinTimeHandler = dbMinTimeHandler;
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
    public async Task<CreatePositionResponse> Handle(
        CreatePositionRequest request,
        CancellationToken cancellationToken)
    {
        // Is position with the same position name found.
        var isPositionFound = await IsPositionWithTheSameNameFoundByPositionNameQueryAsync(
            newPositionName: request.NewPositionName,
            cancellationToken: cancellationToken);

        // Positions with the same position name is found.
        if (isPositionFound)
        {
            // Is position temporarily removed by position name.
            var isPositionTemporarilyRemoved = await IsPositionTemporarilyRemovedByPositionNameQueryAsync(
                newPositionName: request.NewPositionName,
                cancellationToken: cancellationToken);

            // Position with position name is already temporarily removed.
            if (isPositionTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreatePositionResponseStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreatePositionResponseStatusCode.POSITION_ALREADY_EXISTS
            };
        }

        // Create position with new position name.
        var result = await CreatePositionCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreatePositionResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreatePositionResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is position having the same name with
    ///     the new one found.
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

    /// <summary>
    ///     Is position temporarily removed by position name.
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
    ///     True if position already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsPositionTemporarilyRemovedByPositionNameQueryAsync(
        string newPositionName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PositionRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionByNameSpecification(
                    positionName: newPositionName,
                    isCaseSensitive: true),
                _superSpecificationManager.Position.PositionTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to creating a new position with the
    ///     given name and add to database.
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
    ///     True if adding position operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreatePositionCommandAsync(
        CreatePositionRequest request,
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

                    await _unitOfWork.PositionRepository.AddAsync(
                        newEntity: Domain.Entities.Position.InitVer1(
                            positionId: Guid.NewGuid(),
                            positionName: request.NewPositionName,
                            positionRemovedAt: _dbMinTimeHandler.Get(),
                            positionRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID),
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

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
