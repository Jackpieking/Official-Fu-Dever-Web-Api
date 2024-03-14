using MediatR;

namespace FuDever.Application.Interfaces.Messaging;

/// <summary>
///     Represents the query interface.
/// </summary>
/// <typeparam name="TResponse">
///     The query response type.
/// </typeparam>
internal interface IQuery<out TResponse> : IRequest<TResponse>
{
}
