using Application.Features.Hobby.GetAllHobbies.Middlewares;
using MediatR;

namespace Application.Features.Hobby.GetAllHobbies;

/// <summary>
///     Get all hobbies request.
/// </summary>
public sealed class GetAllHobbiesRequest :
    IRequest<GetAllHobbiesResponse>,
    IGetAllHobbiesMiddleware
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
