using FuDever.Application.Features.Role.GetAllRoles;
using FuDever.Application.Features.Role.GetAllRolesByRoleName;
using FuDever.Application.Interfaces.Caching;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.UpdateRoleByRoleId.Middlewares;

/// <summary>
///     Update role by role id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class UpdateRoleByRoleIdCachingMiddleware :
    IPipelineBehavior<
        UpdateRoleByRoleIdRequest,
        UpdateRoleByRoleIdResponse>,
    IUpdateRoleByRoleIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateRoleByRoleIdCachingMiddleware(
        ICacheHandler cacheHandler,
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _cacheHandler = cacheHandler;
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
    public async Task<UpdateRoleByRoleIdResponse> Handle(
        UpdateRoleByRoleIdRequest request,
        RequestHandlerDelegate<UpdateRoleByRoleIdResponse> next,
        CancellationToken cancellationToken)
    {
        // Finding current role by role id.
        var foundRole = await _unitOfWork.RoleRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleByIdSpecification(roleId: request.RoleId),
                //_superSpecificationManager.Role.SelectFieldsFromRoleSpecification.Ver3()
            ],
            cancellationToken: cancellationToken);

        // Role is found by role id.
        if (!Equals(objA: foundRole, objB: default))
        {
            await _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllRolesByRoleNameRequest)}_param_{foundRole.Name.ToLower()}",
                cancellationToken: cancellationToken);
        }

        var response = await next();

        if (response.StatusCode == UpdateRoleByRoleIdResponseStatusCode.OPERATION_SUCCESS)
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
