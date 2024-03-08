﻿using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Features.Hobby.GetAllHobbies.Middlewares;

/// <summary>
///     Get all hobbies request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllHobbiesValidationMiddleware :
    IPipelineBehavior<
        GetAllHobbiesRequest,
        GetAllHobbiesResponse>,
    IGetAllHobbiesMiddleware
{
    private readonly IValidator<GetAllHobbiesRequest> _validator;

    public GetAllHobbiesValidationMiddleware(IValidator<GetAllHobbiesRequest> validator)
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
    public async Task<GetAllHobbiesResponse> Handle(
        GetAllHobbiesRequest request,
        RequestHandlerDelegate<GetAllHobbiesResponse> next,
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
                StatusCode = GetAllHobbiesResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundHobbies = default
            };
        }

        return await next();
    }
}
