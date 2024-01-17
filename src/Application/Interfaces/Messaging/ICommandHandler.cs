using MediatR;

namespace Application.Interfaces.Messaging;

/// <summary>
///     Represents the command handler interface.
/// </summary>
/// <typeparam name="TCommand">
///     The command type.
/// </typeparam>
/// <typeparam name="TResponse">
///     The command response type.
/// </typeparam>
internal interface ICommandHandler<in TCommand, TResponse> :
    IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
{
}
