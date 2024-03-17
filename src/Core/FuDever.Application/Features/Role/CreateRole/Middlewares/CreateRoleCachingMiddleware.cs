using FuDever.Application.Features.Role.GetAllRoles;
using FuDever.Application.Features.Role.GetAllRolesByRoleName;
using FuDever.Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.CreateRole.Middlewares;

/// <summary>
///     Create role request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreateRoleCachingMiddleware :
    IPipelineBehavior<
        CreateRoleRequest,
        CreateRoleResponse>,
    ICreateRoleMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public CreateRoleCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<CreateRoleResponse> Handle(
        CreateRoleRequest request,
        RequestHandlerDelegate<CreateRoleResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == CreateRoleResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllRolesByRoleNameRequest)}_param_{request.NewRoleName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllRolesRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
