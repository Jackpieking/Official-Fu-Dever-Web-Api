using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.EntityBuilders.Department;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Department.CreateDepartment;

/// <summary>
///     Create department request handler.
/// </summary>
internal sealed class CreateDepartmentRequestHandler : IRequestHandler<
    CreateDepartmentRequest,
    CreateDepartmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreateDepartmentRequestHandler(
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
    public async Task<CreateDepartmentResponse> Handle(
        CreateDepartmentRequest request,
        CancellationToken cancellationToken)
    {
        // Is department with the same department name found.
        var isDepartmentFound = await IsDepartmentWithTheSameNameFoundByDepartmentNameQueryAsync(
            newDepartmentName: request.NewDepartmentName,
            cancellationToken: cancellationToken);

        // Departments with the same department name is found.
        if (isDepartmentFound)
        {
            // Is department temporarily removed by department name.
            var isDepartmentTemporarilyRemoved = await IsDepartmentTemporarilyRemovedByDepartmentNameQueryAsync(
                newDepartmentName: request.NewDepartmentName,
                cancellationToken: cancellationToken);

            // Department with department name is already temporarily removed.
            if (isDepartmentTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreateDepartmentResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreateDepartmentResponseStatusCode.DEPARTMENT_ALREADY_EXISTS
            };
        }

        // Create department with new department name.
        var result = await CreateDepartmentCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreateDepartmentResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreateDepartmentResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is department having the same name with
    ///     the new one found.
    /// </summary>
    /// <param name="newDepartmentName">
    ///     New department name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if department already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsDepartmentWithTheSameNameFoundByDepartmentNameQueryAsync(
        string newDepartmentName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentByNameSpecification(
                    departmentName: newDepartmentName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is department temporarily removed by department name.
    /// </summary>
    /// <param name="newDepartmentName">
    ///     New department name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if department already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsDepartmentTemporarilyRemovedByDepartmentNameQueryAsync(
        string newDepartmentName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentByNameSpecification(
                    departmentName: newDepartmentName,
                    isCaseSensitive: true),
                _superSpecificationManager.Department.DepartmentTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to creating a new department with the
    ///     given name and add to database.
    /// </summary>
    /// <param name="request">
    ///     Containing department information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if adding department operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreateDepartmentCommandAsync(
        CreateDepartmentRequest request,
        CancellationToken cancellationToken)
    {
        DepartmentForNewRecordBuilder builder = new();

        var newDepartment = builder
            .WithId(departmentId: Guid.NewGuid())
            .WithName(departmentName: request.NewDepartmentName)
            .WithRemovedAt(departmentRemovedAt: _dbMinTimeHandler.Get())
            .WithRemovedBy(departmentRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
            .Complete();

        if (Equals(objA: newDepartment, objB: default))
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

                    await _unitOfWork.DepartmentRepository.AddAsync(
                        newEntity: newDepartment,
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
