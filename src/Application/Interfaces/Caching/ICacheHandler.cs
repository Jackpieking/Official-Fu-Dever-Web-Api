using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Caching;

/// <summary>
///     Represent interface of caching handler.
/// </summary>
public interface ICacheHandler
{
    /// <summary>
    ///     Get the value base on key.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Type of value.
    /// </typeparam>
    /// <param name="key">
    ///     Key to find the value.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     The task containing value of given key or null.
    /// </returns>
    Task<TSource> GetAsync<TSource>(
        string key,
        CancellationToken cancellationToken)
            where TSource : class;

    /// <summary>
    ///     Set the new key-value pair.
    /// </summary>
    /// <typeparam name="TSource">
    ///     Type of value.
    /// </typeparam>
    /// <param name="key">
    ///     Key to find the value.
    /// </param>
    /// <param name="value">
    ///     Value for the key.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     The task containing empty result.
    /// </returns>
    Task SetAsync<TSource>(
        string key,
        TSource value,
        CancellationToken cancellationToken)
            where TSource : class;

    /// <summary>
    ///     Remove the value by given key.
    /// </summary>
    /// <param name="key">
    ///     Key to find the value.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if remove success. Otherwise, false.
    /// </returns>
    Task<bool> RemoveAsync(
        string key,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Remove range of value by given prefix key.
    /// </summary>
    /// <param name="prefixKey">
    ///     Prefix key to find value.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if remove success. Otherwise, false.
    /// </returns>
    Task<bool> RemoveByPrefixAsync(
        string prefixKey,
        CancellationToken cancellationToken);
}
