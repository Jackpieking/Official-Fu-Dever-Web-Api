using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Platforms" table configuration.
/// </summary>
public sealed class PlatformEntityConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        const string TableName = "Platforms";
        const string TableComment = "Contain platform record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: platform => platform.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: platform => platform.Name)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_100)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: platform => platform.RemovedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: platform => platform.RemovedBy)
            .IsRequired();

        // Table relationship configurations.
        // [Platforms] - [UserPlatforms] (1 - N).
        builder
            .HasMany(navigationExpression: platform => platform.UserPlatforms)
            .WithOne(navigationExpression: userPlatform => userPlatform.Platform)
            .HasForeignKey(foreignKeyExpression: userPlatform => userPlatform.PlatformId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}