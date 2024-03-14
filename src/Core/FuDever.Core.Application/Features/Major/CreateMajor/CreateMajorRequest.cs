using FuDever.Application.Features.Major.CreateMajor.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Major.CreateMajor;

/// <summary>
///     Create major request.
/// </summary>
public sealed class CreateMajorRequest :
    IRequest<CreateMajorResponse>,
    ICreateMajorMiddleware
{
    public string NewMajorName { get; init; }
}
