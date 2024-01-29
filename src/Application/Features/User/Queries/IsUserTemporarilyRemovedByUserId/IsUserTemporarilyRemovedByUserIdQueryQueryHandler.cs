using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;

namespace Application.Features.User.Queries.IsUserRemovedByUserId;

/// <summary>
///     Is user temporarily removed by user id query handler.
/// </summary>
internal sealed class IsUserTemporarilyRemovedByUserIdQueryQueryHandler : IQueryHandler<
    IsUserTemporarilyRemovedByUserIdQuery,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<IsUserTemporarilyRemovedByUserIdQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public IsUserTemporarilyRemovedByUserIdQueryQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<IsUserTemporarilyRemovedByUserIdQuery> validator,
        ICacheHandler cacheHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
        IsUserTemporarilyRemovedByUserIdQuery request,
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
            // Is user temporarily removed.
            return await _unitOfWork.UserRepository.IsFoundBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.User.UserByIdSpecification(userId: request.UserId),
                    _superSpecificationManager.User.UserTemporarilyRemovedSpecification
                ],
                cancellationToken: cancellationToken);
        }

        var cachedKey = $"{nameof(IsUserTemporarilyRemovedByUserIdQueryQueryHandler)}_request_{request.UserId}";

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

        // Is user temporarily removed.
        var isUserTemporarilyRemoved = await _unitOfWork.UserRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.User.UserByIdSpecification(userId: request.UserId),
                _superSpecificationManager.User.UserTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: isUserTemporarilyRemoved,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return isUserTemporarilyRemoved;
    }
}
