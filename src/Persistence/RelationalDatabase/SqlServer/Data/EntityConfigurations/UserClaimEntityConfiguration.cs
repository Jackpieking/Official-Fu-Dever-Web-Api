using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.RelationalDatabase.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "UserClaims" table configuration.
/// </summary>
internal sealed class UserClaimEntityConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        const string TableName = "UserClaims";
        const string TableComment = "Contain user claim record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });
    }
}