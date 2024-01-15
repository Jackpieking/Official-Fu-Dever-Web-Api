using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Cvs" table configuration.
/// </summary>
internal sealed class CvEntityConfiguration : IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        const string TableName = "Cvs";
        const string TableComment = "Contain cv record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: cv => cv.Id);

        // FullName property configuration.
        builder
            .Property(propertyExpression: cv => cv.FullName)
            .HasColumnType(CommonConstant.DbDataType.NVARCHAR_50)
            .IsRequired();

        // Email property configuration.
        builder
            .Property(propertyExpression: cv => cv.Email)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // StudentId property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentId)
            .IsRequired();

        // CvFileId property configuration.
        builder
            .Property(propertyExpression: cv => cv.CvFileId)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: cv => cv.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: cv => cv.CreatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: cv => cv.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: cv => cv.RemovedBy)
            .IsRequired();
    }
}