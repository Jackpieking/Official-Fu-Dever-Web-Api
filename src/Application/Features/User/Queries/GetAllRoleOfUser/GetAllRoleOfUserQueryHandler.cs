using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Queries.GetAllRoleOfUser;

/// <summary>
///     Get all role of user query handler.
/// </summary>
internal sealed class GetAllRoleOfUserQueryHandler : IQueryHandler<
    GetAllRoleOfUserQuery,
    IList<string>>
{
    private readonly IValidator<GetAllRoleOfUserQuery> _validator;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly ICacheHandler _cacheHandler;

    public GetAllRoleOfUserQueryHandler(
        IValidator<GetAllRoleOfUserQuery> validator,
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
    public async Task<IList<string>> Handle(
        GetAllRoleOfUserQuery request,
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
            return await _userManager.GetRolesAsync(user: request.User);
        }

        var cachedKey = $"{nameof(GetAllRoleOfUserQueryHandler)}_request_{request.User.Id}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<IList<string>>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<IList<string>>.NotFound))
        {
            return cacheModel.Value;
        }

        // Find all roles of user.
        var foundUserRoles = await _userManager.GetRolesAsync(user: request.User);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: foundUserRoles,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return foundUserRoles;
    }
}
