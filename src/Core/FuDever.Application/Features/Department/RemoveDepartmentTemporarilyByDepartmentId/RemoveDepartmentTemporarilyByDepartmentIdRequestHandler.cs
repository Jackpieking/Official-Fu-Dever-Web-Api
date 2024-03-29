using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department id request handler.
/// </summary>
internal sealed class RemoveDepartmentTemporarilyByDepartmentIdRequestHandler : IRequestHandler<
    RemoveDepartmentTemporarilyByDepartmentIdRequest,
    RemoveDepartmentTemporarilyByDepartmentIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveDepartmentTemporarilyByDepartmentIdRequestHandler(
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
    public async Task<RemoveDepartmentTemporarilyByDepartmentIdResponse> Handle(
        RemoveDepartmentTemporarilyByDepartmentIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is department found by department id.
        var isDepartmentFound = await IsDepartmentFoundByDepartmentIdQueryAsync(
            departmentId: request.DepartmentId,
            cancellationToken: cancellationToken);

        // Department is not found by department id.
        if (!isDepartmentFound)
        {
            return new()
            {
                StatusCode = RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND
            };
        }

        // Is department temporarily removed by department id.
        var isDepartmentTemporarilyRemoved = await IsDepartmentTemporarilyRemovedByDepartmentIdQueryAsync(
            departmentId: request.DepartmentId,
            cancellationToken: cancellationToken);

        // Department is already temporarily removed by department id.
        if (isDepartmentTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Remove department temporarily by department id.
        var result = await RemoveDepartmentTemporarilyByDepartmentIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to remove this department temporarily.
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
    ///     True if removed successfully. Otherwise, false.
    /// </returns>
    private async Task<bool> RemoveDepartmentTemporarilyByDepartmentIdCommandAsync(
        RemoveDepartmentTemporarilyByDepartmentIdRequest request,
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

                    await _unitOfWork.DepartmentRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Department.DepartmentByIdSpecification(
                                departmentId: request.DepartmentId),
                            _superSpecificationManager.Department.UpdateFieldOfDepartmentSpecification.Ver1(
                                departmentRemovedAt: DateTime.UtcNow,
                                departmentRemovedBy: request.DepartmentRemovedBy)
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
