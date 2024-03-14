using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;

/// <summary>
///     Remove hobby permanently by hobby id request handler.
/// </summary> 
internal sealed class RemoveHobbyPermanentlyByHobbyIdRequestHandler : IRequestHandler<
    RemoveHobbyPermanentlyByHobbyIdRequest,
    RemoveHobbyPermanentlyByHobbyIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveHobbyPermanentlyByHobbyIdRequestHandler(
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
    public async Task<RemoveHobbyPermanentlyByHobbyIdResponse> Handle(
        RemoveHobbyPermanentlyByHobbyIdRequest request,
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
                StatusCode = RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND
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
                StatusCode = RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove hobby permanently by hobby id.
        var result = await RemoveHobbyPermanentlyByHobbyIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to removing hobby permanently with new name.
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
    ///     True if removing hobby permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    private async Task<bool> RemoveHobbyPermanentlyByHobbyIdCommandAsync(
        RemoveHobbyPermanentlyByHobbyIdRequest request,
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

                    var foundUserHobbies = await _unitOfWork.UserHobbyRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserHobby.UserHobbyAsNoTrackingSpecification,
                            _superSpecificationManager.UserHobby.UserHobbyByHobbyIdSpecification(hobbyId: request.HobbyId),
                            _superSpecificationManager.UserHobby.SelectFieldsFromUserHobbySpecification.Ver2(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserHobby in foundUserHobbies)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUserHobby.UserId,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.HobbyRemovedBy,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.UserHobbyRepository.BulkRemoveByHobbyIdAsync(
                        hobbyId: request.HobbyId,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.HobbyRepository.BulkRemoveByHobbyIdAsync(
                        hobbyId: request.HobbyId,
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
