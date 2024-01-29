using System;
using FluentValidation;

namespace Application.Features.User.Queries.IsUserRemovedByUserId;

/// <summary>
///     Is user temporarily removed by user id query model validator.
/// </summary>
public sealed class IsUserTemporarilyRemovedByUserIdQueryValidator : AbstractValidator<IsUserTemporarilyRemovedByUserIdQuery>
{
    public IsUserTemporarilyRemovedByUserIdQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: query => query.UserId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: userId => userId != Guid.Empty);

        RuleFor(expression: query => query.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
