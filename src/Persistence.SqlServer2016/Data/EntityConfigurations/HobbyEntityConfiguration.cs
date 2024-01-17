using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Hobbies" table configuration.
/// </summary>
internal sealed class HobbyEntityConfiguration : IEntityTypeConfiguration<Hobby>
{
    public void Configure(EntityTypeBuilder<Hobby> builder)
    {
        const string TableName = "Hobbies";
        const string TableComment = "Contain hobby record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: hobby => hobby.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: hobby => hobby.Name)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_100)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: hobby => hobby.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: hobby => hobby.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Hobbies] - [UserHobbies] (1 - N).
        builder
            .HasMany(navigationExpression: hobby => hobby.UserHobbies)
            .WithOne(navigationExpression: userHobby => userHobby.Hobby)
            .HasForeignKey(foreignKeyExpression: userHobby => userHobby.HobbyId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}