using FluentValidation;
using System;

namespace FuDever.Application.Features.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role id request validator.
/// </summary>
public sealed class UpdateRoleByRoleIdRequestValidator :
    AbstractValidator<UpdateRoleByRoleIdRequest>
{
    public UpdateRoleByRoleIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.RoleId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: roleId => roleId != Guid.Empty);

        RuleFor(expression: request => request.RoleUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: roleUpdatedBy => roleUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewRoleName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newRoleName =>
                !string.IsNullOrWhiteSpace(value: newRoleName))
            .Must(predicate: newRoleName => newRoleName.Length <=
                Domain.Entities.Role.Metadata.Name.MaxLength)
            .Must(predicate: newRoleName => newRoleName.Length >=
                Domain.Entities.Role.Metadata.Name.MinLength);
    }
}
