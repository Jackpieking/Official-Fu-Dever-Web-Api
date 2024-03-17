using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Role.GetAllRoles;

/// <summary>
///     Get all role response.
/// </summary>
public sealed class GetAllRolesResponse
{
    public GetAllRolesResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Role> FoundRoles { get; init; }

    public sealed class Role
    {
        public Guid RoleId { get; init; }

        public string RoleName { get; init; }
    }
}
