using FluentValidation;

namespace FuDever.Application.Features.Auth.Login;

/// <summary>
///     Login request validator.
/// </summary>
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.Username)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: username =>
                !string.IsNullOrWhiteSpace(value: username))
            .Must(predicate: username => username.Length <=
                Domain.Entities.User.Metadata.UserName.MaxLength)
            .Must(predicate: username => username.Length >=
                Domain.Entities.User.Metadata.UserName.MinLength);

        RuleFor(expression: command => command.Password)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: password =>
                !string.IsNullOrWhiteSpace(value: password))
            .Must(predicate: password => password.Length <=
                Domain.Entities.User.Metadata.Password.MaxLength)
            .Must(predicate: password => password.Length >=
                Domain.Entities.User.Metadata.Password.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
