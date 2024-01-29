using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Queries.FindByUsername;

/// <summary>
///     Find by username query handler.
/// </summary>
internal sealed class FindByUsernameQueryHandler : IQueryHandler<
    FindByUsernameQuery,
    Domain.Entities.User>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IValidator<FindByUsernameQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public FindByUsernameQueryHandler(
        UserManager<Domain.Entities.User> userManager,
        IValidator<FindByUsernameQuery> validator,
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
    public async Task<Domain.Entities.User> Handle(
        FindByUsernameQuery request,
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
            // Find user by username.
            return await _userManager.FindByNameAsync(userName: request.Username);
        }

        var cachedKey = $"{nameof(FindByUsernameQueryHandler)}_request_{request.Username}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<Domain.Entities.User>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<Domain.Entities.User>.NotFound))
        {
            return cacheModel.Value;
        }

        // Find user by username.
        var foundUser = await _userManager.FindByNameAsync(userName: request.Username);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: foundUser,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return foundUser;
    }
}
