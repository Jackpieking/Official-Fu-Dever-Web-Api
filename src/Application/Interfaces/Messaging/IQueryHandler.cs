using MediatR;

namespace Application.Interfaces.Messaging;

/// <summary>
///     Represents the query interface.
/// </summary>
/// <typeparam name="TQuery">
///     The query type.
/// </typeparam>
/// <typeparam name="TResponse">
///     The query response type.
/// </typeparam>
internal interface IQueryHandler<in TQuery, TResponse> :
    IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
{
}
