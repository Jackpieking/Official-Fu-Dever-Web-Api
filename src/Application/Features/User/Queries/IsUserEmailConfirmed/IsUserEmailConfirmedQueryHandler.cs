using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Queries.IsUserEmailConfirmed;

/// <summary>
///     Is user email confirmed query handler.
/// </summary>
internal sealed class IsUserEmailConfirmedQueryHandler : IQueryHandler<
    IsUserEmailConfirmedQuery,
    bool>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IValidator<IsUserEmailConfirmedQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public IsUserEmailConfirmedQueryHandler(
        UserManager<Domain.Entities.User> userManager,
        IValidator<IsUserEmailConfirmedQuery> validator,
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
        IsUserEmailConfirmedQuery request,
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
            // Validate if user email is confirmed.
            return await _userManager.IsEmailConfirmedAsync(user: request.User);
        }

        var cachedKey = $"{nameof(IsUserEmailConfirmedQueryHandler)}_request_{request.User.Id}";

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

        // Validate if user email is confirmed.
        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user: request.User);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: isEmailConfirmed,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return isEmailConfirmed;
    }
}
