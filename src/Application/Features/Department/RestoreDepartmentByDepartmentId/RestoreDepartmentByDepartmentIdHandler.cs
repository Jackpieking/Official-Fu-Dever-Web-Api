using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commons;
using Application.Interfaces.Data;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department id request handler.
/// </summary>
internal sealed class RestoreDepartmentByDepartmentIdHandler : IRequestHandler<
    RestoreDepartmentByDepartmentIdRequest,
    RestoreDepartmentByDepartmentIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public RestoreDepartmentByDepartmentIdHandler(
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
    public async Task<RestoreDepartmentByDepartmentIdResponse> Handle(
        RestoreDepartmentByDepartmentIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is department found by department id.
        var isDepartmentFoundByDepartmentId = await IsDepartmentFoundByDepartmentIdQueryAsync(
            departmentId: request.DepartmentId,
            cancellationToken: cancellationToken);

        // Department is not found by department id.
        if (!isDepartmentFoundByDepartmentId)
        {
            return new()
            {
                StatusCode = RestoreDepartmentByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_FOUND
            };
        }

        // Is department temporarily removed by department id.
        var isDepartmentTemporarilyRemoved = await IsDepartmentTemporarilyRemovedByDepartmentIdQueryAsync(
            departmentId: request.DepartmentId,
            cancellationToken: cancellationToken);

        // Department is not temporarily removed by department id.
        if (!isDepartmentTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestoreDepartmentByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove department permanently by department id.
        var result = await RestoreDepartmentByDepartmentIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RestoreDepartmentByDepartmentIdStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RestoreDepartmentByDepartmentIdStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is department found by department id.
    /// </summary>
    /// <param name="departmentId">
    ///     Department id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if department is found by department
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsDepartmentFoundByDepartmentIdQueryAsync(
        Guid departmentId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentByIdSpecification(departmentId: departmentId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is department temporarily removed by department id.
    /// </summary>
    /// <param name="departmentId">
    ///     Department id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if department is temporarily removed by department id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsDepartmentTemporarilyRemovedByDepartmentIdQueryAsync(
        Guid departmentId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentByIdSpecification(departmentId: departmentId),
                _superSpecificationManager.Department.DepartmentTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to restore department by department id.
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
    ///     True if restoring skill permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    public async Task<bool> RestoreDepartmentByDepartmentIdCommandAsync(
        RestoreDepartmentByDepartmentIdRequest request,
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

                    await _unitOfWork.DepartmentRepository.BulkUpdateByDepartmentIdVer1Async(
                        departmentId: request.DepartmentId,
                        departmentRemovedAt: _dbMinTimeHandler.Get(),
                        departmentRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
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
