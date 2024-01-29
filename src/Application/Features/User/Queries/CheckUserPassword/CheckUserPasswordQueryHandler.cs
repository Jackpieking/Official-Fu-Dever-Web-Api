using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Queries.CheckUserPassword;

/// <summary>
///     Check user password query handler.
/// </summary>
internal sealed class CheckUserPasswordQueryHandler : IQueryHandler<
    CheckUserPasswordQuery,
    bool>
{
    private readonly IValidator<CheckUserPasswordQuery> _validator;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly ICacheHandler _cacheHandler;

    public CheckUserPasswordQueryHandler(
        IValidator<CheckUserPasswordQuery> validator,
        UserManager<Domain.Entities.User> userManager,
        ICacheHandler cacheHandler)
    {
        _userManager = userManager;
        _validator = validator;
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    ///     Entry of new query.
    /// </summary>
    /// <param name="request">
    ///     Query request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<bool> Handle(
        CheckUserPasswordQuery request,
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

        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            // Check user password.
            return await _userManager.CheckPasswordAsync(
                user: request.User,
                password: request.Password);
        }

        var cachedKey = $"{nameof(CheckUserPasswordQueryHandler)}_request_{request.User.Id}_{request.Password}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<bool>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<bool>.NotFound))
        {
            return cacheModel.Value;
        }

        // Check user password.
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(
            user: request.User,
            password: request.Password);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: isPasswordCorrect,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return isPasswordCorrect;
    }
}
