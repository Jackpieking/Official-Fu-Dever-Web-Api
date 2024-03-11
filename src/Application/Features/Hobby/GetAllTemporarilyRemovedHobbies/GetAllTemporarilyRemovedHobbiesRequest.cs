using Application.Features.Hobby.GetAllTemporarilyRemovedHobbies.Middlewares;
using MediatR;

namespace Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;

/// <summary>
///     Request for getting all temporarily removed hobbies.
/// </summary>
public sealed class GetAllTemporarilyRemovedHobbiesRequest :
    IRequest<GetAllTemporarilyRemovedHobbiesResponse>,
    IGetAllTemporarilyRemovedHobbiesMiddleware
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
