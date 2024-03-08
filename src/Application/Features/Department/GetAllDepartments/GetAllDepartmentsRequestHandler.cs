using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.GetAllDepartments;

/// <summary>
///     Get all department request handler.
/// </summary>
internal sealed class GetAllDepartmentsRequestHandler : IRequestHandler<
    GetAllDepartmentsRequest,
    GetAllDepartmentsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllDepartmentsRequestHandler(
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
    public async Task<GetAllDepartmentsResponse> Handle(
        GetAllDepartmentsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all non temporarily removed departments.
        var foundDepartments = await GetAllNonTemporarilyRemovedDepartmentsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllDepartmentsResponseStatusCode.OPERATION_SUCCESS,
            FoundDepartments = foundDepartments.Select(selector: foundDepartment =>
            {
                return new GetAllDepartmentsResponse.Department()
                {
                    DepartmentId = foundDepartment.Id,
                    DepartmentName = foundDepartment.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all departments which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found departments.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Department>> GetAllNonTemporarilyRemovedDepartmentsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentAsNoTrackingSpecification,
                _superSpecificationManager.Department.DepartmentNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Department.SelectFieldsFromDepartmentSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
