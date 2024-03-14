using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;

/// <summary>
///     Request for removing hobby temporarily by hobby id.
/// </summary>
public sealed class RemoveHobbyTemporarilyByHobbyIdRequest :
    IRequest<RemoveHobbyTemporarilyByHobbyIdResponse>,
    IRemoveHobbyTemporarilyByHobbyIdMiddleware
{
    public Guid HobbyId { get; init; }

    public Guid HobbyRemovedBy { get; init; }
}
