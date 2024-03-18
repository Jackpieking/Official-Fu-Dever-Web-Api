using FuDever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represent "UserHobbies" table configuration.
/// </summary>
internal sealed class UserHobbyEntityConfiguration : IEntityTypeConfiguration<UserHobby>
{
    public void Configure(EntityTypeBuilder<UserHobby> builder)
    {
        const string TableName = "UserHobbies";
        const string TableComment = "Contain user hobby record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: userHobby => new
        {
            userHobby.UserId,
            userHobby.HobbyId
        });
    }
}