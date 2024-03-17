using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllRolesByRoleName;

/// <summary>
///     Get all roles by role name request handler.
/// </summary>
internal sealed class GetAllRolesByRoleNameRequestHandler : IRequestHandler<
    GetAllRolesByRoleNameRequest,
    GetAllRolesByRoleNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllRolesByRoleNameRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<GetAllRolesByRoleNameResponse> Handle(
        GetAllRolesByRoleNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all roles by role name.
        var foundRoles = await GetAllRolesByRoleNameQueryAsync(
            roleName: request.RoleName,
            cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllRolesByRoleNameResponseStatusCode.OPERATION_SUCCESS,
            FoundRoles = foundRoles.Select(selector: foundRole =>
            {
                return new GetAllRolesByRoleNameResponse.Role()
                {
                    RoleId = foundRole.Id,
                    RoleName = foundRole.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Retrieves all roles based on
    ///     the role name asynchronously.
    /// </summary>
    /// <param name="roleName">
    ///     The name of the role to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation
    ///     that yields an enumerable collection of roles.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Role>> GetAllRolesByRoleNameQueryAsync(
        string roleName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleAsNoTrackingSpecification,
                _superSpecificationManager.Role.RoleByNameSpecification(
                    roleName: roleName,
                    isCaseSensitive: false),
                _superSpecificationManager.Role.RoleNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Role.SelectFieldsFromRoleSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
