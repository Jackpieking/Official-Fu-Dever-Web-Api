using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "UserSkills" table configuration.
/// </summary>
public sealed class UserSkillEntityConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        const string TableName = "UserSkills";
        const string TableComment = "Contain user skill record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: userSkill => new
        {
            userSkill.UserId,
            userSkill.SkillId
        });
    }
}