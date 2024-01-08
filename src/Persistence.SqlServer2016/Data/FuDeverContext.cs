using System;
using Application.Interfaces.Data;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data.EntityConfigurations;

namespace Persistence.SqlServer2016.Data;

/// <summary>
///
/// </summary>
public sealed class FuDeverContext :
    IdentityDbContext<User, Role, Guid>,
    IFuDeverContext
{
    /// <summary>
    ///     Implementation of entity set getter.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Entity (table) of the database.
    /// </typeparam>
    /// <returns>
    ///     Set of that entity.
    /// </returns>
    public DbSet<TEntity> DomainSet<TEntity>() where TEntity :
        class,
        IBaseEntity
    {
        return Set<TEntity>();
    }

    /// <summary>
    ///     Config tables and seed initial data here.
    /// </summary>
    /// <param name="builder">
    ///     Builder to config the tables and seed data.
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder: builder);

        builder.Seed();

        builder
            .ApplyConfiguration(new BlogCommentEntityConfiguration())
            .ApplyConfiguration(new BlogEntityConfiguration())
            .ApplyConfiguration(new BlogTagEntityConfiguration())
            .ApplyConfiguration(new CvEntityConfiguration())
            .ApplyConfiguration(new DepartmentEntityConfiguration())
            .ApplyConfiguration(new HobbyEntityConfiguration())
            .ApplyConfiguration(new MajorEntityConfiguration())
            .ApplyConfiguration(new PlatformEntityConfiguration())
            .ApplyConfiguration(new PositionEntityConfiguration())
            .ApplyConfiguration(new ProjectEntityConfiguration())
            .ApplyConfiguration(new RefreshTokenEntityConfiguration())
            .ApplyConfiguration(new RoleClaimEntityConfiguration())
            .ApplyConfiguration(new RoleEntityConfiguration())
            .ApplyConfiguration(new SkillEntityConfiguration())
            .ApplyConfiguration(new UserClaimEntityConfiguration())
            .ApplyConfiguration(new UserEntityConfiguration())
            .ApplyConfiguration(new UserHobbyEntityConfiguration())
            .ApplyConfiguration(new UserJoiningStatusEntityConfiguration())
            .ApplyConfiguration(new UserLoginEntityConfiguration())
            .ApplyConfiguration(new UserPlatformEntityConfiguration())
            .ApplyConfiguration(new UserRoleEntityConfiguration())
            .ApplyConfiguration(new UserSkillEntityConfiguration())
            .ApplyConfiguration(new UserTokenEntityConfiguration());
    }
}
