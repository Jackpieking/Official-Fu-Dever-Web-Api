using FluentValidation;

namespace FuDever.Application.Features.Role.GetAllRoles;

/// <summary>
///     Get all role request validator.
/// </summary>
public sealed class GetAllRolesRequestValidator : AbstractValidator<GetAllRolesRequest>
{
    public GetAllRolesRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
