using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common;

/// <summary>
///     Represent set of commonly used methods.
/// </summary>
public static class CustomMethod
{
    /// <summary>
    ///     Create a foreach loop that run synchronously
    ///     in parallel with 2 threads.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of source the loop runs through.
    /// </typeparam>
    /// <param name="sources">
    ///     The source that is iterated through.
    /// </param>
    /// <param name="action">
    ///     What to do with each element.
    /// </param>
    public static void ParallelForEach<TSource>(
        this IEnumerable<TSource> sources,
        Action<TSource> action)
    {
        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 2
        };

        Parallel.ForEach(
            source: sources,
            parallelOptions: parallelOptions,
            body: action);
    }

    /// <summary>
    ///     Create a foreach loop that run asynchronously
    ///     in parallel with 2 threads.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of source the loop runs through.
    /// </typeparam>
    /// <param name="sources">
    ///     The source that is iterated through.
    /// </param>
    /// <param name="func">
    ///     What to do with each element.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    /// <remarks>
    ///     This version including the cancellation token
    ///     in the arguments of the func delegate.
    /// </remarks>
    public static async Task ParallelForEachAsync<TSource>(
        this IEnumerable<TSource> sources,
        Func<TSource, CancellationToken, Task> func)
    {
        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 2
        };

        await Parallel.ForEachAsync(
            source: sources,
            parallelOptions: parallelOptions,
            async (source, cancellationToken) =>
            {
                await func(
                    arg1: source,
                    arg2: cancellationToken);
            });
    }

    /// <summary>
    ///     Create a foreach loop that run asynchronously
    ///     in parallel with 2 threads.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of source the loop runs through.
    /// </typeparam>
    /// <param name="sources">
    ///     The source that is iterated through.
    /// </param>
    /// <param name="func">
    ///     What to do with each element.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    public static async Task ParallelForEachAsync<TSource>(
        this IEnumerable<TSource> sources,
        Func<TSource, Task> func)
    {
        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 2
        };

        await Parallel.ForEachAsync(
            source: sources,
            parallelOptions: parallelOptions,
            async (source, cancellationToken) =>
            {
                await func(arg: source);
            });
    }
}
