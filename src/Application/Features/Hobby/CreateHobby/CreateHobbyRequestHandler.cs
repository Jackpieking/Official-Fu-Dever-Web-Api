using Application.Commons;
using Application.Interfaces.Data;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Hobby.CreateHobby;

/// <summary>
///     Request handler for create hobby.
/// </summary>
internal sealed class CreateHobbyRequestHandler : IRequestHandler<
    CreateHobbyRequest,
    CreateHobbyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreateHobbyRequestHandler(
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
    public async Task<CreateHobbyResponse> Handle(
        CreateHobbyRequest request,
        CancellationToken cancellationToken)
    {
        // Is hobby with the same hobby name found.
        var isHobbyFound = await IsHobbyWithTheSameNameFoundByHobbyNameQueryAsync(
            newHobbyName: request.NewHobbyName,
            cancellationToken: cancellationToken);

        // Hobbies with the same hobby name is found.
        if (isHobbyFound)
        {
            // Is hobby temporarily removed by hobby name.
            var isHobbyTemporarilyRemoved = await IsHobbyTemporarilyRemovedByHobbyNameQueryAsync(
                newHobbyName: request.NewHobbyName,
                cancellationToken: cancellationToken);

            // Hobby with hobby name is already temporarily removed.
            if (isHobbyTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreateHobbyResponseStatusCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreateHobbyResponseStatusCode.HOBBY_ALREADY_EXISTS
            };
        }

        // Create hobby with new hobby name.
        var result = await CreateHobbyCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreateHobbyResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreateHobbyResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is hobby having the same name with
    ///     the new one found.
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

    /// <summary>
    ///     Is hobby temporarily removed by hobby name.
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
    ///     True if hobby already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsHobbyTemporarilyRemovedByHobbyNameQueryAsync(
        string newHobbyName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyByNameSpecification(
                    hobbyName: newHobbyName,
                    isCaseSensitive: true),
                _superSpecificationManager.Hobby.HobbyTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to creating a new hobby with the
    ///     given name and add to database.
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
    ///     True if adding hobby operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreateHobbyCommandAsync(
        CreateHobbyRequest request,
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

                    await _unitOfWork.HobbyRepository.AddAsync(
                        newEntity: Domain.Entities.Hobby.InitVer1(
                            hobbyId: Guid.NewGuid(),
                            hobbyName: request.NewHobbyName,
                            hobbyRemovedAt: _dbMinTimeHandler.Get(),
                            hobbyRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID),
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
