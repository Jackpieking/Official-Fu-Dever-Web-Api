using System.Linq;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of select fields from the "Users"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromUserSpecification :
    BaseSpecification<Domain.Entities.User>,
    ISelectFieldsFromUserSpecification
{
    public ISelectFieldsFromUserSpecification Ver1()
    {
        SelectExpression = user => new()
        {
            UserJoiningStatus = new()
            {
                Type = user.UserJoiningStatus.Type
            }
        };

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver2()
    {
        SelectExpression = user => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Position = new()
            {
                Name = user.Position.Name
            },
            Department = new()
            {
                Name = user.Department.Name
            },
            UserJoiningStatus = new()
            {
                Type = user.UserJoiningStatus.Type
            },
            AvatarUrl = user.AvatarUrl,
            RemovedAt = user.RemovedAt,
            RemovedBy = user.RemovedBy
        };

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver3()
    {
        SelectExpression = user => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Career = user.Career,
            BirthDay = user.BirthDay,
            Email = user.Email,
            HomeAddress = user.HomeAddress,
            PhoneNumber = user.PhoneNumber,
            SelfDescription = user.SelfDescription,
            JoinDate = user.JoinDate,
            EducationPlaces = user.EducationPlaces,
            Position = new()
            {
                Id = user.Position.Id,
                Name = user.Position.Name
            },
            Major = new()
            {
                Id = user.Major.Id,
                Name = user.Major.Name
            },
            Department = new()
            {
                Id = user.Department.Id,
                Name = user.Department.Name
            },
            AvatarUrl = user.AvatarUrl,
            UserPlatforms = user.UserPlatforms.Select(userPlatform => new Domain.Entities.UserPlatform
            {
                PlatformId = userPlatform.PlatformId,
                PlatformUrl = userPlatform.PlatformUrl,
                Platform = new()
                {
                    Name = userPlatform.Platform.Name
                }
            }),
            Workplaces = user.Workplaces,
            Projects = user.Projects.Select(project => new Domain.Entities.Project
            {
                Id = project.Id,
                Title = project.Title,
                AuthorId = project.AuthorId,
                Description = project.Description,
                SourceCodeUrl = project.SourceCodeUrl,
                DemoUrl = project.DemoUrl,
                ThumbnailUrl = project.ThumbnailUrl,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            }),
            UserSkills = user.UserSkills.Select(userSkill => new Domain.Entities.UserSkill
            {
                Skill = Domain.Entities.Skill.Init(userSkill.Skill.Name)
            }),
            UserHobbies = user.UserHobbies.Select(userHobby => new Domain.Entities.UserHobby
            {
                Hobby = new()
                {
                    Name = userHobby.Hobby.Name
                }
            })
        };

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver4()
    {
        SelectExpression = user => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Position = new()
            {
                Name = user.Position.Name
            },
            Department = new()
            {
                Name = user.Department.Name
            },
            UserJoiningStatus = new()
            {
                Type = user.UserJoiningStatus.Type
            },
            AvatarUrl = user.AvatarUrl
        };

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver5()
    {
        SelectExpression = user => new()
        {
            Id = user.Id
        };

        return this;
    }
}
