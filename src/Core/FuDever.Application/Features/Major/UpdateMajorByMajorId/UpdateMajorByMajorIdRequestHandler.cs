using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major id request handler.
/// </summary>
internal sealed class UpdateMajorByMajorIdRequestHandler : IRequestHandler<
    UpdateMajorByMajorIdRequest,
    UpdateMajorByMajorIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateMajorByMajorIdRequestHandler(
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
    public async Task<UpdateMajorByMajorIdResponse> Handle(
        UpdateMajorByMajorIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is major found by major id.
        var isMajorFoundByMajorId = await IsMajorFoundByMajorIdQueryAsync(
            majorId: request.MajorId,
            cancellationToken: cancellationToken);

        // Major is not found by major id.
        if (!isMajorFoundByMajorId)
        {
            return new()
            {
                StatusCode = UpdateMajorByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND
            };
        }

        // Is major temporarily removed by major id.
        var isMajorTemporarilyRemoved = await IsMajorTemporarilyRemovedByMajorIdQueryAsync(
            majorId: request.MajorId,
            cancellationToken: cancellationToken);

        // Major is already temporarily removed by major id.
        if (isMajorTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdateMajorByMajorIdResponseStatusCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is major with the same major name found.
        var isMajorWithTheSameNameFound = await IsMajorWithTheSameNameFoundByMajorNameQueryAsync(
            newMajorName: request.NewMajorName,
            cancellationToken: cancellationToken);

        // Major with the same major name is found.
        if (isMajorWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdateMajorByMajorIdResponseStatusCode.MAJOR_ALREADY_EXISTS
            };
        }

        // Update major by major id.
        var result = await UpdateMajorByMajorIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdateMajorByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateMajorByMajorIdResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is major found by major id.
    /// </summary>
    /// <param name="majorId">
    ///     Major id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if major is found by major
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsMajorFoundByMajorIdQueryAsync(
        Guid majorId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorByIdSpecification(majorId: majorId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is major temporarily removed by major id.
    /// </summary>
    /// <param name="majorId">
    ///     Major id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if major is temporarily removed by major id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsMajorTemporarilyRemovedByMajorIdQueryAsync(
        Guid majorId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorByIdSpecification(majorId: majorId),
                _superSpecificationManager.Major.MajorTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is major having the same name with
    ///     the new one found.
    /// </summary>
    /// <param name="newMajorName">
    ///     New major name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if major already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsMajorWithTheSameNameFoundByMajorNameQueryAsync(
        string newMajorName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorByNameSpecification(
                    majorName: newMajorName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update Major with new name by Major id.
    /// </summary>
    /// <param name="request">
    ///     Containing Major information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if updating Major operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdateMajorByMajorIdCommandAsync(
        UpdateMajorByMajorIdRequest request,
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
                            _superSpecificationManager.User.UserByMajorIdSpecification(
                                majorId: request.MajorId),
                            _superSpecificationManager.User.UpdateFieldOfUserSpecification.Ver2(
                                userUpdatedAt: DateTime.UtcNow,
                                userUpdatedBy: request.MajorUpdatedBy)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.MajorRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Major.MajorByIdSpecification(
                                majorId: request.MajorId),
                            _superSpecificationManager.Major.UpdateFieldOfMajorSpecification.Ver2(
                                majorName: request.NewMajorName)
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
