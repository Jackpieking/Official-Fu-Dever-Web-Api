using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Messaging;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;

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

    internal FindBySkillNameQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<FindBySkillNameQuery> validator)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
        _validator = validator;
    }

    /// <summary>
    ///     Entry of new query.
    /// </summary>
    /// <param name="request">
    ///     Query request modal.
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
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        if (!inputValidationResult.IsValid)
        {
            return [];
        }

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

        return foundSkills;
    }
}
