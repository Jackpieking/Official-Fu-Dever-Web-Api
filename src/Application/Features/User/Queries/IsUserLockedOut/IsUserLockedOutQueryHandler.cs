using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Queries.IsUserLockedOut;

/// <summary>
///     Is user locked out query handler.
/// </summary>
internal sealed class IsUserLockedOutQueryHandler : IQueryHandler<
    IsUserLockedOutQuery,
    bool>
{
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IValidator<IsUserLockedOutQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public IsUserLockedOutQueryHandler(
        SignInManager<Domain.Entities.User> signInManager,
        IValidator<IsUserLockedOutQuery> validator,
        ICacheHandler cacheHandler)
    {
        _signInManager = signInManager;
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
        IsUserLockedOutQuery request,
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

        SignInResult result;

        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            // Check if user password is correct.
            result = await _signInManager.CheckPasswordSignInAsync(
                user: request.User,
                password: request.Password,
                lockoutOnFailure: true);

            return result.IsLockedOut;
        }

        var cachedKey = $"{nameof(IsUserLockedOutQueryHandler)}_request_{request.User.Id}_{request.Password}";

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

        // Check if user password is correct.
        result = await _signInManager.CheckPasswordSignInAsync(
            user: request.User,
            password: request.Password,
            lockoutOnFailure: true);

        var isUserLockedOut = result.IsLockedOut;

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: isUserLockedOut,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return isUserLockedOut;
    }
}
