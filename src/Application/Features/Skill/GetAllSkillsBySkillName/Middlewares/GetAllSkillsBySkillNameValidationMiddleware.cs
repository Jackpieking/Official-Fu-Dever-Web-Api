using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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

    /// <summary>
    ///     Entry to middleware handler.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
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
                StatusCode = GetAllSkillsBySkillNameResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundSkills = default
            };
        }

        return await next();
    }
}
