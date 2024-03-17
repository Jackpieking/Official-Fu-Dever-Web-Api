using FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;

/// <summary>
///     Remove role permanently by role id request.
/// </summary>
public sealed class RemoveRolePermanentlyByRoleIdRequest :
    IRequest<RemoveRolePermanentlyByRoleIdResponse>,
    IRemoveRolePermanentlyByRoleIdMiddleware
{
    public Guid RoleId { get; init; }

    public Guid RoleRemovedBy { get; init; }
}
