using FluentValidation;

namespace Application.Features.User.Queries.IsUserEmailConfirmed;

/// <summary>
///     Is user email confirmed query model validator.
/// </summary>
public sealed class IsUserEmailConfirmedQueryValidator : AbstractValidator<IsUserEmailConfirmedQuery>
{
    public IsUserEmailConfirmedQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: query => query.User)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: user => !Equals(objA: user, objB: default));

        RuleFor(expression: query => query.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
