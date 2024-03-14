using MediatR;

namespace FuDever.Application.Interfaces.Messaging;

/// <summary>
///     Represents the service handler interface.
/// </summary>
/// <typeparam name="TService">
///     The service type.
/// </typeparam>
/// <typeparam name="TResponse">
///     The service response type.
/// </typeparam>
public interface IServiceHandler<in TService, TResponse> :
    IRequestHandler<TService, TResponse>
        where TService : IService<TResponse>
{

}
