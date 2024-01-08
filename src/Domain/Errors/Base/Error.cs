namespace Domain.Errors.Base;

/// <summary>
///     Represent the error.
/// </summary>
public sealed class Error
{
    /// <summary>
    ///     Error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Error description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Represent no error state.
    /// </summary>
    public static readonly Error None = new(
        code: string.Empty,
        description: string.Empty);

    /// <summary>
    ///     Initialize new error with error
    ///     code and error description.
    /// </summary>
    /// <param name="code">
    ///     Error code.
    /// </param>
    /// <param name="description">
    ///     Error description.
    /// </param>
    public Error(
        string code,
        string description)
    {
        Code = code;
        Description = description;
    }
}
