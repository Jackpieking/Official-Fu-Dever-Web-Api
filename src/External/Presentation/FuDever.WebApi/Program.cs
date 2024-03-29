using FuDever.App;
using FuDever.Application;
using FuDever.Domain.Entities;
using FuDever.Firebase;
using FuDever.GoogleSmtp;
using FuDever.PostgresSql;
using FuDever.PostgresSql.Data;
using FuDever.Redis;
using FuDever.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;

// Custom settings.
AppContext.SetSwitch(
    switchName: "Npgsql.DisableDateTimeInfinityConversions",
    isEnabled: true);
Console.OutputEncoding = Encoding.UTF8;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args: args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddApplication();
services.AddRelationalDatabase(configuration: configuration);
services.AddWebApi(configuration: configuration);
services.AddFileObjectStorage();
services.AddIdentityService();
services.AddNotification();
services.AddCachingDatabase(configuration: configuration);

var app = builder.Build();

// Data seeding.
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FuDeverContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    // Can database be connected.
    var canConnect = await context.Database.CanConnectAsync();

    // Database cannot be connected.
    if (!canConnect)
    {
        throw new HostAbortedException(message: "Cannot connect database.");
    }

    // Try seed data.
    var seedResult = await EntityDataSeeding.SeedAsync(
        context: context,
        userManager: userManager,
        roleManager: roleManager,
        cancellationToken: CancellationToken.None);

    // Data cannot be seed.
    if (!seedResult)
    {
        throw new HostAbortedException(message: "Database seeding is false.");
    }
}

// Configure the HTTP request pipeline.
app
    .UseExceptionHandler()
    .UseHttpsRedirection()
    .UseRouting()
    .UseCors()
    .UseAuthentication()
    .UseAuthorization()
    .UseRateLimiter()
    .UseSwagger()
    .UseSwaggerUI(setupAction: options =>
    {
        options.SwaggerEndpoint(
            url: "/swagger/v1/swagger.json",
            name: "v1");
        options.RoutePrefix = string.Empty;
        options.DefaultModelsExpandDepth(depth: -1);
    });

app.MapControllers();

app.Run();
