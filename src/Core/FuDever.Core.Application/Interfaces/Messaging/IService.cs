using MediatR;

namespace FuDever.Application.Interfaces.Messaging;

/// <summary>
///     Represents the service interface.
/// </summary>
/// <typeparam name="TResponse">
///     The service response type.
/// </typeparam>
public interface IService<out TResponse> : IRequest<TResponse>
{

}
