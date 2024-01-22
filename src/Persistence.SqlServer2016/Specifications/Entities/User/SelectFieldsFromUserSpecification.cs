using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using System.Linq;

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
            UserJoiningStatus = Domain.Entities.UserJoiningStatus.Init(user.UserJoiningStatus.Type)
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
            Position = Domain.Entities.Position.Init(user.Position.Name),
            Department = Domain.Entities.Department.Init(user.Department.Name),
            UserJoiningStatus = Domain.Entities.UserJoiningStatus.Init(user.UserJoiningStatus.Type),
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
            Position = Domain.Entities.Position.Init(
                user.PositionId,
                user.Position.Name),
            Major = Domain.Entities.Major.Init(
                user.MajorId,
                user.Major.Name),
            Department = Domain.Entities.Department.Init(
                user.DepartmentId,
                user.Department.Name),
            AvatarUrl = user.AvatarUrl,
            UserPlatforms = user.UserPlatforms.Select(userPlatform => Domain.Entities.UserPlatform.Init(
                userPlatform.PlatformId,
                userPlatform.PlatformUrl,
                Domain.Entities.Platform.Init(userPlatform.Platform.Name))),
            Workplaces = user.Workplaces,
            Projects = user.Projects.Select(project => Domain.Entities.Project.Init(
                project.Id,
                project.Title,
                project.AuthorId,
                project.Description,
                project.SourceCodeUrl,
                project.DemoUrl,
                project.ThumbnailUrl,
                project.CreatedAt,
                project.UpdatedAt)),
            UserSkills = user.UserSkills.Select(userSkill => Domain.Entities.UserSkill.Init(
                Domain.Entities.Skill.Init(userSkill.Skill.Name))),
            UserHobbies = user.UserHobbies.Select(userHobby => Domain.Entities.UserHobby.Init(
                Domain.Entities.Hobby.Init(userHobby.Hobby.Name)))
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
            Position = Domain.Entities.Position.Init(user.Position.Name),
            Department = Domain.Entities.Department.Init(user.Department.Name),
            UserJoiningStatus = Domain.Entities.UserJoiningStatus.Init(user.UserJoiningStatus.Type),
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
