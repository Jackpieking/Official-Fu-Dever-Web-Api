using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "UserPlatforms" table configuration.
/// </summary>
public sealed class UserPlatformEntityConfiguration : IEntityTypeConfiguration<UserPlatform>
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
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();
    }
}