using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Data.EntityConfigurations;

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
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

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
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // Content property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.Content)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: blogComment => blogComment.UpdatedBy)
            .IsRequired();
    }
}