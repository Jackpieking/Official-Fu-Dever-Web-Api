using System.Threading;
using System.Threading.Tasks;
using Application.Features.Skill.GetAllSkillsByName;
using Application.Features.Skill.GetAllSkillsByName.Middlewares;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.GetAllSkillsBySkillName.Middlewares;

/// <summary>
///     Get all skills by skill name
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllSkillsBySkillNameValidationMiddleware :
    IPipelineBehavior<
        GetAllSkillsBySkillNameRequest,
        GetAllSkillsBySkillNameResponse>,
    IGetAllSkillsBySkillNameMiddleware
{
    private readonly IValidator<GetAllSkillsBySkillNameRequest> _validator;

    public GetAllSkillsBySkillNameValidationMiddleware(IValidator<GetAllSkillsBySkillNameRequest> validator)
    {
        _validator = validator;
    }

    public async Task<GetAllSkillsBySkillNameResponse> Handle(
        GetAllSkillsBySkillNameRequest request,
        RequestHandlerDelegate<GetAllSkillsBySkillNameResponse> next,
        CancellationToken cancellationToken)
    {
        // Validate input.
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        // Input is not valid.
        if (!inputValidationResult.IsValid)
        {
            return new()
            {
                StatusCode = GetAllSkillsBySkillNameStatusCode.INPUT_VALIDATION_FAIL,
                FoundSkills = default
            };
        }

        return await next();
    }
}
