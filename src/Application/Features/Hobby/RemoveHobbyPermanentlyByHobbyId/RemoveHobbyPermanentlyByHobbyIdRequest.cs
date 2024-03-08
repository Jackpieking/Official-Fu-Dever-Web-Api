using System;
using Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId.Middlewares;
using MediatR;

namespace Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;

/// <summary>
///     Request for removing hobby permanently by hobby id.
/// </summary>
public sealed class RemoveHobbyPermanentlyByHobbyIdRequest :
    IRequest<RemoveHobbyPermanentlyByHobbyIdResponse>,
    IRemoveHobbyPermanentlyByHobbyIdMiddleware
{
    public Guid HobbyId { get; init; }

    public Guid HobbyRemovedBy { get; init; }
}
