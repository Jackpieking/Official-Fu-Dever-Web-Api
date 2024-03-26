using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of update
///     field of role specification.
/// </summary>
internal sealed class UpdateFieldOfRoleSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IUpdateFieldOfRoleSpecification
{
    public IUpdateFieldOfRoleSpecification Ver1(
        DateTime roleRemovedAt,
        Guid roleRemovedBy)
    {
        // Validate role removed time.
        if (roleRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate role remover.
        if (roleRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                role => role.RemovedAt,
                roleRemovedAt)
            .SetProperty(
                role => role.RemovedBy,
                roleRemovedBy);

        return this;
    }

    public IUpdateFieldOfRoleSpecification Ver2(string roleName)
    {
        // Validate role name.
        if (string.IsNullOrWhiteSpace(value: roleName) ||
            roleName.Length > Domain.Entities.Role.Metadata.Name.MaxLength ||
            roleName.Length < Domain.Entities.Role.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall.SetProperty(
            role => role.Name,
            roleName);

        return this;
    }
}
