using FuDever.Application.Features.Role.GetAllRoles;
using FuDever.Application.Features.Role.GetAllRolesByRoleName;
using FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;
using FuDever.Application.Interfaces.Caching;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId.Middlewares;

/// <summary>
///     Remove role temporarily by role
///     id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveRoleTemporarilyByRoleIdCachingMiddleware :
    IPipelineBehavior<
        RemoveRoleTemporarilyByRoleIdRequest,
        RemoveRoleTemporarilyByRoleIdResponse>,
    IRemoveRoleTemporarilyByRoleIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveRoleTemporarilyByRoleIdCachingMiddleware(
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
    public async Task<RemoveRoleTemporarilyByRoleIdResponse> Handle(
        RemoveRoleTemporarilyByRoleIdRequest request,
        RequestHandlerDelegate<RemoveRoleTemporarilyByRoleIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveRoleTemporarilyByRoleIdResponseStatusCode.OPERATION_SUCCESS)
        {
            var foundRole = await _unitOfWork.RoleRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Role.RoleByIdSpecification(roleId: request.RoleId),
                    //_superSpecificationManager.Role.SelectFieldsFromRoleSpecification.Ver3()
                ],
                cancellationToken: cancellationToken);

            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllRolesByRoleNameRequest)}_param_{foundRole.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllRolesRequest),
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllTemporarilyRemovedRolesRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
