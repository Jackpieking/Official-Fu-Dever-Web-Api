using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

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
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: role => role.RemovedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: role => role.RemovedBy)
            .IsRequired();
    }
}