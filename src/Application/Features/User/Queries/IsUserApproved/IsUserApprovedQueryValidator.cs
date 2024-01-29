using System;
using FluentValidation;

namespace Application.Features.User.Queries.IsUserApproved;

/// <summary>
///     Is user approved query model validator.
/// </summary>
public sealed class IsUserApprovedQueryValidator : AbstractValidator<IsUserApprovedQuery>
{
    public IsUserApprovedQueryValidator()
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
