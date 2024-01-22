using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;
using WebApi.ActionResults;
using WebApi.ApiReturnCodes.Base;
using WebApi.Authorization.Requires;
using WebApi.Common;
using WebApi.Middlewares;
using WebApi.Options.ApiController;
using WebApi.Options.Authentication.Jwt;
using WebApi.Options.Authorization;
using WebApi.Options.RateLimiter.FixedWindow;
using WebApi.Options.Swagger.Swashbuckle;

namespace WebApi;

/// <summary>
///     Configure services for this layer.
/// </summary>
internal static class DependencyInjection
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
    internal static void AddWebApi(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigureAuthentication(configuration: configuration);

        services.ConfigureAuthorization(configuration: configuration);

        services.ConfigureLogging();

        services.ConfigureCORS();

        services.ConfigureController(configuration: configuration);

        services.ConfigureSwagger(configuration: configuration);

        services.ConfigureRateLimiter(configuration: configuration);

        services.ConfigureExceptionHandler();
    }

    /// <summary>
    ///     Configure the authentication service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureAuthentication(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        const string AuthenticationSection = "Authentication";

        var jwtAuthenticationOption = configuration
            .GetRequiredSection(key: AuthenticationSection)
            .Get<JwtAuthenticationOption>();

        services
            .AddAuthentication(configureOptions: config =>
            {
                config.DefaultAuthenticateScheme = jwtAuthenticationOption.Common.DefaultAuthenticateScheme;
                config.DefaultScheme = jwtAuthenticationOption.Common.DefaultScheme;
                config.DefaultChallengeScheme = jwtAuthenticationOption.Common.DefaultChallengeScheme;
            })
            .AddJwtBearer(configureOptions: config =>
            {
                config.TokenValidationParameters = new()
                {
                    ValidateIssuer = jwtAuthenticationOption.Type.Jwt.ValidateIssuer,
                    ValidateAudience = jwtAuthenticationOption.Type.Jwt.ValidateAudience,
                    ValidateLifetime = jwtAuthenticationOption.Type.Jwt.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtAuthenticationOption.Type.Jwt.ValidateIssuerSigningKey,
                    RequireExpirationTime = jwtAuthenticationOption.Type.Jwt.RequireExpirationTime,
                    ValidIssuer = jwtAuthenticationOption.Type.Jwt.ValidIssuer,
                    ValidAudience = jwtAuthenticationOption.Type.Jwt.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        key: new HMACSHA256(
                            key: Encoding.UTF8
                                .GetBytes(
                                    s: jwtAuthenticationOption.Type.Jwt.IssuerSigningKey))
                        .Key)
                };
            });
    }

    /// <summary>
    ///     Configure the authorization service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureAuthorization(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        const string AuthorizationSection = "Authorization";

        var authorizationOption = configuration
            .GetRequiredSection(key: AuthorizationSection)
            .Get<AuthorizationOption>();

        services.AddAuthorization(configure: config =>
        {
            authorizationOption.Common.InvokeHandlersAfterFailure = false;

            // Default policy.
            AuthorizationPolicyBuilder authorizationPolicyBuilder = new();

            authorizationPolicyBuilder
                .AddAuthenticationSchemes(schemes: authorizationOption.Policy.Default.AuthenticationSchemes)
                .RequireAuthenticatedUser();

            authorizationPolicyBuilder.Requirements.Add(item: new AccessTokenRequire());
            authorizationPolicyBuilder.Requirements.Add(item: new AccessTokenExpiredTimeRequire());

            config.DefaultPolicy = authorizationPolicyBuilder.Build();

            // Admin role require policy.
            config.AddPolicy(
                name: nameof(authorizationOption.Policy.AdminRoleRequire),
                configurePolicy: policy =>
                {
                    policy
                        .AddAuthenticationSchemes(schemes: authorizationOption.Policy.AdminRoleRequire.AuthenticationSchemes)
                        .RequireAuthenticatedUser();

                    policy.Requirements.Add(item: new AccessTokenRequire());
                    policy.Requirements.Add(item: new AccessTokenExpiredTimeRequire());
                    policy.Requirements.Add(item: new AdminRoleRequire());
                });

            // refresh access token require policy.
            config.AddPolicy(
                name: nameof(authorizationOption.Policy.RefreshAccessTokenRequire),
                configurePolicy: policy =>
                {
                    policy
                        .AddAuthenticationSchemes(schemes: authorizationOption.Policy.RefreshAccessTokenRequire.AuthenticationSchemes)
                        .RequireAuthenticatedUser();

                    policy.Requirements.Add(item: new AccessTokenExpiredTimeRequire());
                });
        });
    }

    /// <summary>
    ///     Configure the logging service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureLogging(this IServiceCollection services)
    {
        services.AddLogging(configure: config =>
        {
            config.ClearProviders();
            config.AddConsole();
        });
    }

    /// <summary>
    ///     Configure the CORS service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCORS(this IServiceCollection services)
    {
        services.AddCors(setupAction: config =>
        {
            config.AddDefaultPolicy(configurePolicy: policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    /// <summary>
    ///     Configure the controller service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureController(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        const string ApiControllerSection = "ApiController";

        var apiControllerOption = configuration
            .GetRequiredSection(key: ApiControllerSection)
            .Get<ApiControllerOption>();

        services
            .AddControllers(configure: config =>
            {
                config.SuppressAsyncSuffixInActionNames = apiControllerOption.SuppressAsyncSuffixInActionNames;
            })
            .ConfigureApiBehaviorOptions(setupAction: config =>
            {
                config.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new CustomGlobalBadRequestResult();
                };
            });
    }

    /// <summary>
    ///     Configure the swagger service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureSwagger(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddSwaggerGen(setupAction: config =>
        {
            const string SwaggerSection = "Swagger";
            const string SwashbuckleSection = "Swashbuckle";

            var swashbuckleOption = configuration
                .GetRequiredSection(key: SwaggerSection)
                .GetRequiredSection(key: SwashbuckleSection)
                .Get<SwashbuckleOption>();

            config.SwaggerDoc(
                name: swashbuckleOption.Doc.Name,
                info: new()
                {
                    Version = swashbuckleOption.Doc.Info.Version,
                    Title = swashbuckleOption.Doc.Info.Title,
                    Description = swashbuckleOption.Doc.Info.Description,
                    Contact = new()
                    {
                        Name = swashbuckleOption.Doc.Info.Contact.Name,
                        Email = swashbuckleOption.Doc.Info.Contact.Email
                    },
                    License = new()
                    {
                        Name = swashbuckleOption.Doc.Info.License.Name,
                        Url = new(uriString: swashbuckleOption.Doc.Info.License.Url)
                    }
                });

            config.AddSecurityDefinition(
                name: swashbuckleOption.Security.Definition.Name,
                securityScheme: new()
                {
                    Description = swashbuckleOption.Security.Definition.SecurityScheme.Description,
                    Name = swashbuckleOption.Security.Definition.SecurityScheme.Name,
                    In = (ParameterLocation)Enum.ToObject(
                        enumType: typeof(ParameterLocation),
                        value: swashbuckleOption.Security.Definition.SecurityScheme.In),
                    Type = (SecuritySchemeType)Enum.ToObject(
                        enumType: typeof(SecuritySchemeType),
                        value: swashbuckleOption.Security.Definition.SecurityScheme.Type),
                    Scheme = swashbuckleOption.Security.Definition.SecurityScheme.Scheme
                });

            config.AddSecurityRequirement(securityRequirement: new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = (ReferenceType)Enum.ToObject(
                                enumType: typeof(ReferenceType),
                                value: swashbuckleOption.Security.Requirement.OpenApiSecurityScheme.Reference.Type),
                            Id = swashbuckleOption.Security.Requirement.OpenApiSecurityScheme.Reference.Id
                        },
                        Scheme = swashbuckleOption.Security.Requirement.OpenApiSecurityScheme.Scheme,
                        Name = swashbuckleOption.Security.Requirement.OpenApiSecurityScheme.Name,
                        In = (ParameterLocation)Enum.ToObject(
                            enumType: typeof(ParameterLocation),
                            value: swashbuckleOption.Security.Requirement.OpenApiSecurityScheme.In),
                    },
                    swashbuckleOption.Security.Requirement.Values
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            var xmlFilePath = Path.Combine(
                    path1: AppContext.BaseDirectory,
                    path2: xmlFilename);

            config.IncludeXmlComments(filePath: xmlFilePath);
        });
    }

    /// <summary>
    ///     Configure the rate limiter service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureRateLimiter(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddRateLimiter(configureOptions: config =>
        {
            config.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                const string ApiRateLimiterSection = "ApiRateLimiter";
                const string FixedWindowSection = "FixedWindow";

                var fixedWindowRateLimiterOption = configuration
                    .GetRequiredSection(key: ApiRateLimiterSection)
                    .GetRequiredSection(key: FixedWindowSection)
                    .Get<FixedWindowRateLimiterOption>();

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress.ToString(),
                    factory: option => new()
                    {
                        PermitLimit = fixedWindowRateLimiterOption.RemoteIpAddress.PermitLimit,
                        QueueProcessingOrder = (QueueProcessingOrder)Enum.ToObject(
                            enumType: typeof(QueueProcessingOrder),
                            value: fixedWindowRateLimiterOption.RemoteIpAddress.QueueProcessingOrder),
                        QueueLimit = fixedWindowRateLimiterOption.RemoteIpAddress.QueueLimit,
                        Window = TimeSpan.FromSeconds(value: fixedWindowRateLimiterOption.RemoteIpAddress.Window),
                        AutoReplenishment = fixedWindowRateLimiterOption.RemoteIpAddress.AutoReplenishment,
                    });
            });

            config.OnRejected = async (option, cancellationToken) =>
            {
                option.HttpContext.Response.Clear();
                option.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                await option.HttpContext.Response.WriteAsJsonAsync(
                    value: new CommonResponse
                    {
                        ApiReturnCode = BaseApiReturnCode.FAILED,
                        ErrorMessages = new List<string>(capacity: 2)
                        {
                            "Two many request.",
                            "Please try again later."
                        }
                    },
                    cancellationToken: cancellationToken);

                await option.HttpContext.Response.CompleteAsync();
            };
        });
    }

    /// <summary>
    ///     Configure the exception handler service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureExceptionHandler(this IServiceCollection services)
    {
        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();
    }
}
