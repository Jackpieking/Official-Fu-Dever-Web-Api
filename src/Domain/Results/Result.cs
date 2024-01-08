using Domain.Errors.Base;
using System;

namespace Domain.Results;

/// <summary>
///     Represent the result of operation.
/// </summary>
public sealed class Result<TValue>
{
    /// <summary>
    ///     Is operation success.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Error detail.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    ///     Value of the result.
    /// </summary>
    public TValue Value { get; }

    /// <summary>
    ///     Initialize a result object.
    /// </summary>
    /// <remarks>
    ///     This constructor is not exposed to consumer.
    /// </remarks>
    /// <param name="isSuccess">
    ///     Is the result the success one.
    /// </param>
    /// <param name="error">
    ///     The error detail if result is the fail one.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Invalid error detail.
    /// </exception>
    private Result(
        bool isSuccess,
        Error error,
        TValue value)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    /// <summary>
    ///     Initialize a successful result object containing
    ///     return value.
    /// </summary>
    /// <param name="value">
    ///     The return value.
    /// </param>
    public static implicit operator Result<TValue>(TValue value)
    {
        return new(
            isSuccess: true,
            error: Error.None,
            value: value);
    }

    /// <summary>
    ///     Initialize a fail result object containing
    ///     error detail.
    /// </summary>
    /// <param name="error">
    ///     The error detail.
    /// </param>
    public static implicit operator Result<TValue>(Error error)
    {
        return new(
            isSuccess: false,
            error: error,
            value: default);
    }
}
