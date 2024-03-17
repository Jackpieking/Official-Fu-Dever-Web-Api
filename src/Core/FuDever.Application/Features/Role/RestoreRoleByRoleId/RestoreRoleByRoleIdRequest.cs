using FuDever.Application.Features.Role.RestoreRoleByRoleId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Role.RestoreRoleByRoleId;

/// <summary>
///     Restore role by role id request.
/// </summary>
public sealed class RestoreRoleByRoleIdRequest :
    IRequest<RestoreRoleByRoleIdResponse>,
    IRestoreRoleByRoleIdMiddleware
{
    public Guid RoleId { get; init; }
}
