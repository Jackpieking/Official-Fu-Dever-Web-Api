using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Messaging;
using Domain.UnitOfWorks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RefreshToken.Commands.CreateRefreshToken;

/// <summary>
///     Create refresh token command handler.
/// </summary>
internal sealed class CreateRefreshTokenCommandHandler : ICommandHandler<
    CreateRefreshTokenCommand,
    string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateRefreshTokenCommand> _validator;

    public CreateRefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateRefreshTokenCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<string> Handle(
        CreateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        // Validate input.
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        if (!inputValidationResult.IsValid)
        {
            return default;
        }

        return await ExecuteTransactionAsync(
            request: request,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Execute the transaction of the main database.
    /// </summary>
    /// <param name="request">
    ///     Model of the request.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if transaction is success. Otherwise, false.
    /// </returns>
    private async Task<string> ExecuteTransactionAsync(
        CreateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        // Start adding entity transaction.
        var refreshTokenValue = string.Empty;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    var newRefreshToken = Domain.Entities.RefreshToken.Init(
                        claims: request.UserClaims,
                        rememberMe: request.RememberMe,
                        length: request.ValueLength);

                    await _unitOfWork.RefreshTokenRepository.AddAsync(
                        newEntity: newRefreshToken,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken: cancellationToken);

                    refreshTokenValue = newRefreshToken.Value;
                }
                catch
                {
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken: cancellationToken);
                }
                finally
                {
                    await _unitOfWork.DisposeTransactionAsync();
                }
            });

        return refreshTokenValue;
    }
}
