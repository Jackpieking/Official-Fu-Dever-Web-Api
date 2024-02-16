using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Commons;

namespace Persistence.Database.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "Users" table configuration.
/// </summary>
internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        const string TableName = "Users";
        const string TableComment = "Contain user record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // UserJoiningStatusId property configuration.
        builder
            .Property(propertyExpression: user => user.UserJoiningStatusId)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: user => user.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: user => user.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: user => user.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: appUser => appUser.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: user => user.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: user => user.RemovedBy)
            .IsRequired();

        // PositionId property configuration.
        builder
            .Property(propertyExpression: user => user.PositionId)
            .IsRequired();

        // MajorId property configuration.
        builder
            .Property(propertyExpression: user => user.MajorId)
            .IsRequired();

        // DepartmentId property configuration.
        builder
            .Property(propertyExpression: user => user.DepartmentId)
            .IsRequired();

        // FirstName property configuration.
        builder
            .Property(propertyExpression: user => user.FirstName)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: 30))
            .IsRequired();

        // LastName property configuration.
        builder
            .Property(propertyExpression: user => user.LastName)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: 30))
            .IsRequired();

        // Career property configuration.
        builder
            .Property(propertyExpression: user => user.Career)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: 30))
            .IsRequired();

        // Workplaces property configuration.
        builder
            .Property(propertyExpression: user => user.Workplaces)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // EducationPlaces property configuration.
        builder
            .Property(propertyExpression: user => user.EducationPlaces)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // BirthDay property configuration.
        builder
            .Property(propertyExpression: user => user.BirthDay)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // HomeAddress property configuration.
        builder
            .Property(propertyExpression: user => user.HomeAddress)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: 50))
            .IsRequired();

        // SelfDescription property configuration.
        builder
            .Property(propertyExpression: user => user.SelfDescription)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // JoinDate property configuration.
        builder
            .Property(propertyExpression: user => user.JoinDate)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // ActivityPoints property configuration.
        builder
            .Property(propertyExpression: user => user.ActivityPoints)
            .IsRequired();

        // AvatarUrl property configuration.
        builder
            .Property(propertyExpression: user => user.AvatarUrl)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // Table relationship configurations.
        // [Users] - [UserSkills] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserSkills)
            .WithOne(navigationExpression: userSkill => userSkill.User)
            .HasForeignKey(foreignKeyExpression: userSkill => userSkill.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [Blogs] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.Blogs)
            .WithOne(navigationExpression: blog => blog.User)
            .HasForeignKey(foreignKeyExpression: blog => blog.AuthorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [UserHobbies] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserHobbies)
            .WithOne(navigationExpression: userHobby => userHobby.User)
            .HasForeignKey(foreignKeyExpression: userHobby => userHobby.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [Projects] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.Projects)
            .WithOne(navigationExpression: project => project.User)
            .HasForeignKey(foreignKeyExpression: project => project.AuthorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [BlogComments] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.BlogComments)
            .WithOne(navigationExpression: blogComment => blogComment.User)
            .HasForeignKey(foreignKeyExpression: blogComment => blogComment.AuthorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [UserPlatforms] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserPlatforms)
            .WithOne(navigationExpression: userPlatform => userPlatform.User)
            .HasForeignKey(foreignKeyExpression: userPlatform => userPlatform.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [RefreshTokens] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.RefreshTokens)
            .WithOne(navigationExpression: refreshToken => refreshToken.User)
            .HasForeignKey(foreignKeyExpression: refreshToken => refreshToken.CreatedBy)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}