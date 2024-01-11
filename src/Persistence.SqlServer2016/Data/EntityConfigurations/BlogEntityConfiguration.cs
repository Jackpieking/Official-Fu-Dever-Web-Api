using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Blogs" table configuration.
/// </summary>
internal sealed class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        const string TableName = "Blogs";
        const string TableComment = "Contain blog record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: blog => blog.Id);

        // AuthorId property configuration.
        builder
            .Property(propertyExpression: blog => blog.AuthorId)
            .IsRequired();

        // Title property configuration.
        builder
            .Property(propertyExpression: blog => blog.Title)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_100)
            .IsRequired();

        // Thumbnail property configuration.
        builder
            .Property(propertyExpression: blog => blog.Thumbnail)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_200)
            .IsRequired();

        // UploadDate property configuration.
        builder
            .Property(propertyExpression: blog => blog.UploadDate)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // Content property configuration.
        builder
            .Property(propertyExpression: blog => blog.Content)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: blog => blog.CreatedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: blog => blog.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: blog => blog.UpdatedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: blog => blog.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: blog => blog.RemovedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: blog => blog.RemovedBy)
            .IsRequired();

        // Table relationship configurations.
        // [Blogs] - [BlogComments] (1 - N).
        builder
            .HasMany(navigationExpression: blog => blog.BlogComments)
            .WithOne(navigationExpression: blogComment => blogComment.Blog)
            .HasForeignKey(foreignKeyExpression: blogComment => blogComment.BlogId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Blogs] - [BlogTags] (1 - N).
        builder
            .HasMany(navigationExpression: blog => blog.BlogTags)
            .WithOne(navigationExpression: blogTags => blogTags.Blog)
            .HasForeignKey(foreignKeyExpression: blogTags => blogTags.BlogId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}