using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                Name = "Board of Directors"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Academic Board"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Administrative Board"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Events Board"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Media Board"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty
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
                Name = "Volunteering"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Cooking"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Collecting"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Writing"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Camping"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sports"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Yoga"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Photography"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Chess"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Taekwondo"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Birdwatching"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "DIY Crafts"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Games"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Baking"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Fishing"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Coding"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Drawing"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Playing an Instrument"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Painting"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Gardening"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Hiking"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Reading"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Dancing"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Home Improvement"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Surfing"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Traveling"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty
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
                Name = "Software Engineering"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Information Security"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Digital Art Design"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Information System"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Artificial Intelligence"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty
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
                Name = "LinkedIn"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "GitHub"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Facebook"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Youtube"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Twitter"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Instagram"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty
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
                Name = "Club President"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Club President"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Member"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Administrative Department Head"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Academic Department Head"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Secretary"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Media Department Head"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Administrative Department Head"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Events Department Head"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vice Events Department Head"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Academic Department Head"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty
            },
        ];

        builder.Entity<Position>().HasData(data: newPositions);
        #endregion

        #region Skills
        List<Skill> newSkills =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vue.js"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Caching"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Flexbox"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Docker"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Next.js"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "SQL Server"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "HTML/CSS"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Github"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Git"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Node.js"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Dart"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "MySQL"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Authorization"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "JavaScript"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Web Security"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "ASP.NET Core"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Devops"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Python"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Apache"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "PostgreSQL"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "RESTful API"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Express.js"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Flask"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Bootstrap"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Spring Boot"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "GraphQL"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "MongoDB"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "React.js"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "C#"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Agile"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Java"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "C++"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Authentication"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Django"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Angular.js"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Ruby"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "React Native"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Typescript"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "C"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "SQL"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Swift"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Flutter"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "PHP"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Name = string.Empty
            }
        ];

        builder.Entity<Skill>().HasData(data: newSkills);
        #endregion

        #region UserJoiningStatuses
        List<UserJoiningStatus> newUserJoiningStatuses =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Pending"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Approved"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Expired"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = "Rejected"
            },
            new()
            {
                Id = Application.Common.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
                Type = string.Empty
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
                Name = "admin"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "user"
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
            BirthDay = Common.CommonConstant.DbDefaultValue.MIN_DATE_TIME,
            HomeAddress = string.Empty,
            SelfDescription = string.Empty,
            JoinDate = DateTime.UtcNow,
            AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/comic-image-storage.appspot.com/o/blank-profile-picture-973460_1280.png?alt=media&token=2309abba-282c-4f81-846e-6336235103dc",
            CreatedAt = DateTime.UtcNow
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
