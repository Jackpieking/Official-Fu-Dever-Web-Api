using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;

/// <summary>
///     Restore hobby by hobby id request.
/// </summary>
public sealed class RestoreHobbyByHobbyIdRequest :
    IRequest<RestoreHobbyByHobbyIdResponse>,
    IRestoreHobbyByHobbyIdMiddleware
{
    public Guid HobbyId { get; init; }
}
