using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Projects" table configuration.
/// </summary>
internal sealed class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        const string TableName = "Projects";
        const string TableComment = "Contain project record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: project => project.Id);

        // AuthorId property configuration.
        builder
            .Property(propertyExpression: project => project.AuthorId)
            .IsRequired();

        // Title property configuration.
        builder
            .Property(propertyExpression: project => project.Title)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_100)
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