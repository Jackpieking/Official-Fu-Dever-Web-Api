using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.GetAllDepartmentsByDepartmentName;

/// <summary>
///     Get all departments by department name request handler.
/// </summary>
internal sealed class GetAllDepartmentsByDepartmentNameRequestHandler : IRequestHandler<
    GetAllDepartmentsByDepartmentNameRequest,
    GetAllDepartmentsByDepartmentNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllDepartmentsByDepartmentNameRequestHandler(
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
    public async Task<GetAllDepartmentsByDepartmentNameResponse> Handle(
        GetAllDepartmentsByDepartmentNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all departments by department name.
        var foundDepartments = await GetAllDepartmentsByDepartmentNameQueryAsync(
            departmentName: request.DepartmentName,
            cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllDepartmentsByDepartmentNameResponseStatusCode.OPERATION_SUCCESS,
            FoundDepartments = foundDepartments.Select(selector: foundDepartment =>
            {
                return new GetAllDepartmentsByDepartmentNameResponse.Department()
                {
                    DepartmentId = foundDepartment.Id,
                    DepartmentName = foundDepartment.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Retrieves all departments based on
    ///     the department name asynchronously.
    /// </summary>
    /// <param name="departmentName">
    ///     The name of the department to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation
    ///     that yields an enumerable collection of departments.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Department>> GetAllDepartmentsByDepartmentNameQueryAsync(
        string departmentName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.DepartmentRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentAsNoTrackingSpecification,
                _superSpecificationManager.Department.DepartmentByNameSpecification(
                    departmentName: departmentName,
                    isCaseSensitive: false),
                _superSpecificationManager.Department.DepartmentNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Department.SelectFieldsFromDepartmentSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
