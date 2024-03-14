using FuDever.Domain.Entities;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.SqlServer.Data.EntityConfigurations;

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
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: role => role.RemovedBy)
            .IsRequired();
    }
}