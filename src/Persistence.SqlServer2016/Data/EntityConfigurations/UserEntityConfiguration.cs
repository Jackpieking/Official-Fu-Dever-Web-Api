using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;
using System;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "Users" table configuration.
/// </summary>
public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
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
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: user => user.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: user => user.UpdatedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: appUser => appUser.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: user => user.RemovedAt)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
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
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_30)
            .IsRequired();

        // LastName property configuration.
        builder
            .Property(propertyExpression: user => user.LastName)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_30)
            .IsRequired();

        // Career property configuration.
        builder
            .Property(propertyExpression: user => user.Career)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_30)
            .IsRequired();

        // Workplaces property configuration.
        builder
            .Property(propertyExpression: user => user.Workplaces)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // EducationPlaces property configuration.
        builder
            .Property(propertyExpression: user => user.EducationPlaces)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // BirthDay property configuration.
        builder
            .Property(propertyExpression: user => user.BirthDay)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // HomeAddress property configuration.
        builder
            .Property(propertyExpression: user => user.HomeAddress)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_50)
            .IsRequired();

        // SelfDescription property configuration.
        builder
            .Property(propertyExpression: user => user.SelfDescription)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // JoinDate property configuration.
        builder
            .Property(propertyExpression: user => user.JoinDate)
            .HasColumnType(typeName: CustomConstant.DbDataType.DATETIME)
            .IsRequired();

        // ActivityPoints property configuration.
        builder
            .Property(propertyExpression: user => user.ActivityPoints)
            .IsRequired();

        // AvatarUrl property configuration.
        builder
            .Property(propertyExpression: user => user.AvatarUrl)
            .HasColumnType(typeName: CustomConstant.DbDataType.NVARCHAR_MAX)
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
            .HasForeignKey(foreignKeyExpression: refreshToken => refreshToken.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}