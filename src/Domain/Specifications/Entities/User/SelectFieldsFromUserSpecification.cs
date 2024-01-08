using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System.Linq;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class SelectFieldsFromUserSpecification :
    GenericSpecification<AppUserEntity>
{
    public SelectFieldsFromUserSpecification Ver1()
    {
        SelectExpression = user => new()
        {
            UserJoiningStatusEntity = new()
            {
                Name = user.UserJoiningStatusEntity.Name
            }
        };

        return this;
    }

    public SelectFieldsFromUserSpecification Ver2()
    {
        SelectExpression = user => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PositionEntity = new()
            {
                Name = user.PositionEntity.Name
            },
            DepartmentEntity = new()
            {
                Name = user.DepartmentEntity.Name
            },
            UserJoiningStatusEntity = new()
            {
                Name = user.UserJoiningStatusEntity.Name
            },
            AvatarUrl = user.AvatarUrl,
            DeletedAt = user.DeletedAt,
            DeletedBy = user.DeletedBy
        };

        return this;
    }

    public SelectFieldsFromUserSpecification Ver3()
    {
        SelectExpression = user => new AppUserEntity
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Career = user.Career,
            BirthDay = user.BirthDay,
            Email = user.Email,
            HomeAddress = user.HomeAddress,
            PhoneNumber = user.PhoneNumber,
            AboutMe = user.AboutMe,
            JoinDate = user.JoinDate,
            EducationPlaces = user.EducationPlaces,
            PositionEntity = new()
            {
                Id = user.PositionEntity.Id,
                Name = user.PositionEntity.Name
            },
            MajorEntity = new()
            {
                Id = user.MajorEntity.Id,
                Name = user.MajorEntity.Name
            },
            DepartmentEntity = new()
            {
                Id = user.DepartmentEntity.Id,
                Name = user.DepartmentEntity.Name
            },
            AvatarUrl = user.AvatarUrl,
            UserPlatformEntities = user.UserPlatformEntities
                .Select(userPlatform => new UserPlatformEntity
                {
                    PlatformId = userPlatform.PlatformId,
                    Url = userPlatform.Url,
                    PlatformEntity = new()
                    {
                        Name = userPlatform.PlatformEntity.Name
                    }
                }),
            Workplaces = user.Workplaces,
            ProjectEntities = user.ProjectEntities
                .Select(project => new ProjectEntity
                {
                    Id = project.Id,
                    Title = project.Title,
                    AuthorId = project.AuthorId,
                    Description = project.Description,
                    ProjectUrl = project.ProjectUrl,
                    DemoUrl = project.DemoUrl,
                    ThumbnailUrl = project.ThumbnailUrl,
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt
                }),
            UserSkillEntities = user.UserSkillEntities
                .Select(userSkill => new UserSkillEntity
                {
                    SkillEntity = new()
                    {
                        Name = userSkill.SkillEntity.Name
                    }
                }),
            UserHobbyEntities = user.UserHobbyEntities
                .Select(userHobby => new UserHobbyEntity
                {
                    HobbyEntity = new()
                    {
                        Name = userHobby.HobbyEntity.Name
                    }
                })
        };

        return this;
    }

    public SelectFieldsFromUserSpecification Ver4()
    {
        SelectExpression = user => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PositionEntity = new()
            {
                Name = user.PositionEntity.Name
            },
            DepartmentEntity = new()
            {
                Name = user.DepartmentEntity.Name
            },
            UserJoiningStatusEntity = new()
            {
                Name = user.UserJoiningStatusEntity.Name
            },
            AvatarUrl = user.AvatarUrl
        };

        return this;
    }

    public SelectFieldsFromUserSpecification Ver5()
    {
        SelectExpression = user => new()
        {
            Id = user.Id
        };

        return this;
    }
}
