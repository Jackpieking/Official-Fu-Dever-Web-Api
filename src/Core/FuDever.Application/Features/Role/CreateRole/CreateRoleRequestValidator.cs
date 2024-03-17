using FluentValidation;

namespace FuDever.Application.Features.Role.CreateRole;

/// <summary>
///     Create role request validator.
/// </summary>
public sealed class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewRoleName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newRoleName =>
                !string.IsNullOrWhiteSpace(value: newRoleName))
            .Must(predicate: newRoleName => newRoleName.Length <=
                Domain.Entities.Role.Metadata.Name.MaxLength)
            .Must(predicate: newRoleName => newRoleName.Length >=
                Domain.Entities.Role.Metadata.Name.MinLength);
    }
}
