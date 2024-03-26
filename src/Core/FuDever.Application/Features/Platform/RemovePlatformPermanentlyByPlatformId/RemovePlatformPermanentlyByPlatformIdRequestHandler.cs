using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;

/// <summary>
///     Request handler for remove platform permanently by platform id.
/// </summary>
internal sealed class RemovePlatformPermanentlyByPlatformIdRequestHandler : IRequestHandler<
    RemovePlatformPermanentlyByPlatformIdRequest,
    RemovePlatformPermanentlyByPlatformIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemovePlatformPermanentlyByPlatformIdRequestHandler(
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
    public async Task<RemovePlatformPermanentlyByPlatformIdResponse> Handle(
        RemovePlatformPermanentlyByPlatformIdRequest request,
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
                StatusCode = RemovePlatformPermanentlyByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND
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
                StatusCode = RemovePlatformPermanentlyByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove platform permanently by platform id.
        var result = await RemovePlatformPermanentlyByPlatformIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemovePlatformPermanentlyByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemovePlatformPermanentlyByPlatformIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to removing platform permanently with new name.
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
    ///     True if removing platform permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    private async Task<bool> RemovePlatformPermanentlyByPlatformIdCommandAsync(
        RemovePlatformPermanentlyByPlatformIdRequest request,
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

                    var foundUserPlatforms = await _unitOfWork.UserPlatformRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserPlatform.UserPlatformAsNoTrackingSpecification,
                            _superSpecificationManager.UserPlatform.UserPlatformByPlatformIdSpecification(platformId: request.PlatformId),
                            _superSpecificationManager.UserPlatform.SelectFieldsFromUserPlatformSpecification.Ver1()
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserPlatform in foundUserPlatforms)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateAsync(
                            specifications:
                            [
                                _superSpecificationManager.User.UserByIdSpecification(
                                    userId: foundUserPlatform.UserId),
                                _superSpecificationManager.User.UpdateFieldOfUserSpecification.Ver2(
                                    userUpdatedAt: DateTime.UtcNow,
                                    userUpdatedBy: request.PlatformRemovedBy)
                            ],
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.UserPlatformRepository.BulkDeleteAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserPlatform.UserPlatformByPlatformIdSpecification(
                                platformId: request.PlatformId)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.PlatformRepository.BulkDeleteAsync(
                        specifications:
                        [
                            _superSpecificationManager.Platform.PlatformByIdSpecification(
                                platformId: request.PlatformId)
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
