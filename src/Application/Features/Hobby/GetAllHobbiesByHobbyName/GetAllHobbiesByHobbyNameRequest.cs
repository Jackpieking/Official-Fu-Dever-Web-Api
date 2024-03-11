using Application.Features.Hobby.GetAllHobbiesByHobbyName.Middlewares;
using MediatR;

namespace Application.Features.Hobby.GetAllHobbiesByHobbyName;

/// <summary>
///     Request for getting all hobbies by hobby name.
/// </summary>
public sealed class GetAllHobbiesByHobbyNameRequest :
    IRequest<GetAllHobbiesByHobbyNameResponse>,
    IGetAllHobbiesByHobbyNameMiddleware
{
    public string HobbyName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}