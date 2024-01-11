using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "UserRoles" table configuration.
/// </summary>
internal sealed class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        const string TableName = "UserRoles";
        const string TableComment = "Contain user role record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });
    }
}