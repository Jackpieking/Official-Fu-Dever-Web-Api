using FluentValidation;

namespace Application.Features.User.Queries.GetAllRoleOfUser;

/// <summary>
///     Get all role of user query model validator.
/// </summary>
public sealed class GetAllRoleOfUserQueryValidator : AbstractValidator<GetAllRoleOfUserQuery>
{
    public GetAllRoleOfUserQueryValidator()
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
