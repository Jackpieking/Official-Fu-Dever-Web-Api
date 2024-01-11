using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "UserLogins" table configuration.
/// </summary>
internal sealed class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        const string TableName = "UserLogins";
        const string TableComment = "Contain user login record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });
    }
}