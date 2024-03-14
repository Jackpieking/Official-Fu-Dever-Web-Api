using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department id request.
/// </summary>
public sealed class RemoveDepartmentTemporarilyByDepartmentIdRequest :
    IRequest<RemoveDepartmentTemporarilyByDepartmentIdResponse>,
    IRemoveDepartmentTemporarilyByDepartmentIdMiddleware
{
    public Guid DepartmentId { get; init; }

    public Guid DepartmentRemovedBy { get; init; }
}
