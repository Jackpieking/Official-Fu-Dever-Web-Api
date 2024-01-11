using System;

namespace Domain.Exceptions.Base;

/// <summary>
///     Represent base exception for all domain exceptions.
/// </summary>
/// <remarks>
///     All exceptions that are related to domain
///     must be inherit from this base class.
/// </remarks>
public sealed class DomainException : Exception
{
    /// <summary>
    ///     Init an exception including the message.
    /// </summary>
    /// <param name="message">
    ///     Message describing the exception.
    /// </param>
    public DomainException(string message) : base(message: message)
    {
    }
}
