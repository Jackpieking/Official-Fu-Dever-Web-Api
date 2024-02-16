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
    }
}
