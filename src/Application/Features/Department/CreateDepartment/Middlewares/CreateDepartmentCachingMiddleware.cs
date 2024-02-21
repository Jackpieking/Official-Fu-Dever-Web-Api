using Application.Features.Department.GetAllDepartments;
using Application.Features.Department.GetAllDepartmentsByDepartmentName;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.CreateDepartment.Middlewares;

/// <summary>
///     Create department request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreateDepartmentCachingMiddleware :
    IPipelineBehavior<
        CreateDepartmentRequest,
        CreateDepartmentResponse>,
    ICreateDepartmentMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public CreateDepartmentCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
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
    public async Task<CreateDepartmentResponse> Handle(
        CreateDepartmentRequest request,
        RequestHandlerDelegate<CreateDepartmentResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == CreateDepartmentStatusCode.OPERATION_SUCCESS)
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
