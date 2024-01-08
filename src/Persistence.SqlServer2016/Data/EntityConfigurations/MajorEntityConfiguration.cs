using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;
using System;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Majors" table configuration.
/// </summary>
public sealed class MajorEntityConfiguration : IEntityTypeConfiguration<Major>
{
    public void Configure(EntityTypeBuilder<Major> builder)
    {
        const string TableName = "Majors";
        const string TableComment = "Contain major record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: major => major.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: major => major.Name)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_100)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: major => major.RemovedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: major => major.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Majors] - [Users] (1 - N).
        builder
            .HasMany(navigationExpression: major => major.Users)
            .WithOne(navigationExpression: user => user.Major)
            .HasForeignKey(foreignKeyExpression: user => user.MajorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}