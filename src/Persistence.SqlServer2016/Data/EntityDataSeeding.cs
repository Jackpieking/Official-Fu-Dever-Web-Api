using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data;

/// <summary>
///     Represent data seeding for database.
/// </summary>
internal static class EntityDataSeeding
{
    /// <summary>
    ///     Seed data
    /// </summary>
    /// <param name="builder">
    ///     Database builder to apply seeding.
    /// </param>
    internal static void Seed(this ModelBuilder builder)
    {
        #region Departments
        List<Department> newDepartments =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Board of Directors",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Academic Board",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Administrative Board",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Events Board",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Media Board",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty,
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            }
        ];

        builder.Entity<Department>().HasData(data: newDepartments);
        #endregion

        #region Hobbies
        List<Hobby> newHobbies =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Volunteering",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Cooking",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Collecting",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Writing",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Camping",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sports",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Yoga",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Photography",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Chess",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Taekwondo",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Birdwatching",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "DIY Crafts",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Games",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Baking",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Fishing",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Coding",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Drawing",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Playing an Instrument",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Painting",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Gardening",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Hiking",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Reading",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Dancing",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Home Improvement",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Surfing",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Traveling",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty,
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            }
        ];

        builder.Entity<Hobby>().HasData(data: newHobbies);
        #endregion

        #region Majors
        List<Major> newMajors =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Software Engineering",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Information Security",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Digital Art Design",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Information System",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Artificial Intelligence",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty,
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            }
        ];

        builder.Entity<Major>().HasData(data: newMajors);
        #endregion

        #region Platforms
        List<Platform> newPlatforms =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "LinkedIn",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "GitHub",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Facebook",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Youtube",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Twitter",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Instagram",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty,
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            }
        ];

        builder.Entity<Platform>().HasData(data: newPlatforms);
        #endregion

        #region Positions
        List<Position> newPositions =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Club President",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Club President",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Member",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Administrative Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Academic Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Secretary",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Media Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Administrative Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Events Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Events Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Academic Department Head",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty,
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
        ];

        builder.Entity<Position>().HasData(data: newPositions);
        #endregion

        #region Skills
        List<Skill> newSkills =
        [
            new(
                skillName: "Vue.js",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Caching",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Flexbox",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Docker",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Next.js",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "SQL Server",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "HTML/CSS",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Github",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Git",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Node.js",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Dart",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "MySQL",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Authorization",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "JavaScript",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Web Security",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "ASP.NET Core",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Devops",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Python",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Apache",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "PostgreSQL",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "RESTful API",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Express.js",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Flask",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Bootstrap",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Spring Boot",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "GraphQL",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "MongoDB",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "React.js",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "C#",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Agile",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Java",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "C++",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Authentication",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Django",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Angular.js",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Ruby",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "React Native",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Typescript",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "C",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "SQL",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Swift",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "Flutter",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: "PHP",
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
            new(
                skillName: string.Empty,
                skillRemovedAt: CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                skillRemovedBy: Guid.Empty),
        ];

        builder.Entity<Skill>().HasData(data: newSkills);
        #endregion

        #region UserJoiningStatuses
        List<UserJoiningStatus> newUserJoiningStatuses =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Pending",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Approved",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Expired",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Rejected",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Type = string.Empty,
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            }
        ];

        builder.Entity<UserJoiningStatus>().HasData(data: newUserJoiningStatuses);
        #endregion

        #region Roles
        List<Role> newRoles =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
                RemovedBy = Guid.Empty
            }
        ];

        newRoles[0].NormalizedName = newRoles[0].Name.ToUpper();
        newRoles[1].NormalizedName = newRoles[1].Name.ToUpper();

        builder.Entity<Role>().HasData(data: newRoles);
        #endregion

        #region Users
        User admin = new()
        {
            Id = Guid.NewGuid(),
            UserName = "ledinhdangkhoa10a9@gmail.com",
            UserJoiningStatusId = newUserJoiningStatuses[2].Id,
            PositionId = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            MajorId = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            DepartmentId = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            FirstName = string.Empty,
            LastName = string.Empty,
            Career = string.Empty,
            Workplaces = string.Empty,
            EducationPlaces = string.Empty,
            BirthDay = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
            HomeAddress = string.Empty,
            SelfDescription = string.Empty,
            JoinDate = DateTime.UtcNow,
            AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/comic-image-storage.appspot.com/o/blank-profile-picture-973460_1280.png?alt=media&token=2309abba-282c-4f81-846e-6336235103dc",
            CreatedAt = DateTime.UtcNow,
            RemovedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
            RemovedBy = Guid.Empty,
            UpdatedAt = CommonConstant.DbDefaultValue.MIN_DATE_TIME,
            UpdatedBy = Guid.Empty,
        };

        admin.Email = admin.UserName;
        admin.NormalizedUserName = admin.UserName.ToUpper();
        admin.NormalizedEmail = admin.Email.ToUpper();
        admin.EmailConfirmed = true;

        PasswordHasher<User> passwordHasher = new();

        admin.PasswordHash = passwordHasher.HashPassword(
            user: admin,
            password: "Fudeveradmin123@");

        builder.Entity<User>().HasData(data: admin);
        #endregion

        #region UserRoles
        UserRole userRole = new()
        {
            UserId = admin.Id,
            RoleId = newRoles[0].Id
        };

        builder.Entity<UserRole>().HasData(data: userRole);
        #endregion
    }
}
