using FuDever.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuDever.PostgresSql.Data;

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
    ///     Model builder access the database.
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder: builder);

        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        RemoveAspNetPrefixInIdentityTable(builder: builder);

        InitCaseInsensitiveCollation(builder: builder);
    }

    /// <summary>
    ///     Remove "AspNet" prefix in identity table name.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
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

    /// <summary>
    ///     Create new case insensitive collation.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
    /// </param>
    private static void InitCaseInsensitiveCollation(ModelBuilder builder)
    {
        builder.HasCollation(
            name: "case_insensitive",
            locale: "en-u-ks-primary",
            provider: "icu",
            deterministic: false);
    }
}
