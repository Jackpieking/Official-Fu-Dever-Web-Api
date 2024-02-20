using Application.Features.Department.GetAllTemporarilyRemovedDepartments.Middlewares;
using MediatR;

namespace Application.Features.Department.GetAllTemporarilyRemovedDepartments;

/// <summary>
///     Get all temporarily removed departments request.
/// </summary>
public sealed class GetAllTemporarilyRemovedDepartmentsRequest :
    IRequest<GetAllTemporarilyRemovedDepartmentsResponse>,
    IGetAllTemporarilyRemovedDepartmentsMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; } = default;
}
