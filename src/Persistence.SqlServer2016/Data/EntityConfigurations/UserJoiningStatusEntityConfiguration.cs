using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "UserJoiningStatuses" table configuration.
/// </summary>
internal sealed class UserJoiningStatusEntityConfiguration : IEntityTypeConfiguration<UserJoiningStatus>
{
    public void Configure(EntityTypeBuilder<UserJoiningStatus> builder)
    {
        const string TableName = "userJoiningStatuses";
        const string TableComment = "Contain user joining status record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: userJoiningStatus => userJoiningStatus.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: userJoiningStatus => userJoiningStatus.Type)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_50)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: userJoiningStatus => userJoiningStatus.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: userJoiningStatus => userJoiningStatus.RemovedBy)
            .IsRequired();

        // Table relationship configurations.
        // [UserJoiningStatus] - [Users] (1 - N).
        builder
            .HasMany(navigationExpression: userJoiningStatus => userJoiningStatus.Users)
            .WithOne(navigationExpression: user => user.UserJoiningStatus)
            .HasForeignKey(foreignKeyExpression: user => user.UserJoiningStatusId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}