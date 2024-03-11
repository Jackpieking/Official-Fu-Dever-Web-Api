using Application.Features.Major.CreateMajor.Middlewares;
using MediatR;

namespace Application.Features.Major.CreateMajor;

/// <summary>
///     Create major request.
/// </summary>
public sealed class CreateMajorRequest :
    IRequest<CreateMajorResponse>,
    ICreateMajorMiddleware
{
    public string NewMajorName { get; init; }
}
