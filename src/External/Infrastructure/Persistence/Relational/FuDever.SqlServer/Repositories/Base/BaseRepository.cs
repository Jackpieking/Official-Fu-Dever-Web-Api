using FuDever.Domain.Entities.Base;
using FuDever.Domain.Repositories.Base;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Others;
using FuDever.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.SqlServer.Repositories.Base;

/// <summary>
///     Implementation of base repository.
/// </summary>
/// <typeparam name="TEntity">
///     Represent the table of the database or
///     in the simple term, entity of the system.
/// </typeparam>
/// <remarks>
///     All repository classes must inherit from this
///     base class.
/// </remarks>
internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity :
        class,
        IBaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(FuDeverContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(
        TEntity newEntity,
        CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(
            entity: newEntity,
            cancellationToken: cancellationToken);
    }

    public Task AddRangeAsync(
        IEnumerable<TEntity> newEntities,
        CancellationToken cancellationToken)
    {
        return _dbSet.AddRangeAsync(
            entities: newEntities,
            cancellationToken: cancellationToken);
    }

    public Task<bool> IsFoundBySpecificationsAsync(
        IEnumerable<IBaseSpecification<TEntity>> specifications,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> queryable = _dbSet;

        foreach (var specification in specifications)
        {
            queryable = SpecificationEvaluator.Apply(
                queryable: queryable,
                specification: specification);
        }

        return queryable.AnyAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllBySpecificationsAsync(
        IEnumerable<IBaseSpecification<TEntity>> specifications,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> queryable = _dbSet;

        foreach (var specification in specifications)
        {
            queryable = SpecificationEvaluator.Apply(
                queryable: queryable,
                specification: specification);
        }
        return await queryable.ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<TEntity> FindBySpecificationsAsync(
        IEnumerable<IBaseSpecification<TEntity>> specifications,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> queryable = _dbSet;

        foreach (var specification in specifications)
        {
            queryable = SpecificationEvaluator.Apply(
                queryable: queryable,
                specification: specification);
        }

        return queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}