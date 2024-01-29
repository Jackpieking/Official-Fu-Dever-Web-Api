using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillName;

/// <summary>
///     Is skill found by skill name query handler.
/// </summary>
internal sealed class IsSkillFoundBySkillNameQueryHandler : IQueryHandler<
    IsSkillFoundBySkillNameQuery,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<IsSkillFoundBySkillNameQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public IsSkillFoundBySkillNameQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<IsSkillFoundBySkillNameQuery> validator,
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
        IsSkillFoundBySkillNameQuery request,
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
            return await _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Skill.SkillByNameSpecification(
                        skillName: request.SkillName,
                        isCaseSensitive: true),
                ],
                cancellationToken: cancellationToken);
        }

        var cachedKey = $"{nameof(IsSkillFoundBySkillNameQueryHandler)}_request_{request.SkillName}";

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

        // Is skill found by skill name.
        var isSkillFound = await _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByNameSpecification(
                    skillName: request.SkillName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: isSkillFound,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return isSkillFound;
    }
}
