using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "UserRoles" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    /// <summary>
    ///     Bulk remove user roles by role id.
    /// </summary>
    /// <param name="roleId">
    ///     The role id.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     The number of rows affected.
    /// </returns>
    Task<int> BulkRemoveByRoleIdAsync(
        Guid roleId,
        CancellationToken cancellationToken);
}
