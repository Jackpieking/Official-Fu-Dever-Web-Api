using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department id request handler.
/// </summary>
internal sealed class UpdateDepartmentByDepartmentIdRequestHandler : IRequestHandler<
    UpdateDepartmentByDepartmentIdRequest,
    UpdateDepartmentByDepartmentIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateDepartmentByDepartmentIdRequestHandler(
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
    public async Task<UpdateDepartmentByDepartmentIdResponse> Handle(
        UpdateDepartmentByDepartmentIdRequest request,
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
                StatusCode = UpdateDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND
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
                StatusCode = UpdateDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is department with the same department name found.
        var isDepartmentWithTheSameNameFound = await IsDepartmentWithTheSameNameFoundByDepartmentNameQueryAsync(
            newDepartmentName: request.NewDepartmentName,
            cancellationToken: cancellationToken);

        // Department with the same department name is found.
        if (isDepartmentWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdateDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_ALREADY_EXISTS
            };
        }

        // Update department by department id.
        var result = await UpdateDepartmentByDepartmentIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdateDepartmentByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateDepartmentByDepartmentIdResponseStatusCode.OPERATION_SUCCESS
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
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update department with new name by department id.
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
    ///     True if updating department operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdateDepartmentByDepartmentIdCommandAsync(
        UpdateDepartmentByDepartmentIdRequest request,
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

                    await _unitOfWork.UserRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.User.UserByDepartmentIdSpecification(
                                departmentId: request.DepartmentId),
                            _superSpecificationManager.User.UpdateFieldOfUserSpecification.Ver2(
                                userUpdatedAt: DateTime.UtcNow,
                                userUpdatedBy: request.DepartmentUpdatedBy)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.DepartmentRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Department.DepartmentByIdSpecification(
                                departmentId: request.DepartmentId),
                            _superSpecificationManager.Department.UpdateFieldOfDepartmentSpecification.Ver2(
                                departmentName: request.NewDepartmentName)
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
