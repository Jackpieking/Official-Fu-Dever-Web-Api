using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;

/// <summary>
///     Request handler for removing hobby temporarily by hobby id.
/// </summary>
internal sealed class RemoveHobbyTemporarilyByHobbyIdRequestHandler : IRequestHandler<
    RemoveHobbyTemporarilyByHobbyIdRequest,
    RemoveHobbyTemporarilyByHobbyIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveHobbyTemporarilyByHobbyIdRequestHandler(
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

    public async Task<RemoveHobbyTemporarilyByHobbyIdResponse> Handle(
        RemoveHobbyTemporarilyByHobbyIdRequest request,
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
                StatusCode = RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND
            };
        }

        // Is hobby temporarily removed by hobby id.
        var isHobbyTemporarilyRemoved = await IsHobbyTemporarilyRemovedByHobbyIdQueryAsync(
            hobbyId: request.HobbyId,
            cancellationToken: cancellationToken);

        // Hobby is already temporarily removed by hobby id.
        if (isHobbyTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Update hobby by hobby id.
        var result = await RemoveHobbyTemporarilyByHobbyIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to remove this hobby temporarily.
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
    ///     True if removed successfully. Otherwise, false.
    /// </returns>
    private async Task<bool> RemoveHobbyTemporarilyByHobbyIdCommandAsync(
        RemoveHobbyTemporarilyByHobbyIdRequest request,
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
                                hobbyRemovedAt: DateTime.UtcNow,
                                hobbyRemovedBy: request.HobbyRemovedBy)
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
