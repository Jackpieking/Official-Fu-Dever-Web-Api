using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "UserPlatforms" table configuration.
/// </summary>
internal sealed class UserPlatformEntityConfiguration : IEntityTypeConfiguration<UserPlatform>
{
    public void Configure(EntityTypeBuilder<UserPlatform> builder)
    {
        const string TableName = "UserPlatforms";
        const string TableComment = "Contain user platform record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: userPlatform => new
        {
            userPlatform.UserId,
            userPlatform.PlatformId
        });

        // PlatformUrl Property configuration.
        builder
            .Property(propertyExpression: userPlatform => userPlatform.PlatformUrl)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();
    }
}