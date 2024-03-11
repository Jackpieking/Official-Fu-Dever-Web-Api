using Application.Commons;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by department id request handler.
/// </summary>
internal sealed class RemoveDepartmentPermanentlyByDepartmentIdRequestHandler : IRequestHandler<
    RemoveDepartmentPermanentlyByDepartmentIdRequest,
    RemoveDepartmentPermanentlyByDepartmentIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveDepartmentPermanentlyByDepartmentIdRequestHandler(
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
    public async Task<RemoveDepartmentPermanentlyByDepartmentIdResponse> Handle(
        RemoveDepartmentPermanentlyByDepartmentIdRequest request,
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
                StatusCode = RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND
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
                StatusCode = RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove department permanently by department id.
        var result = await RemoveDepartmentPermanentlyByDepartmentIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to remove permanently department by department id.
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
    private async Task<bool> RemoveDepartmentPermanentlyByDepartmentIdCommandAsync(
        RemoveDepartmentPermanentlyByDepartmentIdRequest request,
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

                    var foundUsers = await _unitOfWork.UserRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.User.UserAsNoTrackingSpecification,
                            _superSpecificationManager.User.UserByDepartmentIdSpecification(
                                departmentId: request.DepartmentId),
                            _superSpecificationManager.User.SelectFieldsFromUserSpecification.Ver5(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUser in foundUsers)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer3Async(
                            userId: foundUser.Id,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.DepartmentRemovedBy,
                            userDepartmentId: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.DepartmentRepository.BulkRemoveByDepartmentIdAsync(
                        departmentId: request.DepartmentId,
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
