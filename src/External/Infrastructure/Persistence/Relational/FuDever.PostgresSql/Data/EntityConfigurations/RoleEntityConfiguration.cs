using FuDever.Domain.Entities;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represent "Roles" table configuration.
/// </summary>
internal sealed class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        const string TableName = "Roles";
        const string TableComment = "Contain role record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: role => role.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: role => role.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Roles] - [UserRoles] (1 - N).
        builder
            .HasMany(navigationExpression: role => role.UserRoles)
            .WithOne(navigationExpression: userRole => userRole.Role)
            .HasForeignKey(foreignKeyExpression: userRole => userRole.RoleId)
            .IsRequired();

        // [Roles] - [RoleClaims] (1 - N).
        builder
            .HasMany(navigationExpression: role => role.RoleClaims)
            .WithOne(navigationExpression: roleClaim => roleClaim.Role)
            .HasForeignKey(foreignKeyExpression: roleClaim => roleClaim.RoleId)
            .IsRequired();
    }
}