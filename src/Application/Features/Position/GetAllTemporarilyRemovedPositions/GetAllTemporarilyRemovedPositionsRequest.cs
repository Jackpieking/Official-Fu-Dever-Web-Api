using Application.Features.Department.GetAllDepartmentsByDepartmentName.Middlewares;
using MediatR;

namespace Application.Features.Position.GetAllTemporarilyRemovedPositions;

/// <summary>
///     Get all temporarily removed positions request.
/// </summary>
public sealed class GetAllTemporarilyRemovedPositionsRequest :
    IRequest<GetAllTemporarilyRemovedPositionsResponse>,
    IGetAllDepartmentsByDepartmentNameMiddleware
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
