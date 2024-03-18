using FuDever.Domain.Entities;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represent "BlogComments" table configuration.
/// </summary>
internal sealed class BlogCommentEntityConfiguration : IEntityTypeConfiguration<BlogComment>
{
    public void Configure(EntityTypeBuilder<BlogComment> builder)
    {
        const string TableName = "BlogComments";
        const string TableComment = "Contain blog comment record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: blogComment => blogComment.Id);

        // BlogId property configuration
        builder
            .Property(propertyExpression: blogComment => blogComment.BlogId)
            .IsRequired();

        // AuthorId property configuration
        builder
            .Property(propertyExpression: blogComment => blogComment.AuthorId)
            .IsRequired();

        // UploadDate property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.UploadDate)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // Content property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.Content)
            .HasColumnType(typeName: CommonConstant.DbDataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.UpdatedBy)
            .IsRequired();
    }
}