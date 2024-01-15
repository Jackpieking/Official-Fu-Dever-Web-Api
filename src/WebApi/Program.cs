using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Cache.InMemory;
using Microsoft.AspNetCore.Builder;
using Persistence.SqlServer2016;
using WebApi;

// Custom settings.
Console.OutputEncoding = Encoding.UTF8;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args: args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddPersistenceSqlServer2016(configuration: configuration);
services.AddWebApi(configuration: configuration);
services.AddCacheInMemory(configuration: configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app
    .UseSwagger()
    .UseSwaggerUI(setupAction: options =>
    {
        options.SwaggerEndpoint(
            url: "/swagger/v1/swagger.json",
            name: "v1");
        options.RoutePrefix = string.Empty;
        options.DefaultModelsExpandDepth(depth: -1);
    })
    .UseHttpsRedirection()
    .UseRouting()
    .UseCors()
    .UseAuthentication()
    .UseAuthorization()
    .UseExceptionHandler();

app.MapControllers();

app.Run();
