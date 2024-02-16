namespace Application.Models;

/// <summary>
///     Wrapper around value that is used for caching.
/// </summary>
/// <typeparam name="TSource">
///     Type of source
/// </typeparam>
public sealed class CacheModel<TSource>
{
    /// <summary>
    ///     Value as TSource type.
    /// </summary>
    internal TSource Value { get; private set; }

    /// <summary>
    ///     Represent the state cannot found
    ///     the cached value by key.
    /// </summary>
    public static readonly CacheModel<TSource> NotFound = default;

    private CacheModel()
    {
    }

    /// <summary>
    ///     Init a cache model.
    /// </summary>
    /// <param name="value">
    ///     Value for the cache model.
    /// </param>
    public static implicit operator CacheModel<TSource>(TSource value)
    {
        return new()
        {
            Value = value
        };
    }
}
