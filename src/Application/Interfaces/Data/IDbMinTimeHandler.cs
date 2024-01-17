using System;

namespace Application.Interfaces.Data;

/// <summary>
///     Represent the handler for database min time.
/// </summary>
public interface IDbMinTimeHandler
{
    /// <summary>
    ///     Get the value of database min time.
    /// </summary>
    DateTime Get();
}
