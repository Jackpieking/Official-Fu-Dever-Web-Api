using FuDever.Domain.Entities;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.PostgresSql.Data.EntityConfigurations;

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
            buildAction: table => table.HasComment(comment: TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: blog => blog.Id);

        // AuthorId property configuration.
        builder
            .Property(propertyExpression: blog => blog.AuthorId)
            .IsRequired();

        // Title property configuration.
        builder
            .Property(propertyExpression: blog => blog.Title)
            .HasColumnType(typeName: CommonConstant.DbDataType.VarcharGenerator.Get(
                length: Blog
                    .Metadata
                    .Title
                    .MaxLength))
            .IsRequired();

        // Thumbnail property configuration.
        builder
            .Property(propertyExpression: blog => blog.Thumbnail)
            .HasColumnType(typeName: CommonConstant.DbDataType.VarcharGenerator.Get(
                length: Blog
                    .Metadata
                    .Thumbnail
                    .MaxLength))
            .IsRequired();

        // UploadDate property configuration.
        builder
            .Property(propertyExpression: blog => blog.UploadDate)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // Content property configuration.
        builder
            .Property(propertyExpression: blog => blog.Content)
            .HasColumnType(typeName: CommonConstant.DbDataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: blog => blog.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: blog => blog.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: blog => blog.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: blog => blog.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: blog => blog.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
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