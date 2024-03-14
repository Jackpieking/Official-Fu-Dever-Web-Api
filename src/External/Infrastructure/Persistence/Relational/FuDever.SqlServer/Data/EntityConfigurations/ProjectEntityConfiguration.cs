using FuDever.Domain.Entities;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "Projects" table configuration.
/// </summary>
internal sealed class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        const string tableName = "Projects";
        const string tableComment = "Contain project record.";

        builder.ToTable(
            name: tableName,
            buildAction: table => table.HasComment(comment: tableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: project => project.Id);

        // AuthorId property configuration.
        builder
            .Property(propertyExpression: project => project.AuthorId)
            .IsRequired();

        // Title property configuration.
        builder
            .Property(propertyExpression: project => project.Title)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: Project
                    .Metadata
                    .Title
                    .MaxLength))
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: project => project.Description)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // SourceCodeUrl property configuration.
        builder
            .Property(propertyExpression: project => project.SourceCodeUrl)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // DemoUrl property configuration.
        builder
            .Property(propertyExpression: project => project.DemoUrl)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        //ThumbnailUrl property configuration.
        builder
            .Property(propertyExpression: project => project.ThumbnailUrl)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: project => project.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: project => project.CreatedBy)
            .IsRequired();

        //UpdatedAt property configuration.
        builder
            .Property(propertyExpression: project => project.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: project => project.UpdatedBy)
            .IsRequired();
    }
}