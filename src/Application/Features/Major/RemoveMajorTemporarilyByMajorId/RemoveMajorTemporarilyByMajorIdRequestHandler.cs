using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Major.RemoveMajorTemporarilyByMajorId;

/// <summary>
///     Remove major temporarily by major id request handler.
/// </summary>
internal sealed class RemoveMajorTemporarilyByMajorIdRequestHandler : IRequestHandler<
    RemoveMajorTemporarilyByMajorIdRequest,
    RemoveMajorTemporarilyByMajorIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveMajorTemporarilyByMajorIdRequestHandler(
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
    public async Task<RemoveMajorTemporarilyByMajorIdResponse> Handle(
        RemoveMajorTemporarilyByMajorIdRequest request,
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
                StatusCode = RemoveMajorTemporarilyByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND
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
                StatusCode = RemoveMajorTemporarilyByMajorIdResponseStatusCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Update major by major id.
        var result = await RemoveMajorTemporarilyByMajorIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveMajorTemporarilyByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveMajorTemporarilyByMajorIdResponseStatusCode.OPERATION_SUCCESS
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
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to remove this major temporarily.
    /// </summary>
    /// <param name="request">
    ///     Containing major information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if removed successfully. Otherwise, false.
    /// </returns>
    private async Task<bool> RemoveMajorTemporarilyByMajorIdCommandAsync(
        RemoveMajorTemporarilyByMajorIdRequest request,
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

                    await _unitOfWork.MajorRepository.BulkUpdateByMajorIdVer2Async(
                        majorId: request.MajorId,
                        majorRemovedAt: DateTime.UtcNow,
                        majorRemovedBy: request.MajorRemovedBy,
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
