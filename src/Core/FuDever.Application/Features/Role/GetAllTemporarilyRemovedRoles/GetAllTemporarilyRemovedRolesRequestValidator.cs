using FluentValidation;

namespace FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;

/// <summary>
///     Get all temporarily removed roles request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedRolesRequestValidator :
    AbstractValidator<GetAllTemporarilyRemovedRolesRequest>
{
    public GetAllTemporarilyRemovedRolesRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
