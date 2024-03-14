using FuDever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "BlogTags" configuration.
/// </summary>
internal sealed class BlogTagEntityConfiguration : IEntityTypeConfiguration<BlogTag>
{
    public void Configure(EntityTypeBuilder<BlogTag> builder)
    {
        const string TableName = "BlogTags";
        const string TableComment = "Contain blog tag record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: blogTag => new
        {
            blogTag.BlogId,
            blogTag.SkillId
        });
    }
}