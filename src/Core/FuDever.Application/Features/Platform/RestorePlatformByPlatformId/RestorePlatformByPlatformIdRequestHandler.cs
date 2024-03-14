using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.RestorePlatformByPlatformId;

/// <summary>
///     Restore platform by platform id request handler.
/// </summary>
internal sealed class RestorePlatformByPlatformIdRequestHandler : IRequestHandler<
    RestorePlatformByPlatformIdRequest,
    RestorePlatformByPlatformIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public RestorePlatformByPlatformIdRequestHandler(
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
    public async Task<RestorePlatformByPlatformIdResponse> Handle(
        RestorePlatformByPlatformIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is platform found by platform id.
        var isPlatformFoundByPlatformId = await IsPlatformFoundByPlatformIdQueryAsync(
            platformId: request.PlatformId,
            cancellationToken: cancellationToken);

        // Platform is not found by platform id.
        if (!isPlatformFoundByPlatformId)
        {
            return new()
            {
                StatusCode = RestorePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND
            };
        }

        // Is platform temporarily removed by platform id.
        var isPlatformTemporarilyRemoved = await IsPlatformTemporarilyRemovedByPlatformIdQueryAsync(
            platformId: request.PlatformId,
            cancellationToken: cancellationToken);

        // Platform is not temporarily removed by platform id.
        if (!isPlatformTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestorePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Restore platform by platform id.
        var result = await RestorePlatformByPlatformIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RestorePlatformByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RestorePlatformByPlatformIdResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is platform found by platform id.
    /// </summary>
    /// <param name="platformId">
    ///     Platform id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if platform is found by platform
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsPlatformFoundByPlatformIdQueryAsync(
        Guid platformId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformByIdSpecification(platformId: platformId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is platform temporarily removed by platform id.
    /// </summary>
    /// <param name="platformId">
    ///     Platform id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if platform is temporarily removed by platform id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsPlatformTemporarilyRemovedByPlatformIdQueryAsync(
        Guid platformId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformByIdSpecification(platformId: platformId),
                _superSpecificationManager.Platform.PlatformTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to restore platform by platform id.
    /// </summary>
    /// <param name="request">
    ///     Containing platform information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if restoring platform permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    public async Task<bool> RestorePlatformByPlatformIdCommandAsync(
        RestorePlatformByPlatformIdRequest request,
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

                    await _unitOfWork.PlatformRepository.BulkUpdateByPlatformIdVer1Async(
                        platformId: request.PlatformId,
                        platformRemovedAt: _dbMinTimeHandler.Get(),
                        platformRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
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
