using Application.Features.Major.GetAllTemporarilyRemovedMajors.Middlewares;
using MediatR;

namespace Application.Features.Major.GetAllTemporarilyRemovedMajors;

/// <summary>
///     Get all temporarily removed majors request.
/// </summary>
public sealed class GetAllTemporarilyRemovedMajorsRequest :
    IRequest<GetAllTemporarilyRemovedMajorsResponse>,
    IGetAllTemporarilyRemovedMajorsMiddleware
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
