using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.EntityBuilders.Major;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.CreateMajor;

/// <summary>
///     Create major request handler.
/// </summary>
internal sealed class CreateMajorRequestHandler : IRequestHandler<
    CreateMajorRequest,
    CreateMajorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreateMajorRequestHandler(
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
    public async Task<CreateMajorResponse> Handle(
        CreateMajorRequest request,
        CancellationToken cancellationToken)
    {
        // Is major with the same major name found.
        var isMajorFound = await IsMajorWithTheSameNameFoundByMajorNameQueryAsync(
            newMajorName: request.NewMajorName,
            cancellationToken: cancellationToken);

        // Majors with the same major name is found.
        if (isMajorFound)
        {
            // Is major temporarily removed by major name.
            var isMajorTemporarilyRemoved = await IsMajorTemporarilyRemovedByMajorNameQueryAsync(
                newMajorName: request.NewMajorName,
                cancellationToken: cancellationToken);

            // Major with major name is already temporarily removed.
            if (isMajorTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreateMajorResponseStatusCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreateMajorResponseStatusCode.MAJOR_ALREADY_EXISTS
            };
        }

        // Create major with new major name.
        var result = await CreateMajorCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreateMajorResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreateMajorResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
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

    /// <summary>
    ///     Is major temporarily removed by major name.
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
    ///     True if major already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsMajorTemporarilyRemovedByMajorNameQueryAsync(
        string newMajorName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorByNameSpecification(
                    majorName: newMajorName,
                    isCaseSensitive: true),
                _superSpecificationManager.Major.MajorTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to creating a new major with the
    ///     given name and add to database.
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
    ///     True if adding major operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreateMajorCommandAsync(
        CreateMajorRequest request,
        CancellationToken cancellationToken)
    {
        MajorForNewRecordBuilder builder = new();

        var newMajor = builder
            .WithId(majorId: Guid.NewGuid())
            .WithName(majorName: request.NewMajorName)
            .WithRemovedAt(majorRemovedAt: _dbMinTimeHandler.Get())
            .WithRemovedBy(majorRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
            .Complete();

        if (Equals(objA: newMajor, objB: default))
        {
            return false;
        }

        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.MajorRepository.AddAsync(
                        newEntity: newMajor,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    // Rollback changes in the database.
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken: cancellationToken);
                }
                finally
                {
                    // Dispose database transaction.
                    await _unitOfWork.DisposeTransactionAsync();
                }
            });

        return executedTransactionResult;
    }
    #endregion
}
