using Application.Features.Major.GetAllMajorsByMajorName.Middlewares;
using MediatR;

namespace Application.Features.Major.GetAllMajorsByMajorName;

/// <summary>
///     Get all majors by major name request.
/// </summary>
public sealed class GetAllMajorsByMajorNameRequest :
    IRequest<GetAllMajorsByMajorNameResponse>,
    IGetAllMajorsByMajorNameMiddleware
{
    public string MajorName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}
