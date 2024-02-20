using System;
using Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId.Middlewares;
using MediatR;

namespace Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;

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
