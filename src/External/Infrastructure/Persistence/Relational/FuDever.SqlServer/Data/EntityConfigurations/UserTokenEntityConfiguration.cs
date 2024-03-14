using FuDever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "UserTokens" table configuration.
/// </summary>
internal sealed class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        const string TableName = "UserTokens";
        const string TableComment = "Contain user token record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));
    }
}