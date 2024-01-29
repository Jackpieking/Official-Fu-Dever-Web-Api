using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Queries.FindBySkillName;

/// <summary>
///     Find by skill name query handler.
/// </summary>
internal sealed class FindBySkillNameQueryHandler : IQueryHandler<
    FindBySkillNameQuery,
    IEnumerable<Domain.Entities.Skill>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<FindBySkillNameQuery> _validator;
    private readonly ICacheHandler _cacheHandler;

    public FindBySkillNameQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<FindBySkillNameQuery> validator,
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
    public async Task<IEnumerable<Domain.Entities.Skill>> Handle(
        FindBySkillNameQuery request,
        CancellationToken cancellationToken)
    {
        // Validate input.
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        if (!inputValidationResult.IsValid)
        {
            return [];
        }

        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            // Get all skills by name.
            return await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                    _superSpecificationManager.Skill.SkillByNameSpecification(
                        skillName: request.SkillName,
                        isCaseSensitive: false),
                    _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                    _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
                ],
                cancellationToken: cancellationToken);
        }

        var cachedKey = $"{nameof(FindBySkillNameQueryHandler)}_request_{request.SkillName.ToLower()}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<IEnumerable<Domain.Entities.Skill>>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<IEnumerable<Domain.Entities.Skill>>.NotFound))
        {
            return cacheModel.Value;
        }

        // Get all skills by name.
        var foundSkills = await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillByNameSpecification(
                    skillName: request.SkillName,
                    isCaseSensitive: false),
                _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);

        // Cache the query result.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: foundSkills,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return foundSkills;
    }
}
