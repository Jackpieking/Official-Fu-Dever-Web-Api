using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by department id request.
/// </summary>
public sealed class RemoveDepartmentPermanentlyByDepartmentIdRequest :
    IRequest<RemoveDepartmentPermanentlyByDepartmentIdResponse>,
    IRemoveDepartmentPermanentlyByDepartmentIdMiddleware
{
    public Guid DepartmentId { get; init; }

    public Guid DepartmentRemovedBy { get; init; }
}
