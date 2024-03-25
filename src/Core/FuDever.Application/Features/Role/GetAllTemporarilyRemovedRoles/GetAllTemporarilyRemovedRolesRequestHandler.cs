using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;

/// <summary>
///     Get all temporarily removed roles request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedRolesRequestHandler : IRequestHandler<
    GetAllTemporarilyRemovedRolesRequest,
    GetAllTemporarilyRemovedRolesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedRolesRequestHandler(
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
    public async Task<GetAllTemporarilyRemovedRolesResponse> Handle(
        GetAllTemporarilyRemovedRolesRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed roles.
        var foundTemporarilyRemovedRoles = await GetAllTemporarilyRemovedRolesQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedRolesResponseStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedRoles = foundTemporarilyRemovedRoles.Select(selector: foundRole =>
            {
                return new GetAllTemporarilyRemovedRolesResponse.Role()
                {
                    RoleId = foundRole.Id,
                    RoleName = foundRole.Name,
                    RoleRemovedAt = foundRole.RemovedAt,
                    RoleRemovedBy = foundRole.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all roles which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found roles.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Role>> GetAllTemporarilyRemovedRolesQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleAsNoTrackingSpecification,
                _superSpecificationManager.Role.RoleTemporarilyRemovedSpecification,
                _superSpecificationManager.Role.RoleNameIsNotDefaultSpecification,
                _superSpecificationManager.Role.SelectFieldsFromRoleSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
