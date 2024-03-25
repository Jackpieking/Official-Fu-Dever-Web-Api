using FuDever.Domain.EntityBuilders.Department;
using FuDever.Domain.EntityBuilders.Hobby;
using FuDever.Domain.EntityBuilders.Major;
using FuDever.Domain.EntityBuilders.Platform;
using FuDever.Domain.EntityBuilders.Position;
using FuDever.Domain.EntityBuilders.Project;
using FuDever.Domain.EntityBuilders.Skill;
using FuDever.Domain.EntityBuilders.User;
using FuDever.Domain.EntityBuilders.UserHobby;
using FuDever.Domain.EntityBuilders.UserJoiningStatus;
using FuDever.Domain.EntityBuilders.UserPlatform;
using FuDever.Domain.EntityBuilders.UserSkill;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using System.Linq;

namespace FuDever.PostgresSql.Specifications.Entities.User;

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
        UserForDatabaseRetrievingBuilder userBuilder = new();
        UserJoiningStatusForDatabaseRetrievingBuilder userJoiningStatusBuilder = new();

        SelectExpression = user => userBuilder
            .WithUserJoiningStatus(userJoiningStatusBuilder
                .WithType(user.UserJoiningStatus.Type)
                .Complete())
            .Complete();

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver2()
    {
        UserForDatabaseRetrievingBuilder userBuilder = new();
        PositionForDatabaseRetrievingBuilder positionBuilder = new();
        DepartmentForDatabaseRetrievingBuilder departmentBuilder = new();
        UserJoiningStatusForDatabaseRetrievingBuilder userJoiningStatusBuilder = new();

        SelectExpression = user => userBuilder
            .WithId(user.Id)
            .WithFirstName(user.FirstName)
            .WithLastName(user.LastName)
            .WithEmail(user.Email)
            .WithPosition(positionBuilder
                .WithName(user.Position.Name)
                .Complete())
            .WithDepartment(departmentBuilder
                .WithName(user.Department.Name)
                .Complete())
            .WithUserJoiningStatus(userJoiningStatusBuilder
                .WithType(user.UserJoiningStatus.Type)
                .Complete())
            .WithAvatarUrl(user.AvatarUrl)
            .WithRemovedAt(user.RemovedAt)
            .WithRemovedBy(user.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver3()
    {
        UserForDatabaseRetrievingBuilder userBuilder = new();
        PositionForDatabaseRetrievingBuilder positionBuilder = new();
        UserPlatformForDatabaseRetrievingBuilder userPlatformBuilder = new();
        DepartmentForDatabaseRetrievingBuilder departmentBuilder = new();
        MajorForDatabaseRetrievingBuilder majorBuilder = new();
        ProjectForDatabaseRetrievingBuilder projectBuilder = new();
        PlatformForDatabaseRetrievingBuilder platformBuilder = new();
        UserSkillForDatabaseRetrievingBuilder userSkillBuilder = new();
        UserHobbyForDatabaseRetrievingBuilder userHobbyBuilder = new();
        SkillForDatabaseRetrievingBuilder skillBuilder = new();
        HobbyForDatabaseRetrievingBuilder hobbyBuilder = new();

        SelectExpression = user => userBuilder
            .WithId(user.Id)
            .WithFirstName(user.FirstName)
            .WithLastName(user.LastName)
            .WithCareer(user.Career)
            .WithBirthDay(user.BirthDay)
            .WithEmail(user.Email)
            .WithHomeAddress(user.HomeAddress)
            .WithPhoneNumber(user.PhoneNumber)
            .WithSelfDescription(user.SelfDescription)
            .WithJoinDate(user.JoinDate)
            .WithEducationPlaces(user.EducationPlaces)
            .WithPosition(positionBuilder
                .WithId(user.PositionId)
                .WithName(user.Position.Name)
                .Complete())
            .WithMajor(majorBuilder
                .WithId(user.MajorId)
                .WithName(user.Major.Name)
                .Complete())
            .WithDepartment(departmentBuilder
                .WithId(user.DepartmentId)
                .WithName(user.Department.Name)
                .Complete())
            .WithAvatarUrl(user.AvatarUrl)
            .WithUserPlatforms(user.UserPlatforms
                .Select(userPlatform => userPlatformBuilder
                    .WithPlatformId(userPlatform.PlatformId)
                    .WithPlatformUrl(userPlatform.PlatformUrl)
                    .WithPlatform(platformBuilder
                        .WithName(userPlatform.Platform.Name)
                        .Complete())
                    .Complete()))
            .WithWorkplaces(user.Workplaces)
            .WithProjects(user.Projects
                .Select(project => projectBuilder
                    .WithId(project.Id)
                    .WithTitle(project.Title)
                    .WithAuthorId(project.AuthorId)
                    .WithDescription(project.Description)
                    .WithSourceCodeUrl(project.SourceCodeUrl)
                    .WithDemoUrl(project.DemoUrl)
                    .WithThumbnailUrl(project.ThumbnailUrl)
                    .WithCreatedAt(project.CreatedAt)
                    .WithUpdatedAt(project.UpdatedAt)
                    .Complete()))
            .WithUserSkills(user.UserSkills
                .Select(userSkill => userSkillBuilder
                    .WithSkill(skillBuilder
                        .WithName(userSkill.Skill.Name)
                        .Complete())
                    .Complete()))
            .WithUserHobbies(user.UserHobbies
                .Select(userHobby => userHobbyBuilder
                    .WithHobby(hobbyBuilder
                        .WithName(userHobby.Hobby.Name)
                        .Complete())
                    .Complete()))
            .Complete();

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver4()
    {
        UserForDatabaseRetrievingBuilder userBuilder = new();
        PositionForDatabaseRetrievingBuilder positionBuilder = new();
        DepartmentForDatabaseRetrievingBuilder departmentBuilder = new();
        UserJoiningStatusForDatabaseRetrievingBuilder userJoiningStatusBuilder = new();

        SelectExpression = user => userBuilder
            .WithId(user.Id)
            .WithFirstName(user.FirstName)
            .WithLastName(user.LastName)
            .WithEmail(user.Email)
            .WithPosition(positionBuilder
                .WithName(user.Position.Name)
                .Complete())
            .WithDepartment(departmentBuilder
                .WithName(user.Department.Name)
                .Complete())
            .WithUserJoiningStatus(userJoiningStatusBuilder
                .WithType(user.UserJoiningStatus.Type)
                .Complete())
            .WithAvatarUrl(user.AvatarUrl)
            .Complete();

        return this;
    }

    public ISelectFieldsFromUserSpecification Ver5()
    {
        UserForDatabaseRetrievingBuilder userBuilder = new();

        SelectExpression = user => userBuilder
            .WithId(user.Id)
            .WithPasswordHash(user.PasswordHash)
            .WithLockoutEnd(user.LockoutEnd.Value)
            .WithAccessFailedCount(user.AccessFailedCount)
            .WithEmailConfirmed(user.EmailConfirmed)
            .WithEmail(user.Email)
            .WithAvatarUrl(user.AvatarUrl)
            .Complete();

        return this;
    }
}
