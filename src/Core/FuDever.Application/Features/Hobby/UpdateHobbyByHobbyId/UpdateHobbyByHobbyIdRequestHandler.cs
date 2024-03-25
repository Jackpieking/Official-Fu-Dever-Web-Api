using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;

/// <summary>
///     Update hobby by hobby id request handler.
/// </summary>
internal sealed class UpdateHobbyByHobbyIdRequestHandler : IRequestHandler<
    UpdateHobbyByHobbyIdRequest,
    UpdateHobbyByHobbyIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateHobbyByHobbyIdRequestHandler(
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
    public async Task<UpdateHobbyByHobbyIdResponse> Handle(
        UpdateHobbyByHobbyIdRequest request,
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
                StatusCode = UpdateHobbyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND
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
                StatusCode = UpdateHobbyByHobbyIdResponseStatusCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is hobby with the same hobby name found.
        var isHobbyWithTheSameNameFound = await IsHobbyWithTheSameNameFoundByHobbyNameQueryAsync(
            newHobbyName: request.NewHobbyName,
            cancellationToken: cancellationToken);

        // Hobby with the same hobby name is found.
        if (isHobbyWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdateHobbyByHobbyIdResponseStatusCode.HOBBY_ALREADY_EXISTS
            };
        }

        // Update hobby by hobby id.
        var result = await UpdateHobbyByHobbyIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdateHobbyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateHobbyByHobbyIdResponseStatusCode.OPERATION_SUCCESS
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

    /// <summary>
    ///     Is hobby found by hobby name in
    ///     the in-sensitive way.
    /// </summary>
    /// <param name="newHobbyName">
    ///     New hobby name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if hobby already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsHobbyWithTheSameNameFoundByHobbyNameQueryAsync(
        string newHobbyName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyByNameSpecification(
                    hobbyName: newHobbyName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update hobby with new name by hobby id.
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
    ///     True if updating hobby operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdateHobbyByHobbyIdCommandAsync(
        UpdateHobbyByHobbyIdRequest request,
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

                    await _unitOfWork.UserHobbyRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserHobby.UserHobbyByHobbyIdSpecification(
                                hobbyId: request.HobbyId),
                            _superSpecificationManager.UserHobby.UpdateFieldOfUserHobbySpecification.Ver1(
                                userUpdatedAt: DateTime.UtcNow,
                                userUpdatedBy: request.HobbyUpdatedBy)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.HobbyRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Hobby.HobbyByIdSpecification(
                                hobbyId: request.HobbyId),
                            _superSpecificationManager.Hobby.UpdateFieldOfHobbySpecification.Ver2(
                                hobbyName: request.NewHobbyName)
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
