using FluentValidation;

namespace Application.Features.User.Queries.CheckUserPassword;

/// <summary>
///     Check user password query model validator.
/// </summary>
public sealed class CheckUserPasswordQueryValidator : AbstractValidator<CheckUserPasswordQuery>
{
    public CheckUserPasswordQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: query => query.User)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: user => !Equals(objA: user, objB: default));

        RuleFor(expression: query => query.Password)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: password => !string.IsNullOrWhiteSpace(value: password))
            .Must(predicate: password => password.Length <=
                Domain.Entities.User.Metadata.Password.MaxLength);

        RuleFor(expression: query => query.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
