using FluentValidation;
using FuDever.Application.Features.Department.CreateDepartment;
using FuDever.Application.Features.Department.CreateDepartment.Middlewares;
using FuDever.Application.Features.Department.GetAllDepartments;
using FuDever.Application.Features.Department.GetAllDepartments.Middlewares;
using FuDever.Application.Features.Department.GetAllDepartmentsByDepartmentName;
using FuDever.Application.Features.Department.GetAllDepartmentsByDepartmentName.Middlewares;
using FuDever.Application.Features.Department.GetAllTemporarilyRemovedDepartments;
using FuDever.Application.Features.Department.GetAllTemporarilyRemovedDepartments.Middlewares;
using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;
using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId.Middlewares;
using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;
using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId.Middlewares;
using FuDever.Application.Features.Department.RestoreDepartmentByDepartmentId;
using FuDever.Application.Features.Department.RestoreDepartmentByDepartmentId.Middlewares;
using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;
using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId.Middlewares;
using FuDever.Application.Features.Hobby.CreateHobby;
using FuDever.Application.Features.Hobby.CreateHobby.Middlewares;
using FuDever.Application.Features.Hobby.GetAllHobbies;
using FuDever.Application.Features.Hobby.GetAllHobbies.Middlewares;
using FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName;
using FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName.Middlewares;
using FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;
using FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies.Middlewares;
using FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;
using FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId.Middlewares;
using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;
using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId.Middlewares;
using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;
using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId.Middlewares;
using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;
using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId.Middlewares;
using FuDever.Application.Features.Major.CreateMajor;
using FuDever.Application.Features.Major.CreateMajor.Middlewares;
using FuDever.Application.Features.Major.GetAllMajors;
using FuDever.Application.Features.Major.GetAllMajors.Middlewares;
using FuDever.Application.Features.Major.GetAllMajorsByMajorName;
using FuDever.Application.Features.Major.GetAllMajorsByMajorName.Middlewares;
using FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;
using FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors.Middlewares;
using FuDever.Application.Features.Major.RemoveMajorPermanentlyByMajorId;
using FuDever.Application.Features.Major.RemoveMajorPermanentlyByMajorId.Middlewares;
using FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId;
using FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId.Middlewares;
using FuDever.Application.Features.Major.RestoreMajorByMajorId;
using FuDever.Application.Features.Major.RestoreMajorByMajorId.Middlewares;
using FuDever.Application.Features.Major.UpdateMajorByMajorId;
using FuDever.Application.Features.Major.UpdateMajorByMajorId.Middlewares;
using FuDever.Application.Features.Platform.CreatePlatform;
using FuDever.Application.Features.Platform.CreatePlatform.Middlewares;
using FuDever.Application.Features.Platform.GetAllPlatforms;
using FuDever.Application.Features.Platform.GetAllPlatforms.Middlewares;
using FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName;
using FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName.Middlewares;
using FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;
using FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms.Middlewares;
using FuDever.Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;
using FuDever.Application.Features.Platform.RemovePlatformPermanentlyByPlatformId.Middlewares;
using FuDever.Application.Features.Platform.RemovePlatformTemporarilyByPlatformId;
using FuDever.Application.Features.Platform.RemovePlatformTemporarilyByPlatformId.Middlewares;
using FuDever.Application.Features.Platform.RestorePlatformByPlatformId;
using FuDever.Application.Features.Platform.RestorePlatformByPlatformId.Middlewares;
using FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;
using FuDever.Application.Features.Platform.UpdatePlatformByPlatformId.Middlewares;
using FuDever.Application.Features.Position.CreatePosition;
using FuDever.Application.Features.Position.CreatePosition.Middlewares;
using FuDever.Application.Features.Position.GetAllPositions;
using FuDever.Application.Features.Position.GetAllPositions.Middlewares;
using FuDever.Application.Features.Position.GetAllPositionsByPositionName;
using FuDever.Application.Features.Position.GetAllPositionsByPositionName.Middlewares;
using FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;
using FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions.Middlewares;
using FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;
using FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId.Middlewares;
using FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId;
using FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId.Middlewares;
using FuDever.Application.Features.Position.RestorePositionByPositionId;
using FuDever.Application.Features.Position.RestorePositionByPositionId.Middlewares;
using FuDever.Application.Features.Position.UpdatePositionByPositionId;
using FuDever.Application.Features.Position.UpdatePositionByPositionId.Middlewares;
using FuDever.Application.Features.Skill.CreateSkill;
using FuDever.Application.Features.Skill.CreateSkill.Middlewares;
using FuDever.Application.Features.Skill.GetAllSkills;
using FuDever.Application.Features.Skill.GetAllSkills.Middlewares;
using FuDever.Application.Features.Skill.GetAllSkillsBySkillName;
using FuDever.Application.Features.Skill.GetAllSkillsBySkillName.Middlewares;
using FuDever.Application.Features.Skill.GetAllTemporarilyRemovedSkills;
using FuDever.Application.Features.Skill.GetAllTemporarilyRemovedSkills.Middlewares;
using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;
using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId.Middlewares;
using FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId;
using FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId.Middlewares;
using FuDever.Application.Features.Skill.RestoreSkillBySkillId;
using FuDever.Application.Features.Skill.RestoreSkillBySkillId.Middlewares;
using FuDever.Application.Features.Skill.UpdateSkillBySkillId;
using FuDever.Application.Features.Skill.UpdateSkillBySkillId.Middlewares;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FuDever.Application;

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
            config.RegisterServicesFromAssemblyContaining(
                type: typeof(DependencyInjection)));
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
        services.SkillFeatureMiddlewaresConfig();
        services.DepartmentFeatureMiddlewaresConfig();
        services.PositionFeatureMiddlewaresConfig();
        services.HobbyFeatureMiddlewaresConfig();
        services.PlatformFeatureMiddlewaresConfig();
        services.MajorFeatureMiddlewaresConfig();
    }

    /// <summary>
    ///     Configuring skill features middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void SkillFeatureMiddlewaresConfig(this IServiceCollection services)
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
    ///     Configuring department features middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void DepartmentFeatureMiddlewaresConfig(this IServiceCollection services)
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
    ///     Configuring position features middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void PositionFeatureMiddlewaresConfig(this IServiceCollection services)
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
    ///     Configuring position features middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void HobbyFeatureMiddlewaresConfig(this IServiceCollection services)
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

        #region RemoveHobbyPermanentlyByHobbyId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveHobbyPermanentlyByHobbyIdRequest,
                    RemoveHobbyPermanentlyByHobbyIdResponse>),
                typeof(RemoveHobbyPermanentlyByHobbyIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveHobbyPermanentlyByHobbyIdRequest,
                    RemoveHobbyPermanentlyByHobbyIdResponse>),
                typeof(RemoveHobbyPermanentlyByHobbyIdCachingMiddleware));
        #endregion

        #region RestoreHobbyByHobbyId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreHobbyByHobbyIdRequest,
                    RestoreHobbyByHobbyIdResponse>),
                typeof(RestoreHobbyByHobbyIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreHobbyByHobbyIdRequest,
                    RestoreHobbyByHobbyIdResponse>),
                typeof(RestoreHobbyByHobbyIdCachingMiddleware));
        #endregion
    }

    /// <summary>
    ///     Configuring platform features middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void PlatformFeatureMiddlewaresConfig(this IServiceCollection services)
    {
        #region GetAllPlatforms
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPlatformsRequest,
                    GetAllPlatformsResponse>),
                typeof(GetAllPlatformsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPlatformsRequest,
                    GetAllPlatformsResponse>),
                typeof(GetAllPlatformsCachingMiddleware));
        #endregion

        #region GetAllPlatformsByPlatformName
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPlatformsByPlatformNameRequest,
                    GetAllPlatformsByPlatformNameResponse>),
                typeof(GetAllPlatformsByPlatformNameValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllPlatformsByPlatformNameRequest,
                    GetAllPlatformsByPlatformNameResponse>),
                typeof(GetAllPlatformsByPlatformNameCachingMiddleware));
        #endregion

        #region CreatePlatform
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreatePlatformRequest,
                    CreatePlatformResponse>),
                typeof(CreatePlatformValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreatePlatformRequest,
                    CreatePlatformResponse>),
                typeof(CreatePlatformCachingMiddleware));
        #endregion

        #region UpdatePlatformByPlatformId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdatePlatformByPlatformIdRequest,
                    UpdatePlatformByPlatformIdResponse>),
                typeof(UpdatePlatformByPlatformIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdatePlatformByPlatformIdRequest,
                    UpdatePlatformByPlatformIdResponse>),
                typeof(UpdatePlatformByPlatformIdCachingMiddleware));
        #endregion

        #region RemovePlatformTemporarilyByPlatformId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePlatformTemporarilyByPlatformIdRequest,
                    RemovePlatformTemporarilyByPlatformIdResponse>),
                typeof(RemovePlatformTemporarilyByPlatformIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePlatformTemporarilyByPlatformIdRequest,
                    RemovePlatformTemporarilyByPlatformIdResponse>),
                typeof(RemovePlatformTemporarilyByPlatformIdCachingMiddleware));
        #endregion

        #region GetAllTemporarilyRemovedPlatforms
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedPlatformsRequest,
                    GetAllTemporarilyRemovedPlatformsResponse>),
                typeof(GetAllTemporarilyRemovedPlatformsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedPlatformsRequest,
                    GetAllTemporarilyRemovedPlatformsResponse>),
                typeof(GetAllTemporarilyRemovedPlatformsCachingMiddleware));
        #endregion

        #region RemovePlatformPermanentlyByPlatformId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePlatformPermanentlyByPlatformIdRequest,
                    RemovePlatformPermanentlyByPlatformIdResponse>),
                typeof(RemovePlatformPermanentlyByPlatformIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemovePlatformPermanentlyByPlatformIdRequest,
                    RemovePlatformPermanentlyByPlatformIdResponse>),
                typeof(RemovePlatformPermanentlyByPlatformIdCachingMiddleware));
        #endregion

        #region RestorePlatformByPlatformId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestorePlatformByPlatformIdRequest,
                    RestorePlatformByPlatformIdResponse>),
                typeof(RestorePlatformByPlatformIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestorePlatformByPlatformIdRequest,
                    RestorePlatformByPlatformIdResponse>),
                typeof(RestorePlatformByPlatformIdCachingMiddleware));
        #endregion
    }

    /// <summary>
    ///     Configuring major features middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void MajorFeatureMiddlewaresConfig(this IServiceCollection services)
    {
        #region GetAllMajors
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllMajorsRequest,
                    GetAllMajorsResponse>),
                typeof(GetAllMajorsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllMajorsRequest,
                    GetAllMajorsResponse>),
                typeof(GetAllMajorsCachingMiddleware));
        #endregion

        #region GetAllMajorsByMajorName
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllMajorsByMajorNameRequest,
                    GetAllMajorsByMajorNameResponse>),
                typeof(GetAllMajorsByMajorNameValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllMajorsByMajorNameRequest,
                    GetAllMajorsByMajorNameResponse>),
                typeof(GetAllMajorsByMajorNameCachingMiddleware));
        #endregion

        #region CreateMajor
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateMajorRequest,
                    CreateMajorResponse>),
                typeof(CreateMajorValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    CreateMajorRequest,
                    CreateMajorResponse>),
                typeof(CreateMajorCachingMiddleware));
        #endregion

        #region UpdateMajorByMajorId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateMajorByMajorIdRequest,
                    UpdateMajorByMajorIdResponse>),
                typeof(UpdateMajorByMajorIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    UpdateMajorByMajorIdRequest,
                    UpdateMajorByMajorIdResponse>),
                typeof(UpdateMajorByMajorIdCachingMiddleware));
        #endregion

        #region RemoveMajorPermanentlyByMajorId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveMajorPermanentlyByMajorIdRequest,
                    RemoveMajorPermanentlyByMajorIdResponse>),
                typeof(RemoveMajorPermanentlyByMajorIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveMajorPermanentlyByMajorIdRequest,
                    RemoveMajorPermanentlyByMajorIdResponse>),
                typeof(RemoveMajorPermanentlyByMajorIdCachingMiddleware));
        #endregion

        #region RemoveMajorTemporarilyByMajorId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveMajorTemporarilyByMajorIdRequest,
                    RemoveMajorTemporarilyByMajorIdResponse>),
                typeof(RemoveMajorTemporarilyByMajorIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RemoveMajorTemporarilyByMajorIdRequest,
                    RemoveMajorTemporarilyByMajorIdResponse>),
                typeof(RemoveMajorTemporarilyByMajorIdCachingMiddleware));
        #endregion

        #region RestoreMajorByMajorId
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreMajorByMajorIdRequest,
                    RestoreMajorByMajorIdResponse>),
                typeof(RestoreMajorByMajorIdValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    RestoreMajorByMajorIdRequest,
                    RestoreMajorByMajorIdResponse>),
                typeof(RestoreMajorByMajorIdCachingMiddleware));
        #endregion

        #region GetAllTemporarilyRemovedMajors
        services
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedMajorsRequest,
                    GetAllTemporarilyRemovedMajorsResponse>),
                typeof(GetAllTemporarilyRemovedMajorsValidationMiddleware))
            .AddScoped(
                typeof(IPipelineBehavior<
                    GetAllTemporarilyRemovedMajorsRequest,
                    GetAllTemporarilyRemovedMajorsResponse>),
                typeof(GetAllTemporarilyRemovedMajorsCachingMiddleware));
        #endregion
    }
}
