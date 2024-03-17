using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Role.GetAllRolesByRoleName;

/// <summary>
///     Get all roles by role name response.
/// </summary>
public sealed class GetAllRolesByRoleNameResponse
{
    public GetAllRolesByRoleNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Role> FoundRoles { get; init; }

    public sealed class Role
    {
        public Guid RoleId { get; init; }

        public string RoleName { get; init; }
    }
}
