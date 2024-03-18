using FluentValidation;
using System;

namespace FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;

/// <summary>
///     Remove role permanently by role id request validator.
/// </summary>
public sealed class RemoveRolePermanentlyByRoleIdRequestValidator :
    AbstractValidator<RemoveRolePermanentlyByRoleIdRequest>
{
    public RemoveRolePermanentlyByRoleIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.RoleId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: roleId => roleId != Guid.Empty);

        RuleFor(expression: request => request.RoleRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: roleRemovedBy => roleRemovedBy != Guid.Empty);
    }
}
