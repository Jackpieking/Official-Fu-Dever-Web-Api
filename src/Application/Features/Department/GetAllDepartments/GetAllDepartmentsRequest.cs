using Application.Features.Department.GetAllDepartments.Middlewares;
using MediatR;

namespace Application.Features.Department.GetAllDepartments;

/// <summary>
///     Get all department request.
/// </summary>
public sealed class GetAllDepartmentsRequest :
    IRequest<GetAllDepartmentsResponse>,
    IGetAllDepartmentsMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}
