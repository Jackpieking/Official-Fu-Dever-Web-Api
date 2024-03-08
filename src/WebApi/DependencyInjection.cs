using Configuration.Presentation.WebApi.ApiController;
using Configuration.Presentation.WebApi.Authentication;
using Configuration.Presentation.WebApi.Authorization;
using Configuration.Presentation.WebApi.RateLimiter;
using Configuration.Presentation.WebApi.Swagger;
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
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;
using WebApi.ActionResults;
using WebApi.ApiReturnCodes.Base;
using WebApi.Authorization.Requires;
using WebApi.Commons;
using WebApi.Middlewares;

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

        services.ConfigureCore(configuration: configuration);
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
        var jwtAuthenticationOption = configuration
            .GetRequiredSection(key: "Authentication")
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
                config.TokenValidationParameters = GetTokenValidationParameters(configuration: configuration);
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
        var authorizationOption = configuration
            .GetRequiredSection(key: "Authorization")
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
        var apiControllerOption = configuration
            .GetRequiredSection(key: "ApiController")
            .Get<ApiControllerOption>();

        services
            .AddControllers(configure: config =>
                config.SuppressAsyncSuffixInActionNames =
                    apiControllerOption.SuppressAsyncSuffixInActionNames)
            .ConfigureApiBehaviorOptions(setupAction: config =>
                config.InvalidModelStateResponseFactory = _ =>
                    new CustomGlobalBadRequestResult());
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
            var swashbuckleOption = configuration
                .GetRequiredSection(key: "Swagger")
                .GetRequiredSection(key: "Swashbuckle")
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
                var fixedWindowRateLimiterOption = configuration
                    .GetRequiredSection(key: "ApiRateLimiter")
                    .GetRequiredSection(key: "FixedWindow")
                    .Get<FixedWindowRateLimiterOption>();

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress.ToString(),
                    factory: _ => new()
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
                        ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
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

    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureCore(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services
            .AddScoped(implementationFactory: _ =>
                GetTokenValidationParameters(configuration: configuration))
            .AddScoped<SecurityTokenHandler, JwtSecurityTokenHandler>();
    }

    /// <summary>
    ///     Return pre=defined token validation parameter.
    /// </summary>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    /// <returns>
    ///     Token validation parameter.
    /// </returns>
    private static TokenValidationParameters GetTokenValidationParameters(IConfigurationManager configuration)
    {
        var jwtAuthenticationOption = configuration
            .GetRequiredSection(key: "Authentication")
            .GetRequiredSection(key: "Type")
            .Get<JwtAuthenticationOption>();

        return new()
        {
            ValidateIssuer = jwtAuthenticationOption.Jwt.ValidateIssuer,
            ValidateAudience = jwtAuthenticationOption.Jwt.ValidateAudience,
            ValidateLifetime = jwtAuthenticationOption.Jwt.ValidateLifetime,
            ValidateIssuerSigningKey = jwtAuthenticationOption.Jwt.ValidateIssuerSigningKey,
            RequireExpirationTime = jwtAuthenticationOption.Jwt.RequireExpirationTime,
            ValidIssuer = jwtAuthenticationOption.Jwt.ValidIssuer,
            ValidAudience = jwtAuthenticationOption.Jwt.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                key: new HMACSHA256(
                    key: Encoding.UTF8.GetBytes(
                        s: jwtAuthenticationOption.Jwt.IssuerSigningKey))
                .Key)
        };
    }
}
