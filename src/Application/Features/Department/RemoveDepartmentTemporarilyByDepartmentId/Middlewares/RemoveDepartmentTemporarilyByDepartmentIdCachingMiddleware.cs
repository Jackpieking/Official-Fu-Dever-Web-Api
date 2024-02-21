using Application.Features.Department.GetAllDepartments;
using Application.Features.Department.GetAllDepartmentsByDepartmentName;
using Application.Features.Department.GetAllTemporarilyRemovedDepartments;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId.Middlewares;

/// <summary>
///     Remove department temporarily by department
///     id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveDepartmentTemporarilyByDepartmentIdCachingMiddleware :
    IPipelineBehavior<
        RemoveDepartmentTemporarilyByDepartmentIdRequest,
        RemoveDepartmentTemporarilyByDepartmentIdResponse>,
    IRemoveDepartmentTemporarilyByDepartmentIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveDepartmentTemporarilyByDepartmentIdCachingMiddleware(
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
    public async Task<RemoveDepartmentTemporarilyByDepartmentIdResponse> Handle(
        RemoveDepartmentTemporarilyByDepartmentIdRequest request,
        RequestHandlerDelegate<RemoveDepartmentTemporarilyByDepartmentIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveDepartmentTemporarilyByDepartmentIdStatusCode.OPERATION_SUCCESS)
        {
            var foundDepartment = await _unitOfWork.DepartmentRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Department.DepartmentByIdSpecification(departmentId: request.DepartmentId),
                    _superSpecificationManager.Department.SelectFieldsFromDepartmentSpecification.Ver3()
                ],
                cancellationToken: cancellationToken);

            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllDepartmentsByDepartmentNameRequest)}_param_{foundDepartment.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllDepartmentsRequest),
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllTemporarilyRemovedDepartmentsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
