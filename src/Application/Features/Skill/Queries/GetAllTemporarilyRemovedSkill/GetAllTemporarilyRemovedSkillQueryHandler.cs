using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Application.Models;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Queries.GetAllTemporarilyRemovedSkill;

/// <summary>
///     Get all temporarily removed skill query handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedSkillQueryHandler : IQueryHandler<
    GetAllTemporarilyRemovedSkillQuery,
    IEnumerable<Domain.Entities.Skill>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly ICacheHandler _cacheHandler;

    public GetAllTemporarilyRemovedSkillQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        ICacheHandler cacheHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
        GetAllTemporarilyRemovedSkillQuery request,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                    _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification,
                    _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver2()
                ],
                cancellationToken: cancellationToken);
        }

        var cachedKey = $"{nameof(GetAllTemporarilyRemovedSkillQueryHandler)}_request";

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

        // Get all skills.
        var foundSkills = await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver2()
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
