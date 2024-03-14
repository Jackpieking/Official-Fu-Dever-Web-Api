using FuDever.Application.Features.Major.UpdateMajorByMajorId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major id request.
/// </summary>
public sealed class UpdateMajorByMajorIdRequest :
    IRequest<UpdateMajorByMajorIdResponse>,
    IUpdateMajorByMajorIdMiddleware
{
    public Guid MajorId { get; init; }

    public string NewMajorName { get; init; }

    public Guid MajorUpdatedBy { get; init; }
}
