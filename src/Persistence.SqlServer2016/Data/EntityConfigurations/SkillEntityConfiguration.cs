using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Skills" table configuration.
/// </summary>
internal sealed class SkillEntityConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        const string TableName = "Skills";
        const string TableComment = "Contain skill record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: skill => skill.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: skill => skill.Name)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_100)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: skill => skill.RemovedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: skill => skill.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Skills] - [BlogTags] (1 - N).
        builder
            .HasMany(navigationExpression: skill => skill.BlogTags)
            .WithOne(navigationExpression: blogTag => blogTag.Skill)
            .HasForeignKey(foreignKeyExpression: blogTag => blogTag.SkillId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Skills] - [UserSkills] (1 - N).
        builder
            .HasMany(navigationExpression: skill => skill.UserSkills)
            .WithOne(navigationExpression: userSkill => userSkill.Skill)
            .HasForeignKey(foreignKeyExpression: userSkill => userSkill.SkillId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}