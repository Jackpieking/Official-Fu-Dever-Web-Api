using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.GetAllSkills.Middlewares;

/// <summary>
///     Get all skills request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllSkillsValidationMiddleware :
    IPipelineBehavior<
        GetAllSkillsRequest,
        GetAllSkillsResponse>,
    IGetAllSkillsMiddleware
{
    private readonly IValidator<GetAllSkillsRequest> _validator;

    public GetAllSkillsValidationMiddleware(IValidator<GetAllSkillsRequest> validator)
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
    public async Task<GetAllSkillsResponse> Handle(
        GetAllSkillsRequest request,
        RequestHandlerDelegate<GetAllSkillsResponse> next,
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
                StatusCode = GetAllSkillsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundSkills = default
            };
        }

        return await next();
    }
}
