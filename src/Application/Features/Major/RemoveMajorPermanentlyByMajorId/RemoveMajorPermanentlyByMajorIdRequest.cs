using Application.Features.Major.RemoveMajorPermanentlyByMajorId.Middlewares;
using MediatR;
using System;

namespace Application.Features.Major.RemoveMajorPermanentlyByMajorId;

/// <summary>
///     Remove major permanently by major Id request
/// </summary>
public sealed class RemoveMajorPermanentlyByMajorIdRequest :
    IRequest<RemoveMajorPermanentlyByMajorIdResponse>,
    IRemoveMajorPermanentlyByMajorIdMiddleware
{
    public Guid MajorId { get; init; }

    public Guid MajorRemovedBy { get; init; }
}
