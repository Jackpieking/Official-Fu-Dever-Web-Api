using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using System.Linq;

namespace Persistence.Database.SqlServer.Specifications.Entities.User;

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
        SelectExpression = user => Domain.Entities.User.InitVer2(
            Domain.Entities.UserJoiningStatus.InitVer3(
                user.UserJoiningStatus.Type));

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver2()
    {
        SelectExpression = user => Domain.Entities.User.InitVer3(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            Domain.Entities.Position.InitVer2(user.Position.Name),
            Domain.Entities.Department.InitVer3(user.Department.Name),
            Domain.Entities.UserJoiningStatus.InitVer3(user.UserJoiningStatus.Type),
            user.AvatarUrl,
            user.RemovedAt,
            user.RemovedBy);

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
            Position = Domain.Entities.Position.InitVer3(
                user.PositionId,
                user.Position.Name),
            Major = Domain.Entities.Major.InitVer2(
                user.MajorId,
                user.Major.Name),
            Department = Domain.Entities.Department.InitVer2(
                user.DepartmentId,
                user.Department.Name),
            AvatarUrl = user.AvatarUrl,
            UserPlatforms = user.UserPlatforms.Select(userPlatform =>
                Domain.Entities.UserPlatform.InitVer1(
                    userPlatform.PlatformId,
                    userPlatform.PlatformUrl,
                    Domain.Entities.Platform.InitVer3(
                        userPlatform.Platform.Name))),
            Workplaces = user.Workplaces,
            Projects = user.Projects.Select(project =>
                Domain.Entities.Project.InitVer2(
                    project.Id,
                    project.Title,
                    project.AuthorId,
                    project.Description,
                    project.SourceCodeUrl,
                    project.DemoUrl,
                    project.ThumbnailUrl,
                    project.CreatedAt,
                    project.UpdatedAt)),
            UserSkills = user.UserSkills.Select(userSkill =>
                Domain.Entities.UserSkill.InitVer1(
                    Domain.Entities.Skill.InitVer3(userSkill.Skill.Name))),
            UserHobbies = user.UserHobbies.Select(userHobby =>
                Domain.Entities.UserHobby.InitVer1(
                    Domain.Entities.Hobby.InitVer4(userHobby.Hobby.Name)))
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
            Position = Domain.Entities.Position.InitVer2(user.Position.Name),
            Department = Domain.Entities.Department.InitVer3(user.Department.Name),
            UserJoiningStatus = Domain.Entities.UserJoiningStatus.InitVer3(user.UserJoiningStatus.Type),
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

    public ISelectFieldsFromUserSpecification Ver6()
    {
        SelectExpression = user => new()
        {
            Id = user.Id,
            PasswordHash = user.PasswordHash,
            LockoutEnd = user.LockoutEnd,
            AccessFailedCount = user.AccessFailedCount,
            EmailConfirmed = user.EmailConfirmed,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl
        };

        return this;
    }
}
