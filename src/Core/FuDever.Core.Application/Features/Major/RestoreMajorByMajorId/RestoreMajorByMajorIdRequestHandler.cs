using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.RestoreMajorByMajorId;

/// <summary>
///     Restore major by major id request handler.
/// </summary>
internal sealed class RestoreMajorByMajorIdRequestHandler : IRequestHandler<
    RestoreMajorByMajorIdRequest,
    RestoreMajorByMajorIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public RestoreMajorByMajorIdRequestHandler(
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
    public async Task<RestoreMajorByMajorIdResponse> Handle(
        RestoreMajorByMajorIdRequest request,
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
                StatusCode = RestoreMajorByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND
            };
        }

        // Is major temporarily removed by major id.
        var isMajorTemporarilyRemoved = await IsMajorTemporarilyRemovedByMajorIdQueryAsync(
            majorId: request.MajorId,
            cancellationToken: cancellationToken);

        // Major is not temporarily removed by major id.
        if (!isMajorTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestoreMajorByMajorIdResponseStatusCode.MAJOR_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove major permanently by major id.
        var result = await RestoreMajorByMajorIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RestoreMajorByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RestoreMajorByMajorIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to restore major by major id.
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
    ///     True if restoring skill permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    public async Task<bool> RestoreMajorByMajorIdCommandAsync(
        RestoreMajorByMajorIdRequest request,
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
                        majorRemovedAt: _dbMinTimeHandler.Get(),
                        majorRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
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
