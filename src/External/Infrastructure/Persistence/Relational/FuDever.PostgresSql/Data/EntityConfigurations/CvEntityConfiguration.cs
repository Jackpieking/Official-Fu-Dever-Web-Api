using FuDever.Domain.Entities;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.PostgresSql.Data.EntityConfigurations;

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
            buildAction: table => table.HasComment(comment: TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: cv => cv.Id);

        // FullName property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentFullName)
            .HasColumnType(CommonConstant.DbDataType.VarcharGenerator.Get(
                length: Cv
                    .Metadata
                    .StudentFullName
                    .MaxLength))
            .IsRequired();

        // Email property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentEmail)
            .HasColumnType(typeName: CommonConstant.DbDataType.TEXT)
            .IsRequired();

        // StudentId property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentId)
            .HasColumnType(typeName: CommonConstant.DbDataType.VarcharGenerator.Get(
                length: Cv
                    .Metadata
                    .StudentId
                    .MaxLength))
            .IsRequired();

        // CvFileId property configuration.
        builder
            .Property(propertyExpression: cv => cv.StudentCvFileId)
            .HasColumnType(typeName: CommonConstant.DbDataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: cv => cv.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: cv => cv.CreatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: cv => cv.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: cv => cv.RemovedBy)
            .IsRequired();
    }
}