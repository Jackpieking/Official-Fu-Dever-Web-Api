using FluentValidation;

namespace Application.Features.Position.GetAllPositionsByPositionName;

/// <summary>
///     Get all position by position name request validator.
/// </summary>
public sealed class GetAllPositionsByPositionNameValidator :
    AbstractValidator<GetAllPositionsByPositionNameRequest>
{
    public GetAllPositionsByPositionNameValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PositionName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: positionName =>
                !string.IsNullOrWhiteSpace(value: positionName))
            .Must(predicate: positionName => positionName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength)
            .Must(predicate: positionName => positionName.Length >=
                Domain.Entities.Skill.Metadata.Name.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
