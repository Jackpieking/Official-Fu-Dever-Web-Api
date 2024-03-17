using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;

/// <summary>
///     Get all temporarily removed roles response.
/// </summary>
public sealed class GetAllTemporarilyRemovedRolesResponse
{
    public GetAllTemporarilyRemovedRolesResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Role> FoundTemporarilyRemovedRoles { get; init; }

    public sealed class Role
    {
        public Guid RoleId { get; init; }

        public string RoleName { get; init; }

        public DateTime RoleRemovedAt { get; init; }

        public Guid RoleRemovedBy { get; init; }
    }
}
