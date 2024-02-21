using Application.Features.Department.GetAllDepartments;
using Application.Features.Department.GetAllDepartmentsByDepartmentName;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.UpdateDepartmentByDepartmentId.Middlewares;

/// <summary>
///     Update department by department id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class UpdateDepartmentByDepartmentIdCachingMiddleware :
    IPipelineBehavior<
        UpdateDepartmentByDepartmentIdRequest,
        UpdateDepartmentByDepartmentIdResponse>,
    IUpdateDepartmentByDepartmentIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateDepartmentByDepartmentIdCachingMiddleware(
        ICacheHandler cacheHandler,
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _cacheHandler = cacheHandler;
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    /// <summary>
    ///     Entry to middleware handler.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
    public async Task<UpdateDepartmentByDepartmentIdResponse> Handle(
        UpdateDepartmentByDepartmentIdRequest request,
        RequestHandlerDelegate<UpdateDepartmentByDepartmentIdResponse> next,
        CancellationToken cancellationToken)
    {
        // finding current department by department id.
        var foundDepartment = await _unitOfWork.DepartmentRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Department.DepartmentByIdSpecification(departmentId: request.DepartmentId),
                _superSpecificationManager.Department.SelectFieldsFromDepartmentSpecification.Ver3()
            ],
            cancellationToken: cancellationToken);

        // Department is found by department id.
        if (!Equals(objA: foundDepartment, objB: default))
        {
            await _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllDepartmentsByDepartmentNameRequest)}_param_{foundDepartment.Name.ToLower()}",
                cancellationToken: cancellationToken);
        }

        var response = await next();

        if (response.StatusCode == UpdateDepartmentByDepartmentIdStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllDepartmentsByDepartmentNameRequest)}_param_{request.NewDepartmentName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllDepartmentsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
