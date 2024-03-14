using FuDever.Application.Features.Major.GetAllMajors.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Major.GetAllMajors;

/// <summary>
///     Get all majors request.
/// </summary>
public sealed class GetAllMajorsRequest :
    IRequest<GetAllMajorsResponse>,
    IGetAllMajorsMiddleware
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
