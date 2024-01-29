using FluentValidation;

namespace Application.Features.RefreshToken.Commands.CreateRefreshToken;

/// <summary>
///     Create refresh token command model validator.
/// </summary>
public sealed class CreateRefreshTokenCommandValidator : AbstractValidator<CreateRefreshTokenCommand>
{
    public CreateRefreshTokenCommandValidator()
    {
    }
}
