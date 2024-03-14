using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Majors" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IMajorRepository : IBaseRepository<Major>
{
    /// <summary>
    ///     Bulk update by major id.
    /// </summary>
    /// <param name="majorId">
    ///     Major id.
    /// </param>
    /// <param name="majorName">
    ///     Major name.
    /// </param>
    /// <param name="cancellationToken">
    ///     Cancellation token.
    /// </param>
    /// <returns>
    ///     Number of rows updated.
    /// </returns>
    Task<int> BulkUpdateByMajorIdVer1Async(
        Guid majorId,
        string majorName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Bulk update by major id.
    /// </summary>
    /// <param name="majorId">
    ///     Major id.
    /// </param>
    /// <param name="majorRemovedAt">
    ///     Major removed at.
    /// </param>
    /// <param name="majorRemovedBy">
    ///     Major removed by.
    /// </param>
    /// <param name="cancellationToken">
    ///     Cancellation token.
    /// </param>
    /// <returns>
    ///     Number of rows updated.
    /// </returns>
    Task<int> BulkUpdateByMajorIdVer2Async(
        Guid majorId,
        DateTime majorRemovedAt,
        Guid majorRemovedBy,
        CancellationToken cancellationToken);
}