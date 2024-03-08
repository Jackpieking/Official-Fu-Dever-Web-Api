using Application.Features.Department.CreateDepartment;
using Application.Features.Department.CreateDepartment.Middlewares;
using Application.Features.Department.GetAllDepartments;
using Application.Features.Department.GetAllDepartments.Middlewares;
using Application.Features.Department.GetAllDepartmentsByDepartmentName;
using Application.Features.Department.GetAllDepartmentsByDepartmentName.Middlewares;
using Application.Features.Department.GetAllTemporarilyRemovedDepartments;
using Application.Features.Department.GetAllTemporarilyRemovedDepartments.Middlewares;
using Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;
using Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId.Middlewares;
using Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;
using Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId.Middlewares;
using Application.Features.Department.RestoreDepartmentByDepartmentId;
using Application.Features.Department.RestoreDepartmentByDepartmentId.Middlewares;
using Application.Features.Department.UpdateDepartmentByDepartmentId;
using Application.Features.Department.UpdateDepartmentByDepartmentId.Middlewares;
using Application.Features.Hobby.CreateHobby;
using Application.Features.Hobby.CreateHobby.Middlewares;
using Application.Features.Hobby.GetAllHobbies;
using Application.Features.Hobby.GetAllHobbies.Middlewares;
using Application.Features.Hobby.GetAllHobbiesByHobbyName;
using Application.Features.Hobby.GetAllHobbiesByHobbyName.Middlewares;
using Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;
using Application.Features.Hobby.GetAllTemporarilyRemovedHobbies.Middlewares;
using Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;
using Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId.Middlewares;
using Application.Features.Hobby.UpdateHobbyByHobbyId;
using Application.Features.Hobby.UpdateHobbyByHobbyId.Middlewares;
using Application.Features.Position.CreatePosition;
using Application.Features.Position.CreatePosition.Middlewares;
using Application.Features.Position.GetAllPositions;
using Application.Features.Position.GetAllPositions.Middlewares;
using Application.Features.Position.GetAllPositionsByPositionName;
using Application.Features.Position.GetAllPositionsByPositionName.Middlewares;
using Application.Features.Position.GetAllTemporarilyRemovedPositions;
using Application.Features.Position.GetAllTemporarilyRemovedPositions.Middlewares;
using Application.Features.Position.RemovePositionPermanentlyByPositionId;
using Application.Features.Position.RemovePositionPermanentlyByPositionId.Middlewares;
using Application.Features.Position.RemovePositionTemporarilyByPositionId;
using Application.Features.Position.RemovePositionTemporarilyByPositionId.Middlewares;
using Application.Features.Position.RestorePositionByPositionId;
using Application.Features.Position.RestorePositionByPositionId.Middlewares;
using Application.Features.Position.UpdatePosition;
using Application.Features.Position.UpdatePosition.Middlewares;
using Application.Features.Skill.CreateSkill;
using Application.Features.Skill.CreateSkill.Middlewares;
using Application.Features.Skill.GetAllSkills;
using Application.Features.Skill.GetAllSkills.Middlewares;
using Application.Features.Skill.GetAllSkillsBySkillName;
using Application.Features.Skill.GetAllSkillsBySkillName.Middlewares;
using Application.Features.Skill.GetAllTemporarilyRemovedSkills;
using Application.Features.Skill.GetAllTemporarilyRemovedSkills.Middlewares;
using Application.Features.Skill.RemoveSkillPermanentlyBySkillId;
using Application.Features.Skill.RemoveSkillPermanentlyBySkillId.Middlewares;
using Application.Features.Skill.RemoveSkillTemporarilyBySkillId;
using Application.Features.Skill.RemoveSkillTemporarilyBySkillId.Middlewares;
using Application.Features.Skill.RestoreSkillBySkillId;
using Application.Features.Skill.RestoreSkillBySkillId.Middlewares;
using Application.Features.Skill.UpdateSkillBySkillId;
using Application.Features.Skill.UpdateSkillBySkillId.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void AddApplication(this IServiceCollection services)
    {
        services.ConfigureFluentValidation();

        services.ConfigureMediatR();

        services.ConfigureCore();
    }

    /// <summary>
    ///     Configure mediatR service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration: config =>
            config.RegisterServicesFromAssemblyContaining(type: typeof(DependencyInjection)));
    }

    /// <summary>
    ///     Configure fluent validation service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(type: typeof(DependencyInjection));
    }

    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCore(this IServiceCollection services)
    {
        services.SkillUseCaseMiddlewaresConfig();
        services.DepartmentUseCaseMiddlewaresConfig();
        services.PositionUseCaseMiddlewaresConfig();
        services.HobbyUseCaseMiddlewaresConfig();
    }

    /// <summary>
    ///     Configuring skill use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void SkillUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        #region GetAllSkills
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllSkillsRequest,
                    GetAllSkillsResponse>),
                typeof(GetAllSkillsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllSkillsRequest,
                    GetAllSkillsResponse>),
                typeof(GetAllSkillCachingMiddleware));
        #endregion

        #region GetAllSkillsBySkillName
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllSkillsBySkillNameRequest,
                    GetAllSkillsBySkillNameResponse>),
                typeof(GetAllSkillsBySkillNameValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllSkillsBySkillNameRequest,
                    GetAllSkillsBySkillNameResponse>),
                typeof(GetAllSkillsBySkillNameCachingMiddleware));
        #endregion

        #region CreateSkill
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateSkillRequest,
                    CreateSkillResponse>),
                typeof(CreateSkillValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateSkillRequest,
                    CreateSkillResponse>),
                typeof(CreateSkillCachingMiddleware));
        #endregion

        #region RemoveSkillTemporarilyBySkillId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveSkillTemporarilyBySkillIdRequest,
                    RemoveSkillTemporarilyBySkillIdResponse>),
                typeof(RemoveSkillTemporarilyBySkillIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveSkillTemporarilyBySkillIdRequest,
                    RemoveSkillTemporarilyBySkillIdResponse>),
                typeof(RemoveSkillTemporarilyBySkillIdCachingMiddleware));
        #endregion

        #region UpdateSkillBySkillId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateSkillBySkillIdRequest,
                    UpdateSkillBySkillIdResponse>),
                typeof(UpdateSkillBySkillIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateSkillBySkillIdRequest,
                    UpdateSkillBySkillIdResponse>),
                typeof(UpdateSkillBySkillIdCachingMiddleware));
        #endregion

        #region GetAllTemporarilyRemovedSkills
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedSkillsRequest,
                    GetAllTemporarilyRemovedSkillsResponse>),
                typeof(GetAllTemporarilyRemovedSkillsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedSkillsRequest,
                    GetAllTemporarilyRemovedSkillsResponse>),
                typeof(GetAllTemporarilyRemovedSkillsCachingMiddleware));
        #endregion

        #region RemoveSkillPermanentlyBySkillId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveSkillPermanentlyBySkillIdRequest,
                    RemoveSkillPermanentlyBySkillIdResponse>),
                typeof(RemoveSkillPermanentlyBySkillIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveSkillPermanentlyBySkillIdRequest,
                    RemoveSkillPermanentlyBySkillIdResponse>),
                typeof(RemoveSkillPermanentlyBySkillIdCachingMiddleware));
        #endregion

        #region RestoreSkillBySkillId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreSkillBySkillIdRequest,
                    RestoreSkillBySkillIdResponse>),
                typeof(RestoreSkillBySkillIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreSkillBySkillIdRequest,
                    RestoreSkillBySkillIdResponse>),
                typeof(RestoreSkillBySkillIdCachingMiddleware));
        #endregion
    }

    /// <summary>
    ///     Configuring department use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void DepartmentUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        #region GetAllDepartments
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllDepartmentsRequest,
                    GetAllDepartmentsResponse>),
                typeof(GetAllDepartmentsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllDepartmentsRequest,
                    GetAllDepartmentsResponse>),
                typeof(GetAllDepartmentsCachingMiddleware));
        #endregion

        #region GetAllDepartmentsByDepartmentName
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllDepartmentsByDepartmentNameRequest,
                    GetAllDepartmentsByDepartmentNameResponse>),
                typeof(GetAllDepartmentsByDepartmentNameValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllDepartmentsByDepartmentNameRequest,
                    GetAllDepartmentsByDepartmentNameResponse>),
                typeof(GetAllDepartmentsByDepartmentNameCachingMiddleware));
        #endregion

        #region CreateDepartment
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateDepartmentRequest,
                    CreateDepartmentResponse>),
                typeof(CreateDepartmentValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateDepartmentRequest,
                    CreateDepartmentResponse>),
                typeof(CreateDepartmentCachingMiddleware));
        #endregion

        #region RemoveDepartmentTemporarilyByDepartmentId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveDepartmentTemporarilyByDepartmentIdRequest,
                    RemoveDepartmentTemporarilyByDepartmentIdResponse>),
                typeof(RemoveDepartmentTemporarilyByDepartmentIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveDepartmentTemporarilyByDepartmentIdRequest,
                    RemoveDepartmentTemporarilyByDepartmentIdResponse>),
                typeof(RemoveDepartmentTemporarilyByDepartmentIdCachingMiddleware));
        #endregion

        #region UpdateDepartmentByDepartmentId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateDepartmentByDepartmentIdRequest,
                    UpdateDepartmentByDepartmentIdResponse>),
                typeof(UpdateDepartmentByDepartmentIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateDepartmentByDepartmentIdRequest,
                    UpdateDepartmentByDepartmentIdResponse>),
                typeof(UpdateDepartmentByDepartmentIdCachingMiddleware));
        #endregion

        #region GetAllTemporarilyRemovedDepartments
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedDepartmentsRequest,
                    GetAllTemporarilyRemovedDepartmentsResponse>),
                typeof(GetAllTemporarilyRemovedDepartmentsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedDepartmentsRequest,
                    GetAllTemporarilyRemovedDepartmentsResponse>),
                typeof(GetAllTemporarilyRemovedDepartmentsCachingMiddleware));
        #endregion

        #region RemoveDepartmentPermanentlyByDepartmentId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveDepartmentPermanentlyByDepartmentIdRequest,
                    RemoveDepartmentPermanentlyByDepartmentIdResponse>),
                typeof(RemoveDepartmentPermanentlyByDepartmentIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveDepartmentPermanentlyByDepartmentIdRequest,
                    RemoveDepartmentPermanentlyByDepartmentIdResponse>),
                typeof(RemoveDepartmentPermanentlyByDepartmentIdCachingMiddleware));
        #endregion

        #region RestoreDepartmentByDepartmentId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreDepartmentByDepartmentIdRequest,
                    RestoreDepartmentByDepartmentIdResponse>),
                typeof(RestoreDepartmentByDepartmentIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreDepartmentByDepartmentIdRequest,
                    RestoreDepartmentByDepartmentIdResponse>),
                typeof(RestoreDepartmentByDepartmentIdCachingMiddleware));
        #endregion
    }

    /// <summary>
    ///     Configuring position use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void PositionUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        #region GetAllPositions
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPositionsRequest,
                    GetAllPositionsResponse>),
                typeof(GetAllPositionsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPositionsRequest,
                    GetAllPositionsResponse>),
                typeof(GetAllPositionsCachingMiddleware));
        #endregion

        #region GetAllPositionsByPositionName
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPositionsByPositionNameRequest,
                    GetAllPositionsByPositionNameResponse>),
                typeof(GetAllPositionsByPositionNameValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPositionsByPositionNameRequest,
                    GetAllPositionsByPositionNameResponse>),
                typeof(GetAllPositionsByPositionNameCachingMiddleware));
        #endregion

        #region CreatePosition
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreatePositionRequest,
                    CreatePositionResponse>),
                typeof(CreatePositionValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreatePositionRequest,
                    CreatePositionResponse>),
                typeof(CreatePositionCachingMiddleware));
        #endregion

        #region RemovePositionTemporarilyByPositionId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePositionTemporarilyByPositionIdRequest,
                    RemovePositionTemporarilyByPositionIdResponse>),
                typeof(RemovePositionTemporarilyByPositionIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePositionTemporarilyByPositionIdRequest,
                    RemovePositionTemporarilyByPositionIdResponse>),
                typeof(RemovePositionTemporarilyByPositionIdCachingMiddleware));
        #endregion

        #region UpdatePositionByPositionId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdatePositionByPositionIdRequest,
                    UpdatePositionByPositionIdResponse>),
                typeof(UpdatePositionByPositionIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdatePositionByPositionIdRequest,
                    UpdatePositionByPositionIdResponse>),
                typeof(UpdatePositionByPositionIdCachingMiddleware));
        #endregion

        #region GetAllTemporarilyRemovedPositions
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedPositionsRequest,
                    GetAllTemporarilyRemovedPositionsResponse>),
                typeof(GetAllTemporarilyRemovedPositionsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedPositionsRequest,
                    GetAllTemporarilyRemovedPositionsResponse>),
                typeof(GetAllTemporarilyRemovedPositionsCachingMiddleware));
        #endregion

        #region RemovePositionPermanentlyByPositionId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePositionPermanentlyByPositionIdRequest,
                    RemovePositionPermanentlyByPositionIdResponse>),
                typeof(RemovePositionPermanentlyByPositionIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePositionPermanentlyByPositionIdRequest,
                    RemovePositionPermanentlyByPositionIdResponse>),
                typeof(RemovePositionPermanentlyByPositionIdCachingMiddleware));
        #endregion

        #region RestorePositionByPositionId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestorePositionByPositionIdRequest,
                    RestorePositionByPositionIdResponse>),
                typeof(RestorePositionByPositionIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestorePositionByPositionIdRequest,
                    RestorePositionByPositionIdResponse>),
                typeof(RestorePositionByPositionIdCachingMiddleware));
        #endregion
    }

    /// <summary>
    ///     Configuring position use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void HobbyUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        #region GetAllHobbies
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllHobbiesRequest,
                    GetAllHobbiesResponse>),
                typeof(GetAllHobbiesValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllHobbiesRequest,
                    GetAllHobbiesResponse>),
                typeof(GetAllHobbiesCachingMiddleware));
        #endregion

        #region GetAllHobbiesByHobbyName
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllHobbiesByHobbyNameRequest,
                    GetAllHobbiesByHobbyNameResponse>),
                typeof(GetAllHobbiesByHobbyNameValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllHobbiesByHobbyNameRequest,
                    GetAllHobbiesByHobbyNameResponse>),
                typeof(GetAllHobbiesByHobbyNameCachingMiddleware));
        #endregion

        #region CreateHobby
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateHobbyRequest,
                    CreateHobbyResponse>),
                typeof(CreateHobbyValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateHobbyRequest,
                    CreateHobbyResponse>),
                typeof(CreateHobbyCachingMiddleware));
        #endregion

        #region UpdateHobbyByHobbyId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateHobbyByHobbyIdRequest,
                    UpdateHobbyByHobbyIdResponse>),
                typeof(UpdateHobbyByHobbyIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateHobbyByHobbyIdRequest,
                    UpdateHobbyByHobbyIdResponse>),
                typeof(UpdateHobbyByHobbyIdCachingMiddleware));
        #endregion

        #region RemoveHobbyTemporarilyByHobbyId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveHobbyTemporarilyByHobbyIdRequest,
                    RemoveHobbyTemporarilyByHobbyIdResponse>),
                typeof(RemoveHobbyTemporarilyByHobbyIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveHobbyTemporarilyByHobbyIdRequest,
                    RemoveHobbyTemporarilyByHobbyIdResponse>),
                typeof(RemoveHobbyTemporarilyByHobbyIdCachingMiddleware));
        #endregion

        #region GetAllTemporarilyRemovedHobbies
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedHobbiesRequest,
                    GetAllTemporarilyRemovedHobbiesResponse>),
                typeof(GetAllTemporarilyRemovedHobbiesValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedHobbiesRequest,
                    GetAllTemporarilyRemovedHobbiesResponse>),
                typeof(GetAllTemporarilyRemovedHobbiesCachingMiddleware));
        #endregion
    }
}
