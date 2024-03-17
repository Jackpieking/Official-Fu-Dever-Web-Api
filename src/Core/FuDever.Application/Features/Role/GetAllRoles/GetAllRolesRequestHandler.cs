using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllRoles;

/// <summary>
///     Get all role request handler.
/// </summary>
internal sealed class GetAllRolesRequestHandler : IRequestHandler<
    GetAllRolesRequest,
    GetAllRolesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllRolesRequestHandler(
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
    ///     A task containing the response.
    /// </returns>
    public async Task<GetAllRolesResponse> Handle(
        GetAllRolesRequest request,
        CancellationToken cancellationToken)
    {
        // Get all non temporarily removed roles.
        var foundRoles = await GetAllNonTemporarilyRemovedRolesQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllRolesResponseStatusCode.OPERATION_SUCCESS,
            FoundRoles = foundRoles.Select(selector: foundRole =>
            {
                return new GetAllRolesResponse.Role()
                {
                    RoleId = foundRole.Id,
                    RoleName = foundRole.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all roles which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found roles.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Role>> GetAllNonTemporarilyRemovedRolesQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleAsNoTrackingSpecification,
                _superSpecificationManager.Role.RoleNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Role.SelectFieldsFromRoleSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
