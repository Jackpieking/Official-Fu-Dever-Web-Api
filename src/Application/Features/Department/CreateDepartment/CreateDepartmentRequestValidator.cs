using FluentValidation;

namespace Application.Features.Department.CreateDepartment;

/// <summary>
///     Create department request validator.
/// </summary>
public sealed class CreateDepartmentRequestValidator : AbstractValidator<CreateDepartmentRequest>
{
    public CreateDepartmentRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewDepartmentName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newDepartmentName =>
                !string.IsNullOrWhiteSpace(value: newDepartmentName))
            .Must(predicate: newDepartmentName => newDepartmentName.Length <=
                Domain.Entities.Department.Metadata.Name.MaxLength)
            .Must(predicate: newDepartmentName => newDepartmentName.Length >=
                Domain.Entities.Department.Metadata.Name.MinLength);
    }
}
