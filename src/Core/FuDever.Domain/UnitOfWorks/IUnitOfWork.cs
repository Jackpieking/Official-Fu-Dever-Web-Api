using FuDever.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.UnitOfWorks;

/// <summary>
///     Represent the base unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Blog comment repository.
    /// </summary>
    IBlogCommentRepository BlogCommentRepository { get; }

    /// <summary>
    ///     Blog repository.
    /// </summary>
    IBlogRepository BlogRepository { get; }

    /// <summary>
    ///     Cv repository.
    /// </summary>
    ICvRepository CvRepository { get; }

    /// <summary>
    ///     Department repository.
    /// </summary>
    IDepartmentRepository DepartmentRepository { get; }

    /// <summary>
    ///     Hobby repository.
    /// </summary>
    IHobbyRepository HobbyRepository { get; }

    /// <summary>
    ///     Major repository.
    /// </summary>
    IMajorRepository MajorRepository { get; }

    /// <summary>
    ///     Platform repository.
    /// </summary>
    IPlatformRepository PlatformRepository { get; }

    /// <summary>
    ///     Position repository.
    /// </summary>
    IPositionRepository PositionRepository { get; }

    /// <summary>
    ///     Project repository.
    /// </summary>
    IProjectRepository ProjectRepository { get; }

    /// <summary>
    ///     Refresh token repository.
    /// </summary>
    IRefreshTokenRepository RefreshTokenRepository { get; }

    /// <summary>
    ///     Role repository.
    /// </summary>
    IRoleRepository RoleRepository { get; }

    /// <summary>
    ///     Skill repository.
    /// </summary>
    ISkillRepository SkillRepository { get; }

    /// <summary>
    ///     User hobby repository.
    /// </summary>
    IUserHobbyRepository UserHobbyRepository { get; }

    /// <summary>
    ///     User joining status repository.
    /// </summary>
    IUserJoiningStatusRepository UserJoiningStatusRepository { get; }

    /// <summary>
    ///     User platform repository.
    /// </summary>
    IUserPlatformRepository UserPlatformRepository { get; }

    /// <summary>
    ///     User repository.
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    ///     User skill repository.
    /// </summary>
    IUserSkillRepository UserSkillRepository { get; }

    /// <summary>
    ///     User role repository.
    /// </summary>
    IUserRoleRepository UserRoleRepository { get; }

    /// <summary>
    ///     Create an execution strategy for managing the
    ///     db transaction which is initialized inside.
    /// </summary>
    /// <returns>
    ///     IExecutionStrategy for wrapping transaction inside.
    /// </returns>
    IExecutionStrategy CreateExecutionStrategy();

    /// <summary>
    ///     Create a database transaction.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task CreateTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Commit a database transaction.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Rollback the database transaction.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task RollBackTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Dispose the database transaction after done using
    /// </summary>
    /// <returns>
    ///     A value task containing result of operation.
    /// </returns>
    ValueTask DisposeTransactionAsync();

    /// <summary>
    ///     Save all entity changes to the database.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task SaveToDatabaseAsync(CancellationToken cancellationToken);
}
