using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Data;

/// <summary>
///     Implementation of Fu dever context.
/// </summary>
public sealed class FuDeverContext : IdentityDbContext<User, Role, Guid>
{
    public FuDeverContext(DbContextOptions<FuDeverContext> options) : base(options: options)
    {
    }

    /// <summary>
    ///     Configure tables and seed initial data here.
    /// </summary>
    /// <param name="builder">
    ///     Builder to config the tables and seed data.
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder: builder);

        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        RemoveAspNetPrefixInIdentityTable(builder: builder);
    }

    /// <summary>
    ///     Remove "AspNet" prefix in identity table name.
    /// </summary>
    /// <param name="builder">
    ///     Model builder to access to the entity.
    /// </param>
    private static void RemoveAspNetPrefixInIdentityTable(ModelBuilder builder)
    {
        const string AspNetPrefix = "AspNet";

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();

            if (tableName.StartsWith(value: AspNetPrefix))
            {
                entityType.SetTableName(name: tableName[6..]);
            }
        }
    }
}
