using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;

/// <summary>
///     Restore hobby by hobby id request handler.
/// </summary>
internal sealed class RestoreHobbyByHobbyIdRequestHandler : IRequestHandler<
    RestoreHobbyByHobbyIdRequest,
    RestoreHobbyByHobbyIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public RestoreHobbyByHobbyIdRequestHandler(
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
    public async Task<RestoreHobbyByHobbyIdResponse> Handle(
        RestoreHobbyByHobbyIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is hobby found by hobby id.
        var isHobbyFoundByHobbyId = await IsHobbyFoundByHobbyIdQueryAsync(
            hobbyId: request.HobbyId,
            cancellationToken: cancellationToken);

        // Hobby is not found by hobby id.
        if (!isHobbyFoundByHobbyId)
        {
            return new()
            {
                StatusCode = RestoreHobbyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND
            };
        }

        // Is hobby temporarily removed by hobby id.
        var isHobbyTemporarilyRemoved = await IsHobbyTemporarilyRemovedByHobbyIdQueryAsync(
            hobbyId: request.HobbyId,
            cancellationToken: cancellationToken);

        // Hobby is not temporarily removed by hobby id.
        if (!isHobbyTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestoreHobbyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Restore hobby by hobby id.
        var result = await RestoreHobbyByHobbyIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RestoreHobbyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RestoreHobbyByHobbyIdResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is hobby found by hobby id.
    /// </summary>
    /// <param name="hobbyId">
    ///     Hobby id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if hobby is found by hobby
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsHobbyFoundByHobbyIdQueryAsync(
        Guid hobbyId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyByIdSpecification(hobbyId: hobbyId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is hobby temporarily removed by hobby id.
    /// </summary>
    /// <param name="hobbyId">
    ///     Hobby id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if hobby is temporarily removed by hobby id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsHobbyTemporarilyRemovedByHobbyIdQueryAsync(
        Guid hobbyId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyByIdSpecification(hobbyId: hobbyId),
                _superSpecificationManager.Hobby.HobbyTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to restore hobby by hobby id.
    /// </summary>
    /// <param name="request">
    ///     Containing hobby information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if restoring hobby permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    public async Task<bool> RestoreHobbyByHobbyIdCommandAsync(
        RestoreHobbyByHobbyIdRequest request,
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

                    await _unitOfWork.HobbyRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Hobby.HobbyByIdSpecification(
                                hobbyId: request.HobbyId),
                            _superSpecificationManager.Hobby.UpdateFieldOfHobbySpecification.Ver1(
                                hobbyRemovedAt: _dbMinTimeHandler.Get(),
                                hobbyRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
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
