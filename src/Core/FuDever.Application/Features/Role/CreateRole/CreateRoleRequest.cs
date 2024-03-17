using FuDever.Application.Features.Role.CreateRole.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Role.CreateRole;

/// <summary>
///     Create role request.
/// </summary>
public sealed class CreateRoleRequest :
    IRequest<CreateRoleResponse>,
    ICreateRoleMiddleware
{
    public string NewRoleName { get; init; }
}
