using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.GetAllTemporarilyRemovedDepartments;

/// <summary>
///     Get all temporarily removed departments request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedDepartmentsHandler : IRequestHandler<
    GetAllTemporarilyRemovedDepartmentsRequest,
    GetAllTemporarilyRemovedDepartmentsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedDepartmentsHandler(
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
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<GetAllTemporarilyRemovedDepartmentsResponse> Handle(
        GetAllTemporarilyRemovedDepartmentsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed departments.
        var foundTemporarilyRemovedDepartments = await GetAllTemporarilyRemovedDepartmentsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedDepartmentsStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedDepartments = foundTemporarilyRemovedDepartments.Select(selector: foundDepartment =>
            {
                return new GetAllTemporarilyRemovedDepartmentsResponse.Department()
                {
                    DepartmentId = foundDepartment.Id,
                    DepartmentName = foundDepartment.Name,
                    DepartmentRemovedAt = foundDepartment.RemovedAt,
                    DepartmentRemovedBy = foundDepartment.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all departments which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found departments.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Department>> GetAllTemporarilyRemovedDepartmentsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentAsNoTrackingSpecification,
                _superSpecificationManager.Department.DepartmentTemporarilyRemovedSpecification,
                _superSpecificationManager.Department.SelectFieldsFromDepartmentSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
