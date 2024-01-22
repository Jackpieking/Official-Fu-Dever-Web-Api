using Application.Interfaces.Messaging;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillId;

/// <summary>
///     Is skill found by skill id query handler.
/// </summary>
internal sealed class IsSkillFoundBySkillIdQueryHandler : IQueryHandler<
    IsSkillFoundBySkillIdQuery,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<IsSkillFoundBySkillIdQuery> _validator;

    public IsSkillFoundBySkillIdQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<IsSkillFoundBySkillIdQuery> validator)
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
    public async Task<bool> Handle(
        IsSkillFoundBySkillIdQuery request,
        CancellationToken cancellationToken)
    {
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        if (!inputValidationResult.IsValid)
        {
            return false;
        }

        var isSkillFound = await _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByIdSpecification(skillId: request.SkillId),
            ],
            cancellationToken: cancellationToken);

        return isSkillFound;
    }
}
