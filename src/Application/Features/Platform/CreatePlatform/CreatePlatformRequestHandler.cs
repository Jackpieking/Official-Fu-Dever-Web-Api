using Application.Commons;
using Application.Interfaces.Data;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Platform.CreatePlatform;

/// <summary>
///     Create platform request handler.
/// </summary>
internal sealed class CreatePlatformRequestHandler : IRequestHandler<
    CreatePlatformRequest,
    CreatePlatformResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreatePlatformRequestHandler(
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
    public async Task<CreatePlatformResponse> Handle(
        CreatePlatformRequest request,
        CancellationToken cancellationToken)
    {
        // Is platform with the same platform name found.
        var isPlatformFound = await IsPlatformWithTheSameNameFoundByPlatformNameQueryAsync(
            newPlatformName: request.NewPlatformName,
            cancellationToken: cancellationToken);

        // Platforms with the same platform name is found.
        if (isPlatformFound)
        {
            // Is platform temporarily removed by platform name.
            var isPlatformTemporarilyRemoved = await IsPlatformTemporarilyRemovedByPlatformNameQueryAsync(
                newPlatforName: request.NewPlatformName,
                cancellationToken: cancellationToken);

            // Platform with platform name is already temporarily removed.
            if (isPlatformTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreatePlatformResponseStatusCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreatePlatformResponseStatusCode.PLATFORM_ALREADY_EXISTS
            };
        }

        // Create platform with new platform name.
        var result = await CreatePlatformCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreatePlatformResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreatePlatformResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is platform having the same name with
    ///     the new one found.
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

    /// <summary>
    ///     Is platform temporarily removed by platform name.
    /// </summary>
    /// <param name="newPlatforName">
    ///     New platform name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if platform already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsPlatformTemporarilyRemovedByPlatformNameQueryAsync(
        string newPlatforName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.PlatformRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Platform.PlatformByNameSpecification(
                    platformName: newPlatforName,
                    isCaseSensitive: true),
                _superSpecificationManager.Platform.PlatformTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to creating a new platform with the
    ///     given name and add to database.
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
    ///     True if adding platform operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreatePlatformCommandAsync(
        CreatePlatformRequest request,
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

                    await _unitOfWork.PlatformRepository.AddAsync(
                        newEntity: Domain.Entities.Platform.InitVer1(
                            platformId: Guid.NewGuid(),
                            platformName: request.NewPlatformName,
                            platformRemovedAt: _dbMinTimeHandler.Get(),
                            platformRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID),
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

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
