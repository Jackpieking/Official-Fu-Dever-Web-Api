using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Data.EntityConfigurations;

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
            .Property(propertyExpression: cv => cv.StudentFullName)
            .HasColumnType(CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: Cv
                    .Metadata
                    .StudentFullName
                    .MaxLength))
            .IsRequired();

        // Email property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentEmail)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // StudentId property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentId)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: Cv
                    .Metadata
                    .StudentId
                    .MaxLength))
            .IsRequired();

        // CvFileId property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentCvFileId)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: cv => cv.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: cv => cv.CreatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: cv => cv.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: cv => cv.RemovedBy)
            .IsRequired();
    }
}