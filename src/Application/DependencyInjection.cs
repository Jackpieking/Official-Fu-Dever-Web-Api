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
        #region SkillUseCaseMiddlewares
        // Get all skills request middlewares.
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

        // Get all skills by name request middlewares.
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

        // Create skill request middlewares.
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

        // Remove skill temporarily by skill id request middlewares.
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

        // Update skill by skill id request middlewares.
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

        // Get all temporarily removed skills request middlewares.
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

        // Remove skill permanently by skill id request middlewares.
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

        // Restore skill by skill id request middlewares.
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

        #region DepartmentUseCaseMiddlewares
        // Get all departments request middlewares.
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

        // Get all departments by department name request middlewares.
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

        // Create department request middlewares.
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

        // Remove department temporarily by department id request middlewares.
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

        // Update department by department id request middlewares.
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

        // Get all temporarily removed departments request middlewares.
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

        // Remove department permanently by department id request middlewares.
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

        // Restore department by department id request middlewares.
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
}
