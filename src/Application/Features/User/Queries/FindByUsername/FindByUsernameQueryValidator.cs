using FluentValidation;

namespace Application.Features.User.Queries.FindByUsername;

/// <summary>
///     Find by username query model validator.
/// </summary>
public sealed class FindByUsernameQueryValidator : AbstractValidator<FindByUsernameQuery>
{
    public FindByUsernameQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: query => query.Username)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: username => !string.IsNullOrWhiteSpace(value: username))
            .Must(predicate: username => username.Length <=
                Domain.Entities.User.Metadata.UserName.MaxLength);

        RuleFor(expression: query => query.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
