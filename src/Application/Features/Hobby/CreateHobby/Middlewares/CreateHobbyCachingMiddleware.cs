using Application.Features.Hobby.GetAllHobbies;
using Application.Features.Hobby.GetAllHobbiesByHobbyName;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Hobby.CreateHobby.Middlewares;

/// <summary>
///     Create hobby request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreateHobbyCachingMiddleware :
    IPipelineBehavior<
        CreateHobbyRequest,
        CreateHobbyResponse>,
    ICreateHobbyMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public CreateHobbyCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    ///     Entry to middleware handler.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
    public async Task<CreateHobbyResponse> Handle(
        CreateHobbyRequest request,
        RequestHandlerDelegate<CreateHobbyResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == CreateHobbyResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllHobbiesByHobbyNameRequest)}_param_{request.NewHobbyName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllHobbiesRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
