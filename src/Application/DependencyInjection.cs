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
using Microsoft.Extensions.Configuration;
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
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    public static void AddApplication(
        this IServiceCollection services,
        IConfigurationManager configuration)
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
        {
            config.RegisterServicesFromAssemblyContaining(type: typeof(DependencyInjection));
        });
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
    }

    /// <summary>
    ///     Configuring skill use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void SkillUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        // Get all skills request middleware.
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

        // Get all skills by name request middleware.
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

        // Create skill request middleware.
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

        // Remove skill temporarily by skill id request middleware.
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

        // Update skill by skill id request middleware.
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

        // Get all temporarily removed skills request middleware.
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

        // Remove skill permanently by skill id request middleware.
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

        // Restore skill by skill id request middleware.
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
    }

    /// <summary>
    ///     Configuring department use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void DepartmentUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        // Get all departments request middleware.
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

        // Get all departments by department name request middleware.
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

        // Create department request middleware.
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

        // Remove department temporarily by department id request middleware.
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

        // Update department by department id request middleware.
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

        // Get all temporarily removed departments request middleware.
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

        // Remove department permanently by department id request middleware.
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

        // Restore department by department id request middleware.
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
    }

    /// <summary>
    ///     Configuring position use cases middlewares.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void PositionUseCaseMiddlewaresConfig(this IServiceCollection services)
    {
        // Get all positions request middleware.
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

        // Get all positions by position name request middleware.
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

        // Create position request middleware.
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

        // Remove position temporarily by position id request middleware.
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

        // Update position by position id request middleware.
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

        // Get all temporarily removed positions request middleware.
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

        // Remove position permanently by position id request middleware.
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

        // Restore position by position id request middleware.
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
    }
}
