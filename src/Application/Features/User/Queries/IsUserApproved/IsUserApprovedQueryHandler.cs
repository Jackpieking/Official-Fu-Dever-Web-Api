using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;

namespace Application.Features.User.Queries.IsUserApproved;

/// <summary>
///     Is user approved query handler.
/// </summary>
internal sealed class IsUserApprovedQueryHandler : IQueryHandler<
    IsUserApprovedQuery,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<IsUserApprovedQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public IsUserApprovedQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<IsUserApprovedQuery> validator,
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
        IsUserApprovedQuery request,
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

        Domain.Entities.User foundUser;

        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            // Find user joining status.
            foundUser = await _unitOfWork.UserRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.User.UserAsNoTrackingSpecification,
                    _superSpecificationManager.User.UserByIdSpecification(userId: request.UserId),
                    _superSpecificationManager.User.SelectFieldsFromUserSpecification.Ver1()
                ],
                cancellationToken: cancellationToken);

            // Is user approved.
            return foundUser.UserJoiningStatus.Type.Equals(value: "Approved");
        }

        var cachedKey = $"{nameof(IsUserApprovedQueryHandler)}_request_{request.UserId}";

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

        // Find user joining status.
        foundUser = await _unitOfWork.UserRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.User.UserAsNoTrackingSpecification,
                _superSpecificationManager.User.UserByIdSpecification(userId: request.UserId),
                _superSpecificationManager.User.SelectFieldsFromUserSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);

        // Is user approved.
        var isUserApproved = foundUser.UserJoiningStatus.Type.Equals(value: "Approved");

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: isUserApproved,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return isUserApproved;
    }
}
