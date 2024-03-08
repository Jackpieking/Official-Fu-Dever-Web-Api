using Application.Features.Hobby.CreateHobby.Middlewares;
using MediatR;

namespace Application.Features.Hobby.CreateHobby;

/// <summary>
///     Request to create a new hobby.
/// </summary>
public sealed class CreateHobbyRequest :
    IRequest<CreateHobbyResponse>,
    ICreateHobbyMiddleware
{
    public string NewHobbyName { get; init; }
}
