using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;

/// <summary>
///     Update platform by platform id request handler.
/// </summary>
internal sealed class UpdatePlatformByPlatformIdRequestHandler : IRequestHandler<
    UpdatePlatformByPlatformIdRequest,
    UpdatePlatformByPlatformIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdatePlatformByPlatformIdRequestHandler(
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
    public async Task<UpdatePlatformByPlatformIdResponse> Handle(
        UpdatePlatformByPlatformIdRequest request,
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
                StatusCode = UpdatePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND
            };
        }

        // Is platform temporarily removed by platform id.
        var isPlatformTemporarilyRemoved = await IsPlatformTemporarilyRemovedByPlatformIdQueryAsync(
            platformId: request.PlatformId,
            cancellationToken: cancellationToken);

        // Platform is already temporarily removed by platform id.
        if (isPlatformTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdatePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is platform with the same platform name found.
        var isPlatformWithTheSameNameFound = await IsPlatformWithTheSameNameFoundByPlatformNameQueryAsync(
            newPlatformName: request.NewPlatformName,
            cancellationToken: cancellationToken);

        // Platform with the same platform name is found.
        if (isPlatformWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdatePlatformByPlatformIdResponseStatusCode.PLATFORM_ALREADY_EXISTS
            };
        }

        // Update platform by platform id.
        var result = await UpdatePlatformByPlatformIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdatePlatformByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdatePlatformByPlatformIdResponseStatusCode.OPERATION_SUCCESS
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

    /// <summary>
    ///     Is platform found by platform name in
    ///     the in-sensitive way.
    /// </summary>
    /// <param name="newPlatformName">
    ///     New platform name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if platform already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsPlatformWithTheSameNameFoundByPlatformNameQueryAsync(
        string newPlatformName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformByNameSpecification(
                    platformName: newPlatformName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update platform with new name by platform id.
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
    ///     True if updating platform operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdatePlatformByPlatformIdCommandAsync(
        UpdatePlatformByPlatformIdRequest request,
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
                                    userUpdatedBy: request.PlatformUpdatedBy)
                            ],
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.PlatformRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Platform.PlatformByIdSpecification(
                                platformId: request.PlatformId),
                            _superSpecificationManager.Platform.UpdateFieldOfPlatformSpecification.Ver2(
                                platformName: request.NewPlatformName)
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
