using FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId;

/// <summary>
///     Remove role temporarily by role id request.
/// </summary>
public sealed class RemoveRoleTemporarilyByRoleIdRequest :
    IRequest<RemoveRoleTemporarilyByRoleIdResponse>,
    IRemoveRoleTemporarilyByRoleIdMiddleware
{
    public Guid RoleId { get; init; }

    public Guid RoleRemovedBy { get; init; }
}
