using Application.Features.Department.GetAllDepartmentsByDepartmentName.Middlewares;
using MediatR;

namespace Application.Features.Department.GetAllDepartmentsByDepartmentName;

/// <summary>
///     Get all departments by department name request.
/// </summary>
public sealed class GetAllDepartmentsByDepartmentNameRequest :
    IRequest<GetAllDepartmentsByDepartmentNameResponse>,
    IGetAllDepartmentsByDepartmentNameMiddleware
{
    public string DepartmentName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; } = default;
}
