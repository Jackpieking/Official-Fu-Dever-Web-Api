using FuDever.Application.Features.Role.UpdateRoleByRoleId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role id request.
/// </summary>
public sealed class UpdateRoleByRoleIdRequest :
    IRequest<UpdateRoleByRoleIdResponse>,
    IUpdateRoleByRoleIdMiddleware
{
    public Guid RoleId { get; init; }

    public string NewRoleName { get; init; }

    public Guid RoleUpdatedBy { get; init; }
}
