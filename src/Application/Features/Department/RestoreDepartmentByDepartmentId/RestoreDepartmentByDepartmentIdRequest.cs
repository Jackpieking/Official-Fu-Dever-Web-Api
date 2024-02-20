using System;
using Application.Features.Department.RestoreDepartmentByDepartmentId.Middlewares;
using MediatR;

namespace Application.Features.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department id request.
/// </summary>
public sealed class RestoreDepartmentByDepartmentIdRequest :
    IRequest<RestoreDepartmentByDepartmentIdResponse>,
    IRestoreDepartmentByDepartmentIdMiddleware
{
    public Guid DepartmentId { get; init; }
}
